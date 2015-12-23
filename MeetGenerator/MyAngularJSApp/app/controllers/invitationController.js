'use strict';
app.controller('invitationController', ['$scope', 'invitationService',
    function ($scope, invitationService) {

        $scope.meeting = invitationService.currentMeeting;
        $scope.selectedUser = {};

        this.postInvitation = function () {
            invitationService.postInvitation($scope.buildInvitationObject($scope.meeting.id, $scope.selectedUser.id))
                .then(function (result) {
                    console.log(result);
                    $scope.getMeeting();
                }, function (error) {
                    alert(error.data.message);
                });
        }

        this.deleteInvitation = function () {
            invitationService.deleteInvitation($scope.buildInvitationObject($scope.meeting.id, $scope.selectedUser.id))
                .then(function (result) {
                    console.log(result);
                    $scope.getMeeting();
                }, function (error) {
                    alert(error.data.message);
                });
        }

        $scope.getMeeting = function () {
            invitationService.getMeeting($scope.meeting.id).then(function (result) {
                $scope.updateMeetingInfo(result.data);
                console.log(result);
            }, function (error) {
                alert(error.data.message);
            });
        }

        //this.postInvitationAndUpdate = function () {
        //    this.postInvitation();
        //    this.getMeeting();
        //}

        //this.deleteInvitationAndUpdate = function () {
        //    this.deleteInvitation();
        //    this.getMeeting();
        //}

        $scope.updateMeetingInfo = function (newMeeting) {
            invitationService.currentMeeting = newMeeting;
            $scope.meeting = invitationService.currentMeeting;
        }

        this.clearMeetingInfo = function () {
            invitationService.currentMeeting = {};
            $scope.meeting = {};
        }

        this.selectUser = function (user) {
            $scope.selectedUser = user;
        }

        $scope.buildInvitationObject = function (meetingId, userId) {
            var invitation = {
                meetingId: meetingId,
                userId: userId
            };
            return invitation;
        }
    }]);