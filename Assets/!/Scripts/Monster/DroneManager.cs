using Unity.VisualScripting;
using UnityEngine;

public class DroneManager : MonoBehaviour
{

    public GameObject dronePrefab;
    public GameObject motherPrefab;

    Orbit orbit;
    public void Init()
    {
        
    }
    public void CreateObject()
    {
        Drone drone = new Drone.Builder()
            .WithName("Scout Drone")
            .WithHealth(150)
            .WithSpeed(15f)
            .WithDamage(20)
            .WithDetectionRange(120f)
            .WithAttackRange(15f)
            .WithAttackCooldown(5f)
            .Build();
        
              
        
        GameObject cloneMother = Instantiate(motherPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject cloneChild = Instantiate(dronePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        
        cloneChild.AddComponent<Orbit>().SetTarget(cloneMother.transform);
        orbit = cloneChild.GetComponent<Orbit>();      
        cloneChild.transform.SetParent(cloneMother.transform);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            CreateObject();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            orbit.ChangeState(new AttackState());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            orbit.ChangeState(new AlertState());
        }
    }
}

