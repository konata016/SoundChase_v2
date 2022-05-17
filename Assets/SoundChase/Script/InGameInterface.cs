
public interface IRhythmGame
{
    void OnPause();

    void OnUnPause();

    void OnDispose();
}

public interface INotes
{
    NotesData.NotesType NotesType { get; }

    Range<float> FixHitTime { get; }

    void Initialize(float hitStartTime, float hitEndTime, int laneNumber);

    void SetupAnimation(float speed);

    void OnShowView();

    void OnHideView();

    void OnProcessing();
}

// https://qiita.com/riekure/items/ab6b5deb391399944a15
// ˆË‘¶«‹t“]‚Ì–@‘¥‚É‚Â‚¢‚Äl‚¦‚é
// todo:À‘•‚ğs‚Á‚Ä‚¢‚È‚¢
public interface INotesHit
{
    Range<int> HitLinePositionRange { get; }

    void OnHit();
}

public interface IInputKey
{
    void OnTap();

    void OnLeft();

    void OnRight();
}