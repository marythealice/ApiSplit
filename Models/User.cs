using System.ComponentModel.DataAnnotations;
public class User
{
    public uint Id { get; private set; }
    public string Name { get; private set; }
    public Address Address { get; private set; }
    [EmailAddress]
    public string Email { get; private set; }
    public User() { }
    public User(string name, Address address, string email)
    {
        Name = name;
        Address = address;
        Email = email;
    }
}