using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundEditor
{
    public class ScoreCreation : MonoBehaviour
    {
        [SerializeField] LineRenderer scoreLinePrefab;

        [SerializeField] LineRenderer horizontalLinePrefab;

        [SerializeField] SoundData soundData;

        private List<LineRenderer> scoreLineList = new List<LineRenderer>();

        private static readonly int maxBeat = 16;

        private static readonly float longLineLength = 2;

        private static readonly float middleLineLength = 1.75f;

        private static readonly float shortLineLength = 1.5f;

        private static readonly float horizontalLineHeight = 1.5f;

        private static readonly int maxHorizontalLaneCount = 4;

        public SoundData SoundData => soundData;

        public float HorizontalLineHeight => horizontalLineHeight;

        public int MaxHorizontalLaneCount => maxHorizontalLaneCount;

        public void Initialize(string bgmName, float bgmEndTime)
        {
            soundData.SetName(bgmName);
            soundData.SetEndTime(bgmEndTime);

            var maxCount = soundData.EndTime * maxBeat;
            for (var i = 0; i < maxCount; i++)
            {
                var line = Instantiate(scoreLinePrefab, transform);

                scoreLineList.Add(line);
                scoreLineList[i].gameObject.SetActive(false);
            }

            setupScoreLine(0, soundData.Beat, 0);
            setupHorizontalLine(maxHorizontalLaneCount);
        }

        private void setupScoreLine(float elapsedTime, int beat, int resetLineCount)
        {
            //Žc‚èŽžŠÔ‚ÌŒvŽZ
            var limitTime = soundData.EndTime - elapsedTime;

            //Žc‚èŽžŠÔ‚©‚çZ”Žq—l‚ÉLine‚Ìˆø‚«’¼‚·”‚ðŒˆ‚ß‚é
            var maxCount = Calculation.GetMaxBarCount(limitTime, soundData.Rhythm, soundData.Bpm) * beat;

            //•‚ÌŒvŽZ
            var note = Calculation.GetBarTime(soundData.Rhythm, soundData.Bpm) / beat;
            Debug.Log(Calculation.GetBarTime(soundData.Rhythm, soundData.Bpm));

            //Line‚ðˆø‚­
            for (var i = resetLineCount; i < resetLineCount + maxCount; i++)
            {
                var range = note * i;
                if (i % beat == 0)
                {
                    scoreLineList[i].SetPosition(0, new Vector3(range, 0, 0));
                    scoreLineList[i].SetPosition(1, new Vector3(range, longLineLength, 0));
                    soundData.SetMaxBarCount(i / beat); //‚Æ‚è‚ ‚¦‚¸
                }
                else
                {
                    if (i % 4 == 0)
                    {
                        scoreLineList[i].SetPosition(0, new Vector3(range, 0, 0));
                        scoreLineList[i].SetPosition(1, new Vector3(range, middleLineLength, 0));
                    }
                    else
                    {
                        scoreLineList[i].SetPosition(0, new Vector3(range, 0, 0));
                        scoreLineList[i].SetPosition(1, new Vector3(range, shortLineLength, 0));
                    }
                }

                scoreLineList[i].gameObject.transform.localPosition = new Vector3(range, 0, 0);
                scoreLineList[i].gameObject.SetActive(true);
            }
        }

        private void setupHorizontalLine(int maxCount)
        {
            for (int i = 0; i < maxCount; i++)
            {
                var line = Instantiate(horizontalLinePrefab, transform);
                var h = horizontalLineHeight / maxCount;
                line.SetPosition(0, new Vector3(0, h * i, -0.1f));
                line.SetPosition(1, new Vector3(200, h * i, -0.1f));
            }
        }
    }
}