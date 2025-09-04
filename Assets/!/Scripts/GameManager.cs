using System;
using UnityEngine;
using UnityEngine.AI;
public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
   // public Camera agentCamera;
    public AgentManager agentManager;
    [SerializeField] Transform destination;


    void Start()
    {
        mainCamera.enabled = true;
     //   agentCamera.enabled = false;
    }

    public void StartGame()
    {
        agentManager.StartToRun(destination.position);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            bool isMainActive = mainCamera.enabled;
            mainCamera.enabled = !isMainActive;
           // agentCamera.enabled = isMainActive;
        }
    }
}
