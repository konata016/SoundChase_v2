
public interface IRhythmGame
{
    void OnPause();

    void OnUnPause();

    void OnDispose();
}

public interface IBeat
{
    int Beat { get; }

    void OnBeat();
}

public interface INotes
{
    NotesData.NotesType NotesType { get; }

    Range<float> FixHitTime { get; }

    int LaneNumber { get; }

    void Initialize(float hitStartTime, float hitEndTime, int laneNumber);

    void SetupAnimation(float speed);

    void OnShowView();

    void OnHideView();

    void OnProcessing(bool isHitPlayer);
}

// https://qiita.com/riekure/items/ab6b5deb391399944a15
// 依存性逆転の法則について考える
// todo:実装を行っていない
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