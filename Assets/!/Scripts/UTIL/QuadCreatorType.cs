using UnityEngine;
using UnityEngine.UIElements;

public class QuadCreatorType : MonoBehaviour
{
    [SerializeField] Transform container;
    public float width = 2f;
    public float height = 3f;
    [SerializeField] Material myMat;
    Vector3 position = Vector3.zero;
    Vector3 worldPos;
    
    void Start()
    {

      var  quad = new GameObject("AreaQuad");
        quad.transform.position = position;
        quad.transform.parent = container;
        quad.transform.up = Vector3.up;
        MeshFilter meshFilter = quad.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = quad.AddComponent<MeshRenderer>();
        meshRenderer.material = myMat;// new Material(Shader.Find("Standard"));

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, height, 0),
            new Vector3(width, height, 0)
        };

        int[] triangles = new int[6]
        {
            0, 2, 1,
            2, 3, 1
        };

        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };

        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        meshFilter.mesh = mesh;
    }
}
