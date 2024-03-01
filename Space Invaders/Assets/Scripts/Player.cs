using System.Collections;
using System.Collections.Generic;
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
      if (Input.GetKeyDown(KeyCode.Space))
      {
        GetComponent<Animator>().SetTrigger("Shoot Trigger");
        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
        Debug.Log("Bang!");

        Destroy(shot, 3f);

      }
    }

    void SomeAnimationFrameCallback()
    {
      Debug.Log("something happened in the animation!");
    }
}
