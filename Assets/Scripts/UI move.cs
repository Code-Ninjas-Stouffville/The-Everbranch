using UnityEngine;

public class UImove : MonoBehaviour
{
    [Header("Scroll speed")]
    public float ySpeed = 2f;
    public float xSpeed = 2f;

    [Header("Max Min")]
    public float yMin = -180f;
    public float yMax = 180f;
    public float xMin = -240f;
    public float xMax = 240f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > xMin && transform.position.x < xMax && transform.position.y > yMin && transform.position.y < yMax)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position -= new Vector3(0, ySpeed, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= new Vector3(0, -ySpeed, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= new Vector3(-xSpeed, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position -= new Vector3(xSpeed, 0, 0);
            }
        } else
        {
            if (transform.position.x <= xMin)
            {
                transform.position += new Vector3(xSpeed, 0, 0);
            }
            if (transform.position.x >= xMax)
            {
                transform.position += new Vector3(-xSpeed, 0, 0);
            }
            if (transform.position.y <= yMin)
            {
                transform.position += new Vector3(ySpeed, 0, 0);
            }
            if (transform.position.y >= yMax)
            {
                transform.position += new Vector3(-ySpeed, 0, 0);
            }
        }
    }
}
