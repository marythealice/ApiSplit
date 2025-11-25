using ApiSplit.Models;
using ApiSplit.Repository;
using ApiSplit.Requests;
using Microsoft.EntityFrameworkCore;

namespace ApiSplit.Services;

public class BottleServices(BottleRepository bottleRepository, PerfumeRepository perfumeRepository)
{
    private readonly BottleRepository _bottleRepository = bottleRepository;
    private readonly PerfumeRepository _perfumeRepository = perfumeRepository;

    public async Task<Bottle?> CreateBottle(BottleRequest request)
    {
        if (request.Volume < 0) return null;
        if (request.PricePerMl < 0) return null;
        
        var perfume = await _perfumeRepository.GetPerfume(request.PerfumeId);
        if (perfume == null) return null;
        var bottle = await _bottleRepository.CreateBottle(request);
        return bottle;
    }

}