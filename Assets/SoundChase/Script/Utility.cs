using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static float GetMedian(float min,float max)
    {
        return (min + max) * 0.5f;
    }

    public static T TryConversionStringToEnum<T>(string text) where T : struct
    {
        if (!Enum.TryParse(text, out T word) || !Enum.IsDefined(typeof(T), word))
        {
            SC.Debug.LogError($"‘z’èŠO‚Ì•¶š—ñF{text}");
        }

        return word;
    }
}

public class Range<T>
{
    public readonly T Min;

    public readonly T Max;

    public bool IsMinAndMaxEqual => Min.Equals(Max);

    public Range(T min,T max)
    {
        Min = min;
        Max = max;
    }
}
