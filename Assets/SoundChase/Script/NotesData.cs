using System;
using UnityEngine;

[Serializable]
public class NotesData
{
    public enum NotesType
    {
        Fall,
        JustDodge,
        Technic,
        Point
    }

    [SerializeField]
    private NotesType type;
    public NotesType Type => type;

    [SerializeField]
    private int lanePositionNumber;
    public int LanePositionNumber => lanePositionNumber;

    [SerializeField]
    private float hitStartTime;
    public float HitStartTime => hitStartTime;

    [SerializeField]
    private float hitEndTime;
    public float HitEndTime => hitEndTime;

    public NotesData(
        NotesType notesType,
        int lanePositionNumber,
        float hitStartTime,
        float hitEndTime)
    {
        type = notesType;
        this.lanePositionNumber = lanePositionNumber;
        this.hitStartTime = hitStartTime;
        this.hitEndTime = hitEndTime;
    }
}
