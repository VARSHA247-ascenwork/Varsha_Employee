var ViewEmployeeServiceModule = angular.module('ViewEmployeeServiceModule', ['CommonAppUtility']);

var jsonheaders = { 'headers': { 'accept': 'application/json;odata=verbose' } };

ViewEmployeeServiceModule.service('ViewEmployeeService', function ($http, CommonAppUtilityService) {


    this.UpdateRequest = function (option) {

        return CommonAppUtilityService.CreateItem("/Approve/UpdateComment", option);
    }



});