public class Bottle
{
    public uint Id { get; private set; }
    public decimal InitialVolume { get; private set; }

    public decimal PricePerMl { get; private set; }

    public string? Status { get; private set; }

    public string? Type { get; private set; }

    public uint PerfumeId { get; private set; }

    public Perfume Perfume { get; private set; }

    public List<Split> Splits { get; private set; }

    public decimal CurrentVolume => InitialVolume - Splits.Sum(s => s.Volume);

    private Bottle() { }

    public Bottle(decimal volume, decimal pricePerMl, string type, uint perfumeId)
    {
        InitialVolume = volume;
        PricePerMl = pricePerMl;
        Status = "Open";
        Type = type;
        PerfumeId = perfumeId;
        Splits = new List<Split>();

    }

    public bool AddSplitToBottle(Split split)
    {
        if (split.Volume >= CurrentVolume)
        {
            return false;
        }

        Splits.Append(split);
        return true;
    }
}
