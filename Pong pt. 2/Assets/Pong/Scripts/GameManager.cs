using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public Transform ball;
    public float startSpeed = 3f;
    public GoalTrigger leftGoalTrigger;
    public GoalTrigger rightGoalTrigger;
    public TextMeshProUGUI rightScoreboard;
    public TextMeshProUGUI leftScoreboard;
    public GameObject powerup;

    private int leftPlayerScore = 0;
    private int rightPlayerScore = 0;
    private Vector3 ballStartPos;
    private GameObject currentPowerup;

    private const int scoreToWin = 11;
    private Color[] colors = {
        new Color(107f / 255f, 220f / 255f, 255f / 255f),
        new Color(107f / 255f, 191f / 255f, 255f / 255f),
        new Color(107f / 255f, 122f / 255f, 255f / 255f),
        new Color(137f / 255f, 107f / 255f, 255f / 255f),
        new Color(176f / 255f, 107f / 255f, 255f / 255f),
        new Color(211f / 255f, 107f / 255f, 255f / 255f),
        new Color(240f / 255f, 107f / 255f, 255f / 255f),
        new Color(255f / 255f, 107f / 255f, 228f / 255f),
        new Color(255f / 255f, 107f / 255f, 188f / 255f),
        new Color(255f / 255f, 107f / 255f, 159f / 255f),
        new Color(255f / 255f, 107f / 255f, 127f / 255f),
        new Color(255f / 255f, 20f / 255f, 60f / 255f)
    };

    // Start is called before the first frame update
    void Start()
    {
        currentPowerup = Instantiate(powerup, new Vector3(Random.Range(-6f,6f), 0f, Random.Range(-5f, 5f)), Quaternion.identity);
        currentPowerup.transform.Rotate(Vector3.up, 45f);

        ballStartPos = ball.position;
        Rigidbody ballBody = ball.GetComponent<Rigidbody>();
        ballBody.velocity = new Vector3(1f, 0f, 0f) * startSpeed;
    }

    // If the ball entered the goal area, increment the score, check for win, and reset the ball
    public void OnGoalTrigger(GoalTrigger trigger)
    {
        if (trigger == leftGoalTrigger)
        {
            rightPlayerScore++;
            rightScoreboard.text = $"Score: {rightPlayerScore}";
            rightScoreboard.color = colors[rightPlayerScore];
            Debug.Log($"Right player scored: {rightPlayerScore}");
            if (rightPlayerScore == scoreToWin)
            {
                Debug.Log("Right player wins!");
            }
            else
            {
                ResetBall(-1f);
            }
        }
        else if (trigger == rightGoalTrigger)
        {

            leftPlayerScore++;
            leftScoreboard.text = $"Score: {leftPlayerScore}";
            leftScoreboard.color = colors[leftPlayerScore];
            Debug.Log($"Left player scored: {leftPlayerScore}");
            if (rightPlayerScore == scoreToWin)
            {
                Debug.Log("Right player wins!");
            }
            else
            {
                ResetBall(1f);
            }
        }
    }

    void ResetBall(float directionSign)
    {
        ball.position = ballStartPos;

        // Start the ball within 20 degrees off-center toward direction indicated by directionSign
        directionSign = Mathf.Sign(directionSign);
        Vector3 newDirection = new Vector3(directionSign, 0f, 0f) * startSpeed;
        newDirection = Quaternion.Euler(0f, Random.Range(-20f, 20f), 0f) * newDirection;

        var rbody = ball.GetComponent<Rigidbody>();
        rbody.velocity = newDirection;
        rbody.angularVelocity = new Vector3();

        // We are warping the ball to a new location, start the trail over
        ball.GetComponent<TrailRenderer>().Clear();

        // Spawn a powerup
        if (currentPowerup != null)
        {
            currentPowerup.GetComponent<Powerup>().destroySelf();
        }
        currentPowerup = Instantiate(powerup, new Vector3(Random.Range(-6f, 6f), 0f, Random.Range(-5f, 5f)), Quaternion.identity);
        currentPowerup.transform.Rotate(Vector3.up, 45f);
    }
}
