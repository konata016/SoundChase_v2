using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    [SerializeField] private SoundControllerBase soundManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //soundManager.Play(SESoundController.SoundName.Ball);
        } 

        if (Input.GetKeyDown(KeyCode.S))
        {
            //soundManager.Play(SESoundController.SoundName.BallLost);
        }
    }

    private void playAndStopSound()
    {
    }
}
