using System;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Label = UnityEngine.UIElements.Label;
public struct Ticket
{
    public float ElapsedTime;
    public string Name;
    public Ticket(float r, string name)
    {
        this.ElapsedTime = r;
        this.Name = name;
    }
}

public class Agent : MonoBehaviour
{
    Vector3 destination = Vector3.zero;
    NavMeshAgent navAgent;
    Vector3 originalPos;
    Action<Ticket> RankingAction;
    SpriteRenderer spriteRenderer;
    float startTime;
    float elapsedTime;
    [Header("WorldSpaceUI")]
    [SerializeField] UIDocument _UIDocument;
    VisualElement root;
    Label head;
    Label body;
    Label foot;


    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        originalPos = transform.position;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        root = _UIDocument.rootVisualElement;


        head = root.Q<Label>("head");
        body = root.Q<Label>("body");
        foot = root.Q<Label>("foot");
        head.text = spriteRenderer.sprite.name;
    }

    public void Setup(float speed, Vector3 des)
    {
        body.text = speed.ToString();
        startTime = Time.time;
        elapsedTime = 0;
        navAgent.speed = speed;
        navAgent.SetDestination(des);
        destination = des;

        foot.text = "0";
    }

    public void CallBackAction(Action<Ticket> action)
    {
        RankingAction = action;
    }

    public void Restore()
    {
        elapsedTime = 0;
    }
    public void CallBackGetSprite(Action<SpriteRenderer> action)
    {
        action?.Invoke(spriteRenderer);
    }

    void Update()
    {
        if (destination == Vector3.zero) return;

        if (navAgent.remainingDistance < navAgent.stoppingDistance)
        {
            destination = Vector3.zero;
            elapsedTime = Time.time - startTime;
            RankingAction(new Ticket(elapsedTime, spriteRenderer.sprite.name));
            navAgent.Warp(originalPos);
            foot.text = elapsedTime.ToString();
        }
        else
        {
            foot.text = elapsedTime.ToString();
        }
    }

}
