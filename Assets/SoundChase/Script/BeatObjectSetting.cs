using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatObjectSetting : MonoBehaviour
{
    private float barTime;

    private bool isStop;

    public void Initialize(int rhythm, int bpm)
    {
        barTime = Calculation.GetBarTime(rhythm, bpm);

        foreach (var component in Object.FindObjectsOfType<Component>())
        {
            var iBeat = component as IBeat;
            if (iBeat != null)
            {
                StartCoroutine(onUpdate(iBeat));
            }
        }

        isStop = false;
    }

    private void OnDisable()
    {
        isStop = true;
    }

    private IEnumerator onUpdate(IBeat ibeat)
    {
        var count = 0;
        for (; !isStop;)
        {
            if (isBeatTiming(ibeat.Beat, count))
            {
                count++;
                ibeat.OnBeat();
            }

            yield return null;
        }
    }

    private bool isBeatTiming(int beat, int count)
    {
        //•‚ÌŒvŽZ
        var note = barTime / beat;
        return InGameLoop.Instance.Time >= note * count;
    }
}
