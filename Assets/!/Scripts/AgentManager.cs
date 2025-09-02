
using System;
using System.Collections;

using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;
using Random = UnityEngine.Random;


public class AgentManager : MonoBehaviour
{
    public Agent[] agents;
    Queue agentQueue = new Queue();
    bool hasStarted = false;    
    public void StartToRun(Vector3 pos)
    {
        if (hasStarted) return; 
        hasStarted = true;
        Debug.Log("One time only ");
        agentQueue.Clear();
        StartCoroutine(StartOnebyOne(pos));
    }

    IEnumerator StartOnebyOne(Vector3 pos)
    {
        foreach (var agent in agents)
        {
            float speed = Random.Range(10, 15);
            agent.Setup(speed, pos);
            agent.CallBackAction(CollectAgents);
            yield return new WaitForSeconds(0.5f);
        }
    }


    void CollectAgents(Ticket rank)
    {
        agentQueue.Enqueue(rank);
        Debug.Log("colling Agent " + agentQueue.Count);
        if (agentQueue.Count == agents.Length)
        {
            CheckAllAgents();
            Debug.Log("All has arrived!");
        }
    }
    private void CheckAllAgents()
    {

        foreach (Ticket item in agentQueue)
            Debug.Log($"{item.Name}  time : {item.ElapsedTime}");
        agentQueue.Clear();
        hasStarted = false;
    }
}
