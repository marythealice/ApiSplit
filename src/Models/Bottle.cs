public class Bottle
{
    public uint Id { get; private set; }
    public decimal InitialVolume { get; private set; }

    public decimal PricePerMl { get; private set; }

    public string? Status { get; private set; }
    public uint PerfumeId { get; private set; }

    public Perfume Perfume { get; private set; }

    public List<Split> Splits { get; private set; }

    public decimal CurrentVolume => InitialVolume - Splits.Sum(s => s.Volume);

    private Bottle() { }

    public Bottle(decimal volume, decimal pricePerMl, uint perfumeId)
    {
        
        InitialVolume = volume;
        PricePerMl = pricePerMl;
        Status = "Open";
        PerfumeId = perfumeId;
        Splits = new List<Split>();

    }

    public bool AddSplit(Split split)
    {
        if (split.Volume >= CurrentVolume)
        {
            return false;
        }

        Splits.Add(split);
        return true;
    }
    
    public bool ChangeSplitVolume(uint id, decimal newVolume)
    {
        var split = Splits.Find(s => s.Id == id);
        if (split == null) return false;

        var volumeAvailable = CurrentVolume + split.Volume;
        
        return newVolume <= volumeAvailable && split.ChangeVolume(newVolume);
    }
}
