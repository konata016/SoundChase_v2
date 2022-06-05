using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            instance = (T) FindObjectOfType(typeof(T));
            
            if (instance == null)
            {
                Debug.LogError($"SingletonMonoBehaviour エラー");
            }

            return instance;
        }
    }

    private void Awake()
    {
        //instance = Instance;
        onAwake();
    }

    protected void onAwake() { }
}
