using Microsoft.EntityFrameworkCore;

[Owned]
public class Address
{
    public string RecipientName { get; set; }
    public string StreetName { get; set; }
    public string StreetNumber { get; set; }
    public string ApartmentNumber { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }

    public Address(string recipientName, string streetName, string streetNumber, string state, string city, string zipCode, string apartmentNumber = "")
    {

        RecipientName = recipientName;
        StreetName = streetName;
        StreetNumber = streetNumber;
        ApartmentNumber = apartmentNumber;
        State = state;
        City = city;
        ZipCode = zipCode;

    }

    public override string ToString()
    {
        string apartmentNumberStr = !string.IsNullOrEmpty(ApartmentNumber) ? " / " + ApartmentNumber : "";
        return string.Format("{0} {1}{2} {3:00000-000} {4}", StreetName, StreetNumber, apartmentNumberStr, int.Parse(ZipCode), City);
    }


}

