using System;
using UnityEngine;

public class IGB283Vector3
{
    // Listed below are all the places in this file that are missing functional code.
    // You can change the X on each item to a V when you complete them to help you keep track.
    // 
    // Note: you are not required to complete this file for your assignment. Just fill in the
    //       sections that you need. Recommended items are marked with an asterisk (*)
    // 
    // Code Checklist:
    // - Static Methods:
    //     V Normalize*
    //     V Dot*
    //     V Cross*
    //     V Distance*
    //     X Lerp
    //     X Scale
    //     X Min
    //     X Max
    //     X Angle
    //     V ConvertFrom**
    //     V ConvertTo**
    //
    // - Fields:
    //     V SqrMagnitude
    //     V Magnitude*
    //  
    // - Operators*:
    //     V *
    //     V /
    //     V +
    //     V -
    //     V -
    //
    // - Methods:
    //     V Equals*


    #region Fields and Indexing
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;

    private static readonly System.IndexOutOfRangeException badIndexException = new System.IndexOutOfRangeException("The index must be between 0 and 2.");

    // Support value indexing
    public float this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return x;
                case 1: return y;
                case 2: return z;
                default:
                    throw badIndexException;
            }
        }
        set
        {
            switch (index)
            {
                case 0:
                    {
                        x = value;
                        break;
                    }
                case 1:
                    {
                        y = value;
                        break;
                    }
                case 2:
                    {
                        z = value;
                        break;
                    }
                default:
                    throw badIndexException;
            }
        }
    }
    #endregion



    #region Constructors
    // Parameterless constructor
    public IGB283Vector3()
    {
        x = 0f;
        y = 0f;
        z = 0f;
    }

    // XY constructor
    public IGB283Vector3(float x, float y)
    {
        this.x = x;
        this.y = y;
        this.z = 0f;
    }

    // Full constructor
    public IGB283Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    #endregion



    #region Static Fields
    public static readonly IGB283Vector3 Zero = new IGB283Vector3(0f, 0f, 0f);
    public static readonly IGB283Vector3 One = new IGB283Vector3(1f, 1f, 1f);

    public static readonly IGB283Vector3 NegativeInfinity = new IGB283Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
    public static readonly IGB283Vector3 PositiveInfinity = new IGB283Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

    public static readonly IGB283Vector3 Right = new IGB283Vector3(1f, 0f, 0f);
    public static readonly IGB283Vector3 Up = new IGB283Vector3(0f, 1f, 0f);
    public static readonly IGB283Vector3 Forward = new IGB283Vector3(0f, 0f, 1f);

    public static readonly IGB283Vector3 Left = new IGB283Vector3(-1f, 0f, 0f);
    public static readonly IGB283Vector3 Down = new IGB283Vector3(0f, -1f, 0f);
    public static readonly IGB283Vector3 Back = new IGB283Vector3(0f, 0f, -1f);

    private const int vectorOrder = 3;
    #endregion



    // TODO - Lerp, Scale, Min, Max, Angle 
    #region Static Methods

    // The normalized vector in the same direction with length 1
    public static IGB283Vector3 Normalize(IGB283Vector3 value)
    {
        float x = value.x / value.Magnitude;
        float y = value.y / value.Magnitude;
        float z = value.z / value.Magnitude;

        return new IGB283Vector3(x, y, z);
        
    }

    // The dot product of two vectors
    public static float Dot(IGB283Vector3 a, IGB283Vector3 b)
    {
        float dot = (a.x * b.x + a.y * b.y + a.z * b.z);
        return dot;
    }

    // The cross product of two vectors
    public static IGB283Vector3 Cross(IGB283Vector3 a, IGB283Vector3 b)
    {
        float x = (a.y * b.z - a.z * b.y);
        float y = (a.z * b.x - a.x * b.z);
        float z = (a.x * b.y -  a.y * b.x);

        return new IGB283Vector3(x, y, z);

    }

    // The distance between two points
    public static float Distance(IGB283Vector3 a, IGB283Vector3 b)
    {
        float x = (a.x - b.x);
        float y = (a.y - b.y);
        float z = (a.z - b.z);

        float sqr = (x * x + y * y + z * z);

        float d = Mathf.Sqrt(sqr);
        return d;  
    }

    // Linearly interpolate between two vectors by a given percentage
    public static IGB283Vector3 Lerp(IGB283Vector3 a, IGB283Vector3 b, float t)
    {
        // -- Your Code here --
        // Hint: scale the vector going from a to b by t, and add it to a
        throw new System.NotImplementedException();
    }

    // Perform element-wise multiplication of two vectors
    public static IGB283Vector3 Scale(IGB283Vector3 a, IGB283Vector3 b)
    {
        // -- Your Code here --
        throw new System.NotImplementedException();
    }

    // Create a new vector with the smallest elements from two vectors
    public static IGB283Vector3 Min(IGB283Vector3 a, IGB283Vector3 b)
    {
        // -- Your Code here --
        throw new System.NotImplementedException();
    }

    // Create a new vector with the largest elements from two vectors
    public static IGB283Vector3 Max(IGB283Vector3 a, IGB283Vector3 b)
    {
        // -- Your Code here --
        throw new System.NotImplementedException();
    }

    // The angle betwen two vectors
    public static float Angle(IGB283Vector3 a, IGB283Vector3 b)
    {
        // -- Your Code here --
        // Hint: use the alternative dot product formula to isolate the angle
        throw new System.NotImplementedException();
    }

    // Convert from a Vector3 to IGB283Vector3
    public static IGB283Vector3 ConvertFrom(Vector3 v)
    {
        IGB283Vector3 converted = new IGB283Vector3(v.x, v.y, v.z);
        return converted;
    }

    // Convert from a IGB283Vector3 to Vector3
    public static Vector3 ConvertTo(IGB283Vector3 v)
    {
        Vector3 converted = new Vector3(v.x, v.y, v.z);
        return converted;
    }

    // Convert an array of Vector3 to IGB283Vector3
    public static IGB283Vector3[] ConvertFrom(Vector3[] vectors)
    {
        IGB283Vector3[] converted = new IGB283Vector3[vectors.Length];

        for (int i = 0; i < converted.Length; ++i)
            converted[i] = ConvertFrom(vectors[i]);

        return converted;
    }

    // Convert an array of IGB283Vector3 to Vector3
    public static Vector3[] ConvertTo(IGB283Vector3[] vectors)
    {
        Vector3[] converted = new Vector3[vectors.Length];

        for (int i = 0; i < converted.Length; ++i)
            converted[i] = ConvertTo(vectors[i]);

        return converted;
    }
    #endregion



    
    #region Fields

    // The squared length or distance represented by the vector
    public float SqrMagnitude
    {
        get
        {
            float sqrMagnitude = x * x + y * y + z * z;  
            return sqrMagnitude;
        }
    }

    // The length or distance represented by the vector
    public float Magnitude
    {
        get
        {
            float magnitude = MathF.Sqrt(SqrMagnitude);
            return magnitude;
        }
    }

    // The normalized vector in the same direction with length 1
    public IGB283Vector3 Normalized
    {
        get { return Normalize(this); }
    }
    #endregion


    #region Operators

    // Add two vectors
    public static IGB283Vector3 operator +(IGB283Vector3 a, IGB283Vector3 b)
    {
        float x = a.x + b.x; 
        float y = a.y + b.y;
        float z = a.z + b.z;

        return new IGB283Vector3(x, y, z);
    }

    // Subtract vector b from a
    public static IGB283Vector3 operator -(IGB283Vector3 a, IGB283Vector3 b)
    {
        float x = a.x - b.x;
        float y = a.y - b.y;
        float z = a.z - b.z;

        return new IGB283Vector3(x, y, z);
    }

    // Negate the vector
    public static IGB283Vector3 operator -(IGB283Vector3 a)
    {
        float x = a.x * -1;
        float y = a.y * -1;
        float z = a.z * -1;

        return new IGB283Vector3(x, y,z);
    }

    // Multiply a scalar and a vector
    public static IGB283Vector3 operator *(float scalar, IGB283Vector3 v)
    {
        float x = scalar * v.x;
        float y = scalar * v.y;
        float z = scalar * v.z;

        return new IGB283Vector3(x,y,z);
    }

    // Multiply a vector and a scalar
    public static IGB283Vector3 operator *(IGB283Vector3 v, float scalar)
    {
        float x = v.x * scalar;
        float y = v.y * scalar;
        float z = v.z * scalar;

        return new IGB283Vector3(x, y, z);
    }

    // Divide a vector by a scalar
    public static IGB283Vector3 operator /(IGB283Vector3 v, float scalar)
    {
        float x = v.x / scalar;
        float y = v.y / scalar;
        float z = v.z / scalar;
        return new IGB283Vector3(x, y, z);
    }

    // Check vector equality
    public static bool operator ==(IGB283Vector3 a, IGB283Vector3 b)
    {
        if (a is null)
            return b is null;
        else
            return a.Equals(b);
    }

    // Check vector inequality
    public static bool operator !=(IGB283Vector3 a, IGB283Vector3 b)
    {
        return !(a == b);
    }
    #endregion



    #region Methods

    // Test the equality between this vector and a given other vector
    public bool Equals(IGB283Vector3 other)
    {
        if (other is null)
            return false;

        return x == other.x &&
               y == other.y &&
               z == other.z;
    }


    // Test the equality between this vector and a given object
    public override bool Equals(object obj)
    {
        // Check if the object can be casted to IGB283Vector3
        IGB283Vector3 other = obj as IGB283Vector3;
        if (other is null)
            return false;

        return Equals(other);
    }

    // Generate a hash code based on the current values
    public override int GetHashCode()
    {
        return System.HashCode.Combine(x, y, z);
    }

    public override string ToString()
    {
        return $"({x:N3}, {y:N3}, {z:N3})";
    }

    // Normalize this vector
    public void Normalize()
    {
        IGB283Vector3 normalized = Normalized;
        x = normalized.x;
        y = normalized.y;
        z = normalized.z;
    }
    #endregion
}
