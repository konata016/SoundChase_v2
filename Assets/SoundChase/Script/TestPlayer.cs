using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public sealed class TestPlayer : PlayerBase, IBeat
{
    [SerializeField]
    private Transform playerModel;

    public int Beat => 4;

    private Tweener beatSizeAnimationTweener;

    private Tweener beatRotationAnimationTweener;

    private static readonly Vector3 beatSiz = Vector3.one * 0.8f;

    public void OnBeat()
    {
        beatSizeAnimationTweener.Rewind();
        beatRotationAnimationTweener.Restart();

        beatSizeAnimationTweener = playerModel
            .DOScale(beatSiz, 0.25f)
            .SetEase(Ease.InFlash, 2);

        //beatRotationAnimationTweener = playerModel
        //    .DORotate(new Vector3(360, 0, 0), 0.4f, RotateMode.WorldAxisAdd)
        //    .SetEase(Ease.OutExpo);
    }

    protected override void initialize()
    {
        playerModel.localScale = Vector3.one * 0.5f;
    }

    protected override void onMoveLeft()
    {
        StartCoroutine(Move());
        StartCoroutine(MoveLeft());
    }

    protected override void onMoveRight()
    {
        StartCoroutine(Move());
        StartCoroutine(MoveRight());
    }

    protected override bool onHitFallNotes()
    {
        SC.Debug.Log($"ダメージ");
        return true;
    }

    protected override bool onHitJustDodgeNotes()
    {
        if (isAnyAction)
        {
            SC.Debug.Log($"回避");
            return true;
        }

        return false;
    }

    protected override bool onHitTechnicNotes()
    {
        if (isAnyAction)
        {
            SC.Debug.Log($"テクニック");
            return true;
        }

        return false;
    }

    protected override bool onHitPointNotes()
    {
        SC.Debug.Log($"ポイント");
        return true;
    }

    private IEnumerator Move()
    {
        var tween =
            transform.DOMoveX(horizontalPosition, 0.4f)
            .SetEase(Ease.OutExpo);

        yield return tween.WaitForCompletion();
    }

    private IEnumerator MoveLeft()
    {
        var tween =
            playerModel.DORotate(new Vector3(0, 360f * 2f, 0), 1f, RotateMode.WorldAxisAdd)
            .SetEase(Ease.OutExpo);

        yield return tween.WaitForCompletion();
    }

    private IEnumerator MoveRight()
    {
        var tween =
            playerModel.DORotate(new Vector3(0, -360f * 2f, 0), 1f, RotateMode.WorldAxisAdd)
            .SetEase(Ease.OutExpo);

        yield return tween.WaitForCompletion();
    }
}
