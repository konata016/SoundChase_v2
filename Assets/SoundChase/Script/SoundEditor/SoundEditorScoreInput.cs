using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundEditor
{
    public class SoundEditorScoreInput : MonoBehaviour
    {
        [SerializeField] private SoundEditorNotes soundEditorNotes;

        [SerializeField] private Transform soundEditorNotesRoot;

        private InputNotesData data;

        public void CreateSoundEditorNotes(string importNotesDataName)
        {
            data = new InputNotesData(importNotesDataName);
            

        }
    }
}
