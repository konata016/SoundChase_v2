using System;
using UnityEngine;
using UnityEngine.UI;

namespace SoundEditor
{
    public class SoundEditorButtonController : MonoBehaviour
    {
        [SerializeField] private Button notesDataExportButton;

        [SerializeField] private Button scoreDataExportButton;

        public void Initialize(
            Action onClickNotesDataExportButton,
            Action onClickScoreDataExportButton)
        {
            notesDataExportButton.onClick.RemoveAllListeners();
            scoreDataExportButton.onClick.RemoveAllListeners();

            notesDataExportButton
                .onClick
                .AddListener(() => { onClickNotesDataExportButton?.Invoke(); });

            scoreDataExportButton
                .onClick
                .AddListener(() => { onClickScoreDataExportButton?.Invoke(); });
        }
    }
}
