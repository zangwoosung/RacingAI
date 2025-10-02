

using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target=null;      // The object to orbit around
    public float orbitSpeed = 20f; // Degrees per second
    public float orbitRadius = 5f; // Distance from the target

    private float angle;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    public void ChangeState(IAgentAIState state)
    {
        switch (state)
        {
            case IdleState:
                orbitRadius=15f;
                orbitSpeed = 5f;
               
                break;

            case AlertState:
                orbitRadius = 10f;
                orbitSpeed = 8f;
            
                break;

            case DeadState:
                orbitRadius = 2f;
                orbitSpeed = 1f;
             
                break;

            case AttackState:
                orbitRadius = 3f;
                orbitSpeed = 8f;             
                break;

            default:                
                break;
        }

    }

    void Update()
    {
        if (target == null) return;

        angle += orbitSpeed * Time.deltaTime;
        float radians = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * orbitRadius;
        transform.position = target.position + offset;
    }
}