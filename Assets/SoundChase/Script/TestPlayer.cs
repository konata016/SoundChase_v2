using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public sealed class TestPlayer : PlayerBase
{
    protected override void onMoveLeft()
    {
        StartCoroutine(Move());
    }

    protected override void onMoveRight()
    {
        StartCoroutine(Move());
    }

    protected override void onHitFallNotes()
    {
        SC.Debug.Log($"ダメージ");
    }

    protected override void onHitJustDodgeNotes()
    {
        if (!isAnyAction)
        {
            SC.Debug.Log($"回避");
            return;
        }
    }

    protected override void onHitTechnicNotes()
    {
        if (!isAnyAction)
        {
            SC.Debug.Log($"テクニック");
            return;
        }
    }

    protected override void onHitPointNotes()
    {
        SC.Debug.Log($"ポイント");
    }

    private IEnumerator Move()
    {
        var tween =
            transform.DOMoveX(horizontalPosition, 0.4f)
            .SetEase(Ease.OutExpo);

        yield return tween.WaitForCompletion();
    }
}
