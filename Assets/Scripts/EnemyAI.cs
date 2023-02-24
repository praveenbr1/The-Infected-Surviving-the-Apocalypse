using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent myNavagent;
    [SerializeField] Transform targetPlayer;
    [SerializeField] float enemyChaseRange = 7f;
    float disttanceToTarget = 0f;
    void Start()
    {
        myNavagent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        disttanceToTarget = Vector3.Distance(targetPlayer.position, transform.position);
        if(disttanceToTarget <= enemyChaseRange)
        {
            myNavagent.SetDestination(targetPlayer.position);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position, enemyChaseRange);
    }
}
