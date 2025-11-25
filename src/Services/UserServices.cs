using ApiSplit.Models;
using ApiSplit.Requests;
using Microsoft.EntityFrameworkCore;

namespace ApiSplit.Services;
public class UserServices
{
    private readonly ApiDb _db;

    public UserServices(ApiDb db)
    {
        _db = db;
    }

    public async Task<User> CreateUser(UserRequest request)
    {
        var addressReq = request.Address;
        var address = new Address(
        addressReq.RecipientName,
        addressReq.StreetName,
        addressReq.StreetNumber,
        addressReq.ApartmentNumber,
        addressReq.State,
        addressReq.City,
        addressReq.ZipCode);
        var user = new User(request.Name, address, request.Document);
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUser(uint id)
    {
        return await _db.Users.FindAsync(id);
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _db.Users.ToListAsync();
    }
}