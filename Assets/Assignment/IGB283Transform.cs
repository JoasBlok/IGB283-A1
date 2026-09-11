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
        return Matrix3x3.Translation(position.x, position.y);
    }
    public Matrix3x3 GetRotationMatrix()
    {
        return Matrix3x3.RotationZ(rotation.z);
    }
}
