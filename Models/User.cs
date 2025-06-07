public class User
{
    public uint Id { get; private set; }
    public string Name { get; private set; }
    public Address Address { get; private set; }

    public User() { }
    public User(string name, Address address)
    {
        Name = name;
        Address = address;
    }
}