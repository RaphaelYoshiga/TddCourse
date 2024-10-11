using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;

namespace DevTdd.Course.UnitTests.Day5
{
    public class UserControllerAcceptanceTest
    {
        private UserController _controller;
        private readonly Mock<IUserByIdQuery> _queryMock;

        public UserControllerAcceptanceTest()
        {
            _queryMock = new Mock<IUserByIdQuery>();
            _controller = new UserController(_queryMock.Object, new UserDbToResponseMapper());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void ReturnUserFromMapper(int id)
        {
            var dbRecord = new UserDbRecord()
            {
                ConfidentialProperty = "CONFIDENTIAL",
                Id = id,
                Name = "John"
            };
            _queryMock.Setup(x => x.GetUserById(id))
                .Returns(dbRecord);

            var user = _controller.GetById(id);

            user.Id.Should().Be(id);
            user.Name.Should().Be("John");
        }
    }
}