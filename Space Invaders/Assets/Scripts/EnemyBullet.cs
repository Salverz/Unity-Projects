using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 3;
    private Rigidbody2D myRigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "enemyBullet";
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myRigidbody2D.velocity = Vector2.down * speed;
    }
}
