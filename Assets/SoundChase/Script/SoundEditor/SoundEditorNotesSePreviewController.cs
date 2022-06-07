using UnityEngine;
using SoundName = SESoundController.SoundName;

namespace SoundEditor
{
    public class SoundEditorNotesSePreviewController : MonoBehaviour
    {
        private float hitStartTime;

        private float hitEndTime;

        private bool isPlayed;

        private SoundName seName;

        public void Setup(
            float hitStartTime,
            float hitEndTime,
            NotesData.NotesType notesType)
        {
            this.hitStartTime = hitStartTime;
            this.hitEndTime = hitEndTime;

            seName = GetSoundName(notesType);
            isPlayed = false;
        }

        private void Update()
        {
            if (isTiming(SoundManager.Instance.SoundTimeBGM(), 0.05f))
            {
                if (isPlayed)
                {
                    return;
                }

                isPlayed = true;
                SoundManager.Instance.PlaySE(seName);
            }
            else
            {
                isPlayed = false;
            }
        }

        private bool isTiming(float time, float fixTiming)
        {
            return time > hitStartTime - fixTiming && time < hitEndTime + fixTiming;
        }

        private SoundName GetSoundName(NotesData.NotesType notesType)
        {
            switch (notesType)
            {
                case NotesData.NotesType.Fall: return SoundName.Fall;
                case NotesData.NotesType.JustDodge: return SoundName.JustDodge;
                case NotesData.NotesType.Technic: return SoundName.Technic;
                case NotesData.NotesType.Point: return SoundName.Point;
            }

            return SoundName.Fall;
        }
    }
}
