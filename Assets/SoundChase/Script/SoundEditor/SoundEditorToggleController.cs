using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoundEditor
{
    public class SoundEditorToggleController : MonoBehaviour
    {
        [SerializeField] ToggleGroup toggleGroup;

        [SerializeField] SoundEditorToggle[] soundEditorToggleArr;

        public void Initialize(Action<NotesData.NotesType> onSelect)
        {
            for (var i = 0; i < soundEditorToggleArr.Length; i++)
            {
                soundEditorToggleArr[i].Initialize(onSelect);
            }
        }
    }
}