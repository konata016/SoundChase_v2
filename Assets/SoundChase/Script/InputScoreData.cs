using UnityEngine;

public class InputScoreData
{
    private enum InputDataType
    {
        SongName,
        SongLength,
        MaxBarCount,
        Rhythm,
        Bpm
    }

    public readonly ScoreData scoreData;

    public InputScoreData(string importScoreDataName)
    {
        var textAsset = Resources.Load($"ScoreData/{importScoreDataName}", typeof(TextAsset)) as TextAsset;

        if (textAsset == null)
        {
            SC.Debug.LogError($"テキストアセットが存在しない importScoreDataName：{importScoreDataName}");
            return;
        }

        scoreData = getScoreData(textAsset);
    }

    private ScoreData getScoreData(TextAsset textAsset)
    {
        var ScoreDataTexts = textAsset.text;
        var scoreData = ScoreDataTexts.Split('\n');

        var songName = scoreData[(int)InputDataType.SongName];
        var songLength = float.Parse(scoreData[(int)InputDataType.SongLength]);
        var maxBarCount = int.Parse(scoreData[(int)InputDataType.MaxBarCount]);
        var rhythm = int.Parse(scoreData[(int)InputDataType.Rhythm]);
        var bpm = int.Parse(scoreData[(int)InputDataType.Bpm]);

        return new ScoreData(songName, songLength, maxBarCount, rhythm, bpm);
    }
}
