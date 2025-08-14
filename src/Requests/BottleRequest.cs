namespace ApiSplit.Requests;
public class BottleRequest
{
    public required decimal Volume { get; set; }

    public required decimal PricePerMl { get; set; }
    
    public required uint PerfumeId { get; set; }
}
