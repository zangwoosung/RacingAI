using System;
using UnityEngine;
using UnityEngine.AI;
public struct Ticket
{
    public float ElapsedTime;
    public string Name;
    public Ticket(float r, string name)
    {
        this.ElapsedTime = r;
        this.Name = name;
    }
}

public class Agent : MonoBehaviour
{
    Vector3 destination = Vector3.zero;
    NavMeshAgent navAgent;
    Vector3 originalPos;   
    Action<Ticket> RankingAction;
   
    float startTime;
    float elapsedTime;      
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();       
        originalPos = transform.position;
    }

    public void Setup(float speed, Vector3 des)
    {
        startTime = Time.time;
        elapsedTime = 0;
        navAgent.speed = speed;
        navAgent.SetDestination(des);
        destination = des;
    }

    public void CallBackAction(Action<Ticket> action)
    {
        RankingAction = action;
    }        

    public void Restore()
    {        
        elapsedTime = 0;
    }

    void Update()
    {
        if (destination == Vector3.zero) return;

        if (navAgent.remainingDistance < navAgent.stoppingDistance)
        {
            destination = Vector3.zero;            
            elapsedTime = Time.time - startTime;
            RankingAction(new Ticket(elapsedTime, this.gameObject.name));
            navAgent.Warp(originalPos);
                    
        }
    }
}
