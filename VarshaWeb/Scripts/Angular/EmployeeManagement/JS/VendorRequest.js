var VendorApp = angular.module('VendorApp', ['VendorServiceModule'])

VendorApp.controller('VendorController', function ($scope, VendorService) {



    // validate function
  $(function () {
        'use strict'
        //validation
        $('#AddVendorDetailForm').parsley().on('field:validated', function () {
            var ok = $('.parsley-error').length === 0;
            $('.bs-callout-info').toggleClass('hidden', !ok);
            $('.bs-callout-warning').toggleClass('hidden', ok);
        })
            .on('form:submit', function () {
                $scope.Update();
                $scope.Save();

                return false;
            });

    });   

    $scope.UploadQuotationFiles = [];

    $("#uploadFile").change(function () {

        var file = $("#uploadFile")[0].files[0];
        var reader = new FileReader();

        reader.onload = function (event) {
            var temp = {};
            temp.Name = file.name;
            temp.file = file;
            temp.Flag = "New";
            $scope.UploadQuotationFiles.push(temp);
            $scope.$apply();
        }
        reader.readAsArrayBuffer(file);
    });

    $scope.Remove = function (index) {
        $scope.UploadQuotationFiles.splice(index, 1);
    }

    $scope.Save = function () {

        var Items = {
            VendorCompany: $scope.ngVendorCompany,
            VendorName: $scope.ngVendorname,
            MobileNo: $scope.ngtxtmobile,
            VendorContact: $scope.ngContact,
            VendormailID: $scope.ngEmail,
            Designation: $scope.ngddlDesignation,
            VendorAddress: $scope.ngAddress,
            City: $scope.ngCity,
            States: $scope.ngddlstate,
            Country: $scope.ngddlCountry,
            PanCardNo: $scope.ngPANcardno,
            GstNo: $scope.ngGSTno,
            Remark: $scope.ngRemark,
        }
     //VendorService.Postdata(Items).then(function (response) {
         VendorService.Postdata(Items, $scope.UploadQuotationFiles, "/VendorDashboard/SaveInfo").then(function (response) {

        });
    }

    $scope.Update = function () {
             var Items = {
                 VendorCompany: $('#txtVendorcompany').val(),
                 VendorName: $('#txtVendor_Name').val(),
                 MobileNo: $('#phone').val(),
                 VendorContact: $('#txTelephone').val(),
                 VendormailID: $('#txtVendorEmail').val(),
                 Designation: $('#ddlDesignation').val(),
                 VendorAddress: $('#txtaddress').val(),
                 City: $('#txtCity').val(),
                 States: $('#ddlState').val(),
                 Country: $('#ddlCountry').val(),
                 PanCardNo: $('#txtPANcardno').val(),
                 GstNo: $('#txtGSTno').val(),
                 Remark: $('#txtremark').val(),
        }
      //  VendorService.EditPostdata(Items).then(function (response) {
        VendorService.Postdata(Items, $scope.UploadQuotationFiles, "/VendorDashboard/UpdateInfo").then(function (response) {

        });
    }

    $scope.CancelViewpage = function () {
        Pageredirect("/VendorDashboard");
    }
    
});