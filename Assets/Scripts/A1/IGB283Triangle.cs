using System;
using UnityEngine;

public class IGB283Triangle
{
    // function to make 1 triangle object. 
    public IGB283Vector3[] MakeTriangle(IGB283Vector3 v1, IGB283Vector3 v2, IGB283Vector3 v3)
    {
        return new IGB283Vector3[] { v1, v2, v3 };
    }
    
}
