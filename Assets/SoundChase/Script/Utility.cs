using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            SC.Debug.LogError($"想定外の文字列：{text}");
        }

        return word;
    }
}

public static class JsonUtilityExtension
{
    [Serializable]
    private struct JsonData<T>
    {
        [SerializeField]
        private T[] arr;
        public T[] Arr => arr;

        public JsonData(IEnumerable<T> data)
        {
            arr = data.ToArray();
        }
    }

    public static T[] ImportArr<T>(string dataPath)
    {
        var path = ImportSaveLocationPath(dataPath);
        var asset = $"{Resources.Load<TextAsset>(path)}";

        if (String.IsNullOrEmpty(asset))
        {
            Debug.LogError($"ファイルが存在しません！{path}");
            return null;
        }

        var data = JsonUtility.FromJson<JsonData<T>>(asset);
        return data.Arr;
    }

    public static T Import<T>(string dataPath)
    {
        var path = ImportSaveLocationPath(dataPath);
        var asset = $"{Resources.Load<TextAsset>(path)}";

        if (String.IsNullOrEmpty(asset))
        {
            Debug.LogError($"ファイルが存在しません！{path}");
            return default;
        }

        return JsonUtility.FromJson<T>(asset);
    }

    public static void ExportArr<T>(IEnumerable<T> data, string dataPath, bool prettyPrint = false)
    {
        var json = JsonUtility.ToJson(new JsonData<T>(data), prettyPrint);
        var writer = new StreamWriter(dataPath, false);
        Debug.Log(json);
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }

    public static void Export<T>(T data, string dataPath, bool prettyPrint = false)
    {
        var json = JsonUtility.ToJson(data, prettyPrint);
        var writer = new StreamWriter(dataPath, false);
        writer.Write(json);
        Debug.Log(json);
        writer.Flush();
        writer.Close();
    }

    private static string ImportSaveLocationPath(string dataPath)
    {
        const string Key = "Resources/";
        const string Extension = ".json";

        var adjustedPath = dataPath.Substring(dataPath.IndexOf(Key) + Key.Length);
        return adjustedPath.Remove(adjustedPath.IndexOf(Extension));
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
    /// チェック変更時処理
    /// (Action登録時にRemoveAllListenersが呼ばれた後に登録される)
    /// </summary>
    public static void OnValueChanged(
        this Toggle toggle,
        Action<bool> onValueChanged)
    {
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener((isOn) => onValueChanged(isOn));
    }

    /// <summary>
    /// チェック時処理
    /// (Action登録時にRemoveAllListenersが呼ばれた後に登録される)
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
