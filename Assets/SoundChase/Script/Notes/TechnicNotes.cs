using UnityEngine;

public class TechnicNotes : NotesBase
{
    sealed protected override NotesData.NotesType notesType() => NotesData.NotesType.Technic;

    sealed protected override Range<float> fixHitTime() =>
        new Range<float>(InGameDefine.TechnicNotesFixHitTime / 2, InGameDefine.TechnicNotesFixHitTime / 2);

    sealed protected override void initialize(float hitStartTime, float hitEndTime)
    {
        transform.localPosition = new Vector3(horizontalPosition, 0, hitStartTime);
        changeNotesSize(hitEndTime - hitStartTime);
    }

    private void changeNotesSize(float length)
    {
        var size = transform.localScale;
        transform.localScale = new Vector3(size.x, size.y, length);
    }
}
