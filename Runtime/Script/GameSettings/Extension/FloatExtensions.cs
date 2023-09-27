using UnityEngine;

public static class FloatExtensions
{
    public static float GetAttenuation(  this float value)
    {
        return -80f + 100 * Mathf.Clamp01(value); // return value between -80 to 20
    }
}
