using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static event Action<Vector3> OnHitContactEvent;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet OnCollisionEnter " + collision.gameObject.name);

        collision.gameObject.GetComponent<Agent>().Speed--;

        OnHitContactEvent.Invoke(collision.gameObject.transform.position);

        try
        {

            collision.gameObject.GetComponent<TargetWobble>().TriggerWobble();

        }
        catch (Exception)
        {

            
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet OnTriggerEnter " + other.gameObject.name);
        other.gameObject.GetComponent<Agent>().Speed--;
        OnHitContactEvent.Invoke(other.gameObject.transform.position); ;

        try
        {
            other.gameObject.GetComponent<TargetWobble>().TriggerWobble();

        }
        catch (Exception)
        {


        }

    }

}
