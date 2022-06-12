using System;
using System.Collections;
using UnityEngine;
using SeName = SESoundController.SoundName;

public class PlayerBase : MonoBehaviour, IRhythmGame, IInputKey
{
    public int CurrentLanePositionNumber { get; private set; }

    protected bool isAnyAction { get; private set; }

    private int minLaneMoveLimit;

    private int maxLaneMoveLimit;

    private InGameSeData seData;

    private Action onMove;

    protected float horizontalPosition => CurrentLanePositionNumber * InGameDefine.FixLaneSpace;

    protected virtual void initialize() { }

    protected virtual void onPause() { }

    protected virtual void onUnPause() { }

    protected virtual void onDispose() { }

    protected virtual void onTap() { }

    protected virtual void onMoveLeft() { }

    protected virtual void onMoveRight() { }

    protected virtual bool onHitFallNotes() { return false; }

    protected virtual bool onHitJustDodgeNotes() { return false; }

    protected virtual bool onHitTechnicNotes() { return false; }

    protected virtual bool onHitPointNotes() { return false; }

    public void Initialize(
        InGameSeData seData,
        int minLaneMoveLimit,
        int maxLaneMoveLimit)
    {
        initialize();
        this.seData = seData;
        this.minLaneMoveLimit = minLaneMoveLimit;
        this.maxLaneMoveLimit = maxLaneMoveLimit;
    }

    public bool OnHitNotes(NotesData.NotesType notesType, int notesLanePositionNumber)
    {
        switch (notesType)
        {
            case NotesData.NotesType.Fall:
                return isHitingNotes(seData.HittingFall, notesLanePositionNumber, onHitFallNotes);
            case NotesData.NotesType.JustDodge:
                return isHitingNotes(seData.JustDodge, notesLanePositionNumber, onHitJustDodgeNotes);
            case NotesData.NotesType.Technic:
                return isHitingAllLaneNotes(seData.Technic, onHitTechnicNotes);
            case NotesData.NotesType.Point:
                return isHitingNotes(seData.GettingPoint, notesLanePositionNumber, onHitPointNotes);
        }

        return false;
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
        onMove = ()=> { onTap(); };
    }

    public void OnLeft()
    {
        if (CurrentLanePositionNumber > minLaneMoveLimit)
        {
            StartCoroutine(onAnyAction());
            onMove = () =>
            {
                  CurrentLanePositionNumber--;
                  onMoveLeft();
            };
        }
    }

    public void OnRight()
    {
        if (CurrentLanePositionNumber < maxLaneMoveLimit)
        {
            StartCoroutine(onAnyAction());
            onMove = () =>
            {
                CurrentLanePositionNumber++;
                onMoveRight();
            };
        }
    }

    private bool isHitingNotes(SeName seData, int notesLanePositionNumber, Func<bool> onHit)
    {
        if (CurrentLanePositionNumber != notesLanePositionNumber)
        {
            return false;
        }

        if(onHit?.Invoke() ?? false)
        {
            SoundManager.Instance.PlaySE(seData);
            return true;
        }
;
        return false;
    }

    private bool isHitingAllLaneNotes(SeName seData, Func<bool> onHit)
    {
        if (onHit?.Invoke() ?? false)
        {
            SoundManager.Instance.PlaySE(seData);
            return true;
        }

        return false;
    }

    private IEnumerator onAnyAction()
    {
        isAnyAction = true;
        yield return null;
        onMove?.Invoke();
        isAnyAction = false;
    }
}
