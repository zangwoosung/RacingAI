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

       
    public void Initialize()
    {
        Debug.Log("GameManager Initialized");   
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mainCamera.enabled = true;
        MainUI.SessionBeginEvent += StartSession;
        destination = GameObject.Find("Destination").transform;

       

    }
    public void StartSession()
    {
        agentManager.StartToRun(destination.position);
    }


    
}
