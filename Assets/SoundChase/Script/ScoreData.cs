using System;
using UnityEngine;

[Serializable]
public class ScoreData
{
    [SerializeField]
    private string songName;
    public string SongName => songName;

    [SerializeField]
    private float songLength;
    public float SongLength => songLength;

    [SerializeField]
    private int maxBarCount;
    public int MaxBarCount => maxBarCount;

    [SerializeField]
    private int rhythm;
    public int Rhythm => rhythm;

    [SerializeField]
    private int bpm;
    public int Bpm => bpm;

    public ScoreData(
        string songName,
        float songLength,
        int maxBarCount,
        int rhythm,
        int bpm)
    {
        this.songName = songName;
        this.songLength = songLength;
        this.maxBarCount = maxBarCount;
        this.rhythm = rhythm;
        this.bpm = bpm;
    }
}
