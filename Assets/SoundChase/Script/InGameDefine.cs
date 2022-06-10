using UnityEngine;

public class InGameDefine
{
    public static readonly float NotesShowTime = 3;

    public static readonly float NotesHideTime = 3;

    public static readonly float FixLaneSpace = 1.2f;

    public static readonly float JustDodgeNotesFixHitTime = 0.1f;

    public static readonly float PointNotesFixHitTime = 0.05f;

    public static readonly float FallNotesFixHitTime = 0.02f;

    public static readonly float TechnicNotesFixHitTime = 0.1f;

    public static readonly string NotesTagName = "Notes";

    private static Camera mainCamera;
    public static Camera MainCamera => mainCamera ?? Camera.main;

    public static string NotesDataSaveLocationPath(string dataName) =>
        $"{ResourcesFolderPath}/Data/NotesData/{dataName}.json";

    public static string ScoreDataSaveLocationPath(string dataName) =>
         $"{ResourcesFolderPath}/Data/ScoreData/{dataName}.json";

    private static string ResourcesFolderPath => $"{Application.dataPath}/SoundChase/Resources";

    public static Color GetNotesSymbolColor(NotesData.NotesType type)
    {
        switch (type)
        {
            case NotesData.NotesType.Fall: return Color.gray;
            case NotesData.NotesType.JustDodge: return Color.blue;
            case NotesData.NotesType.Technic: return Color.yellow;
            case NotesData.NotesType.Point: return Color.red;
        }

        return Color.white;
    }
}

public class Calculation
{
    /// <summary>
    /// 拍子
    /// 
    /// rhythm: 拍子数
    /// </summary>
    public static float GetBeat(int rhythm)
    {
        return 4f / rhythm;
    }

    /// <summary>
    /// 1小節の時間
    /// 
    /// rhythm: 拍子数
    /// bpm:    曲の速さ
    /// </summary>
    public static float GetBarTime(int rhythm, int bpm)
    {
        return (float)rhythm * 60 / (float)bpm;
    }

    /// <summary>
    /// 残りの小節数
    /// 
    /// time:   現在の時間
    /// rhythm: 拍子数
    /// bpm:    曲の速さ
    /// </summary>
    public static int GetMaxBarCount(float time, int rhythm, int bpm)
    {
        return (int)(time / (60 * (float)rhythm / (float)bpm));
    }

    public static float MsToS(long ms)
    {
        return ms * 0.001f;
    }

    public static long SToMs(float s)
    {
        return (long)(s * 1000f);
    }
}
