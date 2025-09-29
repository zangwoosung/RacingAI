using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RadiusRenderer : MonoBehaviour
{
    public float radius = 5f;
    public int segments = 100;
    private LineRenderer line;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.loop = true;

        // Set color   Red //  Green 
        line.material = new Material(Shader.Find("Sprites/Default")); // Simple shader
        line.startColor = Color.red;
        line.endColor = Color.red;

    }

    void Update()
    {
        Vector3 center = transform.position;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * 2 * Mathf.PI / segments;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            line.SetPosition(i, new Vector3(x, 0, z) + center);
        }
    }
}