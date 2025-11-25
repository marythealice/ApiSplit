using ApiSplit.Requests;
using ApiSplit.Validators;


namespace ApiSplit.UnitTests;

public class UserTests
{
    [Fact]
    public void Given_Valid_UserRequest_Parameters_Then_UserRequest_Is_Validated()
    {
        // Arrange

        var addressRequest = new AddressRequest
        {
            RecipientName = "Maria Alice Nantes",
            StreetName = "Candida Lima de Barros",
            StreetNumber = "537",
            State =  "Mato Grosso do Sul",
            City = "Campo Grande", 
            ZipCode = "79041390",
            ApartmentNumber = ""
           
        };
        var userRequest = new UserRequest
        {
            Name = "Maria Alice Nantes",
            Address = addressRequest,
            Document = "06384538105"
        };

        // Act

        var userValidator = new UserRequestValidator();
        
        var result = userValidator.Validate(userRequest);
        

        // Assert

        Assert.True(result.IsValid);
    }

    [Fact]

    public void Given_Invalid_UserRequest_Parameters_Then_UserRequest_Is_NotValidated()
    {
        // Arrange
    
        var addressRequest = new AddressRequest
        {
            RecipientName = "Maria Alice Nantes",
            StreetName = "Candida Lima de Barros",
            StreetNumber = "537",
            State =  "Mato Grosso do Sul",
            City = "Campo Grande", 
            ZipCode = "79041390",
            ApartmentNumber = ""
           
        };
        var userRequest = new UserRequest
        {
            Name = "Maria Alice Nantes",
            Address = addressRequest,
            Document = "0638453810" // invalid document length
        };
    
        // Act
        
        var userValidator = new UserRequestValidator();
        
        var result = userValidator.Validate(userRequest);
        
        
        // Assert
        
        Assert.False(result.IsValid);
    }
}