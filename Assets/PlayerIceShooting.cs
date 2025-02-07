using System;
using UnityEngine;

public class PlayerIceShooting : MonoBehaviour
{
    public GameObject ice;
    public GameObject player;
    public Transform firePoint;
    public float iceSpeed = 30;

    Vector2 lookDirection;
    float lookAngle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // lookDirection = new Vector2(lookDirection.x - transform.position.x, lookDirection.y - transform.position.y);
        // lookDirection = Camera.main.WorldToScreenPoint(Input.mousePosition);
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(player.transform.position.x, player.transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject iceClone = Instantiate(ice);
            iceClone.transform.position = firePoint.position;
            iceClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

            iceClone.GetComponent<Rigidbody2D>().linearVelocity = firePoint.right * iceSpeed;

        }
    }
}
