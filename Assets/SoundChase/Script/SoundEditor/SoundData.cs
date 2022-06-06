using UnityEngine;

namespace SoundEditor
{
    [System.Serializable]
    public class SoundData
    {
        [SerializeField, Header("‹È–¼")]
        private string name;

        [SerializeField, Header("‹È‚Ì’·‚³")]
        private float endTime = 60;

        [SerializeField, Header("¬ß”")]
        private int maxBarCount;

        [SerializeField, Header("”Žq”")]
        private int rhythm = 4;

        [SerializeField, Header("1¬ß‚É’u‚­‰¹•„”")]
        private int beat = 16;

        [SerializeField, Header("‹È‚Ì‘¬‚³")]
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
