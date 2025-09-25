using UnityEngine;

public class IdleState : IAgentAIState
{
    public void Enter(AgentAI agent) => Debug.Log("Entering Idle State");
    public void Execute(AgentAI agent)
    {
        // Idle behavior
        if (agent.IsPlayerNearby())
            agent.ChangeState(new AlertState());
    }
    public void Exit(AgentAI agent) => Debug.Log("Exiting Idle State");
}

