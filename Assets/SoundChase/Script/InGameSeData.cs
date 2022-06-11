using SoundName = SESoundController.SoundName;

public class InGameSeData
{
    public readonly SoundName HittingFall;

    public readonly SoundName JustDodge;

    public readonly SoundName Technic;

    public readonly SoundName GettingPoint;

    public InGameSeData()
    {
        HittingFall = SoundName.Fall;
        JustDodge = SoundName.JustDodge;
        Technic = SoundName.Technic;
        GettingPoint = SoundName.Point;
    }

    public InGameSeData(
        SoundName hittingFall,
        SoundName justDodge,
        SoundName technic,
        SoundName gettingPoint)
    {
        HittingFall = hittingFall;
        JustDodge = justDodge;
        Technic = technic;
        GettingPoint = gettingPoint;
    }
}
