using DevTdd.Course.UnitTests.Day5;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTdd.Course.UnitTests.Day7
{
    public class SaveUserControllerShould
    {
        public SaveUserControllerShould()
        {
        }

        [Theory]
        [InlineData(2, "John", "2-John")]
        [InlineData(3, "Raph", "3-Raph")]
        public void Save(int id, string name, string expectedConfidential)
        {
            var saverUserCommandMock = new Mock<ISaveUserCommand>();
            var mapperMock = new Mock<IUserRequestToDbRecordMapper>();
            var controller = new SaveUserController(saverUserCommandMock.Object, mapperMock.Object);

            var userRequest = new UserRequest()
            {
                Id = id,
                Name = name
            };
            var expectedRecord = new UserDbRecord();
            mapperMock.Setup(p => p.UserRequestToDbMapper(userRequest))
                .Returns(expectedRecord);

            controller.Save(userRequest);

            saverUserCommandMock.Verify(x => x.Save(expectedRecord), Times.Once);
        }
    }

    public class SaveUserController
    {
        private ISaveUserCommand _saveUserCommand;
        private readonly IUserRequestToDbRecordMapper _userRequestToDbRecordMapper;

        public SaveUserController(ISaveUserCommand saveUserCommand,
            IUserRequestToDbRecordMapper userRequestToDbRecordMapper)
        {
            _saveUserCommand = saveUserCommand;
            _userRequestToDbRecordMapper = userRequestToDbRecordMapper;
        }

        public void Save(UserRequest userRequest)
        {
            var userDbRecord = _userRequestToDbRecordMapper.UserRequestToDbMapper(userRequest);
            _saveUserCommand.Save(userDbRecord);
        }
    }

    public interface ISaveUserCommand
    {
        void Save(UserDbRecord userDbRecord);
    }

    public interface IUserRequestToDbRecordMapper
    {
        UserDbRecord UserRequestToDbMapper(UserRequest userRequest);
    }

    public class UserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserRequestToDbRecordMapper
    {
        public UserRequestToDbRecordMapper()
        {
        }

        public UserDbRecord UserRequestToDbMapper(UserRequest userRequest)
        {
            var userDbRecord = new UserDbRecord()
            {
                Id = userRequest.Id,
                Name = userRequest.Name,
                ConfidentialProperty = userRequest.Id + "-" + userRequest.Name
            };
            return userDbRecord;
        }
    }
}
