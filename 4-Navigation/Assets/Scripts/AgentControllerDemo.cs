using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentControllerDemo : MonoBehaviour
{
    public Transform agentDestination;
    NavMeshAgent myAgent; 
    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        myAgent.destination = agentDestination.position;
    }
}
