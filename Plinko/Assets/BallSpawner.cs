using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnTransform;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 spawnPosition = spawnTransform.position + new Vector3(0, 0, Random.Range(-8.0f, 8.0f));
            Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
