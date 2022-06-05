using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoundEditor
{
    public class SoundEditorToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        [SerializeField] private NotesData.NotesType notesType;

        public void Initialize(Action<NotesData.NotesType> onSelect)
        {
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener((x) =>
            {
                if (toggle.isOn)
                {
                    onSelect?.Invoke(notesType);
                }
            });
        }

        public void Select()
        {
            toggle.Select();
        }
    }
}
