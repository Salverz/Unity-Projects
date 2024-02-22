using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerLive : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 100f;
    public float jumpImpulse = 50f;
    public float jumpBoost = 3f;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity += Vector3.right * horizontalMovement * Time.deltaTime * acceleration;
        Debug.Log($"Move {rb.velocity}");

        //CapsuleCollider col = GetComponent<CapsuleCollider>();
        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + .03f;

        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + Vector3.down * halfHeight;

        isGrounded = Physics.Raycast(startPoint, Vector3.down, halfHeight);
        Color lineColor = (isGrounded) ? Color.red : Color.blue;
        Debug.DrawLine(startPoint, endPoint, lineColor, 0f, true);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        }
        else if (!isGrounded && Input.GetKey(KeyCode.Space))
        {
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.up * jumpBoost);
            }
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            Vector3 newVel = rb.velocity;
            newVel.x = Mathf.Clamp(newVel.x, -maxSpeed, maxSpeed);
            rb.velocity = newVel;
        }

        if (Mathf.Abs(horizontalMovement) < .5f)
        {
            Vector3 newVel = rb.velocity;
            newVel.x *= 0.9f;
            rb.velocity = newVel;
        }

        float yaw = (rb.velocity.x > 0) ? 90 : -90;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
}
