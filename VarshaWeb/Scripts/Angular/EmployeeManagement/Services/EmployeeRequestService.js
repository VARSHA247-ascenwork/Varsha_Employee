
var EmployeeServiceModule = angular.module('EmployeeServiceModule', ['CommonAppUtility'])
var jsonheaders = { 'headers': { 'accept': 'application/json;odata=verbose' } };


EmployeeServiceModule.service('EmployeeService', function ($http, CommonAppUtilityService) {

    this.Postdata = function (option, path) {

        return CommonAppUtilityService.CreateItem(path, option);
    }
});






