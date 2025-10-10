using System;
using System.Collections;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public static event Action<Vector3> OnFireEvent;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed = 20f;
    public string Name { get; private set; }
    public int Health { get; private set; }
    public float Speed { get; private set; }
    public int Damage { get; private set; }
    public float DetectionRange { get; private set; }
    public float AttackRange { get; private set; }
    public float AttackCooldown { get; private set; }
    GameObject player = null;

    void OpenFire()
    {
        StartCoroutine(OpenFireCoroutine());    
    }

    int ammo = 10;  
    IEnumerator OpenFireCoroutine()
    {
        ammo = 2000;

        while (ammo > 0)
        {

            Vector3 direction = (player.transform.position - firePoint.position).normalized;
            OnFireEvent?.Invoke(firePoint.position);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = direction * bulletSpeed;
            yield return new WaitForSeconds(0.5f);
            Destroy(bullet, 1f);
            ammo--;
        }

       
    }

    void Start()
    {
        StartCoroutine(FindAgents());
    }   
    public void init()
    {
        StartCoroutine(FindAgents());
    }
    IEnumerator FindAgents()
    {

        Debug.Log("start to find Agent");
        player = null;
        // Keep checking until the player is found
        while (player == null)
        {
            player = GameObject.FindWithTag("Agent");
            yield return null; // Wait for the next frame
        }
        yield return null;
        OpenFire();
        Debug.Log("Player found: " + player.name);
    }

    public class Builder
    {
        string name = "Drone";
        int health = 100;
        float speed = 10;
        int damage = 10;
        float detectionRange = 101;
        float attackRange = 10;
        float attackCooldown = 10;



        public Builder WithName(string name)
        {
            this.name = name;
            return this;
        }
        public Builder WithHealth(int health)
        {
            this.health = health;
            return this;
        }
        public Builder WithSpeed(float speed)
        {
            this.speed = speed;
            return this;
        }
        public Builder WithDamage(int damage)
        {
            this.damage = damage;
            return this;
        }
        public Builder WithDetectionRange(float range)
        {
            this.detectionRange = range;
            return this;
        }
        public Builder WithAttackRange(float range)
        {
            this.attackRange = range;
            return this;
        }
        public Builder WithAttackCooldown(float cooldown)
        {
            this.attackCooldown = cooldown;
            return this;
        }
        public Drone Build()
        {

            Drone drone = new GameObject("Drone").AddComponent<Drone>();
            drone.Name = this.name;
            drone.Health = this.health;
            drone.Speed = this.speed;
            drone.Damage = this.damage;
            drone.DetectionRange = this.detectionRange;
            drone.AttackRange = this.attackRange;
            drone.AttackCooldown = this.attackCooldown;
            return drone;
        }
    }
}
