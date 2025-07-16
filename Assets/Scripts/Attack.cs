using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float cooldown = 0f;
    public bool isAttack = false;
    public Movement movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && cooldown <= 0f)
        {
            cooldown = movement.attackSpeed;
            gameObject.transform.Rotate(0, 0, movement.spinSpeed);
            isAttack = true;
        }

        if (gameObject.transform.rotation.eulerAngles.z > 1 || gameObject.transform.rotation.eulerAngles.z < -1)
        {
            gameObject.transform.Rotate(0, 0, movement.spinSpeed);
        }
        else
        {
            isAttack = false;
        }

        if (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
        }
    }
}
