using UnityEngine;

abstract public class NotesBase : MonoBehaviour, INotes
{
    protected abstract NotesData.NotesType notesType();

    protected abstract Range<float> fixHitTime();

    protected float horizontalPosition { get; private set; }

    protected virtual void initialize(float hitStartTime, float hitEndTime) { }

    protected virtual void setupAnimation(float speed) { }

    protected virtual void onHit(bool isHitPlayer) { }

    protected virtual void onShowView() { }

    protected virtual void onHideView() { }

    public NotesData.NotesType NotesType => notesType();

    public Range<float> FixHitTime => fixHitTime();

    public int LaneNumber => laneNumber;
    private int laneNumber;

    public void Initialize(float hitStartTime, float hitEndTime, int laneNumber)
    {
        var fixHitStartTime = hitStartTime - fixHitTime().Min;
        var fixHitEndTime = hitEndTime + fixHitTime().Max;

        horizontalPosition = laneNumber * InGameDefine.FixLaneSpace;
        initialize(fixHitStartTime, fixHitEndTime);

        this.laneNumber = laneNumber;
    }

    public void SetupAnimation(float speed)
    {
        setupAnimation(speed);
    }

    public void OnShowView()
    {
        onShowView();
    }

    public void OnHideView()
    {
        onHideView();
    }

    public void OnProcessing(bool isHitPlayer)
    {
        onHit(isHitPlayer);
    }
}
