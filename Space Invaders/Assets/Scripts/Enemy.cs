using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public GameObject enemyController;
    public delegate void EnemyDied(int pointsWorth);
    public static event EnemyDied OnEnemyDied;

    private int moveDirection = -1;
    public int points = 10;
    private float speed = 0.2f;
    private bool moveDown = false;

    // Start is called before the first frame update
    void Start()
    {
        EnemyController.OnChangeEnemyDirection += ChangeDirection;
        EnemyController.OnOneEnemyDied += IncreaseSpeed;
    }

    void IncreaseSpeed() {
        gameObject.GetComponent<Animator>().speed += .02f;
    }

    void ChangeDirection(int direction)
    {
        moveDirection = direction;
        moveDown = true; 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);

            OnEnemyDied.Invoke(points);
            EnemyController.OnChangeEnemyDirection -= ChangeDirection;
            EnemyController.OnOneEnemyDied -= IncreaseSpeed;
            Destroy(gameObject);
        }
    }

    void AnimationEvent() {
        Vector3 currentPos = gameObject.transform.position;

        Vector3 newPos;
        if (moveDown)
        {
            newPos = new Vector3(currentPos.x, currentPos.y - .5f, currentPos.z);
            moveDown = false;
        }
        else
        {
            newPos = new Vector3(currentPos.x + speed * moveDirection, currentPos.y, currentPos.z);
        }
        gameObject.transform.position = newPos;
    }
}
