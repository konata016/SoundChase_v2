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
            float depth,
            bool isSePreview)
        {
            var path = InGameDefine.NotesDataSaveLocationPath(importNotesDataName);
            var data = JsonUtilityExtension.ImportArr<NotesData>(path, true);

            // json形式でない場合のjsonを読み込む場合はこれを復活させる
            //var a = new InputNotesData(importNotesDataName);
            //var data = a.NotesDataArr;

            if (data == null)
            {
                Debug.LogError("データが存在しないためノーツの配置を復元出来ませんでした！");
                return;
            }

            var height = editorHorizontalLineHeight / maxHorizontalLaneCount;

            for (var i = 0; i < data.Length; i++)
            {
                var notesData = data[i];
                var notes = Instantiate(soundEditorNotes, soundEditorNotesRoot);

                var y = getPositionY(height, notesData.LanePositionNumber);
                var startPosition = new Vector2(notesData.HitStartTime, y);
                var endPosition = new Vector2(notesData.HitEndTime, y);

                notes.Initialize(notesData.Type, height, isSePreview);
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
