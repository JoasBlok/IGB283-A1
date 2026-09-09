using UnityEngine;

public class Assessment1 : MonoBehaviour
{
    [SerializeField] private float triangleCount;
    [SerializeField] private Material material;
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
        
    
        IGB283Vector3[] vectorArray = new IGB283Vector3[]
        {
            new IGB283Vector3(0,0,0),
            new IGB283Vector3(0,1,0),
            new IGB283Vector3(1,0,0)
        };

        mesh.vertices = IGB283Vector3.ConvertTo(vectorArray);
        mesh.colors = new Color[] { color, color, color };
        mesh.triangles = new int[] { 0, 1, 2 };

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
