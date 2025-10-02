using UnityEngine;

public class Drone : MonoBehaviour
{
    
    public string Name { get; private set; }    
    public int Health { get; private set; }
    public float Speed { get; private set; }
    public int Damage { get; private set; }
    public float DetectionRange { get; private set; }
    public float AttackRange { get; private set; }
    public float AttackCooldown { get; private set; }




    public class Builder
    {
        string name = "Drone";
        int health=100;
        float speed=10;
        int damage=10;
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
