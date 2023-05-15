using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectExtensions 
{
    public static int ToInt(this object obj)
    {
        int.TryParse(Convert.ToString(obj), out var value);
        return value;
    }
    public static float ToFloat(this object obj)
    {
        float.TryParse(Convert.ToString(obj), out var value);
        return value;
    }
    public static bool ToBool(this object obj)
    {
        bool.TryParse(Convert.ToString(obj), out var value);
        return value;
    }
}
