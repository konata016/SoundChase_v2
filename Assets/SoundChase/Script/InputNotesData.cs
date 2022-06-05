using UnityEngine;

public class InputNotesData
{
    private enum InputDataType
    {
        NotesType,
        HitStartTime,
        HitEndTime,
        LanePositionNumber
    }

    public readonly NotesData[] NotesDatas;

    public InputNotesData(string importNotesDataName)
    {
        var textAsset = Resources.Load($"NotesData/{importNotesDataName}", typeof(TextAsset)) as TextAsset;

        if (textAsset == null)
        {
            SC.Debug.LogError($"テキストアセットが存在しない importNotesDataName：{importNotesDataName}");
            return;
        }

        NotesDatas = getNotesDatas(textAsset);
    }

    private NotesData[] getNotesDatas(TextAsset textAsset)
    {
        var notesDataTexts = textAsset.text;
        var notesData = notesDataTexts.Split('\n');

        var notesDatas = new NotesData[notesData.Length - 1];

        for (int i = 0; i < notesData.Length - 1; i++)
        {
            var data = notesData[i].Split(',');

            SC.Debug.Log(data[0]);

            var notesType = getNotesType(data);
            var lanePositionNumber = getLanePositionNumber(data);
            var hitStartTime = getHitStartTime(data);
            var hitEndTime = getHitEndTime(data);

            notesDatas[i] =
                new NotesData(notesType, lanePositionNumber, hitStartTime, hitEndTime);
        }

        return notesDatas;
    }

    private NotesData.NotesType getNotesType(string[] data)
    {
        return Utility.TryConversionStringToEnum<NotesData.NotesType>(data[(int)InputDataType.NotesType]);
    }

    private int getLanePositionNumber(string[] data)
    {
        return int.Parse(data[(int)InputDataType.LanePositionNumber]);
    }

    private float getHitStartTime(string[] data)
    {
        return float.Parse(data[(int)InputDataType.HitStartTime]);
    }

    private float getHitEndTime(string[] data)
    {
        return float.Parse(data[(int)InputDataType.HitEndTime]);
    }
}
