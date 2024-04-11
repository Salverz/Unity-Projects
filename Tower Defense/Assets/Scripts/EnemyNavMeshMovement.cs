using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshMovement : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;

    private NavMeshAgent navMeshAgent;
    private Quaternion startOrientation;
    private Enemy enemy;
    private Vector3 endPosition;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        endPosition = GameObject.Find("END").transform.position;
        navMeshAgent.destination = endPosition;
        startOrientation = transform.rotation;
        enemy = GetComponent<Enemy>();
        targetGroup = GameObject.Find("Target Group").GetComponent<CinemachineTargetGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.speed = enemy.speed;
        transform.rotation = startOrientation;

        if (Vector3.Distance(transform.position, endPosition) < 3f)
        {
            EndPath();
        }
    }
    
    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        targetGroup.RemoveMember(transform);
        Destroy(gameObject);
    }
}
