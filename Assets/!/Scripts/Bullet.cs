using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet OnCollisionEnter " + collision.gameObject.name);  
        if (collision.gameObject.name == "CubeB")
        {
            //  speed ±ï±â.
            //Destroy(collision.gameObject); // Destroy target
           // Destroy(gameObject); // Destroy bullet
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet OnTriggerEnter " + other.gameObject.name);  
        
    }   

}
