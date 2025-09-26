using System;
using UnityEngine;

public class AgentAI : MonoBehaviour
{
    private IAgentAIState currentState;
    [SerializeField] private CircleFollower circleFollower;

    public GameObject bulletPrefab;
    public GameObject simpleBulletPrefab;
    public Transform firePoint;
    public Transform fireTarget;
    public float bulletSpeed = 20f;
    public Transform player;
    public float rotationSpeed = 5f;
    int ammo = 3;
    void Start()
    {
        ChangeState(new IdleState());
    }
    void RotateTowardsTarget(Transform player, Transform target, float rotationSpeed)
    {
        Vector3 direction = (target.position - player.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        player.rotation = Quaternion.Slerp(player.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
    Transform FindNearestTarget(Transform player)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Agent");
        // Debug.Log("Found " + targets.Length + " targets.");
        Transform nearestTarget = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(player.position, target.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTarget = target.transform;
            }
        }

        return nearestTarget;
    }
    public void FireAtPoint(Vector3 targetPoint)
    {
        ammo--;
        Debug.Log("Firing at point: " + targetPoint);
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        GameObject bullet = Instantiate(simpleBulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = direction * bulletSpeed;

        Destroy(bullet, 1f);
    }
    public float detectionRadius = 5f;
    bool isBlocked = false;
    Transform nearestTarget;
    void Update()
    {


        nearestTarget = FindNearestTarget(player);

        if (nearestTarget == null) return;

        float distance = Vector3.Distance(transform.position, nearestTarget.transform.position);

        if (distance > detectionRadius)
        {
            ChangeState(new IdleState());
            return;
        }
        if (distance <= detectionRadius && distance > 3)
        {
            ChangeState(new AlertState());
            return;
        }
        if (distance <= 3)
        {
            ChangeState(new AttackState());
            return;
        }
    }

    public void ChangeState(IAgentAIState newState)
    {
        if (currentState != null && newState != null)
        {
            if (currentState.GetType() == newState.GetType())
            {                
                return;
            }
         }

        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
        currentState?.Execute(this);

        circleFollower.ChangeColor(newState);
    }

    public void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    public bool IsPlayerNearby()
    {
        return false;


    }
    public bool IsPlayerCloseEnough()
    {
        return false;

    }
    public bool IsDead()
    {
        return  (ammo > 0) ?  true:  false;    

    }

    public void PerformAttack()
    {
        FireAtPoint(nearestTarget.transform.position);
    }
    public void DestroySelf() => Destroy(gameObject);

    public void LootAtTarget()
    {
        RotateTowardsTarget(player, nearestTarget, rotationSpeed);
    }
}
