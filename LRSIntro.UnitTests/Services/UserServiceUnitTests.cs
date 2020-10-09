using AutoMapper;
using FluentAssertions;
using LRSIntro.DTO;
using LRSIntro.Mapping;
using LRSIntro.Models;
using LRSIntro.Repositories;
using LRSIntro.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LRSIntro.UnitTests.Services
{
    [TestClass]
    public class UserServiceUnitTests
    {
        #region User Service Variables

        private IMapper _mapper;
        private IUserService _userService;
        private Mock<IRepository<User>> _mockRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IUserTitleRepository> _mockUserTitleRepository;
        private Mock<IUserTypeRepository> _mockUserTypeRepository;

        #endregion

        #region TestInit

        [TestInitialize]
        public void Setup()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
            _mockRepository = new Mock<IRepository<User>>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserTypeRepository = new Mock<IUserTypeRepository>();
            _mockUserTitleRepository = new Mock<IUserTitleRepository>();
            _userService = new UserService(_mockUserRepository.Object, _mapper, _mockUserTypeRepository.Object, _mockUserTitleRepository.Object);
        }

        #endregion

        #region Test Methods

        [TestMethod]
        [TestCategory("Success")]
        public void GetAllUsersSuccessTest()
        {
            // prepare
            List<User> users = new List<User>() {
                new User{ Id = 1, Name = "test", Surname = "test", UserTitleId = 1, UserTypeId = 1 }
            };
            _ = _mockUserRepository.Setup(x => x.GetAllUsersWithDetailsAsync()).ReturnsAsync(users);

            //execute
            var result = _userService.GetAllUsersWithDetailsAsync().Result;

            //assert
            int totalUsers = users.Count();
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), totalUsers);
        }

        [TestMethod]
        [TestCategory("Success")]
        public void GetUserByIdSuccessTest()
        {
            //prepare
            int id = 1;
            var user = new User { Id = 1, Name = "test", Surname = "test", UserTitleId = 1, UserTypeId = 1 };
            _ = _mockUserRepository.Setup(x => x.GetUserByIdAsync(id)).ReturnsAsync(user);

            //execute
            var result = _userService.GetUserByIdAsync(id).Result;

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public void GetUserByIdFailTest()
        {
            //prepare
            int id = -1;

            //execute
            var ex = Assert.ThrowsExceptionAsync<ArgumentException>(
              () => _userService.GetUserByIdAsync(id));

            //assert
            _ = ex.Result.Should().BeOfType(typeof(ArgumentException));
        }

        [TestMethod]
        [TestCategory("Success")]
        public void AddUserSuccess()
        {
            //prepare
            var data = new UserAddOrUpdateDTO
            {
                Name = "test",
                UserTitleId = 1,
                UserTypeId = 1,
                IsActive = true,
                EmailAddress = "test@test.com"

            };

            var newUser = new User
            {
                Id = 1,
                Name = "test",
                UserTitleId = 1,
                UserTypeId = 1,
                IsActive = true,
                EmailAddress = "test@test.com"
            };

            _ = _mockUserRepository.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(newUser);
            _ = _mockUserRepository.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(newUser);


            //execute
            var res = _userService.AddUserAsync(data).Result;

            //assert
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(UserDTO));
        }

        [TestMethod]
        [TestCategory("Fail")]
        public void AddUserFail()
        {
            UserAddOrUpdateDTO data = null;

            var ex = Assert.ThrowsExceptionAsync<ArgumentException>(
              () => _userService.AddUserAsync(data));

            _ = ex.Result.Should().BeOfType(typeof(ArgumentException));
        }
        #endregion
    }
}
