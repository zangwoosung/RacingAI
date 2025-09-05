using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using static UnityEditor.PlayerSettings;
using Random = UnityEngine.Random;

public class AgentManager : MonoBehaviour
{
    public UnityEvent<string> sessionCompleteEvent;
    public UnityEvent<Ticket> sessionOverEvent;
    public UnityEvent<List<string>> GetNamesEvent;
    public Agent[] agents;
    Queue agentQueue = new Queue();
    bool hasStarted = false;

     void  GetNames()
    {
        List<string> names=new List<string>();
        
        for (int i = 0; i < agents.Length; i++)        
            names.Add(agents[i].name);
           
        GetNamesEvent?.Invoke(names);           

    }
    public void StartToRun(Vector3 pos)
    {
        if (hasStarted) return;
        hasStarted = true;
        Debug.Log("One time only ");
        agentQueue.Clear();
        StartCoroutine(StartOnebyOne(pos));
    }

    int count = 0;
    IEnumerator StartOnebyOne(Vector3 pos)
    {
        count = 0;
        foreach (var agent in agents)
        {
            yield return null;//
            float speed = Random.Range(10, 15);
            agent.Setup(speed, pos);
            agent.CallBackAction(CollectAgents);

            Debug.Log("count: " + count);
            yield return null;// new WaitForSeconds(0.5f);
        }
    }


    void CollectAgents(Ticket rank)
    {
        agentQueue.Enqueue(rank);
        count++;
        Debug.Log("colling Agent " + agentQueue.Count);
        if (count == agents.Length)
        {
            //sessionOverEvent?.Invoke(agentQueue.Dequeue());
            CheckAllAgents();
            Debug.Log("All has arrived!");
            //sessionCompleteEvent?.Invoke("");
            count = 0;

        }
    }
    private void CheckAllAgents()
    {

        foreach (Ticket item in agentQueue)
            Debug.Log($"{item.Name}  time : {item.ElapsedTime}");

        Ticket ticket = (Ticket)agentQueue.Dequeue();

        sessionOverEvent?.Invoke(ticket);
        //sessionOverEvent
        //firstAgent = agentQueue.Dequeue().;  
        agentQueue.Clear();
        hasStarted = false;
    }
}
