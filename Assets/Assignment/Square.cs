using UnityEngine;
public class Square : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private IGB283Vector3 startPoint = new IGB283Vector3(0, 0, 0);

    private IGB283Transform objectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Add a mesh filter and mesh renderer, getting and setting useful variables
        Mesh mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        // Clear all vertex and index data from the mesh
        mesh.Clear();
        // Create a triangle between (0,0,0), (0,1,0), and (1,1,0)
        IGB283Vector3[] vertices = 
        {
            new IGB283Vector3(-0.5f, -0.5f, 0),
            new IGB283Vector3( 0.5f, -0.5f, 0),
            new IGB283Vector3( 0.5f,  0.5f, 0),
            new IGB283Vector3(-0.5f,  0.5f, 0)
        };

        mesh.vertices = IGB283Vector3.ConvertTo(vertices);

        // Set the triangle colours
        Color triColour = new Color(0.8f, 0.3f, 0.3f, 1f); // Pale red
        mesh.colors = new Color[]
        {
            triColour,
            triColour,
            triColour,
            triColour
        };
        // Specify which vertices the triangle uses
        mesh.triangles = new int[] {
            0, 2, 1,
            0, 3, 2 };

        objectTransform = new IGB283Transform();
        objectTransform.Translate(startPoint);

        Matrix3x3 t = objectTransform.GetTranslationMatrix();
        
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            vertices[i] = t.MultiplyPoint(vertices[i]);    
        }
        mesh.vertices = IGB283Vector3.ConvertTo(vertices); 
        mesh.RecalculateBounds();
    }
}