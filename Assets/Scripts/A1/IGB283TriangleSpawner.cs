using System.Collections.Generic;
using UnityEngine;

public class IGB283TriangleSpawner : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private Color color = Color.white;

    private Mesh mesh;

    private IGB283Transform objectTransform;

    private IGB283Vector3 startPoint = new IGB283Vector3(-5, 0, 0);
    private IGB283Vector3 endPoint = new IGB283Vector3(5, 0, 0);

    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float rotationSpeed = 90f;

    private bool movingRight = true;

   
    IGB283Vector3[] diamondVertices =
    {
        new IGB283Vector3(0, 0.4f, 0),
        new IGB283Vector3(0, 0, 0),
        new IGB283Vector3(0.1f, 0.2f, 0),
        new IGB283Vector3(0.2f, 0.4f, 0),
        new IGB283Vector3(0.1f, 0.6f, 0),
        new IGB283Vector3(0, 0.8f, 0),
        new IGB283Vector3(-0.1f, 0.6f, 0),
        new IGB283Vector3(-0.2f, 0.4f, 0),
        new IGB283Vector3(-0.1f, 0.2f, 0)
    };

    void Start()
    {
        mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;

        mesh.Clear();
        mesh.RecalculateNormals();

        objectTransform = new IGB283Transform();

        //CreateObject();
    }

    void Update()
    {
        AnimateObject();
        CreateObject();
    }

    void AnimateObject()
    {
        // Rotate using IGB283Transform
        objectTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Move between the two points using our own Transform
        float direction;

        if (movingRight)
        {
            direction = 1f;
        }
        else
        {
            direction = -1f;
        }

        objectTransform.Translate(
            new IGB283Vector3(
                direction * movementSpeed * Time.deltaTime,
                0,
                0
            )
        );

        // Check whether we reached the endpoints
        if (objectTransform.position.x >= endPoint.x)
        {
            objectTransform.position.x = endPoint.x;
            movingRight = false;
        }

        if (objectTransform.position.x <= startPoint.x)
        {
            objectTransform.position.x = startPoint.x;
            movingRight = true;
        }

        
    }

    void CreateObject()
    {
        List<IGB283Vector3> vertices = new List<IGB283Vector3>();
        List<int> triangles = new List<int>();

        // Get rotation from our own Transform
        Matrix3x3 rotationMatrix =
            objectTransform.GetRotationMatrix();

        // Create the four diamonds
        for (int i = 0; i < 4; i++)
        {
            float angle = i * 90f;

            Matrix3x3 diamondRotation =
                Matrix3x3.RotationZ(angle);

            int vertexOffset = vertices.Count;

            for (int j = 0; j < diamondVertices.Length; j++)
            {
                // Rotate the diamond
                IGB283Vector3 vertex =
                    diamondRotation.MultiplyVector3(
                        diamondVertices[j]
                    );

                // Rotate the entire object
                vertex =
                    rotationMatrix.MultiplyVector3(vertex);

                // Translate the entire object
                vertex += objectTransform.position;

                vertices.Add(vertex);
            }

            AddDiamondTriangles(triangles, vertexOffset);
        }

        mesh.vertices =
            IGB283Vector3.ConvertTo(vertices.ToArray());

        Color[] colors = new Color[vertices.Count];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = color;
        }

        mesh.colors = colors;
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateBounds();
    }

    void AddDiamondTriangles(List<int> triangles, int offset)
    {
        triangles.Add(offset + 0);
        triangles.Add(offset + 5);
        triangles.Add(offset + 6);

        triangles.Add(offset + 0);
        triangles.Add(offset + 6);
        triangles.Add(offset + 7);

        triangles.Add(offset + 0);
        triangles.Add(offset + 7);
        triangles.Add(offset + 8);

        triangles.Add(offset + 0);
        triangles.Add(offset + 8);
        triangles.Add(offset + 1);

        triangles.Add(offset + 0);
        triangles.Add(offset + 1);
        triangles.Add(offset + 2);

        triangles.Add(offset + 0);
        triangles.Add(offset + 2);
        triangles.Add(offset + 3);

        triangles.Add(offset + 0);
        triangles.Add(offset + 3);
        triangles.Add(offset + 4);

        triangles.Add(offset + 0);
        triangles.Add(offset + 4);
        triangles.Add(offset + 5);
    }
}
