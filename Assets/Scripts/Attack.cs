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

    void Start()
    {
        originalLocalPosition = transform.localPosition;
        originalLocalRotation = transform.localRotation;
    }
    void Update()
    {
        // Start attack if space is pressed and cooldown is ready
        if (Input.GetKey(KeyCode.Space) && cooldown <= 0f)
        {
            cooldown = weaponStats.currentWeapon.attackSpeed;
            isAttacking = true;
            rotationAccumulated = 0f;
        }

        if (isAttacking)
        {
            float rotationStep = weaponStats.currentWeapon.spinSpeed * 360 * Time.deltaTime;
            transform.Rotate(0f, 0f, rotationStep);
            rotationAccumulated += rotationStep;

            attackTimer += Time.deltaTime;

            // Push weapon outward
            if (rotationAccumulated <= 360f)
            {
                Vector3 pos = transform.localPosition;
                pos.x = Mathf.Lerp(pos.x, weaponStats.currentWeapon.range, Time.deltaTime * 25); // Faster push
                transform.localPosition = pos;
            }
            if (attackTimer >= 0.5)
            {
                isAttacking = false;    
            }
        }
        else
        {
            attackTimer = 0;
            // Return weapon to original position
            Vector3 pos = transform.localPosition;
            pos.x = Mathf.Lerp(pos.x, originalLocalPosition.x, Time.deltaTime * 25f); // Smooth return
            transform.rotation = originalLocalRotation;
            transform.localPosition = pos;
        }

        // Cooldown countdown
        if (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
        }
    }

}
