namespace DevTdd.Course.UnitTests.Day8;

public class UserVerificationRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleNames { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public VerificationAddress Address { get; set; }
}