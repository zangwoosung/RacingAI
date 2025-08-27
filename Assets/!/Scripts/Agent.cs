using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Agent : MonoBehaviour
{
    public Vector3 destination = Vector3.zero;
    NavMeshAgent agent;
    Vector3 originalPos;
    Action<string> myAction;
    Func<int, string> myFunc;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalPos = transform.position;
    }

    public void Setup(float speed, Vector3 des)
    {
        agent.speed = speed;
        destination = des;
        agent.SetDestination(des);
    }

    public void CallBackAction(Action<string> action)
    {
        myAction = action;
    }

    public void CallBackFunc(Func<int, string> func)
    {
        myFunc = func;
    }

    void Restore()
    {
        transform.position = originalPos;
    }

    void Update()
    {
        if (destination == Vector3.zero) return;

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            //agent.enabled = false;
            //transform.position = originalPos;
            //destination = Vector3.zero;

            //myAction(this.gameObject.name);

            //myFunc(1);
               
            
        }
    }

    
}
