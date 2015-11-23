using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetGenerator.Model.Models;
using System.Text.RegularExpressions;
using MeetGenerator.DataValidators;

namespace MeetGenerator.Tests
{
    /// <summary>
    /// Сводное описание для UserDataValidatorTest
    /// </summary>
    [TestClass]
    public class UserDataValidatorTest
    {

        [TestMethod]
        public void ValidUserFirstNameTest()
        {   
            //arrange
            String firstName = "Вася";
            List<string> ErrorList = new List<string>();
            bool result;

            //act
            result = UserDataValidator.IsValidUserFirstName(firstName, ErrorList);
            Console.WriteLine(ErrorList);

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InvalidUserFirstNameTest()
        {
            //arrange
            String firstName = null;
            List<string> ErrorList = new List<string>(); ;
            bool result;

            //act
            result = UserDataValidator.IsValidUserFirstName(firstName, ErrorList);
            Console.WriteLine(ErrorList);

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidEmailRegexpTest()
        {
            //arrange
            bool result = true;
            List<string> ErrorList = new List<string>();
            List<String> emails = new List<String>();
            emails.Add("joe@home.org");
            emails.Add("joe@joebob.name");
            emails.Add("joe&bob@bob.com");
            emails.Add("~joe@bob.com");
            emails.Add("joe$@bob.com");
            emails.Add("joe+bob@bob.com");
            emails.Add("joe@home.com");
            emails.Add("joe.bob@home.com");
            emails.Add("joe@his.home.com");
            emails.Add("a@abc.org");
            emails.Add("a@abc-xyz.org");
            emails.Add("secondUser411@test.com");

            //act
            foreach (String email in emails)
            {
                result = result & UserDataValidator.IsValidEmail(email, ErrorList);
            }

            foreach (String email in ErrorList)
            {
                Console.WriteLine(email);
            }

            //asserts
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InvalidEmailRegexpTest()
        {
            //arrange
            bool result = false;
            List<string> ErrorList = new List<string>();
            List<String> emails = new List<String>();
            emails.Add("joe"); // should fail
            emails.Add("joe@home"); // should fail
            emails.Add("a@b.c"); // should fail because .c is only one character but must be 2-4 characters
            emails.Add("joe-bob[at]home.com"); // should fail because [at] is not valid
            emails.Add("joe.@bob.com"); // should fail because there is a dot at the end of the local-part
            emails.Add(".joe@bob.com"); // should fail because there is a dot at the beginning of the local-part
            emails.Add("john..doe@bob.com"); // should fail because there are two dots in the local-part
            emails.Add("john.doe@bob..com"); // should fail because there are two dots in the domain
            emails.Add("joe<>bob@bob.com"); // should fail because <> are not valid
            emails.Add("joe@his.home.com."); // should fail because it can't end with a period
            emails.Add("john.doe@bob-.com"); // should fail because there is a dash at the start of a domain part
            emails.Add("john.doe@-bob.com"); // should fail because there is a dash at the end of a domain part
            emails.Add("a@10.1.100.1a");  // Should fail because of the extra character
            emails.Add("joe<>bob@bob.com\n"); // should fail because it end with \n
            emails.Add("joe<>bob@bob.com\r"); // should fail because it ends with \r

            //act
            foreach (String email in emails)
            {
                result = result & UserDataValidator.IsValidEmail(email, ErrorList);
            }
            Console.WriteLine(ErrorList);

            foreach (String email in ErrorList)
            {
                Console.WriteLine(email);
            }

            //asserts
            //Assert.IsFalse(result);
        }

        [TestMethod]
        public void CompleteValidUserObjectTest()
        {
            //arrange
            User user = new User
            {
                Email = "test" + new Random(DateTime.Now.Millisecond).Next() + "@test.com",
                FirstName = "Vsiliy",
                LastName = "Lastname",
                Id = Guid.NewGuid()
            };
            List<string> result;

            //act
            result = UserDataValidator.IsCompleteValidUserObject(user);

            //assert
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void CompleteInvalidUserObjectTest()
        {
            //arrange
            User user = new User
            {
                Email = @"/test\" + new Random(DateTime.Now.Millisecond).Next() + "@test.c0m",
                FirstName = null,
                LastName = null,
                Id = Guid.NewGuid()
            };
            List<string> result;

            //act
            result = UserDataValidator.IsCompleteValidUserObject(user);

            //assert
            Assert.IsFalse(result.Count == 0);
        }
    }
}
