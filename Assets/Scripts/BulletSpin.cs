using UnityEngine;

public class BulletSpin : MonoBehaviour
{
    public float spinSpeed = 1f;

    void Update()
    {
        // Rotate around Z axis
        transform.Rotate(0f, 0f, spinSpeed * 360f * Time.deltaTime);
    }

    // Make the bullet face the target at spawn
    public void SetDirection(Vector3 targetPosition)
    {
        targetPosition.z = 0f;
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
