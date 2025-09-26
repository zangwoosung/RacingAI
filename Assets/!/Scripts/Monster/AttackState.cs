using UnityEngine;

public class AttackState : IAgentAIState
{

    public void Enter(AgentAI agent)
    { 
          agent.LootAtTarget();

    }
    public void Execute(AgentAI agent)
    {
        agent.LookAtPlayer();
        agent.FireAtPoint(agent.player.position);       
        //agent.PerformAttack();
        if (agent.IsDead())
            agent.ChangeState(new DeadState());
    }

    
    public void Exit(AgentAI agent) => Debug.Log("Exiting Attack State");
}
