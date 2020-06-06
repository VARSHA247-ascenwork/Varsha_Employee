
var EmployeeServiceModule = angular.module('EmployeeServiceModule', ['CommonAppUtility'])
var jsonheaders = { 'headers': { 'accept': 'application/json;odata=verbose' } };


EmployeeServiceModule.service('EmployeeService', function ($http, CommonAppUtilityService) {

    this.Postdata = function (option) {

        return CommonAppUtilityService.CreateItem("/EmployeeDashboard/SaveInfo", option);
    }
    this.EmpPostdata = function (option) {

        return CommonAppUtilityService.CreateItem("/EmployeeDashboard/UpdateInfo", option);
    }
});






