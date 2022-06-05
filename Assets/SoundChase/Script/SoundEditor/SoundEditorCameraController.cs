using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundEditor
{
    public class SoundEditorCameraController : MonoBehaviour
    {
        public void OnUpdate(float soundTime)
        {
            var pos = transform.position;
            pos.x = soundTime;
            transform.position = pos;
        }
    }
}
