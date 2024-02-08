using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Ball")
        {
            return;
        }

        collision.rigidbody.velocity = new Vector3(collision.rigidbody.velocity.x * -1, 0, collision.rigidbody.velocity.z);
    }
}
