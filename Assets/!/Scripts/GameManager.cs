using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public NavMeshAgent agent;
    public AgentManager agentManager;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Vector3 pos = hitInfo.point;

                pos.y = 0;// = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
                          // agent.SetDestination(pos);

                agentManager.StartToRun(pos);

            }
        }
    }
}
