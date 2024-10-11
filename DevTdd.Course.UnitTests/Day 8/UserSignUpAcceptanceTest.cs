
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
            _controller = new SignUpController(_verifierApiMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public void SuccessfulSignUp()
        {
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

        private bool AssertVerificationRequest(UserVerificationRequest userVerificationRequest, UserVerificationRequest expectedVerificationRequest)
        {
            return userVerificationRequest.Email == expectedVerificationRequest.Email;
        }

        private bool AssertSavedUser(UserDomain userDomain, UserDomain expectedUserDomain)
        {
            return userDomain.Email == expectedUserDomain.Email && userDomain.Status == expectedUserDomain.Status;
        }
    }
}