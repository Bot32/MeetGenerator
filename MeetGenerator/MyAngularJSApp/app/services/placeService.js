'use strict';
app.factory('placeService', ['$http', 'ngSettings', function ($http, ngSettings) {

    var serviceBase = ngSettings.apiServiceBaseUri;

    var placeServiceFactory = {};

    var _currentPlace = {
        id: '',
        address: 'Kolotushkin str., Pushkin h.',
        description: 'This is Pushkin house.'
    };

    var _getPlace = function (id) {
        return $http.get(serviceBase + 'api/Place/' + id + '/').then(function (result) {
            return result;
        });
    };

    var _postPlace = function (place) {
        return $http.post(serviceBase + 'api/Place/', place).then(function (result) {
            return result;
        });
    };

    var _updatePlace = function (place) {
        return $http.put(serviceBase + 'api/Place/', place).then(function (result) {
            return result;
        });
    };

    var _deletePlace = function (id) {
        return $http.delete(serviceBase + 'api/Place/' + id + '/').then(function (result) {
            return result;
        });
    };

    placeServiceFactory.postPlace = _postPlace;
    placeServiceFactory.updatePlace = _updatePlace;
    placeServiceFactory.getPlace = _getPlace;
    placeServiceFactory.deletePlace = _deletePlace;
    placeServiceFactory.currentPlace = _currentPlace;

    return placeServiceFactory;
}]);