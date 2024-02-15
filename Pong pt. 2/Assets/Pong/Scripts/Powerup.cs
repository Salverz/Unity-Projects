using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PaddleReference>().getLeftPaddle().GetComponent<Paddle>().setPaddleSize(Random.Range(3f, 8f));
        other.GetComponent<PaddleReference>().getRightPaddle().GetComponent<Paddle>().setPaddleSize(Random.Range(3f,8f));

        other.GetComponent<PaddleReference>().getLeftPaddle().GetComponent<Paddle>().setPaddleSpeed(Random.Range(2f, 6f));
        other.GetComponent<PaddleReference>().getRightPaddle().GetComponent<Paddle>().setPaddleSpeed(Random.Range(2f, 6f));
        Destroy(gameObject);
    }

    public void destroySelf()
    {
        Destroy(gameObject);
    }
}
