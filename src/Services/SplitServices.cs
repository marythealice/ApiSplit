public class SplitServices
{
    private readonly ApiDb _db;

    public SplitServices(ApiDb db)
    {
        _db = db;
    }

    public async Task<Split> CreateSplit(SplitRequest request)
    {
        var volume = request.Volume;
        var split = new Split(request.BottleId, volume, request.UserId, request.Type);

        _db.Splits.Add(split);
        await _db.SaveChangesAsync();
        return split;
    }


}