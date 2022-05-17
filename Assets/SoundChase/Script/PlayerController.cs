using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerBase player;

    public void Initialize()
    {
        player.Initialize(0, 4);
    }
}
