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

        public Transform WeaponPivot;

        public Transform Weapon;
        public Attack attackManager;
        //public WeaponStats ws;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            movementDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            cam.position = transform.position + new Vector3(0,0,-10);

            // Get mouse position in world space
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f; // Ensure z is zero for 2D

            // Calculate direction from character to mouse
            Vector3 direction = mouseWorldPosition - WeaponPivot.position;
            direction.z = 0f;
            // Calculate angle and   apply rotation
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            WeaponPivot.rotation = Quaternion.Euler(0f, 0f, angle);
            if (!attackManager.isAttacking)
            {
                Weapon.up = direction.normalized;
            }

        }

        void FixedUpdate()
        {
            rb.linearVelocity = movementDir * movementSpeed;
        }
    }
