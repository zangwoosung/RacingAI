using System.Threading;
using UnityEngine;

public class AlertState : IAgentAIState
{
    public void Enter(AgentAI agent) => Debug.Log("Entering Alert State");
    public void Execute(AgentAI agent)
    {        
        if (agent.IsPlayerCloseEnough())
            agent.ChangeState(new AttackState());
    }
    public void Exit(AgentAI agent) { Debug.Log("Exiting Alert State"); }
}
