namespace DevTdd.Course.UnitTests.Day5;

public interface IUserDbToResponseMapper
{
    UserResponse Map(UserDbRecord userDbRecord);
}

public class UserDbToResponseMapper : IUserDbToResponseMapper
{
    public UserResponse Map(UserDbRecord userDbRecord)
    {
        return new UserResponse()
        {
            Id = userDbRecord.Id,
            Name = userDbRecord.Name
        };
    }
}