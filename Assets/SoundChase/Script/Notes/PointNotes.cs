using DG.Tweening;
using UnityEngine;

public class PointNotes : NotesBase
{
    private static readonly float animationTime = 5;

    [SerializeField]
    private MeshRenderer objectMesh;

    private Tween showingAnimation;

    sealed protected override NotesData.NotesType notesType() => NotesData.NotesType.Point;

    sealed protected override Range<float> fixHitTime() =>
        new Range<float>(InGameDefine.PointNotesFixHitTime / 2, InGameDefine.PointNotesFixHitTime / 2);

    sealed protected override void initialize(float hitStartTime, float hitEndTime)
    {
        transform.localPosition = new Vector3(horizontalPosition, 0, hitStartTime);

        changeNotesSize();

        objectMesh.enabled = false;
    }

    sealed protected override void setupAnimation(float speed)
    {
        const float jumpPower = 3;

        showingAnimation =
            transform.DOLocalMoveY(jumpPower, animationTime / speed)
            .SetEase(Ease.OutFlash, 2);

        showingAnimation.Pause();
    }

    sealed protected override void onShowView()
    {
        objectMesh.enabled = true;
        showingAnimation.Play();
    }

    sealed protected override void onHideView()
    {
        objectMesh.enabled = false;
    }

    private void changeNotesSize()
    {
        var size = transform.localScale;
        size.z /= transform.lossyScale.z;
        transform.localScale = size;
    }
}
