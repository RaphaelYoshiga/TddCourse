
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

    public class UserDomain
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleNames { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public VerificationResult Status { get; set; }
    }

    public interface IUserRepository
    {
        void Save(UserDomain userDomain);
    }

    public class UserVerificationRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleNames { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public VerificationAddress Address { get; set; }
    }
    public class VerificationAddress
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
    }

    public interface IVerifierApi
    {
        VerificationResult Verify(UserVerificationRequest verificationRequest);
    }

    public enum VerificationResult
    {
        Denied,
        Success,
        ManualReferral
    }

    public class SignUpController
    {
        public SignUpController(IVerifierApi verifierApi, IUserRepository userRepository)
        {
        }

        public void SignUp(SignUpRequest request)
        {
        }
    }

    public class SignUpRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleNames { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public AddressRequest Address { get; set; }


    }
    public class AddressRequest
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
    }
}