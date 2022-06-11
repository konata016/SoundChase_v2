using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerBase player;

    public void Initialize()
    {
        player.Initialize(new InGameSeData(), 0, 4);
    }
}
