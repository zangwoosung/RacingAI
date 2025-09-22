using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static event Action<Vector3> OnHitContactEvent;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet OnCollisionEnter " + collision.gameObject.name);

        collision.gameObject.GetComponent<Agent>().Speed=8;

        OnHitContactEvent.Invoke(collision.gameObject.transform.position); ;

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet OnTriggerEnter " + other.gameObject.name);
        other.gameObject.GetComponent<Agent>().Speed = 8;
        OnHitContactEvent.Invoke(other.gameObject.transform.position); ;

    }

}
