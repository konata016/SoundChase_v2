using UnityEngine;
using UnityEngine.UI;
using SoundEditor;
using System.Collections.Generic;
using TMPro;

public class SoundEditorManager : MonoBehaviour
{
    [SerializeField] private ScoreCreation scoreCreation;

    [SerializeField] private SoundEditorCameraController cameraController;

    [SerializeField] private PointerManager pointerManager;

    [SerializeField] private NotesEditController notesEditController;

    [SerializeField] private SoundEditorToggleController toggleController;

    [SerializeField] private SoundEditorButtonController buttonController;

    [SerializeField] private TextMeshProUGUI importDataNameText;

    [SerializeField] private TextMeshProUGUI exportDataNameText;

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

        setDataName();
    }

    private void Start()
    {
        isPaused = true;
    }

    private void OnValidate()
    {
        setDataName();
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

        var dataList = new List<NotesData>();
        var soundEditorNotesRoot = notesEditController.transform;
        var filePath = InGameDefine.NotesDataSaveLocationPath(exportDataName);

        soundEditorNotesRoot.AccessAllChildComponent<SoundEditorNotes>(
            (notes) => { dataList.Add(notes.GetNotesData()); });

        JsonUtilityExtension.ExportArr(dataList, filePath, false);
    }

    private void onClickScoreDataExportButton()
    {
        Debug.Log("楽曲データの書き出し");
        var filePath = InGameDefine.ScoreDataSaveLocationPath(exportDataName);
        JsonUtilityExtension.Export(scoreCreation.SoundData, filePath, false);
    }

    private void setDataName()
    {
        importDataNameText.text = $"Import: {importDataName}";
        exportDataNameText.text = $"Export: {exportDataName}";
    }
}
