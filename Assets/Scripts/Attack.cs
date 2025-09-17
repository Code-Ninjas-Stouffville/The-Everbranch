using UnityEngine;

public class Attack : MonoBehaviour
{
    public float cooldown = 0f;
    public bool isAttacking = false;
    public float rotationAccumulated = 0f;

    public Movement movement;
    public WeaponStats weaponStats;
    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;
    public float attackTimer;

    public Transform weaponPivot;

    // Speeds (tweak in Inspector)
    public float pushOutSpeed = 25f;
    public float retractSpeed = 25f;

    void Start()
    {
        originalLocalPosition = transform.localPosition;
        originalLocalRotation = transform.localRotation;
    }

    void Update()
    {
        // Mouse world pos (2D)
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        // Direction & distance from pivot to mouse (2D)
        Vector3 direction = mouseWorldPosition - weaponPivot.position;
        direction.z = 0f;
        float distanceToMouse = direction.magnitude;

        // Clamp to weapon range
        float maxReach = (float)weaponStats.currentWeapon.range;
        float targetReach = Mathf.Min(distanceToMouse, maxReach);

        // Start attack once per key press
        if (Input.GetKey(KeyCode.Space) && cooldown <= 0f)
        {
            cooldown = weaponStats.currentWeapon.attackSpeed;
            isAttacking = true;
            rotationAccumulated = 0f;
            attackTimer = 0f;
        }

        if (isAttacking)
        {
            if (weaponStats.currentWeapon.type == WeaponType.Melee)
            {
                // Spin the weapon
                float rotationStep = weaponStats.currentWeapon.spinSpeed * 360f * Time.deltaTime;
                transform.Rotate(0f, 0f, rotationStep);
                rotationAccumulated += rotationStep;
                attackTimer += Time.deltaTime;

                // Extend along the pivot's local +X toward the cursor distance (clamped)
                Vector3 targetLocal = originalLocalPosition + Vector3.right * targetReach;
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocal, Time.deltaTime * pushOutSpeed);

                // End after one spin or a small time window
                if (rotationAccumulated >= 360f || attackTimer >= 0.5f)
                {
                    isAttacking = false;
                }
            }
            else // Ranged weapon
            {
                GameObject bullet = Instantiate(
                    weaponStats.currentWeapon.bullet,
                    transform.position,
                    Quaternion.identity
                );

                float spread = weaponStats.currentWeapon.spread;
                Vector3 randSpread = direction.normalized +
                    new Vector3(Random.Range(-spread * 0.1f, spread * 0.1f),
                                Random.Range(-spread * 0.1f, spread * 0.1f),
                                0);

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = randSpread * weaponStats.currentWeapon.projectSpeed;
                }

                // 👇 destroy bullet after projectLife seconds
                if (weaponStats.currentWeapon.projectLife > 0)
                {
                    Destroy(bullet, weaponStats.currentWeapon.projectLife);
                }

                isAttacking = false;
            }
        }
        else
        {
            // Retract & reset
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                originalLocalPosition,
                Time.deltaTime * retractSpeed
            );
            transform.localRotation = originalLocalRotation;
        }

        // Cooldown
        if (cooldown > 0f)
            cooldown -= Time.deltaTime;
    }
}
