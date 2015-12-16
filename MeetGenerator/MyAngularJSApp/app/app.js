
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

    $routeProvider.otherwise({ redirectTo: "/home" });
});

var serviceBase = 'http://meetgen.azurewebsites.net/';
app.constant('ngSettings', {
    apiServiceBaseUri: serviceBase,
});

app.run(function ($rootScope) {
    $rootScope.$on('$routeChangeStart', function (event, current, previous, reject) {
        $rootScope.updateMeetingInfo;
    });
});
