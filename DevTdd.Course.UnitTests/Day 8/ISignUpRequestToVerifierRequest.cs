using FluentAssertions;

namespace DevTdd.Course.UnitTests.Day8;

public class SignUpRequestToVerifierRequestShould
{
    [Theory]
    [InlineData("Raphael", "Yoshi", "midl", "raphael.yoishi@test.com", "+44073555")]
    [InlineData("John", "Lll", "mid", "test.yoishi@test.com", "+44654455")]
    public void Map(string firstName, string lastName, string middleNames, string email, string phoneNumber)
    {
        var mapper = new SignUpRequestToVerifierRequest();
        var signUpRequest = new SignUpRequest()
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            MiddleNames = middleNames,
            PhoneNumber = phoneNumber
        };

        var userVerificationRequest = mapper.Map(signUpRequest);

        userVerificationRequest.FirstName.Should().Be(firstName);
        userVerificationRequest.LastName.Should().Be(lastName);
        userVerificationRequest.MiddleNames.Should().Be(middleNames);
        userVerificationRequest.Email.Should().Be(email);
        userVerificationRequest.PhoneNumber.Should().Be(phoneNumber);
    }
}

public interface ISignUpRequestToVerifierRequest
{
    UserVerificationRequest Map(SignUpRequest signUpRequest);
}

public class SignUpRequestToVerifierRequest : ISignUpRequestToVerifierRequest
{
    public UserVerificationRequest Map(SignUpRequest signUpRequest)
    {
        return new UserVerificationRequest()
        {
            Email = signUpRequest.Email,
            FirstName = signUpRequest.FirstName,
            LastName = signUpRequest.LastName,
            MiddleNames = signUpRequest.MiddleNames,
            PhoneNumber = signUpRequest.PhoneNumber,
        };
    }
}