using System;
using UnityEngine;

abstract public class SoundControllerBase : MonoBehaviour
{

    [SerializeField] private CriAtomSource criAtomSource;

    private CriAtomExPlayback atomExPlayback;

    protected abstract string cueSheetName();

    protected abstract void onInitialize();

    private void Awake()
    {
        if (cueSheetName() == "")
        {
            return;
        }

        criAtomSource.cueSheet = cueSheetName();

        onInitialize();
    }

    public void Play<T>(T name) where T : Enum
    {
        if (criAtomSource == null)
        {
            return;
        }

        atomExPlayback = criAtomSource.Play($"{name}");
    }

    public void Pause(bool isPaused)
    {
        criAtomSource.player.Pause(isPaused);
    }

    public void Stop()
    {
        criAtomSource.Stop();
    }

    public float SoundEndTime<T>(T name) where T : Enum
    {
        CriAtomEx.CueInfo cueInfo;

        if (!String.IsNullOrEmpty(cueSheetName()))
        {
            var acb = CriAtom.GetAcb(cueSheetName());

            if (acb.GetCueInfo($"{name}", out cueInfo))
            {
                return Calculation.MsToS(cueInfo.length);
            }
        }

        return -1;
    }

    public float SoundTime()
    {
        return Calculation.MsToS(atomExPlayback.GetTimeSyncedWithAudio());
    }

    public void Seek(float time)
    {
        criAtomSource.player.SetStartTime(Calculation.SToMs(time));
    }

    public bool IsPaused()
    {
        return criAtomSource.IsPaused();
    }

    public bool IsPlaying()
    {
        return criAtomSource.status == CriAtomSource.Status.Playing;
    }

    public void ChangeVolume(float volume)
    {
        criAtomSource.volume = volume;
    }
}
