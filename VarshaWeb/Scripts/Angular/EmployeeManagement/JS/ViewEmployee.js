var ViewRequestapp = angular.module('ViewRequestapp', ['ViewRequestServiceModule'])

ViewRequestapp.controller('ViewRequestController', function ($scope, $http, ViewRequestService) {




    $scope.Cancal = function () {
        Pageredirect("/Index");
    }
});