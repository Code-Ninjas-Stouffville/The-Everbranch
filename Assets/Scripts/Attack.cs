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

    public float pushOutSpeed = 25f;
    public float retractSpeed = 25f;

    void Start()
    {
        originalLocalPosition = transform.localPosition;
        originalLocalRotation = transform.localRotation;
    }

    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector3 direction = mouseWorldPosition - weaponPivot.position;
        direction.z = 0f;
        float distanceToMouse = direction.magnitude;

        float maxReach = (float)weaponStats.currentWeapon.range;
        float targetReach = Mathf.Min(distanceToMouse, maxReach);

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
                float rotationStep = weaponStats.currentWeapon.spinSpeed * 360f * Time.deltaTime;
                transform.Rotate(0f, 0f, rotationStep);
                rotationAccumulated += rotationStep;
                attackTimer += Time.deltaTime;

                Vector3 targetLocal = originalLocalPosition + Vector3.right * targetReach;
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocal, Time.deltaTime * pushOutSpeed);

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

                // Add spin behavior to projectile
                BulletSpin spin = bullet.AddComponent<BulletSpin>();
                spin.spinSpeed = weaponStats.currentWeapon.spinSpeed;

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

                if (weaponStats.currentWeapon.projectLife > 0)
                {
                    Destroy(bullet, weaponStats.currentWeapon.projectLife);
                }

                isAttacking = false;
            }
        }
        else
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                originalLocalPosition,
                Time.deltaTime * retractSpeed
            );
            transform.localRotation = originalLocalRotation;
        }

        if (cooldown > 0f)
            cooldown -= Time.deltaTime;
    }
}
