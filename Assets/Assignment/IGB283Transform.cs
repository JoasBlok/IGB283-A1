using UnityEngine;
public class IGB283Transform
{
    public IGB283Vector3 position;
    public IGB283Vector3 rotation;
    public IGB283Vector3 scale;

    public IGB283Transform()
    {
        position = new IGB283Vector3(0f, 0f, 0f);
        rotation = new IGB283Vector3(0f, 0f, 0f);
        scale = new IGB283Vector3(1f, 1f, 1f);
    }

    public void Translate(IGB283Vector3 translation)
    {
        position += translation;
    }

    public void Rotate(float x, float y, float z)
    {
        rotation.x += x;
        rotation.y += y;
        rotation.z += z;
    }

    public Matrix3x3 GetTranslationMatrix()
    {
        return Translation(position.x, position.y);
    }
    public Matrix3x3 GetRotationMatrix()
    {
        return RotationZ(rotation.z);
    }

    public Matrix3x3 RotationZ(float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;

        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        return new Matrix3x3(
             cos, -sin, 0,
             sin, cos, 0,
               0, 0, 1
        );
    }

    public static Matrix3x3 Translation(float x, float y)
    {
        return new Matrix3x3(
            1, 0, x,
            0, 1, y,
            0, 0, 1
        );
    }

    public Matrix3x3 Scale(float x, float y)
    {
        return new Matrix3x3(
            x, 0, 0,
            0, y, 0,
            0, 0, 1
            );
    }
}
