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
        static List<User> testUsers = new List<User>();

        [TestMethod]
        public void CreateUserTest()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = GenerateUser();
            user.FirstName = "TestUser";

            //act
            userRepository.Create(user);

            //asserts
            var resultUser = userRepository.Get(user.Id);
            Assert.IsTrue(CompareUsers(user, resultUser));
        }

        [TestMethod]
        public void BusyEmailTest()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = GenerateUser();

            //act  
            userRepository.Create(user);
            bool result = userRepository.IsEmailBusy(user.Email);

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NotBusyEmailTest()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);

            //act
            bool result = userRepository.IsEmailBusy(DateTime.Now.Millisecond + "@test.com");

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UserExistTest()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = GenerateUser();

            //act
            userRepository.Create(user);

            //asserts
            bool result = userRepository.IsUserExist(user.Id);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            User user = GenerateUser();

            //act
            userRepository.Create(user);

            //asserts
            bool userExist = userRepository.IsUserExist(user.Id);
            if (userExist)
            {
                Console.WriteLine("User exist.");
            }
            else
            {
                Console.WriteLine("User not exist!");
            }
            userRepository.Delete(user.Id);

            //assert
            bool userDeleted = userRepository.IsUserExist(user.Id);

            if (userDeleted)
            {
                Console.WriteLine("User not deleted!");
            }
            else
            {
                Console.WriteLine("User deleted.");
            }

            Assert.IsTrue(userExist & !userDeleted);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            //arrange
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            var firstUser = GenerateUser();

            var secondUser = new User
            {
                Id = firstUser.Id,
                Email = "secondUser" + DateTime.Now.Millisecond + "@test.com",
                FirstName = "secondUserFirstName",
                LastName = "secondUserLastName"
            };

            //act
            userRepository.Create(firstUser);
            userRepository.Update(secondUser);

            //asserts
            var resultUser = userRepository.Get(firstUser.Id);
            Assert.IsTrue(CompareUsers(resultUser, secondUser));
        }

        User GenerateUser()
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test" + (DateTime.Now.Millisecond * new Random().Next(10000)) + "@test.com",
                FirstName = "name",
                LastName = "lastname"
            };
            testUsers.Add(user);
            return user;
        }

        bool CompareUsers(User first, User second)
        {
            return first.Id.Equals(second.Id) &
                   first.Email.Equals(second.Email) &
                   first.FirstName.Equals(second.FirstName) &
                   first.LastName.Equals(second.LastName);
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            var userRepository = new UserRepository(Properties.Resources.ConnectionString);
            foreach (User user in testUsers)
            {
                userRepository.Delete(user.Id);
            }
        }
    }
}
