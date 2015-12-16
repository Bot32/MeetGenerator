'use strict';
app.factory('invitationService', ['$http', 'ngSettings', function ($http, ngSettings) {

    var serviceBase = ngSettings.apiServiceBaseUri;

    var invitationServiceFactory = {};

    var _currentMeeting = {
        id: '',
        title: 'Best meeting',
        description: '',
        date: '2016-01-01',
        owner: {},
        place: {},
        invitedPeople: {},
    };

    var _getMeeting = function (id) {
        return $http.get(serviceBase + 'api/Meeting/' + id + '/').then(function (result) {
            return result;
        });
    };

    var _postInvitation = function (invitation) {
        return $http.post(serviceBase + 'api/Invitation/', invitation).then(function (result) {
            return result;
        });
    };

    var _deleteInvitation = function (invitation) {
        return $http.delete(serviceBase + 'api/Invitation/' + invitation.meetingId + '/' + invitation.userId + '/').then(function (result) {
            return result;
        });
    };

    invitationServiceFactory.getMeeting = _getMeeting;
    invitationServiceFactory.postInvitation = _postInvitation;
    invitationServiceFactory.deleteInvitation = _deleteInvitation;
    invitationServiceFactory.currentMeeting = _currentMeeting;

    return invitationServiceFactory;
}]);