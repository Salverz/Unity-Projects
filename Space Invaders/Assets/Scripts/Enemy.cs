using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDied(int pointsWorth);
    public static event EnemyDied OnEnemyDied;

    public int points = 10;

    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ouch!");
        Destroy(collision.gameObject);

        OnEnemyDied.Invoke(points);
        Destroy(gameObject);
    }
}
