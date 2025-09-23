using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static event Action<Vector3> OnHitContactEvent; //

    void OnCollisionEnter(Collision collision)
    {       

        collision.gameObject.GetComponent<Agent>().Speed--;
        OnHitContactEvent.Invoke(collision.gameObject.transform.position); ;

    }
    private void OnTriggerEnter(Collider other)
    {
        try
        {
            other.gameObject.GetComponent<Agent>().Speed--;   
            OnHitContactEvent.Invoke(other.gameObject.transform.position); 
        }
        catch (Exception)
        {
            Debug.Log("No Agent...");
        }        

    }

}
