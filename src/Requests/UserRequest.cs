namespace ApiSplit.Requests;
public class UserRequest
{
    public required string Name { get; set; }
    public required AddressRequest Address { get; set; }
    public required string Document { get; set; }
}