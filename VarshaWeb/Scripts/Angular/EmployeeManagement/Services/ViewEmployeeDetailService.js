var ViewRequestServiceModule = angular.module('ViewRequestServiceModule', ['CommonAppUtility']);

var jsonheaders = { 'headers': { 'accept': 'application/json;odata=verbose' } };

ViewRequestServiceModule.service('ViewRequestService', function ($http, CommonAppUtilityService) {


    this.UpdateRequest = function (option) {

        return CommonAppUtilityService.CreateItem("/EmployeeDashboard/SaveInfo", option);
    }



});