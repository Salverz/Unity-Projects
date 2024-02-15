using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleReference : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject leftPaddle;
    public GameObject rightPaddle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getLeftPaddle() { return leftPaddle; }
    public GameObject getRightPaddle() { return rightPaddle; }
}
