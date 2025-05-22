public class BottleRequest
{
    public uint Volume { get; set; }

    public decimal PricePerMl { get; set; }

    public required string Type { get; set; }

    public uint PerfumeId { get; set; }
}
