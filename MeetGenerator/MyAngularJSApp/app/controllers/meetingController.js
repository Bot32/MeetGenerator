'use strict';
app.controller('meetingController', ['$scope', 'meetingService', '$rootScope',
    function ($scope, meetingService, $rootScope) {

    $scope.meeting = meetingService.currentMeeting;
    $scope.user = $scope.meeting.owner;
    $scope.place = $scope.meeting.place;

    this.getMeeting = function () {
        meetingService.getMeeting($scope.meeting.id).then(function (result) {       
            $scope.updateMeetingInfo(result.data);
            console.log(result);
        }, function (error) {
            alert(error.data.message);
        });
    }

    this.postMeeting = function () {
        meetingService.postMeeting($scope.meeting).then(function (result) {
            $scope.updateMeetingInfo(result.data);
            console.log(result);
        }, function (error) {
            alert(error.data.message);
        });
    }

    this.updateMeeting = function () {
        meetingService.updateMeeting($scope.meeting).then(function (result) {
            $scope.updateMeetingInfo(result.data);
            console.log(result);
        }, function (error) {
            alert(error.data.message);
        });
    }

    this.deleteMeeting = function (meeting) {
        meetingService.deleteMeeting($scope.meeting.id).then(function (result) {
            meetingService.currentMeeting = {};
            $scope.meeting = {};
            console.log(result);
        }, function (error) {
            alert(error.data.message);
        });
    }

    $scope.updateMeetingInfo = function (newMeeting) {
        meetingService.currentMeeting = newMeeting;
        meetingService.currentMeeting.date = new Date(newMeeting.date).toISOString().split('T')[0];
        $scope.meeting = meetingService.currentMeeting;
        $scope.user = $scope.meeting.owner;
        $scope.place = $scope.meeting.place;
    }

    this.clearMeetingInfo = function () {
        meetingService.currentMeeting = {};
        $scope.meeting = {};
        $scope.user = {};
        $scope.place = {};
    }

    //$rootScope.$on('$routeChangeStart', function (event, current, previous, reject) {
    //    $scope.meeting = meetingService.currentMeeting;
    //    console.log(meetingService.currentMeeting);
    //});
}]);