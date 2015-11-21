using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetGenerator.Repository.SQL.Repositories;
using MeetGenerator.Model.Models;
using MeetGenerator.Tests;


namespace MeetGenerator.Tests
{
    /// <summary>
    /// Сводное описание для PlaceRepositoryTest
    /// </summary>
    [TestClass]
    public class PlaceRepositoryTest
    {

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Create_ShouldCreate()
        {
            //arrange
            var placeRep = new PlaceRepository(Properties.Resources.ConnectionString);
            Place place = TestDataHelper.GeneratePlace();

            //act
            placeRep.CreatePlace(place);
            Place resultPlace = placeRep.GetPlace(place.Id);

            //assert
            TestDataHelper.PrintPlaceInfo(place);
            TestDataHelper.PrintPlaceInfo(resultPlace);
            Assert.IsTrue(TestDataHelper.ComparePlaces(place, resultPlace));
        }

        [TestMethod]
        public void Delete_ShouldDelete()
        {
            //arrange
            var placeRep = new PlaceRepository(Properties.Resources.ConnectionString);
            Place place = TestDataHelper.GeneratePlace();

            //act
            placeRep.CreatePlace(place);
            Place resultPlace = placeRep.GetPlace(place.Id);
            if (resultPlace != null) TestDataHelper.PrintPlaceInfo(place);
            else Console.WriteLine("Place was not create");

            placeRep.DeletePlace(resultPlace.Id);

            resultPlace = placeRep.GetPlace(place.Id);
            if (resultPlace != null) Console.WriteLine("Place was not deleted");
            else Console.WriteLine("Place deleted");

            //assert
            Assert.IsNull(resultPlace);
        }

        [TestMethod]
        public void Update_ShouldUpdate()
        {
            //arrange
            var placeRep = new PlaceRepository(Properties.Resources.ConnectionString);
            Place firstPlace = TestDataHelper.GeneratePlace();
            Place secondPlace = firstPlace;
            secondPlace.Address = "second address";
            secondPlace.Description = "second Descroption";

            //act
            placeRep.CreatePlace(firstPlace);
            placeRep.UpdatePlaceInfo(secondPlace);
            Place resultPlace = placeRep.GetPlace(secondPlace.Id);

            //assert
            TestDataHelper.PrintPlaceInfo(firstPlace);
            TestDataHelper.PrintPlaceInfo(secondPlace);
            TestDataHelper.PrintPlaceInfo(resultPlace);
            Assert.IsTrue(TestDataHelper.ComparePlaces(secondPlace, resultPlace));
        }


        [TestCleanup()]
        public void MyTestCleanup()
        {
            TestDataHelper.ClearDB();
        }
    }
}
