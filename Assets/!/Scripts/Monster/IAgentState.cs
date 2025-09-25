
using System.Threading;
using UnityEngine;

public interface IAgentAIState 
{   
        void Enter(AgentAI agent);
        void Execute(AgentAI agent);
        void Exit(AgentAI agent); 

}
