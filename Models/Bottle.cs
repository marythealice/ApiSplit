public class Bottle
{
    public uint Id { get; private set; }
    public uint Volume { get; private set; }

    public decimal PricePerMl { get; private set; }

    public string? Status { get; private set; }

    public string? Type { get; private set; }

    public uint PerfumeId { get; private set; }

    public Perfume Perfume { get; private set; }

    public Bottle(uint volume, decimal pricePerMl, string type, uint perfumeId)
    {
        Volume = volume;
        PricePerMl = pricePerMl;
        Status = "Open";
        Type = type;
        PerfumeId = perfumeId;
    }

    public uint GetBottleVolume()
    {
        return Volume;
    }

    public uint AddToVolume(uint volume)
    {
        Volume += volume;
        return Volume;
    }

    public uint SubtractFromVolume(uint volume)
    {
        Volume -= volume;
        return Volume;
    }


}
