using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

           if(Physics.Raycast(ray, out RaycastHit hitInfo))
           {
                Vector3 pos = hitInfo.point;

                pos.y = 0;// = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
                agent.SetDestination(pos);


            }   
        }
    }
}
