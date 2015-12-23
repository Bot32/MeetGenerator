
var app = angular.module('AngularMeetGenApp', ['ngRoute', 'LocalStorageModule', ]);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController as loginCtrl",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController as signupCtrl",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/users", {
        controller: "userController as userCtrl",
        templateUrl: "/app/views/users.html"
    });

    $routeProvider.when("/places", {
        controller: "placeController as placeCtrl",
        templateUrl: "/app/views/places.html"
    });

    $routeProvider.when("/meetings", {
        controller: "meetingController as meetingCtrl",
        templateUrl: "/app/views/meetings.html"
    });

    $routeProvider.when("/invitations", {
        controller: "invitationController as invitationCtrl",
        templateUrl: "/app/views/invitations.html"
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "/app/views/associate.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

//var serviceBase = 'http://localhost:61689/';
var serviceBase = 'http://meetgen.azurewebsites.net/';
app.constant('ngSettings', {
    apiServiceBaseUri: serviceBase,
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);
