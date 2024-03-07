using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public Sprite brokenBarrier;
    public bool flipSprite;
    private int hitCount = 0;

    void OnTriggerEnter2D(Collider2D collider)
    {
        hitCount += 1;
        Debug.Log("HIT");
        Destroy(collider.gameObject);
        if (hitCount >= 2)
        {
            Destroy(gameObject);
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = brokenBarrier; 
        gameObject.GetComponent<SpriteRenderer>().flipX = flipSprite;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        hitCount += 1;
        Debug.Log("HIT");
        Destroy(collision.gameObject);
        if (hitCount >= 2)
        {
            Destroy(gameObject);
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = brokenBarrier; 
        gameObject.GetComponent<SpriteRenderer>().flipX = flipSprite; 
    }
}
