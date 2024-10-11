using FluentAssertions;

namespace DevTdd.Course.UnitTests.Day8;


public class SignUpRequestToUserDomainShould
{
    [Theory]
    [InlineData("raphael.yoishi@test.com", "Raphael", "Yoshi", "Mas", "+44092222", VerificationResult.Success)]
    [InlineData("test.yoishi@test.com", "John", "Test", "assds", "+4402292222", VerificationResult.ManualReferral)]
    public void Build(string email, string firstName, string lastName, string middleName, string phoneNumber, VerificationResult status)
    {
        var mapper = new SignUpRequestToUserDomain();
        var signUpRequest = new SignUpRequest() { 
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            MiddleNames = middleName,
            PhoneNumber = phoneNumber,
        };

        var userDomain = mapper.Build(signUpRequest, status);

        userDomain.Email.Should().Be(email);
        userDomain.FirstName.Should().Be(firstName);
        userDomain.LastName.Should().Be(lastName);
        userDomain.MiddleNames.Should().Be(middleName);
        userDomain.PhoneNumber.Should().Be(phoneNumber);
        userDomain.Status.Should().Be(status);
    }
}
public interface ISignUpRequestToUserDomain
{
    UserDomain Build(SignUpRequest signUpRequest, VerificationResult status);
}

public class SignUpRequestToUserDomain : ISignUpRequestToUserDomain
{
    public UserDomain Build(SignUpRequest signUpRequest, VerificationResult status)
    {
        return new UserDomain()
        {
            Email = signUpRequest.Email,
            FirstName = signUpRequest.FirstName,
            LastName = signUpRequest.LastName,
            MiddleNames = signUpRequest.MiddleNames,
            PhoneNumber = signUpRequest.PhoneNumber,
        
            Status = status
        };
    }
}