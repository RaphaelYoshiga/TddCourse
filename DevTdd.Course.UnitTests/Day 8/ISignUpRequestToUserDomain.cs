using FluentAssertions;

namespace DevTdd.Course.UnitTests.Day8;


public class SignUpRequestToUserDomainShould
{
    [Theory]
    [InlineData("raphael.yoishi@test.com", "Raphael", VerificationResult.Success)]
    [InlineData("test.yoishi@test.com", "John", VerificationResult.ManualReferral)]
    public void Build(string email, string firstName, VerificationResult status)
    {
        var mapper = new SignUpRequestToUserDomain();
        var signUpRequest = new SignUpRequest() { Email = email, FirstName = firstName};

        var userDomain = mapper.Build(signUpRequest, status);

        userDomain.Email.Should().Be(email);
        userDomain.FirstName.Should().Be(firstName);
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
            Status = status
        };
    }
}