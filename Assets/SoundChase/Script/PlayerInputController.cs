using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private enum DirectionType
    {
        None,
        Left,
        Right,
        Tap
    }

    private DirectionType dirType;

    private IInputKey player;

    private float swipeDecisionDistance;

    private float switchingSwipeDecisionDistance;

    float dpi;

    public void Initialize(
        IInputKey player,
        float swipeDecisionDistance,
        float switchingSwipeDecisionDistance)
    {
        this.player = player;
        this.swipeDecisionDistance = swipeDecisionDistance;
        this.switchingSwipeDecisionDistance = switchingSwipeDecisionDistance;

        dpi = Screen.dpi;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dirType = DirectionType.None;
        player.OnTap();
    }

    public void OnDrag(PointerEventData eventData)
    {
        var x = eventData.delta.x / dpi;
        if (x < -getSwipeDecisionDistance(dirType) && dirType != DirectionType.Left)
        {
            dirType = DirectionType.Left;
            player.OnLeft();
        }
        else if (x > getSwipeDecisionDistance(dirType) && dirType != DirectionType.Right)
        {
            dirType = DirectionType.Right;
            player.OnRight();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dirType = DirectionType.None;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            player.OnTap();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            player.OnLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            player.OnRight();
        }
    }

    private float getSwipeDecisionDistance(DirectionType directionType)
    {
        return
            directionType == DirectionType.None ?
            swipeDecisionDistance : switchingSwipeDecisionDistance;
    }
}
