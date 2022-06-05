using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField] private SoundControllerBase se;

    [SerializeField] private SoundControllerBase bgm;

    [SerializeField, Range(0, 1)] private float defaultVolume;

    public bool IsPausedBGM => bgm.IsPaused();

    public bool IsPlayingBGM => bgm.IsPlaying();

    private void Awake()
    {
        ChangeMasterVolume(defaultVolume);
    }

    public void PlaySE(SESoundController.SoundName name)
    {
        se.Play(name);
    }

    public void PlayBGM(BGMSoundController.SoundName name)
    {
        bgm.Play(name);
    }

    public void PauseBGM(bool isPaused)
    {
        bgm.Pause(isPaused);
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public float SoundEndTimeBGM(BGMSoundController.SoundName name)
    {
        return bgm.SoundEndTime(name);
    }

    public float SoundTimeBGM()
    {
        return bgm.SoundTime();
    }

    public void SeekBGM(float time)
    {
        bgm.Seek(time);
    }

    public void ChangeMasterVolume(float volume)
    {
        bgm.ChangeVolume(volume);
        se.ChangeVolume(volume);
    }
}
