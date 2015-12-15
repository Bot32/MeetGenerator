'use strict';
app.factory('userService', ['$http', 'ngSettings', function ($http, ngSettings) {

    var serviceBase = ngSettings.apiServiceBaseUri;

    var userServiceFactory = {};

    var _currentUser = {
        email: 'vasya@pupa.com',
        firstName: '',
        id: '',
        lastName: '',
    };

    var _getUserByEmail = function (email) {
        return $http.get(serviceBase + 'api/User/' + email + '/').then(function (result) {
            return result;
        });
    };

    var _getUserById = function (id) {
        return $http.get(serviceBase + 'api/User/' + id + '/').then(function (result) {
            return result;
        });
    };

    var _postUser = function (user) {
        return $http.post(serviceBase + 'api/User/', user).then(function (result) {
            return result;
        });
    };

    var _updateUser = function (user) {
        return $http.put(serviceBase + 'api/User/', user).then(function (result) {
            return result;
        });
    };

    var _deleteUser = function (id) {
        return $http.delete(serviceBase + 'api/User/' + id + '/').then(function (result) {
            return result;
        });
    };

    userServiceFactory.postUser = _postUser;
    userServiceFactory.updateUser = _updateUser;
    userServiceFactory.getUserByEmail = _getUserByEmail;
    userServiceFactory.getUserByID = _getUserById;
    userServiceFactory.deleteUser = _deleteUser;
    userServiceFactory.currentUser = _currentUser;

    return userServiceFactory;
}]);