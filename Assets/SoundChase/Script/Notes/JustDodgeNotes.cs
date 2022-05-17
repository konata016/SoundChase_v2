using DG.Tweening;
using UnityEngine;

public class JustDodgeNotes : NotesBase
{
    private static readonly float animationTime = 10;

    [SerializeField]
    private Transform objectRoot;

    [SerializeField]
    private MeshRenderer objectMesh;

    private Vector3 size;

    private Tween showingAnimation;

    sealed protected override NotesData.NotesType notesType() => NotesData.NotesType.JustDodge;

    sealed protected override Range<float> fixHitTime() =>
        new Range<float>(InGameDefine.JustDodgeNotesFixHitTime / 2, InGameDefine.JustDodgeNotesFixHitTime / 2);

    sealed protected override void initialize(float hitStartTime, float hitEndTime)
    {
        Debug.Log($"hitStartTime: {hitStartTime} hitEndTime: {hitEndTime}");
        Debug.Log($"Min: {fixHitTime().Min} Max: {fixHitTime().Max}");
        transform.localPosition = new Vector3(horizontalPosition, 0, hitStartTime);
        changeNotesSize(hitEndTime - hitStartTime);

        objectMesh.enabled = false;
    }

    sealed protected override void setupAnimation(float speed)
    {
        objectRoot.localScale = new Vector3(size.x, 0, size.z);
        showingAnimation =
            objectRoot.DOScaleY(size.y, animationTime / speed).SetEase(Ease.OutQuart);
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

    private void changeNotesSize(float length)
    {
        var localScale = transform.localScale;
        size = new Vector3(localScale.x, localScale.y, length);
    }

    private void changeNotesSize()
    {
        var localScale = transform.localScale;
        localScale.z /= transform.lossyScale.z;
        size = localScale;
    }
}
