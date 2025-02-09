using UnityEngine;

public class MenuBobbing : MonoBehaviour
{
    public float amplitude; // Adjust height of bobbing
    public float frequency;   // Adjust speed of bobbing
    private float startY;           // Initial Y position

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
