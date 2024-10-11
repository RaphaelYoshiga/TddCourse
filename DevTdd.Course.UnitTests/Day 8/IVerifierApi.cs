namespace DevTdd.Course.UnitTests.Day8;

public interface IVerifierApi
{
    VerificationResult Verify(UserVerificationRequest verificationRequest);
}