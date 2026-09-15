using System.Collections.Generic;
using UnityEngine;

public class IGB283TriangleSpawner : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private Color startColor = Color.blue;
    [SerializeField] private Color endColor = Color.red;

    private Mesh mesh;
    private IGB283Transform objectTransform;

    [SerializeField]
    private IGB283Vector3 startPoint =
        new IGB283Vector3(-5, 0, 0);

    [SerializeField]
    private IGB283Vector3 endPoint =
        new IGB283Vector3(5, 2, 0);

    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 1.5f;

    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float rotationSpeed = 90f;

    private bool movingRight = true;

    IGB283Vector3[] diamondVertices =
    {
        // Outer edge
        new IGB283Vector3( 0.0f,  0.9f, 0),   // 0 - leaf tip
        new IGB283Vector3( 0.18f, 0.72f, 0),  // 1
        new IGB283Vector3( 0.32f, 0.50f, 0),  // 2
        new IGB283Vector3( 0.30f, 0.28f, 0),  // 3
        new IGB283Vector3( 0.15f, 0.10f, 0),  // 4
        new IGB283Vector3( 0.0f,  0.0f, 0),   // 5 - base
        new IGB283Vector3(-0.12f, 0.12f, 0),  // 6
        new IGB283Vector3(-0.22f, 0.30f, 0),  // 7
        new IGB283Vector3(-0.20f, 0.50f, 0),  // 8
        new IGB283Vector3(-0.12f, 0.70f, 0),  // 9

        // Internal vertices
        new IGB283Vector3( 0.0f,  0.25f, 0),  // 10 - central vein
        new IGB283Vector3( 0.02f, 0.45f, 0),  // 11
        new IGB283Vector3( 0.0f,  0.65f, 0),  // 12
        new IGB283Vector3(-0.08f, 0.38f, 0),  // 13
        new IGB283Vector3( 0.10f, 0.35f, 0)   // 14
    };

    void Start()
    {
        mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;

        objectTransform = new IGB283Transform();

        // Create the mesh structure once.
        CreateObject();

        // Set the initial position.
        objectTransform.Translate(startPoint);

        // Apply the initial transform and colour.
        UpdateObjectTransform();
    }

    void Update()
    {
        AnimateObject();
        UpdateObjectTransform();
    }

    void AnimateObject()
    {
        // Rotate the object.
        objectTransform.Rotate(
            0,
            0,
            rotationSpeed * Time.deltaTime
        );

        // Determine movement direction.
        float direction = movingRight ? 1f : -1f;

        // Move the object.
        objectTransform.Translate(
            new IGB283Vector3(
                direction * movementSpeed * Time.deltaTime,
                0,
                0
            )
        );

        // Check whether the object reached the end point.
        if (objectTransform.position.x >= endPoint.x)
        {
            objectTransform.position.x = endPoint.x;
            movingRight = false;
        }

        // Check whether the object reached the start point.
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

        // Create four diamonds rotated around their centre.
        for (int i = 0; i < 4; i++)
        {
            float angle = i * 90f;

            Matrix3x3 diamondRotation =
                objectTransform.RotationZ(angle);

            int vertexOffset = vertices.Count;

            for (int j = 0; j < diamondVertices.Length; j++)
            {
                IGB283Vector3 vertex =
                    diamondRotation.MultiplyVector3(diamondVertices[j]);

                vertices.Add(vertex);
            }

            AddDiamondTriangles(triangles, vertexOffset);
        }

        // Set the mesh's base vertices and triangles.
        mesh.vertices =
            IGB283Vector3.ConvertTo(vertices.ToArray());

        mesh.triangles = triangles.ToArray();

        // Initialise the colour array.
        Color[] colors = new Color[vertices.Count];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = startColor;
        }

        mesh.colors = colors;

        mesh.RecalculateBounds();
    }

    void UpdateObjectTransform()
    {
        List<IGB283Vector3> transformedVertices =
            new List<IGB283Vector3>();

        // Get the object's current transformation matrices.
        Matrix3x3 rotationMatrix =
            objectTransform.GetRotationMatrix();

        Matrix3x3 translationMatrix =
            objectTransform.GetTranslationMatrix();

        // Calculate how far the object has travelled.
        float t =
            (objectTransform.position.x - startPoint.x) /
            (endPoint.x - startPoint.x);

        t = Mathf.Clamp01(t);

        // Scale based on the object's position.
        float currentScale =
            Mathf.Lerp(minScale, maxScale, t);

        Matrix3x3 scaleMatrix =
            objectTransform.Scale(currentScale, currentScale);

        // Combine the transformations.
        Matrix3x3 transformationMatrix =
            translationMatrix *
            rotationMatrix *
            scaleMatrix;

        // Transform every vertex of the mesh.
        for (int i = 0; i < 4; i++)
        {
            float angle = i * 90f;

            Matrix3x3 diamondRotation =
                objectTransform.RotationZ(angle);

            for (int j = 0; j < diamondVertices.Length; j++)
            {
                IGB283Vector3 vertex =
                    diamondRotation.MultiplyVector3(
                        diamondVertices[j]
                    );

                vertex =
                    transformationMatrix.MultiplyPoint(vertex);

                transformedVertices.Add(vertex);
            }
        }

        // Update the mesh's vertex positions.
        mesh.vertices =
            IGB283Vector3.ConvertTo(
                transformedVertices.ToArray()
            );

        // Update the colour based on the object's position.
        Color currentColor =
            Color.Lerp(startColor, endColor, t);

        Color[] colors =
            new Color[transformedVertices.Count];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = currentColor;
        }

        mesh.colors = colors;

        mesh.RecalculateBounds();
    }

    void AddTriangle(
        List<int> triangles,
        int offset,
        int a,
        int b,
        int c)
    {
        triangles.Add(offset + a);
        triangles.Add(offset + c);
        triangles.Add(offset + b);
    }

    void AddDiamondTriangles(
        List<int> triangles,
        int offset)
    {
        // Left side
        AddTriangle(triangles, offset, 0, 9, 12);
        AddTriangle(triangles, offset, 0, 12, 11);
        AddTriangle(triangles, offset, 9, 8, 13);
        AddTriangle(triangles, offset, 9, 13, 12);
        AddTriangle(triangles, offset, 8, 7, 13);
        AddTriangle(triangles, offset, 7, 6, 13);
        AddTriangle(triangles, offset, 6, 5, 10);
        AddTriangle(triangles, offset, 6, 10, 13);

        // Right side
        AddTriangle(triangles, offset, 0, 11, 12);
        AddTriangle(triangles, offset, 0, 12, 1);
        AddTriangle(triangles, offset, 1, 12, 14);
        AddTriangle(triangles, offset, 1, 14, 2);
        AddTriangle(triangles, offset, 2, 14, 3);
        AddTriangle(triangles, offset, 3, 14, 4);
        AddTriangle(triangles, offset, 4, 14, 10);
        AddTriangle(triangles, offset, 4, 10, 5);

        // Centre
        AddTriangle(triangles, offset, 12, 13, 10);
        AddTriangle(triangles, offset, 12, 10, 11);
        AddTriangle(triangles, offset, 11, 10, 14);
        AddTriangle(triangles, offset, 13, 6, 10);
    }
}
