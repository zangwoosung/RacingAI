using UnityEngine;

public class Engagement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // Where the bullet spawns
    public Transform fireTarget;
    public float bulletSpeed = 20f;

    public void FireAtPoint(Vector3 targetPoint)
    {
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = direction * bulletSpeed;
        Destroy(bullet, 1f); 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            FireAtPoint(fireTarget.position);

        }
    }

}
