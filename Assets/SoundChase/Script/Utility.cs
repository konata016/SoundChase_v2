using System;
using UnityEngine;
using UnityEngine.UI;

public class Utility
{
    public static float GetMedian(float min,float max)
    {
        return (min + max) * 0.5f;
    }

    public static T TryConversionStringToEnum<T>(string text) where T : struct, Enum
    {
        if (!Enum.TryParse(text, out T word) || !Enum.IsDefined(typeof(T), word))
        {
            SC.Debug.LogError($"�z��O�̕�����F{text}");
        }

        return word;
    }
}

public static class TransformExtension
{
    public static void AccessAllChildComponent<T>(
        this Transform transform,
        Action<T> onAccessedComponent)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var component = child.GetComponent<T>();
            onAccessedComponent?.Invoke(component);
        }
    }
}

public static class ToggleExtension
{
    /// <summary>
    /// �`�F�b�N�ύX������
    /// (Action�o�^����RemoveAllListeners���Ă΂ꂽ��ɓo�^�����)
    /// </summary>
    public static void OnValueChanged(
        this Toggle toggle,
        Action<bool> onValueChanged)
    {
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener((isOn) => onValueChanged(isOn));
    }

    /// <summary>
    /// �`�F�b�N������
    /// (Action�o�^����RemoveAllListeners���Ă΂ꂽ��ɓo�^�����)
    /// </summary>
    public static void OnSelected(
        this Toggle toggle,
        Action onSelected)
    {
        toggle.OnValueChanged((isOn) =>
        {
            if (isOn)
            {
                onSelected?.Invoke();
            }
        });
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
