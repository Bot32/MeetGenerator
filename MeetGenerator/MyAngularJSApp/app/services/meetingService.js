'use strict';
app.factory('meetingService', ['$http', 'ngSettings', function ($http, ngSettings) {

    var serviceBase = ngSettings.apiServiceBaseUri;

    var meetingServiceFactory = {};

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

    var _postMeeting = function (meeting) {
        return $http.post(serviceBase + 'api/Meeting/', meeting).then(function (result) {
            return result;
        });
    };

    var _updateMeeting = function (meeting) {
        return $http.put(serviceBase + 'api/Meeting/', meeting).then(function (result) {
            return result;
        });
    };

    var _deleteMeeting = function (id) {
        return $http.delete(serviceBase + 'api/Meeting/' + id + '/').then(function (result) {
            return result;
        });
    };

    meetingServiceFactory.postMeeting = _postMeeting;
    meetingServiceFactory.updateMeeting = _updateMeeting;
    meetingServiceFactory.getMeeting = _getMeeting;
    meetingServiceFactory.deleteMeeting = _deleteMeeting;
    meetingServiceFactory.currentMeeting = _currentMeeting;

    return meetingServiceFactory;
}]);