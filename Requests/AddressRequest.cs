public class AddressRequest
{
    public required string RecipientName { get; set; }
    public required string StreetName { get; set; }
    public required string StreetNumber { get; set; }
    public string ApartmentNumber { get; set; }
    public required string State { get; set; }
    public required string City { get; set; }
    public required string ZipCode { get; set; }
}