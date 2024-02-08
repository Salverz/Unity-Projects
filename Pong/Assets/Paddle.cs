using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float unitsPerSecond = 3f;

    public GameObject player1Paddle;
    public GameObject player2Paddle;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Player1") != 0) {
            Vector3 newVelocity = new Vector3(-20, 0, 0) * Input.GetAxis("Player1");
            player1Paddle.GetComponent<Rigidbody>().velocity = newVelocity;
        }
        if (Input.GetAxis("Player2") != 0)
        {
            Vector3 newVelocity = new Vector3(-20, 0, 0) * Input.GetAxis("Player2");
            player2Paddle.GetComponent<Rigidbody>().velocity = newVelocity;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Ball")
        {
            return;
        }

        Rigidbody rb = collision.rigidbody;
        
        float contactPoint = collision.contacts[0].point.x;
        float pointOnPaddle = this.gameObject.GetComponent<Rigidbody>().position.x - contactPoint;

        float reflectionAngle = (pointOnPaddle / 2.5f) * 50f;
        rb.velocity = new Vector3(reflectionAngle, 0, rb.velocity.z * -1);

    }
}
