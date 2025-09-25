using UnityEngine;

public class AgentAI : MonoBehaviour
{
    private IAgentAIState currentState;
    [SerializeField] private CircleFollower circleFollower;

    public GameObject bulletPrefab;
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
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = direction * bulletSpeed;
      
        Destroy(bullet, 1f);
    }
    public float detectionRadius = 50f;
    bool isBlocked = false;    
    void Update()
    {
        if (isBlocked) return;

        if (ammo <= 0)
        {
            ChangeState(new DeadState());
         isBlocked = true;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ammo--;

            return;
        }


        Transform nearestTarget = FindNearestTarget(player);

        if (nearestTarget == null) return;

        float distance = Vector3.Distance(transform.position, nearestTarget.transform.position);
        Debug.Log("Distance to nearest target: " + distance);


        if (distance <= detectionRadius)
        {
            Debug.Log("Target is within radius!");
            RotateTowardsTarget(player, nearestTarget, rotationSpeed);
            FireAtPoint(nearestTarget.transform.position);
            ChangeState(new AlertState());
        }
        else
        {
            Debug.Log("Target is not");
        }

       


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
     
        

    }

    public void ChangeState(IAgentAIState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
        circleFollower.ChangeColor(newState);
    }

    // Example helper methods
    public bool IsPlayerNearby()
    {
        return false;


    }
    public bool IsPlayerCloseEnough()
    {
        return false;

    }
    public bool IsDead() { return false; }

    public void PerformAttack()
    {

    }
    public void DestroySelf() => Destroy(gameObject);
}
