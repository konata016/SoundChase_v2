using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestPlayer : PlayerBase
{
    sealed protected override void onMoveLeft()
    {
        StartCoroutine(Move());
    }

    sealed protected override void onMoveRight()
    {
        StartCoroutine(Move());
    }

    sealed protected override void onHitFallNotes()
    {
        SC.Debug.Log($"ダメージ");
    }

    sealed protected override void onHitJustDodgeNotes()
    {
        if (!isAnyAction)
        {
            SC.Debug.Log($"回避");
            return;
        }
    }

    sealed protected override void onHitTechnicNotes()
    {
        if (!isAnyAction)
        {
            SC.Debug.Log($"テクニック");
            return;
        }
    }

    sealed protected override void onHitPointNotes()
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
