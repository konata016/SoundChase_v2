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
        SC.Debug.Log($"�_���[�W");
    }

    protected override void onHitJustDodgeNotes()
    {
        if (!isAnyAction)
        {
            SC.Debug.Log($"���");
            return;
        }
    }

    protected override void onHitTechnicNotes()
    {
        if (!isAnyAction)
        {
            SC.Debug.Log($"�e�N�j�b�N");
            return;
        }
    }

    protected override void onHitPointNotes()
    {
        SC.Debug.Log($"�|�C���g");
    }

    private IEnumerator Move()
    {
        var tween =
            transform.DOMoveX(horizontalPosition, 0.4f)
            .SetEase(Ease.OutExpo);

        yield return tween.WaitForCompletion();
    }
}
