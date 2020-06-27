
var VendorServiceModule = angular.module('VendorServiceModule', ['CommonAppUtility'])
var jsonheaders = { 'headers': { 'accept': 'application/json;odata=verbose' } };


VendorServiceModule.service('VendorService', function ($http, CommonAppUtilityService) {

   this.Postdata = function (option, files, path) {


        var fileData = new FormData();
        //files[0].file

        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i].file);
        }

        var objArr = [];
        objArr.push(option);

        fileData.append('VendorDetails', JSON.stringify(objArr));
        return CommonAppUtilityService.DataWithFile(path, fileData);
    }  


    
});
