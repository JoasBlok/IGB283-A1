using UnityEngine;

public class ClockHands : MonoBehaviour
{

    [SerializeField] private float angle;
    [SerializeField] private Material material;
    [SerializeField] private float width;
    [SerializeField] private float length;
    [SerializeField] private Color color = Color.white;
    private Mesh mesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Add a mesh filter and mesh renderer, getting and setting useful variables
        mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        // Clear all vertex and index data from the mesh
        mesh.Clear();
        // Create a rectangle between (0,0,0), (0,1,0), (1,1,0), (1,0,0)
        mesh.vertices = new Vector3[]
        {

        new Vector3 (0,0,0),
        new Vector3 (0,length,0),
        new Vector3 (width,length, 0),
        new Vector3 (width,0,0),
        };
      
        mesh.colors = new Color[]
        {
            color,
            color,
            color, color,
        };
        // Specify which vertices the triangle uses
        // Specify which vertices the triangles use for each hand
        mesh.triangles = new int[]
        {
            // Seconds hand (vertices 0, 1, 2, 3)
            0, 1, 2,
            0, 2, 3
        };
    }

    // Update is called once per frame
    void Update()
    {
        RotateTriangle();
    }

    private Matrix3x3 Rotate(float angle)
    {

        // Calculate sin and cos of the angle once
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        // Create a new matrix for rotation
        Matrix3x3 r = new Matrix3x3(
        new Vector3(cos, -sin, 0),
        new Vector3(sin, cos, 0),
        new Vector3(0, 0, 1));

        return r;
    }

    private void RotateTriangle()
    {
        // Get the current mesh vertices
        Vector3[] vertices = mesh.vertices;
        Matrix3x3 r = Rotate(angle * Time.deltaTime);

        // Rotate every vertex in the mesh
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = r.MultiplyPoint(vertices[i]);
        }

        // Update the mesh vertices
        mesh.vertices = vertices;
        // Recalculate the mesh bounds
        mesh.RecalculateBounds();
    }
}
