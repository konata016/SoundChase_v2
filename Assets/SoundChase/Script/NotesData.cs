public class NotesData
{
    public enum NotesType
    {
        Fall,
        JustDodge,
        Technic,
        Point
    }

    public readonly NotesType Type;

    public readonly int LanePositionNumber;

    public readonly float HitStartTime;

    public readonly float HitEndTime;

    public NotesData(
        NotesType notesType,
        int lanePositionNumber,
        float hitStartTime,
        float hitEndTime)
    {
        Type = notesType;
        LanePositionNumber = lanePositionNumber;
        HitStartTime = hitStartTime;
        HitEndTime = hitEndTime;
    }
}
