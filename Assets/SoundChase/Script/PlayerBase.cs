using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IRhythmGame, IInputKey
{
    private int minLaneMoveLimit;

    private int maxLaneMoveLimit;

    private int currentLanePositionNumber;

    protected float horizontalPosition => currentLanePositionNumber * InGameDefine.FixLaneSpace;

    protected bool isAnyAction { get; private set; }

    protected virtual void initialize() { }

    protected virtual void onPause() { }

    protected virtual void onUnPause() { }

    protected virtual void onDispose() { }

    protected virtual void onTap() { }

    protected virtual void onMoveLeft() { }

    protected virtual void onMoveRight() { }

    protected virtual void onHitFallNotes() { }

    protected virtual void onHitJustDodgeNotes() { }

    protected virtual void onHitTechnicNotes() { }

    protected virtual void onHitPointNotes() { }

    public void Initialize(int minLaneMoveLimit, int maxLaneMoveLimit)
    {
        initialize();
        this.minLaneMoveLimit = minLaneMoveLimit;
        this.maxLaneMoveLimit = maxLaneMoveLimit;
    }

    public void OnHitNotes(NotesData.NotesType notesType, int notesLanePositionNumber)
    {
        switch (notesType)
        {
            case NotesData.NotesType.Fall:
                onHitNotes(notesLanePositionNumber, onHitFallNotes);
                break;
            case NotesData.NotesType.JustDodge:
                onHitNotes(notesLanePositionNumber, onHitJustDodgeNotes);
                break;
            case NotesData.NotesType.Technic:
                onHitTechnicNotes();
                break;
            case NotesData.NotesType.Point:
                onHitNotes(notesLanePositionNumber, onHitPointNotes);
                break;
        }
    }

    public void OnPause()
    {
        onPause();
    }

    public void OnUnPause()
    {
        onUnPause();
    }

    public void OnDispose()
    {
        onDispose();
    }

    public void OnTap()
    {
        StartCoroutine(onAnyAction());
        onTap();
    }

    public void OnLeft()
    {
        if (currentLanePositionNumber > minLaneMoveLimit)
        {
            StartCoroutine(onAnyAction());

            currentLanePositionNumber--;
            onMoveLeft();
        }
    }

    public void OnRight()
    {
        if (currentLanePositionNumber < maxLaneMoveLimit)
        {
            StartCoroutine(onAnyAction());

            currentLanePositionNumber++;
            onMoveRight();
        }
    }

    private void onHitNotes(int notesLanePositionNumber, Action onHit)
    {
        if(currentLanePositionNumber!= notesLanePositionNumber)
        {
            return;
        }

        onHit?.Invoke();
    }

    private IEnumerator onAnyAction()
    {
        isAnyAction = true;
        yield return null;
        isAnyAction = false;
    }
}
