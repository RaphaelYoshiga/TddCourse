using DevTdd.Course.UnitTests.Day5;
using Moq;

namespace DevTdd.Course.UnitTests.Day8;
public class SignUpControllerShould
{
    private SignUpController _controller;
    private Mock<IVerifierApi> _verifierApiMock;
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<ISignUpRequestToVerifierRequest> _signUpUserRequestToVerifierRequestMock;
    private Mock<ISignUpRequestToUserDomain> _signUpRequestToUserDomainMock;

    public SignUpControllerShould()
    {
        _verifierApiMock = new Mock<IVerifierApi>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _signUpUserRequestToVerifierRequestMock = new Mock<ISignUpRequestToVerifierRequest>();
        _signUpRequestToUserDomainMock = new Mock<ISignUpRequestToUserDomain>();
        _controller = new SignUpController(_verifierApiMock.Object,
            _userRepositoryMock.Object,
            _signUpUserRequestToVerifierRequestMock.Object,
            _signUpRequestToUserDomainMock.Object);
    }

    [Theory]
    [InlineData(VerificationResult.Success)]
    [InlineData(VerificationResult.ManualReferral)]
    public void SaveSuccessful(VerificationResult verificationResult)
    {
        var userVerificationRequest = new UserVerificationRequest();
        var signUpRequest = new SignUpRequest();
        _signUpUserRequestToVerifierRequestMock.Setup(x => x.Map(signUpRequest))
            .Returns(userVerificationRequest);
        _verifierApiMock.Setup(x => x.Verify(userVerificationRequest))
            .Returns(verificationResult);

        var userDomain = new UserDomain();
        _signUpRequestToUserDomainMock.Setup(x => x.Build(signUpRequest, verificationResult))
            .Returns(userDomain);

        _controller.SignUp(signUpRequest);

        _userRepositoryMock.Verify(x => x.Save(userDomain), Times.Once);
    }

    [Theory]
    [InlineData(VerificationResult.Denied)]
    public void NotSaveUserDenied(VerificationResult verificationResult)
    {
        var userVerificationRequest = new UserVerificationRequest();
        var signUpRequest = new SignUpRequest();
        _signUpUserRequestToVerifierRequestMock.Setup(x => x.Map(signUpRequest))
            .Returns(userVerificationRequest);
        _verifierApiMock.Setup(x => x.Verify(userVerificationRequest))
            .Returns(verificationResult);

        var userDomain = new UserDomain();
        _signUpRequestToUserDomainMock.Setup(x => x.Build(signUpRequest, verificationResult))
            .Returns(userDomain);

        _controller.SignUp(signUpRequest);

        _userRepositoryMock.Verify(x => x.Save(It.IsAny<UserDomain>()), Times.Never);
    }
}

public class SignUpController
{
    private IVerifierApi _verifierApi;
    private IUserRepository _userRepository;
    private ISignUpRequestToVerifierRequest _signUpRequestToVerifierRequest;
    private ISignUpRequestToUserDomain _signUpRequestToUserDomain;

    public SignUpController(IVerifierApi verifierApi,
        IUserRepository userRepository, 
        ISignUpRequestToVerifierRequest signUpRequestToVerifierRequest,
        ISignUpRequestToUserDomain signUpRequestToUserDomain)
    {
        _signUpRequestToUserDomain = signUpRequestToUserDomain;
        _signUpRequestToVerifierRequest = signUpRequestToVerifierRequest;
        _userRepository = userRepository;
        _verifierApi = verifierApi;
    }


    public void SignUp(SignUpRequest request)
    {
        var userVerificationRequest = _signUpRequestToVerifierRequest.Map(request);
        var verificationResult = _verifierApi.Verify(userVerificationRequest);
        if (verificationResult == VerificationResult.Denied)
            return;

        var userDomain = _signUpRequestToUserDomain.Build(request, verificationResult);
        _userRepository.Save(userDomain);
    }
}