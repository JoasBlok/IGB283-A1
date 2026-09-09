using UnityEngine;
public class IGB283Transform
{
    public IGB283Vector3 position;
    public IGB283Vector3 rotation;
    public IGB283Vector3 scale;

    public IGB283Transform()
    {
        position = IGB283Vector3.Zero;
        rotation = IGB283Vector3.Zero;
        scale = IGB283Vector3.One;
    }

    public void Rotate(float x, float y, float z)
    {
        rotation.x += x;
        rotation.y += y;
        rotation.z += z;
    }
}

