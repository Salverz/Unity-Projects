using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  public GameObject bullet;

  public Transform shottingOffset;
  public AudioClip shootSound;
  public AudioClip deathSound;
  public delegate void PlayerDiedEvent();
  public static event PlayerDiedEvent OnPlayerDiedEvent;

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
          GetComponent<Animator>().SetTrigger("Shoot Trigger");
          AudioSource audio = GetComponent<AudioSource>();
          audio.PlayOneShot(shootSound);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
      GetComponent<Animator>().SetTrigger("Died");
      AudioSource audio = GetComponent<AudioSource>();
      audio.PlayOneShot(deathSound);
      Debug.Log("PLAYER HIT!");
    }

    void PlayerDied()
    {
      OnPlayerDiedEvent.Invoke();
    }
}