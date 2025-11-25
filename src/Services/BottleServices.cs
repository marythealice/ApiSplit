using ApiSplit.Models;
using ApiSplit.Requests;
using Microsoft.EntityFrameworkCore;

namespace ApiSplit.Services;

public class BottleServices(ApiDb db)
{
    private readonly ApiDb _db = db;
    public async Task<Bottle> CreateBottle(BottleRequest request)
    {
        var bottle = new Bottle(request.Volume, request.PricePerMl, request.PerfumeId);
        _db.Bottles.Add(bottle);
        await _db.SaveChangesAsync();
        return bottle;
    }

    public async Task<Bottle?> GetBottle(uint id)
    {
        return await _db.Bottles
        .Include(b => b.Perfume)
        .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<List<Bottle>> GetAllBottles()
    {
        return await _db.Bottles.ToListAsync();
    }










}