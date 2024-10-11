using DevTdd.Course.UnitTests.Day5;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using BadRequestResult = Microsoft.AspNetCore.Mvc.BadRequestResult;
using OkResult = Microsoft.AspNetCore.Mvc.OkResult;

namespace DevTdd.Course.UnitTests.Day7
{
    public class SaveUserControllerShould
    {
        private Mock<ISaveUserCommand> _saverUserCommandMock;
        private Mock<IUserRequestToDbRecordMapper> _mapperMock;
        private SaveUserController _controller;

        public SaveUserControllerShould()
        {
            _saverUserCommandMock = new Mock<ISaveUserCommand>();
            _mapperMock = new Mock<IUserRequestToDbRecordMapper>();
            _controller = new SaveUserController(_saverUserCommandMock.Object, _mapperMock.Object);
        }

        [Theory]
        [InlineData(2, "John")]
        [InlineData(3, "Raph")]
        public void Save(int id, string name)
        {
            var userRequest = new UserRequest()
            {
                Id = id,
                Name = name
            };
            var expectedRecord = new UserDbRecord();
            _mapperMock.Setup(p => p.UserRequestToDbMapper(userRequest))
                .Returns(expectedRecord);

            var result = _controller.Save(userRequest);

            _saverUserCommandMock.Verify(x => x.Save(expectedRecord), Times.Once);
            result.Should().BeOfType<OkResult>();
        }

        [Theory]
        [InlineData(2, "")]
        public void ReturnBadRequest(int id, string name)
        {
            var userRequest = new UserRequest()
            {
                Id = id,
                Name = name
            };

            var result = _controller.Save(userRequest);

            _mapperMock.Verify(p => p.UserRequestToDbMapper(It.IsAny<UserRequest>()), Times.Never);
            _saverUserCommandMock.Verify(x => x.Save(It.IsAny<UserDbRecord>()), Times.Never);
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void ReturnInternalServerError()
        {
            var userRequest = new UserRequest()
            {
                Id = 1,
                Name = "test"
            };
            var expectedRecord = new UserDbRecord();
            _mapperMock.Setup(p => p.UserRequestToDbMapper(userRequest))
                .Returns(expectedRecord);
            _saverUserCommandMock.Setup(x => x.Save(expectedRecord))
                .Throws<Exception>();

            var result = _controller.Save(userRequest);

            result.Should().BeOfType<ObjectResult>();
            ((ObjectResult)result).StatusCode.Should().Be(500);
        }

        [Fact]
        public void ReturnTooManyRequest()
        {
            var userRequest = new UserRequest()
            {
                Id = 1,
                Name = "test"
            };
            var expectedRecord = new UserDbRecord();
            _mapperMock.Setup(p => p.UserRequestToDbMapper(userRequest))
                .Returns(expectedRecord);
            _saverUserCommandMock.Setup(x => x.Save(expectedRecord))
                .Throws<DatabaseOverloadException>();

            var result = _controller.Save(userRequest);

            result.Should().BeOfType<ObjectResult>();
            ((ObjectResult)result).StatusCode.Should().Be(429);
        }
    }

    public class DatabaseOverloadException : Exception
    {

    }

    public class SaveUserController : ControllerBase
    {
        private ISaveUserCommand _saveUserCommand;
        private readonly IUserRequestToDbRecordMapper _userRequestToDbRecordMapper;

        public SaveUserController(ISaveUserCommand saveUserCommand,
            IUserRequestToDbRecordMapper userRequestToDbRecordMapper)
        {
            _saveUserCommand = saveUserCommand;
            _userRequestToDbRecordMapper = userRequestToDbRecordMapper;
        }

        public IActionResult Save(UserRequest userRequest)
        {
            if (userRequest.Name == "")
            {
                return BadRequest();
            }

            try
            {
                var userDbRecord = _userRequestToDbRecordMapper.UserRequestToDbMapper(userRequest);
                _saveUserCommand.Save(userDbRecord);
            }
            catch (DatabaseOverloadException e)
            {
                return new ObjectResult(null) { StatusCode = 429 };
            }
            catch (Exception e)
            {
                return Problem();
            }

            return Ok();
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
