using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNotes :  NotesBase
{
    protected override NotesData.NotesType notesType() => NotesData.NotesType.Point;

    protected override Range<float> fixHitTime()
    {
        throw new System.NotImplementedException();
    }
}
