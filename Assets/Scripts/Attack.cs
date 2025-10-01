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

        float maxReach = weaponStats.currentWeapon != null ? weaponStats.currentWeapon.range : 1f;

        if (Input.GetKey(KeyCode.Space) && cooldown <= 0f)
        {
            cooldown = weaponStats.currentWeapon.attackSpeed;
            isAttacking = true;
            rotationAccumulated = 0f;
            attackTimer = 0f;

            // Fire ranged weapon immediately
            if (weaponStats.currentWeapon.type == WeaponType.Range)
            {
                FireProjectile(mouseWorldPosition);
                isAttacking = false; // reset so it doesn't keep firing
            }
        }

        if (isAttacking && weaponStats.currentWeapon.type == WeaponType.Melee)
        {
            // Melee attack logic (spin and push)
            rotationAccumulated += weaponStats.currentWeapon.spinSpeed * 360f * Time.deltaTime;
            transform.Rotate(0f, 0f, weaponStats.currentWeapon.spinSpeed * 360f * Time.deltaTime);
            attackTimer += Time.deltaTime;

            Vector3 targetLocal = originalLocalPosition + Vector3.right * Mathf.Min(Vector3.Distance(mouseWorldPosition, weaponPivot.position), maxReach);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocal, Time.deltaTime * pushOutSpeed);

            if (rotationAccumulated >= 360f || attackTimer >= 0.5f)
                isAttacking = false;
        }
        else
        {
            // Retract weapon for melee
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalLocalPosition, Time.deltaTime * retractSpeed);
            transform.localRotation = originalLocalRotation;
        }

        if (cooldown > 0f)
            cooldown -= Time.deltaTime;
    }

    void FireProjectile(Vector3 targetPosition)
    {
        GameObject bullet = Instantiate(weaponStats.currentWeapon.bullet, transform.position, Quaternion.identity);

        // Set bullet rotation
        BulletSpin spin = bullet.GetComponent<BulletSpin>();
        if (spin != null)
        {
            spin.spinSpeed = weaponStats.currentWeapon.spinSpeed;
            spin.SetDirection(targetPosition);
        }

        // Set bullet velocity
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector3 direction = (targetPosition - bullet.transform.position).normalized;
            rb.linearVelocity = direction * weaponStats.currentWeapon.projectSpeed;
        }

        // Destroy after lifetime
        if (weaponStats.currentWeapon.projectLife > 0)
        {
            Destroy(bullet, weaponStats.currentWeapon.projectLife);
        }
    }
}
