using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;

namespace DevTdd.Course.UnitTests.Day5
{
    public class UserControllerShould
    {
        private UserController _controller;
        private readonly Mock<IUserByIdQuery> _queryMock;
        private readonly Mock<IUserDbToResponseMapper> _mapperMock;

        public UserControllerShould()
        {
            _queryMock = new Mock<IUserByIdQuery>();
            _mapperMock = new Mock<IUserDbToResponseMapper>();
            _controller = new UserController(_queryMock.Object, _mapperMock.Object);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void ReturnUserFromMapper(int id)
        {
            var dbRecord = new UserDbRecord();
            _queryMock.Setup(x => x.GetUserById(id))
                .Returns(dbRecord);
            var expectedResponse = new UserResponse();
            _mapperMock.Setup(x => x.Map(dbRecord))
                .Returns(expectedResponse);

            var user = _controller.GetById(id);

            user.Should().Be(expectedResponse);
        }
    }

    public class UserController
    {
        private IUserByIdQuery _query;
        private IUserDbToResponseMapper _userDbToResponseMapper;

        public UserController(IUserByIdQuery query, IUserDbToResponseMapper userDbToResponseMapper)
        {
            _userDbToResponseMapper = userDbToResponseMapper;
            _query = query;
        }

        public UserResponse GetById(int id)
        {
            var userDbRecord = _query.GetUserById(id);
            return _userDbToResponseMapper.Map(userDbRecord);
        }
    }
}
