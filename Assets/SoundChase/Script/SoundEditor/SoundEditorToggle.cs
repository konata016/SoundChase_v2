using System;
using UnityEngine;
using UnityEngine.UI;

namespace SoundEditor
{
    public class SoundEditorToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        [SerializeField] private Image lineImage;

        [SerializeField] private NotesData.NotesType notesType;

        public void Initialize(Action<NotesData.NotesType> onSelect)
        {
            toggle.OnSelected(() => onSelect?.Invoke(notesType));
            lineImage.color = InGameDefine.GetNotesSymbolColor(notesType);
        }

        public void Select()
        {
            toggle.Select();
        }
    }
}
