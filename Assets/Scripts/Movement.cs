using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementDir;

    public Transform stick;
    public Transform cam;

    [Header("Player Stats")]
    public int Health = 5;
    public int movementSpeed = 5;

    public WeaponStats weaponStats;
    //public WeaponStats ws;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) == false)
        {
            movementDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else
        {
            movementDir = new Vector2(0, 0);
        }

        stick.position = transform.position + new Vector3(Input.GetAxis("Horizontal") * weaponStats.currentWeapon.range, Input.GetAxis("Vertical") * weaponStats.currentWeapon.range, -1);
        cam.position = transform.position + new Vector3(0,0,-10);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movementDir * movementSpeed;
    }
}
