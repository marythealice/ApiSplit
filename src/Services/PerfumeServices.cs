using ApiSplit.Models;
using ApiSplit.Repository;
using ApiSplit.Requests;
using ApiSplit.Validators;
using Microsoft.EntityFrameworkCore;
namespace ApiSplit.Services;
public class PerfumeServices(PerfumeRepository perfumeRepository, PerfumeRequestValidator validator)
{
    private readonly PerfumeRepository _perfumeRepository = perfumeRepository;
    private readonly PerfumeRequestValidator _requestValidator = new PerfumeRequestValidator();

    public async Task<Perfume?> CreatePerfumeAsync(PerfumeRequest request)
    {
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
            return null;
        var perfume = await _perfumeRepository.CreatePerfume(request);
        return perfume;
    }
}