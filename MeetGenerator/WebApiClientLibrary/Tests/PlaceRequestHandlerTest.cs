using MeetGenerator.Model.Models;
using MeetGenerator.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiClientLibrary.RequestHadlers;

namespace WebApiClientLibrary.Tests
{
    [TestClass]
    public class PlaceRequestHandlerTest
    { 
        string hostAddress = Properties.Resources.host_address;

        [TestMethod]
        public async Task Create_ShouldReturnCreate()
        {
            //arrange 
            var placeHandler = new PlaceRequestHandler(hostAddress);
            Place place = TestDataHelper.GeneratePlace();

            //act
            HttpResponseMessage response = await placeHandler.Create(place);

            //assert
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Get_ShouldReturnOk()
        {
            //arrange 
            var placeHandler = new PlaceRequestHandler(hostAddress);
            Place place = TestDataHelper.GeneratePlace();

            //act
            HttpResponseMessage response1 = await placeHandler.Create(place);
            place = await response1.Content.ReadAsAsync<Place>();
            HttpResponseMessage resultResponse = await placeHandler.Get(place.Id);

            //assert
            Console.WriteLine(resultResponse.StatusCode);
            Assert.IsTrue(resultResponse.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Update_ShouldReturnCreated()
        {
            //arrange 
            var placeHandler = new PlaceRequestHandler(hostAddress);
            Place place = TestDataHelper.GeneratePlace();

            //act
            HttpResponseMessage response = await placeHandler.Create(place);
            Place resultPlace = await response.Content.ReadAsAsync<Place>();

            HttpResponseMessage resultResponse = await placeHandler.Update(resultPlace);

            //assert
            Assert.IsTrue(resultResponse.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnOk()
        {
            //arrange 
            var placeHandler = new PlaceRequestHandler(hostAddress);
            Place place = TestDataHelper.GeneratePlace();

            //act
            HttpResponseMessage response = await placeHandler.Create(place);
            Place resultPlace = await response.Content.ReadAsAsync<Place>();

            HttpResponseMessage resultResponse = await placeHandler.Delete(resultPlace.Id);

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
