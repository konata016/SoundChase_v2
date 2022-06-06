using UnityEngine;
using UnityEngine.UI;
using SoundEditor;


public class SoundEditorManager : MonoBehaviour
{
    [SerializeField] private ScoreCreation scoreCreation;

    [SerializeField] private SoundEditorCameraController cameraController;

    [SerializeField] private PointerManager pointerManager;

    [SerializeField] private NotesEditController notesEditController;

    [SerializeField] private SoundEditorScoreExport scoreExport;

    [SerializeField] private SoundEditorToggleController toggleController;

    [SerializeField] private SoundEditorButtonController buttonController;

    [SerializeField] private Slider seekBar;

    [SerializeField] private BGMSoundController.SoundName bgmName;

    [SerializeField] private string importDataName;

    [SerializeField] private string exportDataName;

    private bool isPaused;

    private void Awake()
    {
        var bgmEndTime = SoundManager.Instance.SoundEndTimeBGM(bgmName);

        scoreCreation.Initialize($"{bgmName}", bgmEndTime);

        pointerManager.Initialize();

        notesEditController.Initialize(
            pointerManager, 
            scoreCreation.HorizontalLineHeight,
            scoreCreation.MaxHorizontalLaneCount);

        toggleController.Initialize(notesEditController.SetNotesType);

        buttonController.Initialize(
            onClickNotesDataExportButton,
            onClickScoreDataExportButton);

        seekBar.maxValue = bgmEndTime;

        notesEditController.ImportNotesData(importDataName);
    }

    private void Start()
    {
        isPaused = true;
    }

    private void Update()
    {
        cameraController.OnUpdate(seekBar.value);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                SoundManager.Instance.StopBGM();
            }
            else
            {
                SoundManager.Instance.SeekBGM(seekBar.value);
                SoundManager.Instance.PlayBGM(bgmName);
            }
        }

        if (!isPaused)
        {
            seekBar.value = SoundManager.Instance.SoundTimeBGM();
        }
    }

    private void onClickNotesDataExportButton()
    {
        Debug.Log("ノーツデータの書き出し");
        scoreExport.ExportNotesData(exportDataName);
    }

    private void onClickScoreDataExportButton()
    {
        Debug.Log("楽曲データの書き出し");
        scoreExport.ExportScoreData(scoreCreation.SoundData, exportDataName);
    }
}
