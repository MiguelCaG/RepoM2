using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;

    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        initialPosition = new Vector3(21.8f, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if(transform.position.x <= -19.2f)
        {
            transform.position = initialPosition;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(speed, 0f, 0f);
    }
}
