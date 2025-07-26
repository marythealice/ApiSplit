public class SplitServices
{
    private readonly ApiDb _db;

    public SplitServices(ApiDb db)
    {
        _db = db;
    }

    public async Task<Split> CreateSplit(SplitRequest request)
    {
        var split = new Split(request.BottleId, request.Volume, request.UserId, request.PricePerMl,request.Type);

        _db.Splits.Add(split);
        await _db.SaveChangesAsync();
        return split;
    }


}