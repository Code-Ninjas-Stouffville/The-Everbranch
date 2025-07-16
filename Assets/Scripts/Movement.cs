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

    [Header("Stick Stats")]
    public string What = "";
    public int Tier = 0;
    public int damage = 10;
    public int attackSpeed = 2;
    public int spinSpeed = 10;
    public int range = 1;
    private SceneSwitch info;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        stick.position = transform.position + new Vector3(Input.GetAxis("Horizontal") * range, Input.GetAxis("Vertical") * range, -1);
        cam.position = transform.position + new Vector3(0,0,-10);
        What = info.Weapon;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movementDir * movementSpeed;
    }
}
