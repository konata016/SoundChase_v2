using DG.Tweening;
using UnityEngine;

public class FallNotes : NotesBase
{
    private static readonly float animationTime = 10;

    [SerializeField]
    private MeshRenderer objectMesh;

    private Vector3 position;

    private Tween showingAnimation;

    sealed protected override NotesData.NotesType notesType() => NotesData.NotesType.Fall;

    sealed protected override Range<float> fixHitTime() =>
        new Range<float>(-InGameDefine.FallNotesFixHitTime, InGameDefine.FallNotesFixHitTime);

    sealed protected override void initialize(float hitStartTime, float hitEndTime)
    {
        position = new Vector3(horizontalPosition, 0, hitStartTime);
        changeNotesSize(hitEndTime - hitStartTime);

        objectMesh.enabled = false;
    }

    sealed protected override void setupAnimation(float speed)
    {
        const int range = 20;
        const float startHeight = 15;
        const float startDistance = 10;
        const float whileHeight = 5;

        var startPositionX = Random.Range(horizontalPosition - range, horizontalPosition + range);
        var animationPathPosition = new Vector3[]
        {
            new Vector3(startPositionX, position.y + startHeight, position.z - startDistance),
            new Vector3(position.x, position.y + whileHeight, position.z),
            position
        };

        transform.localPosition = animationPathPosition[0];

        showingAnimation = transform.DOLocalPath(
                animationPathPosition,
                animationTime / speed,
                PathType.CatmullRom)
                .SetOptions(false)
                .SetEase(Ease.OutQuart);

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
        const float minSize = 0.01f;
        var size = transform.localScale;
        size.z = length == 0 ? minSize : length;
        transform.localScale = size;
    }
}
