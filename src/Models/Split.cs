
public enum SplitStatus
{
    Unpaid = 1,
    Paid,
    Canceled
};

public enum SplitType
{
    APC = 0,
    Three = 3,
    Five = 5,
    Ten = 10,
    Fifteen = 15

};


public class Split
{
    public uint Id { get; private set; }
    public decimal Price { get; set; }
    public decimal PricePerMl { get; set; }
    public DateTime OrderDate { get; private set; }
    public uint BottleId { get; private set; }
    public Bottle Bottle { get; private set; }
    public decimal Volume { get; private set; }
    public SplitStatus Status { get; private set; }
    public SplitType Type { get; private set; }
    public DateTime? PayDate { get; private set; }
    public uint UserId { get; private set; }

    private Split() { }
    public Split(uint bottleId, decimal volume, uint userId, decimal pricePerMl ,SplitType splitType)
    {
        OrderDate = DateTime.UtcNow;
        BottleId = bottleId;
        Volume = volume;
        Status = SplitStatus.Unpaid;
        UserId = userId;
        PricePerMl = pricePerMl;
        Price = volume * pricePerMl;
        Type = splitType;

    }

    public bool ChangeVolume(decimal volume)
    {
        Volume = volume;
        return true;
    }


    public bool IsTFB()
    {
        return Type == SplitType.APC;
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


}