using UnityEngine;

public class UImove : MonoBehaviour
{
    [Header("Scroll speed")]
    public float ySpeed = 2f;
    public float xSpeed = 2f;

    [Header("Max Min")]
    public float yMin = -2000f;
    public float yMax = 2000f;
    public float xMin = -2000f;
    public float xMax = 3000f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && transform.position.y > yMin)
        {
            transform.position -= new Vector3(0, ySpeed, 0);
        }
        if (Input.GetKey(KeyCode.S) && transform.position.y < yMax)
        {
            transform.position -= new Vector3(0, -ySpeed, 0);
        }
        if (Input.GetKey(KeyCode.A) && transform.position.x < xMax)
        {
            transform.position -= new Vector3(-xSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x > xMin)
        {
            transform.position -= new Vector3(xSpeed, 0, 0);
        }
    }
}
