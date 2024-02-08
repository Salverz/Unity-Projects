using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject ball;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Ball")
        {
            return;
        }

        if (this.name == "Left Goal")
        {
            Debug.Log("Player 2 scored!");
            ball.GetComponent<BallController>().incrementScore(2);
            ball.GetComponent<BallController>().resetBall(1);
        } else if (this.name == "Right Goal")
        {
            Debug.Log("Player 1 scored!");
            ball.GetComponent<BallController>().incrementScore(1);
            ball.GetComponent<BallController>().resetBall(2);
        }
        
    }
}
