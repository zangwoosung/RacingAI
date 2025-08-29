
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
    public void StartToRun(Vector3 pos)
    {
        agentQueue.Clear();        
        StartCoroutine(StartOnebyOne(pos));
    }

    IEnumerator StartOnebyOne(Vector3 pos)
    {
        foreach (var agent in agents)
        {
            float speed = Random.Range(10, 50);
            agent.Setup(speed, pos);
            agent.CallBackAction(CollectAgents);
            yield return null;
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
            foreach (Ticket item in agentQueue)
                Debug.Log($"{item.Name}  time : {item.ElapsedTime}");
        }
    }
}
