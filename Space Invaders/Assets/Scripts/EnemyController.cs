using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscore;
    public int horizontalEnemyLength = 5;
    public int verticalEnemyLength = 3;
    public GameObject enemyBullet;
    public GameObject infoText;
    public TextMeshProUGUI scoreTable;
    private int currentEnemyDirection = -1;
    private List<GameObject> enemies = new List<GameObject>();
    private int score;
    private float lastBulletTime = 10;
    public delegate void ChangeEnemyDirection(int direction);
    public static event ChangeEnemyDirection OnChangeEnemyDirection;

    public delegate void OneEnemyDied();
    public static event OneEnemyDied OnOneEnemyDied;

    // Start is called before the first frame update
    void Start()
    {   
        Invoke("HandleStart", 3);
    }

    void HandleStart()
    {
        infoText.SetActive(false);
        scoreTable.enabled = false;

        float horizontalOffset = 13f / (horizontalEnemyLength - 1);
        float verticalOffset = 3.5f / (verticalEnemyLength - 1);
        for (int i = 0; i < horizontalEnemyLength; i++) {
            for (int k = 0; k < verticalEnemyLength; k++) {
                GameObject enemy;
                if (k == 4)
                {
                    enemy = Instantiate(enemyPrefab2, new Vector3(i * horizontalOffset - 6.5f, k * verticalOffset + 1, 0), Quaternion.identity);
                    enemy.GetComponent<Enemy>().points = 50;
                }
                else if (k == 3 || k == 2)
                {
                   enemy = Instantiate(enemyPrefab1, new Vector3(i * horizontalOffset - 6.5f, k * verticalOffset + 1, 0), Quaternion.identity);
                    enemy.GetComponent<Enemy>().points = 20;
                }
                else
                {
                    enemy = Instantiate(enemyPrefab3, new Vector3(i * horizontalOffset - 6.5f, k * verticalOffset + 1, 0), Quaternion.identity);
                    enemy.GetComponent<Enemy>().points = 10;
                }
                enemies.Add(enemy);
            }
        }
        Enemy.OnEnemyDied += HandleEnemyDied;
        highscore.text = "HI-SCORE\n" + FormatScore(PlayerPrefs.GetInt("highscore"));
    }

    void HandleEnemyDied(int pointsWorth)
    {
        AddScore(pointsWorth);
        OnOneEnemyDied.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject enemy in enemies) {
            if (enemy == null)
            {
                continue;
            }
            if (Time.realtimeSinceStartup - lastBulletTime >= 3)
            {
                Instantiate(enemyBullet, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
                lastBulletTime = Time.realtimeSinceStartup;
            }
            if (enemy.transform.position.x >= 9.5)
            {
                if (currentEnemyDirection == 1)
                {
                    currentEnemyDirection = -1;
                    OnChangeEnemyDirection.Invoke(-1);
                }
                break;
            }
            else if (enemy.transform.position.x <= -9.5)
            {
                if (currentEnemyDirection == -1)
                {
                    currentEnemyDirection = 1;
                    OnChangeEnemyDirection.Invoke(1);
                }
                break;
            }
        }
    }

    string FormatScore(int score)
    {
        string finalScoreText = "";
        string scoreString = score.ToString();
        for (int i = 0; i < 4 - scoreString.Length; i++)
        {
            finalScoreText += "0";
        }
        return finalScoreText + scoreString;
    }

    void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if (score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", score);
        }
        scoreText.text = "SCORE\n" + FormatScore(score);
    }
}
