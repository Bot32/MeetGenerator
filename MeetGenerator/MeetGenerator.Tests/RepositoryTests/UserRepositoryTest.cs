using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetGenerator.Repository.SQL;
using MeetGenerator.Model.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using MeetGenerator.Repository.SQL.Repositories;

namespace MeetGenerator.Tests
{
    [TestClass]
    public class UserRepositoryTest
    {

        [TestMethod]
        public void Create_ShouldCreateUser()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = TestDataHelper.GenerateUser();
            user.FirstName = "TestUser";

            //act
            userRepository.CreateUser(user);

            //asserts
            var resultUser = userRepository.GetUser(user.Id);
            TestDataHelper.PrintUserInfo(resultUser);
            Assert.IsTrue(TestDataHelper.CompareUsers(user, resultUser));
        }

        [TestMethod]
        public void Get_ByEmail_ShouldReturnUser()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = TestDataHelper.GenerateUser();

            //act
            userRepository.CreateUser(user);
            var resultUser = userRepository.GetUser(user.Email);

            //asserts
            TestDataHelper.PrintUserInfo(resultUser);
            Assert.IsTrue(TestDataHelper.CompareUsers(user, resultUser));
        }

        [TestMethod]
        public void Get_NonExistUserByEmail_ShoulReturnNull()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = TestDataHelper.GenerateUser();

            //act
            var resultUser = userRepository.GetUser(user.Email);

            //asserts
            Assert.IsNull(resultUser);
        }

        [TestMethod]
        public void Get_NotExistUserById_ShouldReturnNull()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = TestDataHelper.GenerateUser();

            //act
            var resultUser = userRepository.GetUser(user.Id);

            //asserts
            Assert.IsNull(resultUser);
        }

        [TestMethod]
        public void IsEmailBusy_WithBusyEmail_ShouldReturnBusy()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = TestDataHelper.GenerateUser();

            //act  
            userRepository.CreateUser(user);
            User resultUser = userRepository.GetUser(user.Email);

            //assert
            Assert.IsNotNull(resultUser);
        }

        [TestMethod]
        public void IsEmailBusy_ShoildReturnNotBusy()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = TestDataHelper.GenerateUser();

            //act  
            User resultUser = userRepository.GetUser(user.Email);

            //assert
            Assert.IsNull(resultUser);
        }

        [TestMethod]
        public void IsUserExist_ShouldExist()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = TestDataHelper.GenerateUser();

            //act
            userRepository.CreateUser(user);

            //asserts
            User result = userRepository.GetUser(user.Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete_ShouldDeleteUser()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = TestDataHelper.GenerateUser();

            //act
            userRepository.CreateUser(user);
            User userBeforeDelete = userRepository.GetUser(user.Id);
            if (userBeforeDelete == null)
            {
                Console.WriteLine("User not exist!");
            }
            else
            {
                Console.WriteLine("User exist.");
            }
            userRepository.DeleteUser(user.Id);

            //assert
            User userAfterDelete = userRepository.GetUser(user.Id);

            if (userAfterDelete == null)
            {
                Console.WriteLine("User deleted.");
            }
            else
            {
                Console.WriteLine("User not deleted!");
            }

            Assert.IsTrue(!(userBeforeDelete == null) & (userAfterDelete == null));
        }

        [TestMethod]
        public void Update_ShouldUpdateUser()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            var firstUser = TestDataHelper.GenerateUser();

            var secondUser = new User
            {
                Id = firstUser.Id,
                Email = "second" + (DateTime.Now.Millisecond * new Random().Next(10000)) + "@test.com",
                FirstName = "secondUserFirstName",
                LastName = "secondUserLastName"
            };

            //act
            userRepository.CreateUser(firstUser);
            userRepository.UpdateUser(secondUser);

            //asserts
            var resultUser = userRepository.GetUser(firstUser.Id);
            TestDataHelper.PrintUserInfo(firstUser);
            TestDataHelper.PrintUserInfo(secondUser);
            TestDataHelper.PrintUserInfo(resultUser);
            Assert.IsTrue(TestDataHelper.CompareUsers(resultUser, secondUser));
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            TestDataHelper.ClearDB();
        }
    }
}
