using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    private int player1Score;
    private int player2Score;

    public void incrementScore(int player)
    {
        if (player == 1)
        {
            player1Score += 1;
        } else
        {
            player2Score += 1;
        }
        Debug.Log($"Player 1: {player1Score} | Player 2: {player2Score}");

        if (player1Score >= 11 || player2Score >= 11) {
            Debug.Log("Game Over");
            player1Score = 0;
            player2Score = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(40, 0, 20);

        player1Score = 0;
        player2Score = 0;
        Debug.Log($"Player 1: {player1Score} | Player 2: {player2Score}");
    }

    public void resetBall(int winner)
    {

        rb.transform.position = new Vector3(-10, 0, 0);
        if (winner == 1)
        {
            rb.velocity = new Vector3(40, 0, 20);
        } else
        {
            rb.velocity = new Vector3(40, 0, -20);
        }
    }
}
