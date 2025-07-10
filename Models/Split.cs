public enum SplitStatus
{
    Unpaid = 1,
    Paid,
    Canceled
};

public enum Volume
{
    Three = 3,
    Five = 5,
    Ten = 10,
    Fifteen = 15
};

public class Split
{
    public uint Id { get; private set; }
    public DateTime OrderDate { get; private set; }
    public uint BottleId { get; private set; }
    public Bottle Bottle { get; private set; }
    public Volume Volume { get; private set; }
    public SplitStatus Status { get; private set; }
    public DateTime? PayDate { get; private set; }
    public uint UserId { get; private set; }


    public Split(uint bottleId, Volume volume, uint userId)
    {
        OrderDate = DateTime.UtcNow;
        BottleId = bottleId;
        Volume = volume;
        Status = SplitStatus.Unpaid;
        UserId = userId;

    }

    public bool Pay()
    {
        if (Status == SplitStatus.Unpaid)
        {
            Status = SplitStatus.Paid;
            PayDate = DateTime.UtcNow;
            return true;
        }

        return false;
    }

    public bool Cancel()
    {
        if (Status == SplitStatus.Unpaid)
        {
            Status = SplitStatus.Canceled;

            return true;
        }

        return false;
    }

    public void ChangeVolume(Volume volume)
    {
        Volume = volume;
    }
}