public class ScoreData
{
    public readonly string SongName;

    public readonly float SongLength;

    public readonly int MaxBarCount;

    public readonly int Rhythm;

    public readonly int Bpm;

    public ScoreData(
        string songName,
        float songLength,
        int maxBarCount,
        int rhythm,
        int bpm)
    {
        SongName = songName;
        SongLength = songLength;
        MaxBarCount = maxBarCount;
        Rhythm = rhythm;
        Bpm = bpm;
    }
}
