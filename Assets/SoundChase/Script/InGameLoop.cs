using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLoop : SingletonMonoBehaviour<InGameLoop>
{
    [SerializeField]
    private PlayerInputController playerInputController;

    [SerializeField]
    private BeatObjectSetting beatObjectSetting;

    [SerializeField]
    private Transform notesRoot;

    [SerializeField]
    private NotesBase[] notesBases;

    [SerializeField]
    private PlayerBase player;

    [SerializeField]
    private string inputDataName = "Test2";

    public float Time { get; private set; }

    private float speed => notesRoot.localScale.z;

    private bool isPaused = true;

    // Start is called before the first frame update
    private void Start()
    {
        var notesDataPath = InGameDefine.NotesDataSaveLocationPath(inputDataName);
        var scoreDataPath= InGameDefine.ScoreDataSaveLocationPath(inputDataName);

        var notesDataArr = JsonUtilityExtension.ImportArr<NotesData>(notesDataPath);
        var scoreData = JsonUtilityExtension.Import<ScoreData>(scoreDataPath);

        for (int i = 0; i < notesDataArr.Length; i++)
        {
            createNotes(notesDataArr[i]);
        }

        player.Initialize(new InGameSeData(), 0, 3);
        playerInputController.Initialize(player, 0.035f, 0.032f);

        beatObjectSetting.Initialize(scoreData.Rhythm, scoreData.Bpm);

        SoundManager.Instance.PlayBGM(scoreData.SongNameType);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;
        }

        addTime();
        var pos = notesRoot.localPosition;
        pos.z = -Time * speed;
        notesRoot.localPosition = pos;
    }

    private void createNotes(NotesData notesData)
    {
        for (int i = 0; i < notesBases.Length; i++)
        {
            if(notesData.Type!= notesBases[i].NotesType)
            {
                continue;
            }

            var notesBase = Instantiate(notesBases[i], notesRoot);

            notesBase.Initialize(
                notesData.HitStartTime,
                notesData.HitEndTime,
                notesData.LanePositionNumber);

            notesBase.SetupAnimation(speed);

            StartCoroutine(updateNotes(notesBase, notesData));

            break;
        }
    }

    private IEnumerator updateNotes(INotes notes, NotesData data)
    {
        var showTime = data.HitStartTime - InGameDefine.NotesShowTime;
        var hideTime = data.HitEndTime + InGameDefine.NotesHideTime;
        var hitStartTime = data.HitStartTime - notes.FixHitTime.Min;
        var hitEndTime = data.HitEndTime + notes.FixHitTime.Max;

        yield return weightTimer(showTime);
        notes.OnShowView();

        yield return weightTimer(hitStartTime);

        yield return weightTimer(hitEndTime, () => onHitNotes(notes, data));

        yield return weightTimer(hideTime);
        notes.OnHideView();

        yield return null;
    }

    private bool onHitNotes(INotes notes, NotesData data)
    {
        var isHit = player.OnHitNotes(data.Type, data.LanePositionNumber);

        if (player.CurrentLanePositionNumber == notes.LaneNumber)
        {
            notes.OnProcessing(isHit);
            return isHit;
        }

        return false;
    }

    private IEnumerator weightTimer(float waitTime, Func<bool> onUpdateTime = null)
    {

        for (var isTimeOut = false; !isTimeOut;)
        {
            isTimeOut = waitTime < Time;

            if (onUpdateTime?.Invoke() ?? false)
            {
                isTimeOut = true;
            }

            yield return null;
        }
    }

    private void addTime()
    {
        Time = SoundManager.Instance.SoundTimeBGM();
    }
}
