using ApiSplit.Models;

namespace ApiSplit.Requests;

public class SplitRequest
{
    public required uint BottleId { get; set; }
    public required uint Volume { get; set; }
    public required uint UserId { get; set; }
    public decimal PricePerMl { get; set; }
    public SplitType Type { get; set; }
}