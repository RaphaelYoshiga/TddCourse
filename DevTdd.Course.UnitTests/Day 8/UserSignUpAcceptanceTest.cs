
using FluentAssertions;
using Moq;

namespace DevTdd.Course.UnitTests.Day8
{
    public class UserSignUpAcceptanceTest
    {
        private readonly Mock<IVerifierApi> _verifierApiMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private SignUpController _controller;

        public UserSignUpAcceptanceTest()
        {
            _verifierApiMock = new Mock<IVerifierApi>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _controller = new SignUpController(_verifierApiMock.Object,
                _userRepositoryMock.Object, 
                new SignUpRequestToVerifierRequest(), 
                new SignUpRequestToUserDomain());
        }

        [Fact]
        public void SuccessfulSignUp()
        {
            var expectedVerificationRequest = new UserVerificationRequest()
            {
                FirstName = "John",
                LastName = "Doe",
                MiddleNames = "Michael",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
            };
            _verifierApiMock.Setup(x => x.Verify(It.Is<UserVerificationRequest>(p => AssertVerificationRequest(p, expectedVerificationRequest))))
                .Returns(VerificationResult.Success);

            var signUpRequest = new SignUpRequest()
            {
                FirstName = "John",
                LastName = "Doe",
                MiddleNames = "Michael",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = new AddressRequest
                {
                    Line1 = "123 Main St",
                    Line2 = "Apt 4B",
                    Postcode = "12345",
                    City = "New York"
                }
            };
            _controller.SignUp(signUpRequest);

            var expectedUserDomain = new UserDomain()
            {
                FirstName = "John",
                LastName = "Doe",
                MiddleNames = "Michael",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Status = VerificationResult.Success
            };
            _userRepositoryMock.Verify(x => x.Save(It.Is<UserDomain>(p => AssertSavedUser(p, expectedUserDomain))), Times.Once);
        }

        private bool AssertVerificationRequest(UserVerificationRequest userVerificationRequest, 
            UserVerificationRequest expectedVerificationRequest)
        {
            userVerificationRequest.Email.Should().Be(expectedVerificationRequest.Email);
            userVerificationRequest.FirstName.Should().Be(expectedVerificationRequest.FirstName);
            userVerificationRequest.LastName.Should().Be(expectedVerificationRequest.LastName);
            userVerificationRequest.MiddleNames.Should().Be(expectedVerificationRequest.MiddleNames);
            userVerificationRequest.PhoneNumber.Should().Be(expectedVerificationRequest.PhoneNumber);

            return true;
        }

        private bool AssertSavedUser(UserDomain userDomain, UserDomain expectedUserDomain)
        {
            userDomain.Email.Should().Be(expectedUserDomain.Email);
            userDomain.FirstName.Should().Be(expectedUserDomain.FirstName);
            userDomain.LastName.Should().Be(expectedUserDomain.LastName);
            userDomain.MiddleNames.Should().Be(expectedUserDomain.MiddleNames);
            userDomain.PhoneNumber.Should().Be(expectedUserDomain.PhoneNumber);
            userDomain.Status.Should().Be(expectedUserDomain.Status);
            return true;
        }
    }
}