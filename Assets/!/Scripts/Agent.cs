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
    public Sprite AgentSprite;
    public Ticket(string name, float time, Sprite sp)
    {
        this.Name = name;
        this.ElapsedTime = time;
        this.AgentSprite = sp;

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

    private int health;

    public int Health
    {
        get { return health; }
        set { health = value;
        
            if(health <=0)
            {
                Debug.Log("»ç¶óÁü.");

                Destroy(this);
            }
        
        }
    }


    private int speed;

    public int Speed
    {
        get { return speed; }
        set
        {

            speed = value;
            body.text = speed.ToString();
            Debug.Log("Agent Speed set to " + speed);
        }
    }



    void Awake()
    {

        navAgent = GetComponent<NavMeshAgent>();
        originalPos = transform.position;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        root = _UIDocument.rootVisualElement;


        head = root.Q<Label>("head");
        body = root.Q<Label>("body");
        foot = root.Q<Label>("foot");
        head.text = spriteRenderer.sprite.name;

        Speed = (int)10;
        Health = 10;
    }

    public void Setup(float speed, Vector3 des)
    {
        body.text = speed.ToString();
        this.Speed = (int)speed;
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
            RankingAction(new Ticket(spriteRenderer.sprite.name, elapsedTime, spriteRenderer.sprite));
            navAgent.Warp(originalPos);
            foot.text = elapsedTime.ToString();

        }
        else
        {
            foot.text = elapsedTime.ToString();
        }
    }

}
