using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    private LineRenderer lineRenderer;

    public void SetPoints(Transform a, Transform b)
    {
        pointA = a;
        pointB = b;
    }

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (pointA != null && pointB != null)
        {
            lineRenderer.SetPosition(0, pointA.position);
            lineRenderer.SetPosition(1, pointB.position);
        }
    }
}
