using LootLocker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class AgentManager : MonoBehaviour
{    
    public static event Action<List<MyData>>  OnSessionEndWithRankingEvent;
  //  public UnityEvent<Ticket, SpriteRenderer> AllAgentUnityEvent;
    public static event Action<Ticket, SpriteRenderer> AllAgentUnityEvent;
    
    public UnityEvent<Ticket> AgentUnityEvent;
    public WhiteLabelLoginTool leaderboardRacingAI;
    public MyPick myPickSO;
   // MainUI mainUI;
    // public AgentsSO agentsSO;
    public Queue agentQueue = new Queue();
    Agent[] agents;
    Queue spriteQueue = new Queue();
    bool hasStarted = false;
    int count = 0;
    string myPick = string.Empty;


    public void Initialize()
    {

       

        agents = new Agent[7];  
        agents[0] = GameObject.Find("Agents01").GetComponent<Agent>();
        agents[1] = GameObject.Find("Agents02").GetComponent<Agent>();
        agents[2] = GameObject.Find("Agents03").GetComponent<Agent>();
        agents[3] = GameObject.Find("Agents04").GetComponent<Agent>();
        agents[4] = GameObject.Find("Agents05").GetComponent<Agent>();
        agents[5] = GameObject.Find("Agents06").GetComponent<Agent>();
        agents[6] = GameObject.Find("Agents07").GetComponent<Agent>();
        Debug.Log("AgentManager Initialized");  
    }   
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


        List<MyData> DataList = new List<MyData>();   
        // 순위 추출 하기. 
        int index = 0;
        foreach (Ticket tempTicket in agentQueue)
        {
            Debug.Log(tempTicket.AgentSprite.name);
            
            Debug.Log("tempTicket.Name " + tempTicket.Name);
            if (myPickSO.pick == tempTicket.Name)
            {
                myPickSO.rank = index;
                Debug.Log("your picke came at " + index);
                //leaderboardRacingAI.UploadScore(index.ToString());
                //break;
            }
            index++;
            DataList.Add(new MyData(tempTicket.Name, tempTicket.ElapsedTime.ToString("F2"), tempTicket.AgentSprite));
        }
        MainUI mainui = UnityEngine.Object.FindFirstObjectByType<MainUI>();
        mainui.Setup(DataList); 
        // mainUI.Setup(DataList);
        OnSessionEndWithRankingEvent?.Invoke(DataList);
        AllAgentUnityEvent?.Invoke(new Ticket(), null); 
        agentQueue.Clear();
        spriteQueue.Clear();
        hasStarted = false;

    }
}
