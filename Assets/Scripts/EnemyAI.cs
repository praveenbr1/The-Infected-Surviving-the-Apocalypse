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
   
    float distanceToTarget = 0f;
    bool isProvoked;
    Animator myAnimator;
    void Start()
    {
        myNavagent = GetComponent<NavMeshAgent>();
        myAnimator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(targetPlayer.position, transform.position);

        if(isProvoked)
        {
            ChaseTarget();
        }

        else if(distanceToTarget <= enemyChaseRange)
        {
            isProvoked= true;
        }
      
        
    }

    private void ChaseTarget()
    {
       if(distanceToTarget >= myNavagent.stoppingDistance)
        {
            myAnimator.SetTrigger("isRunning");
            myNavagent.SetDestination(targetPlayer.position);
        }

       else if(distanceToTarget <= myNavagent.stoppingDistance) 
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        Debug.Log($"{name} caught me {targetPlayer.name}");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position, enemyChaseRange);
    }
}
