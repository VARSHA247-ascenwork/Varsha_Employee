var EmployeeProfileServiceModule = angular.module('EmployeeProfileServiceModule', ['CommonAppUtility'])
var jsonheaders = { 'headers': { 'accept': 'application/json;odata=verbose' } };

EmployeeProfileServiceModule.service('EmployeeProfileService', function ($http, CommonAppUtilityService) {

    
    this.SaveRequest = function (option, path) {

        return CommonAppUtilityService.CreateItem(path, option);
    }
});