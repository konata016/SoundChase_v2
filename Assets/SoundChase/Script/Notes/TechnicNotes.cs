using UnityEngine;

public class TechnicNotes : NotesBase
{
    sealed protected override NotesData.NotesType notesType() => NotesData.NotesType.Technic;

    sealed protected override Range<float> fixHitTime() =>
        new Range<float>(InGameDefine.TechnicNotesFixHitTime, InGameDefine.TechnicNotesFixHitTime);

    sealed protected override void initialize(float hitStartTime, float hitEndTime)
    {
        var posZ = hitEndTime - hitStartTime;
        var sizeZ = (hitEndTime + fixHitTime().Max) - (hitStartTime - fixHitTime().Min);
        transform.localPosition = new Vector3(horizontalPosition, 0, hitStartTime - (posZ / 2));
        changeNotesSize(sizeZ);
    }

    private void changeNotesSize(float length)
    {
        var size = transform.localScale;
        transform.localScale = new Vector3(size.x, size.y, length);
    }
}
