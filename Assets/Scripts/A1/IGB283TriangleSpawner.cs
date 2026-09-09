using System.Collections.Generic;
using UnityEngine;

public class IGB283TriangleSpawner : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private Color color = Color.white;

    private Mesh mesh;

    // The 9 vertices that make up one diamond
    IGB283Vector3[] diamondVertices =
    {
        new IGB283Vector3(0, 4, 0),   // 0 centre
        new IGB283Vector3(0, 0, 0),    // 1 bottom
        new IGB283Vector3(1, 2, 0),    // 2 lower-right
        new IGB283Vector3(2, 4, 0),    // 3 right
        new IGB283Vector3(1, 6, 0),    // 4 upper-right
        new IGB283Vector3(0, 8, 0),    // 5 top
        new IGB283Vector3(-1, 6, 0),   // 6 upper-left
        new IGB283Vector3(-2, 4, 0),   // 7 left
        new IGB283Vector3(-1, 2, 0)    // 8 lower-left
    };

    void Start()
    {
        // Create the mesh
        mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;

        mesh.Clear();

        // Lists to hold the entire star
        List<IGB283Vector3> vertices = new List<IGB283Vector3>();
        List<int> triangles = new List<int>();

        // Create the four diamonds
        for (int i = 0; i < 4; i++)
        {
            float angle = i * 90f;

            Matrix3x3 rotation = Matrix3x3.RotationZ(angle);

            // Remember where this diamond's vertices start
            int vertexOffset = vertices.Count;

            // Rotate every vertex of the diamond
            for (int j = 0; j < diamondVertices.Length; j++)
            {
                IGB283Vector3 rotated =
                    rotation.MultiplyVector3(diamondVertices[j]);

                vertices.Add(rotated);
            }

            // Add the 8 triangles for this diamond
            triangles.Add(vertexOffset + 0);
            triangles.Add(vertexOffset + 5);
            triangles.Add(vertexOffset + 6);

            triangles.Add(vertexOffset + 0);
            triangles.Add(vertexOffset + 6);
            triangles.Add(vertexOffset + 7);

            triangles.Add(vertexOffset + 0);
            triangles.Add(vertexOffset + 7);
            triangles.Add(vertexOffset + 8);

            triangles.Add(vertexOffset + 0);
            triangles.Add(vertexOffset + 8);
            triangles.Add(vertexOffset + 1);

            triangles.Add(vertexOffset + 0);
            triangles.Add(vertexOffset + 1);
            triangles.Add(vertexOffset + 2);

            triangles.Add(vertexOffset + 0);
            triangles.Add(vertexOffset + 2);
            triangles.Add(vertexOffset + 3);

            triangles.Add(vertexOffset + 0);
            triangles.Add(vertexOffset + 3);
            triangles.Add(vertexOffset + 4);

            triangles.Add(vertexOffset + 0);
            triangles.Add(vertexOffset + 4);
            triangles.Add(vertexOffset + 5);
        }

        // Convert our custom vertices to Unity vertices
        mesh.vertices = IGB283Vector3.ConvertTo(vertices.ToArray());

        // Give every vertex a colour
        Color[] colors = new Color[vertices.Count];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = color;
        }

        mesh.colors = colors;

        // Set triangle indices
        mesh.triangles = triangles.ToArray();

        // Optional: recalculate the mesh bounds
        mesh.RecalculateBounds();
    }
}
