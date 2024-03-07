using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
  public GameObject bullet;

  public Transform shottingOffset;

    private void Start()
    {
        Enemy.OnEnemyDied += EnemyOnOnEnemyDied;

    }

    void EnemyOnOnEnemyDied(int pointsWorth)
    {
        Debug.Log("player recieved 'Enemy Died'");
    }

    private void OnDestroy()
    {
        Enemy.OnEnemyDied -= EnemyOnOnEnemyDied;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKey(KeyCode.Space))
      {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("bullet");
        if (bullets.Length == 0)
        {
          GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
        }
      }

      if (Input.GetKey(KeyCode.A))
      {
        Vector3 currentPos = gameObject.transform.position;
        Vector3 newPos = new Vector3(currentPos.x - .02f, currentPos.y, currentPos.z);

        gameObject.transform.position = newPos;
      }
      else if (Input.GetKey(KeyCode.D))
      {
        Vector3 currentPos = gameObject.transform.position;
        Vector3 newPos = new Vector3(currentPos.x + .02f, currentPos.y, currentPos.z);

        gameObject.transform.position = newPos;
      }
    }

    void SomeAnimationFrameCallback()
    {
      Debug.Log("something happened in the animation!");
    }
}