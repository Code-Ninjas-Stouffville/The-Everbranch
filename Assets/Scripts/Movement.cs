using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementDir;

    public Transform stick;
    public Transform cam;

    [Header("Player Stats")]
    public int Health = 5;
    public float movementSpeed = 5f;          // changed to float for smoother math
    public float sprintMultiplier = 2f;       // how much faster when holding Shift

    public WeaponStats weaponStats;

    public Transform WeaponPivot;
    public Transform Weapon;
    public Attack attackManager;

    [Header("Movement Boundary (Rectangle)")]
    [Tooltip("If true, uses the player's start position as the rectangle center.")]
    public bool useStartAsCenter = true;

    [Tooltip("Optional: provide a custom center for the rectangle. Ignored if 'useStartAsCenter' is true.")]
    public Transform boundaryCenter;

    [Tooltip("Half-width of the allowed area.")]
    public float halfWidth = 60f;

    [Tooltip("Half-height of the allowed area.")]
    public float halfHeight = 50f;

    private Vector2 centerPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (useStartAsCenter || boundaryCenter == null)
            centerPos = transform.position;
        else
            centerPos = boundaryCenter.position;
    }

    void Update()
    {
        movementDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        cam.position = transform.position + new Vector3(0, 0, -10);

        // Mouse look
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector3 direction = mouseWorldPosition - WeaponPivot.position;
        direction.z = 0f;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        WeaponPivot.rotation = Quaternion.Euler(0f, 0f, angle);

        if (!attackManager.isAttacking)
        {
            Weapon.up = direction.normalized;
        }
    }

    void FixedUpdate()
    {
        // Check if sprint key is held
        float currentSpeed = movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= sprintMultiplier;
        }

        Vector2 desiredVel = movementDir * currentSpeed;
        Vector2 currentPos = rb.position;
        Vector2 nextPos = currentPos + desiredVel * Time.fixedDeltaTime;

        if (!useStartAsCenter && boundaryCenter != null)
            centerPos = boundaryCenter.position;

        // Clamp next position inside rectangle
        float clampedX = Mathf.Clamp(nextPos.x, centerPos.x - halfWidth, centerPos.x + halfWidth);
        float clampedY = Mathf.Clamp(nextPos.y, centerPos.y - halfHeight, centerPos.y + halfHeight);
        Vector2 clampedNext = new Vector2(clampedX, clampedY);

        // Adjust velocity so the player moves smoothly along edges
        Vector2 clampedDelta = clampedNext - currentPos;
        rb.linearVelocity = clampedDelta / Time.fixedDeltaTime;   // ✅ use velocity, not linearVelocity
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 c = (useStartAsCenter || boundaryCenter == null)
            ? (Application.isPlaying ? (Vector3)centerPos : transform.position)
            : boundaryCenter.position;

        Vector3 topLeft = new Vector3(c.x - halfWidth, c.y + halfHeight, 0f);
        Vector3 topRight = new Vector3(c.x + halfWidth, c.y + halfHeight, 0f);
        Vector3 bottomRight = new Vector3(c.x + halfWidth, c.y - halfHeight, 0f);
        Vector3 bottomLeft = new Vector3(c.x - halfWidth, c.y - halfHeight, 0f);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
