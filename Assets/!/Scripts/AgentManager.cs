
using System;
using System.Collections;

using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class AgentManager : MonoBehaviour
{
    public Agent[] agents;   

    Queue agentQueue = new Queue();
    public void StartToRun(Vector3 pos)
    {
        agentQueue.Clear();
        foreach (var agent in agents)
        {
            float speed = Random.Range(10, 50);
            agent.Setup(speed, pos);
            agent.CallBackAction(CollectAgents);

        }

    }


    void CollectAgents(Ticket rank)
    {
        agentQueue.Enqueue(rank);
        CheckAllAgents();
    }
    private void CheckAllAgents()
    {
        if (agentQueue.Count == agents.Length)
        {
            Debug.Log($"All has arrived!!!!!!!!!!!!!!!!!!!!!!!!");
            foreach (Ticket item in agentQueue)
                Debug.Log($"{item.Name}  time : {item.ElapsedTime}");

        }
    }
}
