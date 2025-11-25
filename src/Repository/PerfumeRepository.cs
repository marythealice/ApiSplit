using ApiSplit.Models;
using ApiSplit.Requests;
using Microsoft.EntityFrameworkCore;

namespace ApiSplit.Repository;

public class PerfumeRepository(ApiDb db)
{
    private readonly ApiDb _db = db;
    public async Task<Perfume> CreatePerfume(PerfumeRequest request)
    {
        var perfume = new Perfume(request.Name, request.Volume);
        _db.Perfumes.Add(perfume);
        await _db.SaveChangesAsync();
        return perfume;
    }

    public async Task<Perfume?> GetPerfume(uint id)
    {
        return await _db.Perfumes.FindAsync(id);
    }

    public async Task<List<Perfume>> GetAllPerfumes()
    {
        return await _db.Perfumes.ToListAsync();
    }

    public async Task<int> RemovePerfume(uint id)
    {
        var perfume = await GetPerfume(id);
        if (perfume == null) return 0;
        
        _db.Perfumes.Remove(perfume);
        return await _db.SaveChangesAsync();

    }
}