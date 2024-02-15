using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DemoCarController : MonoBehaviour
{
    public Camera audioListener;
    public Canvas hoverCanvas;
    public TextMeshProUGUI hoverText;
    public AudioSource engineIdleSource;
    public AudioSource hornSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hornSource.Play();
        }

        Vector3 fromCarToCamera = audioListener.transform.position - transform.position;
        float distFromCamera = fromCarToCamera.magnitude;

        hoverText.text = $"Distance: {distFromCamera}";

        hoverCanvas.transform.LookAt(audioListener.transform.position, Vector3.up);
    }
}
