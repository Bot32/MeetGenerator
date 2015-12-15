
var app = angular.module('AngularMeetGenApp', ['ngRoute', 'LocalStorageModule', ]);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/users", {
        controller: "userController as userCtrl",
        templateUrl: "/app/views/users.html"
    });

    $routeProvider.when("/places", {
        templateUrl: "/app/views/places.html"
    });

    $routeProvider.when("/meetings", {
        templateUrl: "/app/views/meetings.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

var serviceBase = 'http://meetgen.azurewebsites.net/';
app.constant('ngSettings', {
    apiServiceBaseUri: serviceBase,
});
