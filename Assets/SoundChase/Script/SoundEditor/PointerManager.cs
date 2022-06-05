using System;
using UnityEngine;

namespace SoundEditor
{
    public class PointerManager : MonoBehaviour
    {
        private static readonly float maxRayDistance = 10f;

        private bool isLeftMouseButtonHold;

        private Vector2 hitRayObjectPosition;

        private Action<Vector2> onLeftMouseButtonDown;

        private Action<Vector2> onLeftMouseButtonUp;

        private Action<Vector2> onLeftMouseButtonHold;

        private Action<Collider2D> onRightMouseButtonDown;

        public void Initialize(
            Action<Vector2> onLeftMouseButtonDown = null,
            Action<Vector2> onLeftMouseButtonUp = null,
            Action<Vector2> onLeftMouseButtonHold = null,
            Action<Collider2D> onRightMouseButtonDown = null)
        {
            this.onLeftMouseButtonDown = onLeftMouseButtonDown;
            this.onLeftMouseButtonUp = onLeftMouseButtonUp;
            this.onLeftMouseButtonHold = onLeftMouseButtonHold;
            this.onRightMouseButtonDown = onRightMouseButtonDown;
        }

        public void Update()
        {
            var pos = InGameDefine.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(pos, Vector2.zero, maxRayDistance);

            var collider = hit.collider;

            if (Input.GetMouseButtonDown(1) && !isLeftMouseButtonHold)
            {
                onRightMouseButtonDown?.Invoke(collider);
                return;
            }

            if (collider != null)
            {
                if (collider.tag == InGameDefine.NotesTagName)
                {
                    return;
                }

                hitRayObjectPosition = collider.transform.position;
                hitRayObjectPosition.y = pos.y;
            }
            else
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                isLeftMouseButtonHold = true;
                onLeftMouseButtonDown?.Invoke(hitRayObjectPosition);
            }

            if (Input.GetMouseButton(0))
            {
                onLeftMouseButtonHold?.Invoke(hitRayObjectPosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                onLeftMouseButtonUp?.Invoke(hitRayObjectPosition);
                isLeftMouseButtonHold = false;
            }
        }
    }
}
