
var ClientServiceModule = angular.module('ClientServiceModule', ['CommonAppUtility'])
var jsonheaders = { 'headers': { 'accept': 'application/json;odata=verbose' } };


ClientServiceModule.service('ClientService', function ($http, CommonAppUtilityService) {

   this.Postdata = function (option, files, path) {


        var fileData = new FormData();
        //files[0].file

        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i].file);
        }

        var objArr = [];
        objArr.push(option);

       fileData.append('ClientDetails', JSON.stringify(objArr));
        return CommonAppUtilityService.DataWithFile(path, fileData);
    }  


    
});
