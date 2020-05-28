var NewEmployeeServiceModule = angular.module('NewEmployeeServiceModule', ['CommonAppUtility']);

var jsonheaders = { 'headers': { 'accept': 'application/json;odata=verbose' } };

NewEmployeeServiceModule.service('NewEmployeeService', function ($http, CommonAppUtilityService) {


    this.SaveRequest = function (option,doc) {
        

        return CommonAppUtilityService.CreateItem("/NewEmployee/SaveInfo", option);
    }

});

