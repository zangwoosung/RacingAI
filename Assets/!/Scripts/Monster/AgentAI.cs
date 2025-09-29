using System;
using System.Collections;
using UnityEngine;

public class AgentAI : MonoBehaviour
{
    public static event Action<Vector3> OnFireEvent;
    private IAgentAIState currentState;
    [SerializeField] private CircleFollower circleFollower;
    [SerializeField] AnimationController animController;

    public GameObject bulletPrefab;
    public GameObject simpleBulletPrefab;
    public Transform firePoint;
    public Transform fireTarget;
    public float bulletSpeed = 20f;
    public Transform player;
    public float rotationSpeed = 5f;
    int ammo = 3;
    public float detectionRadius = 10f;
    bool isBlocked = false;
    Transform nearestTarget;
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

    public void OpenFire()
    {
        StartCoroutine(OpenFireCoroutine());
    }
    public void CeaseFire()
    {
        StopAllCoroutines();
    }
    IEnumerator OpenFireCoroutine()
    {
        ammo = 10;

        while (ammo > 0)
        {
            Vector3 direction = (nearestTarget.position - firePoint.position).normalized;
            OnFireEvent?.Invoke(firePoint.position);
            GameObject bullet = Instantiate(simpleBulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = direction * bulletSpeed;
            yield return new WaitForSeconds(0.5f);
            Destroy(bullet, 1f);
            ammo--;
        }

        ChangeState(new DeadState());
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

    bool isDead = false;
    void Update()
    {
        Debug.Log("Current State: " + currentState.GetType().Name);
        if( Input.GetKeyDown(KeyCode.K))
        {
            ammo = 10;
            ChangeState(new IdleState());
        }
        if (currentState.GetType().Name == "DeadState") return;



        nearestTarget = FindNearestTarget(player);

        if (nearestTarget == null) return;

        LookAtPlayer();

        float distance = Vector3.Distance(transform.position, nearestTarget.transform.position);

        if (distance > detectionRadius)
        {
            ChangeState(new IdleState());
            return;
        }
        if (distance <= detectionRadius && distance > 5)
        {
            ChangeState(new AlertState());
            return;
        }
        if (distance <= 5)
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
        Debug.Log("Changing state to: " + newState?.GetType().Name);
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
        currentState?.Execute(this);

        circleFollower.ChangeColor(newState);
        animController.PlayAnimation(newState);

    }

    public void LookAtPlayer()
    {
        Vector3 direction = (nearestTarget.position - transform.position).normalized;
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
        return (ammo > 0) ? true : false;

    }

    public void PerformAttack()
    {
        FireAtPoint(nearestTarget.transform.position);
    }
    public void DestroySelf()
    {
        //Destroy(gameObject);

    }

    public void LootAtTarget()
    {
        RotateTowardsTarget(player, nearestTarget, rotationSpeed);
    }
}
