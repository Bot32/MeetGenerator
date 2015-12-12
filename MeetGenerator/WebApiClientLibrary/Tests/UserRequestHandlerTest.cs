using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClientLibrary.RequestHadlers;
using MeetGenerator.Model.Models;
using System.Net.Http;
using MeetGenerator.Tests;


namespace WebApiClientLibrary.Tests
{
    [TestClass]
    public class UserRequestHandlerTest
    {
        string hostAddress = Properties.Resources.host_address;

        [TestMethod]
        public async Task Create_ShouldReturnCreate()
        {
            //arrange 
            var userHandler = new UserRequestHandler(hostAddress);
            User user = TestDataHelper.GenerateUser();

            //act
            HttpResponseMessage response = await userHandler.Create(user);

            //assert
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Get_ByEmail_ShouldReturnOk()
        {
            //arrange 
            var userHandler = new UserRequestHandler(hostAddress);
            User user = TestDataHelper.GenerateUser();

            //act
            await userHandler.Create(user);
            HttpResponseMessage response = await userHandler.Get(user.Email);

            //assert
            Console.WriteLine(response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Get_ById_ShouldReturnOk()
        {
            //arrange 
            var userHandler = new UserRequestHandler(hostAddress);
            User user = TestDataHelper.GenerateUser();

            //act
            await userHandler.Create(user);

            HttpResponseMessage response = await userHandler.Get(user.Email);
            User resultUser = await response.Content.ReadAsAsync<User>();

            HttpResponseMessage resultResponse = await userHandler.Get(resultUser.Id.ToString());

            //assert
            Assert.IsTrue(resultResponse.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Get_ByEmail_ShouldReturnSameUser()
        {
            //arrange 
            var userHandler = new UserRequestHandler(hostAddress);
            User user = TestDataHelper.GenerateUser();

            //act
            await userHandler.Create(user);

            HttpResponseMessage response = await userHandler.Get(user.Email);
            User resultUser = await response.Content.ReadAsAsync<User>();

            //assert
            Assert.IsTrue(user.Email == resultUser.Email);
            Assert.IsTrue(user.FirstName == resultUser.FirstName);
            Assert.IsTrue(user.LastName == resultUser.LastName);
        }

        [TestMethod]
        public async Task Update_ShouldReturnCreated()
        {
            //arrange 
            var userHandler = new UserRequestHandler(hostAddress);
            User user = TestDataHelper.GenerateUser();

            //act
            HttpResponseMessage response = await userHandler.Create(user);
            User resultUser = await response.Content.ReadAsAsync<User>();

            HttpResponseMessage resultResponse = await userHandler.Update(resultUser);

            //assert
            Console.WriteLine(resultResponse.StatusCode);
            Assert.IsTrue(resultResponse.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnOk()
        {
            //arrange 
            var userHandler = new UserRequestHandler(hostAddress);
            User user = TestDataHelper.GenerateUser();

            //act
            HttpResponseMessage response = await userHandler.Create(user);
            User resultUser = await response.Content.ReadAsAsync<User>();

            HttpResponseMessage resultResponse = await userHandler.Delete(resultUser.Id);

            //assert
            Assert.IsTrue(resultResponse.IsSuccessStatusCode);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            TestDataHelper.ClearDB();
        }

    }
}
