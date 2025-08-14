using ApiSplit.Models;
using Microsoft.EntityFrameworkCore;
namespace ApiSplit.Services;
public class PerfumeServices
{
    private readonly ApiDb _db;

    public PerfumeServices(ApiDb db)
    {
        _db = db;
    }

    public async Task<Perfume> CreatePerfume(string name, uint volume)
    {
        var perfume = new Perfume(name, volume);
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

    public async Task<int> RemovePerfume(Perfume perfume)
    {
        _db.Perfumes.Remove(perfume);
        return await _db.SaveChangesAsync();

    }
}