using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLoop : MonoBehaviour
{
    [SerializeField]
    private PlayerInputController playerInputController;

    [SerializeField]
    private Transform notesRoot;

    [SerializeField]
    private NotesBase[] notesBases;

    [SerializeField]
    private PlayerBase player;

    [SerializeField]
    private string inputDataName = "Test2";

    public float time;

    private float speed => notesRoot.localScale.z;

    // Start is called before the first frame update
    private void Start()
    {
        var path = InGameDefine.NotesDataSaveLocationPath(inputDataName);
        var notesDataArr = JsonUtilityExtension.ImportArr<NotesData>(path);

        for (int i = 0; i < notesDataArr.Length; i++)
        {
            createNotes(notesDataArr[i]);
        }

        player.Initialize(new InGameSeData(), 0, 4);
        playerInputController.Initialize(player, 0.035f, 0.032f);
    }

    // Update is called once per frame
    private void Update()
    {
        addTime();
        var pos = notesRoot.localPosition;
        pos.z = -time * speed;
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

        yield return weightTimer(hitEndTime, () => onHittingToNotes(notes, data));

        yield return weightTimer(hideTime);
        notes.OnHideView();

        yield return null;
    }

    private void onHittingToNotes(INotes notes, NotesData data)
    {
        player.OnHitNotes(data.Type, data.LanePositionNumber);
        notes.OnProcessing();
    }

    private IEnumerator weightTimer(float waitTime, Action onUpdateTime = null)
    {
        for (bool isTimeOut = false; !isTimeOut;)
        {
            isTimeOut = waitTime < time;
            onUpdateTime?.Invoke();
            yield return null;
        }
    }

    private void addTime()
    {
        time += Time.deltaTime;
    }
}
