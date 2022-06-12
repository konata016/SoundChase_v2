using DG.Tweening;
using UnityEngine;

public sealed class PointNotes : NotesBase
{
    private static readonly float animationTime = 5;

    [SerializeField]
    private MeshRenderer objectMesh;

    private Tween showingAnimation;

    private bool isHitPlayer;

    protected override NotesData.NotesType notesType() => NotesData.NotesType.Point;

    protected override Range<float> fixHitTime() =>
        new Range<float>(InGameDefine.PointNotesFixHitTime, InGameDefine.PointNotesFixHitTime);

    protected override void initialize(float hitStartTime, float hitEndTime)
    {
        transform.localPosition = new Vector3(horizontalPosition, 0, hitStartTime);

        changeNotesSize();

        objectMesh.enabled = false;
    }

    protected override void setupAnimation(float speed)
    {
        const float jumpPower = 3;

        showingAnimation =
            transform.DOLocalMoveY(jumpPower, animationTime / speed)
            .SetEase(Ease.OutFlash, 2);

        showingAnimation.Pause();
    }

    protected override void onShowView()
    {
        objectMesh.enabled = true;
        showingAnimation.Play();
    }

    protected override void onHideView()
    {
        objectMesh.enabled = false;
    }

    protected override void onHit(bool isHitPlayer)
    {
        if (!this.isHitPlayer)
        {
            objectMesh.enabled = false;
            this.isHitPlayer = isHitPlayer;
        }
    }

    private void changeNotesSize()
    {
        var size = transform.localScale;
        size.z /= transform.lossyScale.z;
        transform.localScale = size;
    }
}
