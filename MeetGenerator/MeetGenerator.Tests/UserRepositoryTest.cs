using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetGenerator.Repository.SQL;
using MeetGenerator.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using MeetGenerator.Repository.SQL.DataValidators;

namespace MeetGenerator.Tests
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void CreateUserTest()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            var user = new User
            {
                Email = "test" + new Random(DateTime.Now.Millisecond).Next() + "@test.com",
                FirstName = "name",
                LastName = "lastname",
                Id = Guid.NewGuid()
            };
            //act
            userRepository.Create(user);
            //asserts
            var resultUser = userRepository.Get(user.Id);
            Assert.AreEqual(user.Email, resultUser.Email);
        }
    }
}
