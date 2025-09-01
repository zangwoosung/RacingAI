using System;
using UnityEngine;
using UnityEngine.AI;
public class GameManager : MonoBehaviour
{
    public Camera mainCamera;   
    public Camera agentCamera;
    public AgentManager agentManager;
    [SerializeField] Transform destination;
   

    void Start()
    {
        mainCamera.enabled = true;
        agentCamera.enabled = false;
    }

   
   

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            agentManager.StartToRun(destination.position);
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if (Physics.Raycast(ray, out RaycastHit hitInfo))
            //{
            //    Vector3 pos = hitInfo.point;
            //    pos.y = 0;
            //    agentManager.StartToRun(pos);

            //}
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            bool isMainActive = mainCamera.enabled;
            mainCamera.enabled = !isMainActive;
            agentCamera.enabled = isMainActive;
        }
    }
}
