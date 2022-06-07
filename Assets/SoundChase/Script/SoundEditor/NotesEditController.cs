using System;
using UnityEngine;
using UnityEngine.UI;

namespace SoundEditor
{
    public class NotesEditController : MonoBehaviour
    {
        [SerializeField] private SoundEditorNotes soundEditorNotes;

        [SerializeField] private SoundEditorScoreInput scoreInput;

        [SerializeField] private Toggle sePreviewToggle;

        private NotesData.NotesType selectedNotesType;

        private SoundEditorNotes currentNotes;

        private float editorHorizontalLineHeight;

        private int maxHorizontalLaneCount;

        private static readonly float notesDepth = -2f;

        public void Initialize(
            PointerManager pointer,
            float editorHorizontalLineHeight,
            int maxHorizontalLaneCount)
        {
            pointer.Initialize(
                onLeftMouseButtonDown,
                onLeftMouseButtonUp,
                onLeftMouseButtonHold,
                onRightMouseButtonDown);

            scoreInput.Initialize(
                editorHorizontalLineHeight,
                maxHorizontalLaneCount,
                soundEditorNotes,
                transform);

            this.editorHorizontalLineHeight = editorHorizontalLineHeight;
            this.maxHorizontalLaneCount = maxHorizontalLaneCount;

            sePreviewToggle.OnValueChanged((isOn) => onSePreviewToggle(isOn));
        }

        public void SetNotesType(NotesData.NotesType type)
        {
            selectedNotesType = type;
        }

        public void ImportNotesData(string importNotesDataName)
        {
            if (String.IsNullOrEmpty(importNotesDataName))
            {
                return;
            }

            scoreInput.CreateSoundEditorNotes(importNotesDataName, notesDepth);
        }

        private void onLeftMouseButtonDown(Vector2 hitRayPosition)
        {
            var pos = position(hitRayPosition);

            currentNotes = createNotes();
            currentNotes.SetStartPosition(pos, notesDepth);
            currentNotes.SetLanePositionNumber(getLanePositionNumber(pos.y));

            if (selectedNotesType != NotesData.NotesType.Fall)
            {
                setEndNotesPosition(pos);
            }
        }

        private void onLeftMouseButtonHold(Vector2 hitRayPosition)
        {
            if (currentNotes == null)
            {
                return;
            }

            var pos = hitRayPosition;
            pos.y = currentNotes.Height;
            currentNotes.SetEndPosition(pos, notesDepth);
        }

        private void onLeftMouseButtonUp(Vector2 hitRayPosition)
        {
            if (currentNotes == null)
            {
                return;
            }

            var pos = hitRayPosition;
            pos.y = currentNotes.Height;
            setEndNotesPosition(pos);
        }

        private void onRightMouseButtonDown(Collider2D collider)
        {
            if (collider == null)
            {
                return;
            }

            if (collider.tag != InGameDefine.NotesTagName)
            {
                return;
            }

            Destroy(collider.gameObject);
        }

        private SoundEditorNotes createNotes()
        {
            var notes = Instantiate(soundEditorNotes, transform);

            var h = editorHorizontalLineHeight / maxHorizontalLaneCount;
            notes.Initialize(selectedNotesType, h);

            return notes;
        }

        private void setEndNotesPosition(Vector2 position)
        {
            currentNotes.SetEndPosition(position, notesDepth);
            currentNotes.SetFixedPosition();
            currentNotes = null;
        }

        private Vector2 position(Vector2 hitRayPosition)
        {
            var y = Mathf.Clamp(hitRayPosition.y, 0, editorHorizontalLineHeight);
            var h = editorHorizontalLineHeight / maxHorizontalLaneCount;

            for (int i = 0; i < maxHorizontalLaneCount; i++)
            {
                if (y > h * i && y <= h * (i + 1))
                {
                    return new Vector2(hitRayPosition.x, (h * i) + (h / 2));
                }
            }

            return new Vector2(hitRayPosition.x, 0);
        }

        private int getLanePositionNumber(float hitRayPositionY)
        {
            var y = Mathf.Clamp(hitRayPositionY, 0, editorHorizontalLineHeight);
            var h = editorHorizontalLineHeight / maxHorizontalLaneCount;

            for (int i = 0; i < maxHorizontalLaneCount; i++)
            {
                if (y > h * i && y <= h * (i + 1))
                {
                    Debug.Log($"LaneNumber : {i}");
                    return i;
                }
            }

            return 0;
        }

        private void onSePreviewToggle(bool isPreview)
        {
            transform.AccessAllChildComponent<SoundEditorNotesSePreviewController>(
                (sePreview) =>
                {
                    sePreview.enabled = isPreview;
                });
        }
    }
}
