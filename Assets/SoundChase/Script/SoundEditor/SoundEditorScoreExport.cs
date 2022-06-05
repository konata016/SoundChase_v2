using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SoundEditor
{
    public class SoundEditorScoreExport : MonoBehaviour
    {
        [SerializeField] private Transform soundEditorNotesRoot;

        public void ExportNotesData(string fileName)
        {
            var dataList = new List<string>();

            for (var i = 0; i < soundEditorNotesRoot.childCount; i++)
            {
                var child = soundEditorNotesRoot.GetChild(i);
                var soundEditorNotes = child.GetComponent<SoundEditorNotes>();

                dataList.Add(getData(soundEditorNotes.GetNotesData()));
            }

            var filePath = $"Assets/SoundChase/Resources/NotesData/{fileName}.json";
            File.WriteAllLines(filePath, dataList);
        }

        public void ExportScoreData(SoundData soundData, string fileName)
        {
            List<string> dataList = new List<string>();

            dataList.Add($"{soundData.Name}");
            dataList.Add($"{soundData.EndTime}");
            dataList.Add($"{soundData.MaxBarCount}");
            dataList.Add($"{soundData.Rhythm}");
            dataList.Add($"{soundData.Bpm}");

            var filePath = $"Assets/SoundChase/Resources/ScoreData/{fileName}.json";
            File.WriteAllLines("Assets/test.json", dataList);
        }

        private string getData(NotesData data)
        {
            var str = 
                $"{data.Type}," +
                $"{data.HitStartTime}," +
                $"{data.HitEndTime}," +
                $"{data.LanePositionNumber}";

            return str;
        }
    }
}
