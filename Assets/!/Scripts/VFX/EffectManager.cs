using System;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class EffectManager : MonoBehaviour
{
   
    public GameObject particlePrefab; // Assign your prefab in Inspector
    public GameObject firefab; // Assign your prefab in Inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bullet.OnHitContactEvent += OnHitContactEvent;
        DeadState.OnAgentDeath += PlayEndPS;
        AgentAI.OnFireEvent += OnHitContactEvent;
    }

  

    public  void OnHitContactEvent(Vector3 pos)
    {
        Debug.Log("EffectManager OnHitContactEvent " + pos);    
        GameObject psInstance = Instantiate(particlePrefab, pos, Quaternion.identity);
        ParticleSystem ps = psInstance.GetComponent<ParticleSystem>();
        ps.Play();

        Destroy(psInstance, ps.main.duration + ps.main.startLifetime.constant);
    }
    public void PlayEndPS(Vector3 pos)
    {
        Debug.Log("EffectManager OnHitContactEvent " + pos);
        GameObject psInstance = Instantiate(firefab, pos, Quaternion.identity);
        ParticleSystem ps = psInstance.GetComponent<ParticleSystem>();
        ps.Play();

        Destroy(psInstance, ps.main.duration + ps.main.startLifetime.constant);
    }
    public void OnHitDistanceEvent(Transform obj, float degree)
    {

    }
    public void SpawnParticles(Vector3 position)
    {
        // Optional cleanup
    }
}




