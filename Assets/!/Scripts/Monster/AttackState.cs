using UnityEngine;

public class AttackState : IAgentAIState
{
    public void Enter(AgentAI agent) => Debug.Log("Entering Attack State");
    public void Execute(AgentAI agent)
    {
        // Attack behavior
        agent.PerformAttack();
        if (agent.IsDead())
            agent.ChangeState(new DeadState());
    }
    public void Exit(AgentAI agent) => Debug.Log("Exiting Attack State");
}
