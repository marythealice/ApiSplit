using Microsoft.EntityFrameworkCore;

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
        Address address = new Address(addressReq.RecipientName, addressReq.StreetName, addressReq.StreetName, addressReq.ApartmentNumber,
        addressReq.State, addressReq.City, addressReq.ZipCode);
        var user = new User(request.Name, address);
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
}