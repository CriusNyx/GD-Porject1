using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Series
{
    /// <summary>
    /// Get the nth element of the sequence 0, 1, -1, 2, -2, 3, -3 ...
    /// </summary>
    /// <param name="elementIndex"></param>
    /// <returns></returns>
    public static int GrowingZigZag(int n)
    {
        // ((n + 1) / 2) determines the integer value of the output
        // ((n % 2) + 1 / 2) determines the sign of the output
        return ((n + 1) / 2) * ((n % 2) * 2 - 1);
    }

    /// <summary>
    /// Same as growing zig zag, but offset by +1/2;
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static float HalfGrowingZigZag(int n)
    {
        return -GrowingZigZag(n) + 0.5f;
    }
}