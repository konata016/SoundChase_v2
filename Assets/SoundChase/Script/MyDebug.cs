using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC {

    public class Debug : MonoBehaviour
    {
        public static void Log(string message)
        {
#if UNITY_EDITOR
            //UnityEngine.Debug.Log(message);
# endif
        }

        public static void LogError(string message)
        {
#if UNITY_EDITOR
            //UnityEngine.Debug.LogError(message);
# endif
        }
    }
}
