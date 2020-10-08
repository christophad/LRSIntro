using AutoMapper;
using FluentAssertions;
using LRSIntro.DTO;
using LRSIntro.Mapping;
using LRSIntro.Models;
using LRSIntro.Repositories;
using LRSIntro.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LRSIntro.UnitTests.Services
{
    [TestClass]
    public class UserServiceUnitTests
    {
        #region User Service Variables


        private IMapper _mapper;
        private IUserService _userService;
        private IUserRepository _userRepository;
        private IUserTitleRepository _userTitleRepository;
        private IUserTypeRepository _userTypeRepository;
        private LRSIntroContext _lRSIntroContext;
        private DbContextOptions<LRSIntroContext> _dBContextOptions;

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
            _dBContextOptions = new DbContextOptionsBuilder<LRSIntroContext>().UseInMemoryDatabase("LRSIntro").Options;

            _lRSIntroContext = new LRSIntroContext(_dBContextOptions);

            _lRSIntroContext.UserTitle.Add(new UserTitle
            {
                Id = 1,
                Description = "Mr"
            });

            _lRSIntroContext.UserType.Add(new UserType
            {
                Id = 1,
                Description = "Developer",
                Code = "Dev"
            });

            _lRSIntroContext.User.Add(new User
            {
                Id = 1,
                Name = "Test Name",
                Surname = "Test surname",
                BirthDate = DateTime.Now,
                EmailAddress = "test@test.gr",
                UserTypeId = 1,
                UserTitleId = 1,
                IsActive = true
            });
            _lRSIntroContext.SaveChanges();

            _userRepository = new UserRepository(_lRSIntroContext);
            _userTypeRepository = new UserTypeRepository(_lRSIntroContext);
            _userTitleRepository = new UserTitleRepository(_lRSIntroContext);
            _userService = new UserService(_userRepository, _mapper, _userTypeRepository, _userTitleRepository, null);
        }

        #endregion

        #region Test Methods
        [TestMethod]
        [TestCategory("Success")]
        public void GetAllUsersSuccessTest()
        {
            var result = _userService.GetAllUsersWithDetailsAsync().Result;
            int totalUsers = _lRSIntroContext.User.Count();
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), totalUsers);
        }

        [TestMethod]
        [TestCategory("Success")]
        public void GetUserByIdSuccessTest()
        {
            int id = 1;
            var result = _userService.GetUserByIdAsync(id).Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public void GetUserByIdFailTest()
        {
            int id = -1;

            var ex = Assert.ThrowsExceptionAsync<ArgumentException>(
              () => _userService.GetUserByIdAsync(id));

            _ = ex.Result.Should().BeOfType(typeof(ArgumentException));
        }

        [TestMethod]
        [TestCategory("Success")]
        public void AddUserSuccess()
        {
            var data = new UserAddOrUpdateDTO
            {
                Name = "test",
                UserTitle = 1,
                UserType = 1,
                IsActive = true,
                EmailAddress = "test@test.com"

            };

            var res = _userService.AddUserAsync(data).Result;
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
