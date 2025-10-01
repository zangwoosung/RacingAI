using UnityEngine;

public class RobotManager : MonoBehaviour
{


    [SerializeField] Transform[] targetPos;
    [SerializeField] GameObject[] robotPrefabs;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize()
    {
        for (int i = 0; i < robotPrefabs.Length; i++)
        {
            Instantiate(robotPrefabs[i], targetPos[i].position, Quaternion.identity);
        }   

    }

}
