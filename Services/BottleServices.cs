using Microsoft.EntityFrameworkCore;

public class BottleServices
{
    private readonly ApiDb _db;

    public BottleServices(ApiDb db)
    {
        _db = db;
    }
    public async Task<Bottle> CreateBottle(BottleRequest request)
    {
        var bottle = new Bottle(request.Volume, request.PricePerMl, request.Type, request.PerfumeId);
        _db.Bottles.Add(bottle);
        await _db.SaveChangesAsync();
        return bottle;
    }

    public async Task<Bottle?> BottleExists(uint id)
    {
        return await _db.Bottles
        .Include(b => b.Perfume)
        .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<List<Bottle>> GetAllAsync()
    {
        return await _db.Bottles.ToListAsync();
    }







}