using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentsSO", menuName = "Scriptable Objects/AgentsSO")]
public class AgentsSO : ScriptableObject
{
   public  List<MyPick> agentsList = new List<MyPick>();
}
