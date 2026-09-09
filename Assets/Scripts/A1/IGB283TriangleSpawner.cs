using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using UnityEngine;

public class IGB283TriangleSpawner : MonoBehaviour
{
    public GameObject trianglePrefab;
    [SerializeField] private float triangleCount;
    [SerializeField] private Material material;
    [SerializeField] private Color color = Color.white;
    private Mesh mesh;

    IGB283Vector3[] diamondVertices =
{
    new IGB283Vector3(0, 4, 0),   // 0 centre
    new IGB283Vector3(0, 0, 0),   // 1 bottom
    new IGB283Vector3(1, 2, 0),   // 2
    new IGB283Vector3(2, 4, 0),   // 3 right
    new IGB283Vector3(1, 6, 0),   // 4
    new IGB283Vector3(0, 8, 0),   // 5 top
    new IGB283Vector3(-1, 6, 0),  // 6
    new IGB283Vector3(-2, 4, 0),  // 7 left
    new IGB283Vector3(-1, 2, 0)   // 8
};


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Add a mesh filter and mesh renderer, getting and setting useful variables
        mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        // Clear all vertex and index data from the mesh
        mesh.Clear();

        //IGB283Vector3[] vertices = IGB283Triangle.MakeTriangle(v1, v2, v3);
        mesh.vertices = IGB283Vector3.ConvertTo(diamondVertices);

        mesh.colors = new Color[] { color, color, color, color, color, color, color, color, color };
        mesh.triangles = new int[]
        {
            0, 5, 6,  // top-left
            0, 6, 7,  // upper-left
            0, 7, 8,  // lower-left
            0, 8, 1,  // bottom-left

            0, 1, 2,  // bottom-right
            0, 2, 3,  // lower-right
            0, 3, 4,  // upper-right
            0, 4, 5   // top-right
        };

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void CreateObject()
    //{ 
    //    List<IGB283Vector3> vertices  = new List<IGB283Vector3>();

    //    for (int i = 0; i< 32; i++)
    //    {
    //        IGB283Vector3[] triangle = SpawnTriangle(i);
    //        vertices.Add(triangle[0]);
    //        vertices.Add(triangle[1]);
    //        vertices.Add(triangle[2]);
    //    }
    //}

    //IGB283Vector3[] SpawnTriangle(int i)
    //{
        
        
    //    IGB283Vector3 v1 = 

       
    //    return trianglePrefab.GetComponent<IGB283Triangle>().MakeTriangle(v1, v2, v3);

    //}
    
}
