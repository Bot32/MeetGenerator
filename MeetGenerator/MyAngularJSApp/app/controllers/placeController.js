'use strict';
app.controller('placeController', ['$scope', 'placeService', 'meetingService',
    function ($scope, placeService, meetingService) {

    $scope.place = placeService.currentPlace;

    this.getPlace = function () {
        placeService.getPlace($scope.place.id).then(function (result) {
            $scope.place = result.data;
            placeService.currentPlace = result.data;
            console.log(result);
        }, function (error) {
            alert(error.data.message);
        });
    }

    this.postPlace = function () {
        placeService.postPlace($scope.place).then(function (result) {
            $scope.place = result.data;
            placeService.currentPlace = result.data;
            console.log(result);
        }, function (error) {
            alert(error.data.message);
        });
    }

    this.updatePlace = function () {
        placeService.updatePlace($scope.place).then(function (result) {
            $scope.place = result.data;
            placeService.currentPlace = result.data;
            console.log(result);
        }, function (error) {
            alert(error.data.message);
        });
    }

    this.deletePlace = function () {
        placeService.deletePlace($scope.place.id).then(function (result) {
            $scope.place = {};
            placeService.currentPlace = {};
            console.log(result);
        }, function (error) {
            alert(error.data.message);
        });
    }

    this.toMeeting = function () {
        meetingService.currentMeeting.place = jQuery.extend({}, $scope.place);
    }

    this.clearPlaceInfo = function () {
        $scope.place = {};
        placeService.currentPlace = {};
    }
}]);