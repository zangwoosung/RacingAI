using LootLocker;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class AgentManager : MonoBehaviour
{
    
    public UnityEvent<Ticket, SpriteRenderer> AllAgentUnityEvent;
    public UnityEvent<Ticket> AgentUnityEvent;
    public WhiteLabelLoginTool leaderboardRacingAI;
    public Agent[] agents;

    //  스크립터블 오브젝  하나 생성. 
    public MyPick myPickSO;
    public Queue agentQueue = new Queue();
    Queue spriteQueue = new Queue();
    bool hasStarted = false;
    int count = 0;
    string myPick = string.Empty;
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
        firstTicket.Name = string.Empty;

        count = 0;
        foreach (var agent in agents)
        {
            yield return null;
            float speed = Random.Range(10, 50);
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
        agentQueue.Enqueue(ticket);
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
        Debug.Log("모두 골인 ");
        AllAgentUnityEvent?.Invoke(firstTicket, firstSprite);

        // 순위 추출 하기. 
        int index = 0;
        foreach (Ticket tempTicket in agentQueue)
        {
            Debug.Log($" {tempTicket.Name } 은 {index} 등입니다." );
            
            if (myPickSO.pick == tempTicket.Name)  //
            {
                myPickSO.rank = index;
                
                //leaderboardRacingAI.UploadScore(index.ToString());
                break;
            }
            index++;
        }

        agentQueue.Clear();
        spriteQueue.Clear();
        hasStarted = false;

    }
}
