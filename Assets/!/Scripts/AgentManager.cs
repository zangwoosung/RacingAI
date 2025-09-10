using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class AgentManager : MonoBehaviour
{

    public UnityEvent<Ticket, SpriteRenderer> AllAgentUnityEvent;
    public UnityEvent<Ticket> AgentUnityEvent;
    public Agent[] agents;

    Queue agentQueue = new Queue();
    Queue spriteQueue = new Queue();
    bool hasStarted = false;
    int count = 0;
    public void StartToRun(Vector3 pos)
    {
        if (hasStarted) return;
        hasStarted = true;
        agentQueue.Clear();
        spriteQueue.Clear();
       
        StartCoroutine(StartOnebyOne(pos));
    }

    IEnumerator StartOnebyOne(Vector3 pos)
    {
        firstSprite = null;
        firstTicket = new Ticket();
        firstTicket.Name=string.Empty;

        count = 0;
        foreach (var agent in agents)
        {
            yield return null;
            float speed = Random.Range(10, 1);
            agent.Setup(speed, pos);
            agent.CallBackAction(CollectAgents);
            agent.CallBackGetSprite(GetSprite);
            yield return null;
        }
    }

    SpriteRenderer firstSprite = null;
    Ticket firstTicket;

    void GetSprite(SpriteRenderer spriterenderer)
    {
        if (firstSprite == null)
        {
            firstSprite = spriterenderer;
            Debug.Log("first sprite " + firstSprite.sprite.name);
        }
    }

    void CollectAgents(Ticket ticket)
    {
       
        if (firstTicket.Name == string.Empty)
        {
            firstTicket = ticket;
         
        }

        count++;

        if (count == agents.Length)
        {
            CheckAllAgents();
            count = 0;
        }
    }
    private void CheckAllAgents()
    {      
        AllAgentUnityEvent?.Invoke(firstTicket, firstSprite);
       

        agentQueue.Clear();
        spriteQueue.Clear();
        hasStarted = false;
        
    }
}
