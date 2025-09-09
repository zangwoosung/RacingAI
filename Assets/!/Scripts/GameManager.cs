using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
  
    public AgentManager agentManager;
    [SerializeField] Transform destination;


    void Start()
    {
        mainCamera.enabled = true;
       
    }

    public void StartGame()
    {
        agentManager.StartToRun(destination.position);
    }


    
}
