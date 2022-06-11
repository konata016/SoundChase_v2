using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC {

    public class Debug
    {
        public static void Log(string message)
        {
#if UNITY_EDITOR
            //Debug.Log(message);
# endif
        }

        public static void LogError(string message)
        {
#if UNITY_EDITOR
            //Debug.LogError(message);
# endif
        }
    }
}
