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
}

public class Calculation
{
    /// <summary>
    /// ���q
    /// 
    /// rhythm: ���q��
    /// </summary>
    public static float GetBeat(int rhythm)
    {
        return 4f / rhythm;
    }

    /// <summary>
    /// 1���߂̎���
    /// 
    /// rhythm: ���q��
    /// bpm:    �Ȃ̑���
    /// </summary>
    public static float GetBarTime(int rhythm, int bpm)
    {
        return (float)rhythm * 60 / (float)bpm;
    }

    /// <summary>
    /// �c��̏��ߐ�
    /// 
    /// time:   ���݂̎���
    /// rhythm: ���q��
    /// bpm:    �Ȃ̑���
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
