using UnityEngine;

namespace SoundEditor
{
    [System.Serializable]
    public class SoundData
    {
        [SerializeField, Header("�Ȗ�")]
        private string name;

        [SerializeField, Header("�Ȃ̒���")]
        private float endTime = 60;

        [SerializeField, Header("���ߐ�")]
        private int maxBarCount;

        [SerializeField, Header("���q��")]
        private int rhythm = 4;

        [SerializeField, Header("1���߂ɒu��������")]
        private int beat = 16;

        [SerializeField, Header("�Ȃ̑���")]
        private int bpm = 120;

        public string Name => name;
        public float EndTime => endTime;
        public int MaxBarCount => maxBarCount;
        public int Rhythm => rhythm;
        public int Beat => beat;
        public int Bpm => bpm;

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetEndTime(float endTime)
        {
            this.endTime = endTime;
        }

        public void SetMaxBarCount(int maxBarCount)
        {
            this.maxBarCount = maxBarCount;
        }
    }
}
