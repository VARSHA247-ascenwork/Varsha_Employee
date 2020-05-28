var NewEmployeeapp = angular.module('NewEmployeeapp', ['NewEmployeeServiceModule'])

NewEmployeeapp.controller('NewRequestController', function ($scope, $http, NewEmployeeService) {




    $scope.Save = function () {

        var data = {
            'FirstName': $scope.ngtxtfirstname,
            'MiddleName': $scope.ngtxtMiddlename,
            'LastName': $scope.ngtxtlastname,
            
        }

        NewEmployeeService.SaveRequest(data).then(function (response) {
            console.log(response);
            if (response.status == 200) {
                
                Pageredirect("/Approve");
            } else {

                alert("Error");
            }

        });

    }




});


var jsonheaders = { 'headers': { 'accept': 'application/json;odata=verbose' } };

NewEmployeeapp.service('NewEmployeeService', function ($http, CommonAppUtilityService) {


    this.SaveRequest = function (option, doc) {


        return CommonAppUtilityService.CreateItem("/NewEmployee/SaveInfo", option);
    }

});
