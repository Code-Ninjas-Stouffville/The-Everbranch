using UnityEngine;

public class BulletSpin : MonoBehaviour
{
    public float spinSpeed = 1f;

    void Update()
    {
        float rotationStep = spinSpeed * 360f * Time.deltaTime;
        transform.Rotate(0f, 0f, rotationStep);
    }
}
