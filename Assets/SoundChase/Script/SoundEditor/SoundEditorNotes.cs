using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundEditor
{
    public class SoundEditorNotes : MonoBehaviour
    {
        [SerializeField] private LineRenderer line;

        [SerializeField] private BoxCollider2D collider;

        [SerializeField] private SoundEditorNotesSePreviewController notesSePreviewController;

        private int lanePositionNumber;

        private NotesData.NotesType type;

        private float colliderHeight;

        public float Height => line.GetPosition(0).y;

        public void Initialize(NotesData.NotesType type, float colliderHeight)
        {
            this.type = type;
            this.colliderHeight = colliderHeight;

            line.positionCount = 2;

            setColor();
        }

        public NotesData GetNotesData()
        {
            return new NotesData(
                type,
                lanePositionNumber,
                line.GetPosition(0).x,
                line.GetPosition(1).x);
        }

        public void SetLanePositionNumber(int num)
        {
            lanePositionNumber = num;
        }

        public void SetStartPosition(Vector2 position, float depth)
        {
            var pos = new Vector3(position.x, position.y, depth);
            transform.localPosition = pos;
            line.SetPosition(0, pos);
        }

        public void SetEndPosition(Vector2 position, float depth)
        {
            var pos = new Vector3(position.x, position.y, depth);
            line.SetPosition(1, pos);
        }

        public void SetFixedPosition()
        {
            trimPosition();
            setupColliderSize();

            notesSePreviewController.Setup(
                line.GetPosition(0).x,
                line.GetPosition(1).x,
                type);
        }

        private void trimPosition()
        {
            if (line.GetPosition(0).x > line.GetPosition(1).x)
            {
                var startPos = line.GetPosition(0);
                var endPos = line.GetPosition(1);

                SetStartPosition(endPos, endPos.z);
                SetEndPosition(startPos, startPos.z);
            }
        }

        private void setupColliderSize()
        {
            var length = line.GetPosition(1).x - line.GetPosition(0).x;

            if (length == 0)
            {
                length = 0.05f;
            }

            collider.offset = new Vector2(length / 2, 0);
            collider.size = new Vector2(length, colliderHeight);
        }

        private void setColor()
        {
            var color = InGameDefine.GetNotesSymbolColor(type);
            line.startColor = color;
            line.endColor = color;
        }
    }
}
