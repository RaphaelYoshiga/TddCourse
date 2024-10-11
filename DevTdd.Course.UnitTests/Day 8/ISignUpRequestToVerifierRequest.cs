using FluentAssertions;

namespace DevTdd.Course.UnitTests.Day8;

public class SignUpRequestToVerifierRequestShould
{
    [Theory]
    [InlineData("raphael.yoishi@test.com")]
    [InlineData("test.yoishi@test.com")]
    public void Map(string email)
    {
        var mapper = new SignUpRequestToVerifierRequest();
        var signUpRequest = new SignUpRequest() { Email = email };

        var userVerificationRequest = mapper.Map(signUpRequest);

        userVerificationRequest.Email.Should().Be(email);
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
            Email = signUpRequest.Email
        };
    }
}