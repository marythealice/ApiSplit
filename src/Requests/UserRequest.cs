using System.ComponentModel.DataAnnotations;

public class UserRequest
{
    public required string Name { get; set; }
    public required AddressRequest Address { get; set; }
    public required string Email { get; set; }
}