﻿'use strict';
app.controller('userController', ['$scope', 'userService', 'meetingService',
    function ($scope, userService, meetingService) {
    $scope.user = userService.currentUser;

    this.getByEmail = function () {
        userService.getUserByEmail($scope.user.email).then(function (result) {
            $scope.user = result.data;
            userService.currentUser = result.data;
            console.log(result);
        }, function (error) {
            alert(error.data.message);
        });
    }

    this.getById = function () {
        userService.getUserByEmail($scope.user.id).then(function (result) {
            $scope.user = result.data;
            userService.currentUser = result.data;
            console.log(result);
        }, function (error) {
            alert(error.data.message);
        });
    }

    this.postUser = function () {
        userService.postUser($scope.user).then(function (result) {
            $scope.user = result.data;
            userService.currentUser = result.data;
            console.log(result);
        }, function (error) {
            alert(error.data.message);
        });
    }

    this.updateUser = function () {
        userService.updateUser($scope.user).then(function (result) {
            $scope.user = result.data;
            userService.currentUser = result.data;
            console.log(result);
        }, function (error) {
            alert(error.data.message);
        });
    }

    this.deleteUser = function () {
        userService.deleteUser($scope.user.id).then(function (result) {
            $scope.user = {};
            userService.currentUser = {};
            console.log(result);
        }, function (error) {
            alert(error.data.message);
        });
    }

    this.toMeeting = function () {
        meetingService.currentMeeting.owner = jQuery.extend({}, $scope.user);
    }

    this.clearUserInfo = function () {
        $scope.user = {};
        userService.currentUser = {};
    }
}]);