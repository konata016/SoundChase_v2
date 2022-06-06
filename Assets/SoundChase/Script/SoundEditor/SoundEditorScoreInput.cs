using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundEditor
{
    public class SoundEditorScoreInput : MonoBehaviour
    {
        private float editorHorizontalLineHeight;

        private int maxHorizontalLaneCount;

        private SoundEditorNotes soundEditorNotes;

        private Transform soundEditorNotesRoot;

        public void Initialize(
            float editorHorizontalLineHeight,
            int maxHorizontalLaneCount,
            SoundEditorNotes soundEditorNotes,
            Transform soundEditorNotesRoot)
        {
            this.editorHorizontalLineHeight = editorHorizontalLineHeight;
            this.maxHorizontalLaneCount = maxHorizontalLaneCount;
            this.soundEditorNotes = soundEditorNotes;
            this.soundEditorNotesRoot = soundEditorNotesRoot;
        }

        public void CreateSoundEditorNotes(
            string importNotesDataName,
            float depth)
        {
            var data = new InputNotesData(importNotesDataName);
            var height = editorHorizontalLineHeight / maxHorizontalLaneCount;

            for (var i = 0; i < data.NotesDataArr.Length; i++)
            {
                var notesData = data.NotesDataArr[i];
                var notes = Instantiate(soundEditorNotes, soundEditorNotesRoot);

                var y = getPositionY(height, notesData.LanePositionNumber);
                var startPosition = new Vector2(notesData.HitStartTime, y);
                var endPosition = new Vector2(notesData.HitEndTime, y);

                notes.Initialize(notesData.Type, height);
                notes.SetLanePositionNumber(notesData.LanePositionNumber);
                notes.SetStartPosition(startPosition, depth);
                notes.SetEndPosition(endPosition, depth);
                notes.SetFixedPosition();
            }
        }

        private float getPositionY(float editorHeight, int lanePositionNumber)
        {
            return (editorHeight * lanePositionNumber) + (editorHeight / 2);
        }
    }
}
