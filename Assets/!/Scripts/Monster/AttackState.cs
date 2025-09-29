using UnityEngine;

public class AttackState : IAgentAIState
{

    public void Enter(AgentAI agent)
    {
        agent.LootAtTarget();

    }
    public void Execute(AgentAI agent)
    {
        agent.OpenFire();

        //if (agent.IsDead())
        //    agent.ChangeState(new DeadState());
    }


    public void Exit(AgentAI agent)
    {
        agent.CeaseFire();
        Debug.Log("Exiting Attack State");

    }
}
