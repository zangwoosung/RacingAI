using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Engagement : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform fireTarget;
    public float bulletSpeed = 20f;
    public Transform player;
    public float rotationSpeed = 5f;

    void Start()
    {
        if (player == null)
        {
            player = this.transform;
        }
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
        //OnFireAtPoint.Invoke();
        Destroy(bullet, 1f);
    }
    public float detectionRadius = 50f;
    void Update()
    {
        Transform nearestTarget = FindNearestTarget(player);

        if (nearestTarget == null) return;

        float distance = Vector3.Distance(transform.position, nearestTarget.transform.position);
        Debug.Log("Distance to nearest target: " + distance);

        if (Input.GetMouseButtonDown(0)) // Left click
        {
            if (distance <= detectionRadius)
            {
                Debug.Log("Target is within radius!");
                RotateTowardsTarget(player, nearestTarget, rotationSpeed);
                FireAtPoint(nearestTarget.transform.position);

            }
            else
            {
                Debug.Log("Target is not");
            }

        }



    }

}
