using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleFollower : MonoBehaviour
{
    public float radius = 1f;
    public int segments = 36;
    private LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.loop = true;
        line.material.color = Color.green;
    }
    public void ChangeColor(IAgentAIState state)
    {
        switch (state)
        {
            case IdleState:
                line.material.color = Color.green;
                break;

            case AlertState:
                line.material.color = Color.yellow;
                break;

            case DeadState:
                line.material.color = Color.gray;
                break;

            case AttackState:
                line.material.color = Color.red;
                break;

            default:
                line.material.color = Color.white;
                break;
        }

    }

    void Update()
    {
        Vector3[] points = new Vector3[segments + 1];
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * Mathf.PI * 2f / segments;
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            points[i] = transform.position + offset;
        }
        line.SetPositions(points);
    }
}
