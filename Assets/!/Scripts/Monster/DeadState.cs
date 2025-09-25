using System;
using UnityEngine;

public class DeadState : IAgentAIState
{
   public static event Action<Vector3> OnAgentDeath;
    public void Enter(AgentAI agent) {

        OnAgentDeath.Invoke(agent.transform.position);

        Debug.Log("Entering Dead State"); 
    
    }
    public void Execute(AgentAI agent)
    {        
        OnAgentDeath.Invoke(agent.transform.position);
        Debug.Log("Execute");

        agent.DestroySelf();
    }
    public void Exit(AgentAI agent) => Debug.Log("Exiting Dead State");
}