using UnityEngine;

public class BulletLifetime : MonoBehaviour
{
    public float lifetime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake ()
    {
        Destroy(this.gameObject, lifetime);
    }
}
