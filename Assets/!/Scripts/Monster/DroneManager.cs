using UnityEngine;

public class DroneManager : MonoBehaviour
{

    public GameObject dronePrefab;

    private void Start()
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
        Debug.Log($"Created Drone: {drone.Name}, Health: {drone.Health}, Speed: {drone.Speed}, Damage: {drone.Damage}, Detection Range: {drone.DetectionRange}, Attack Range: {drone.AttackRange}, Attack Cooldown: {drone.AttackCooldown}");
       
        GameObject clone = Instantiate(dronePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        clone.transform.SetParent(drone.transform);
    }
}

