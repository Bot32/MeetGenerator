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
        public void CreateUserTest()
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
        public void GetUserByEmailTest()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = TestDataHelper.GenerateUser();

            //act
            userRepository.CreateUser(user);

            //asserts
            var resultUser = userRepository.GetUser(user.Email);
            TestDataHelper.PrintUserInfo(resultUser);
            Assert.IsTrue(TestDataHelper.CompareUsers(user, resultUser));
        }

        [TestMethod]
        public void GetNonExistUserByEmailTest()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = TestDataHelper.GenerateUser();

            //act

            //asserts
            var resultUser = userRepository.GetUser(user.Email);
            Assert.IsNull(resultUser);
        }

        [TestMethod]
        public void GetNotExistUserByIdTest()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = TestDataHelper.GenerateUser();

            //act

            //asserts
            var resultUser = userRepository.GetUser(user.Id);
            Assert.IsNull(resultUser);
        }

        [TestMethod]
        public void BusyEmailTest()
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
        public void NotBusyEmailTest()
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
        public void UserExistTest()
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
        public void DeleteUserTest()
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
        public void UpdateUserTest()
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

        //[ClassCleanup()]
        //public static void ClassCleanup()
        //{
        //    var userRepository = new UserRepository(Properties.Resources.ConnectionString);
        //    foreach (User user in testUsers)
        //    {
        //        userRepository.DeleteUser(user.Id);
        //    }
        //}
    }
}
