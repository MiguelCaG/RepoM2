using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;

    private float speed = 6f;
    private float rotationLerpSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        rb.velocity = moveDirection * speed;

        if (moveDirection != Vector3.zero)
        {
            float targetRotation = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

            float newRotation = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation, rotationLerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, newRotation, 0f);
        }
    }
}
