var VendorApp = angular.module('VendorApp', ['VendorServiceModule', 'CommonAppUtility'])

VendorApp.controller('VendorController', function ($scope,  $timeout,VendorService, CommonAppUtilityService) {
    

    $(function () {

        //validation
        $('#AddVendorDetailForm').parsley().on('field:validated', function () {
            var ok = $('.parsley-error').length === 0;
            $('.bs-callout-info').toggleClass('hidden', !ok);
            $('.bs-callout-warning').toggleClass('hidden', ok);
        })
            .on('form:submit', function () {
            //   $scope.Update();
               $scope.Save();

                return false;
            });
        //table
        setTimeout(function () {
            $('#Vendortable').DataTable({
                responsive: true,
                language: {
                    searchPlaceholder: 'Search...',
                    sSearch: '',
                    lengthMenu: '_MENU_ ',
                }

            });
        }, 2000);
        //Dropdown
        $('.select2').select2({
            placeholder: 'Choose one',
            searchInputPlaceholder: 'Search',
            width: '100%',
            dropdownParent: $("#ModalPopUp")

        }); 
    })

    $scope.AddVendor = function () {
        $scope.editVendor = false;
        $scope.addVendor = true;
          $('#ddlCountry').on("change", function () {
        getState();
    })

        $scope.ngVendorCompany ="";
        $scope.ngVendorname ="";
        $scope.ngtxtmobile = "";
        $scope.ngContact = "";
        $scope.ngEmail = "";
        $scope.ngAddress ="";
        $scope.ngCity ="";
        $scope.ngPANcardno = "";
        $scope.ngGSTno ="";
        $scope.ngRemark = "";
        $scope.ngddlState = "";
        $scope.ngddlCountry = "";
        $scope.ngddlDesignation = "";
    }


    $scope.getVendordata = function () {
        var obj = {
            Id: 1
        }
        CommonAppUtilityService.CreateItem("/Vendor/getVendordata", obj).then(function (response) {
            console.log(response);

            $scope.Vendordata = response.data;
        });
    }
    $scope.getVendordata();  
    
   
    $scope.ViewVendor = function (VDid) {
        var obj = {
            VId: VDid
        }
        CommonAppUtilityService.CreateItem("/Vendor/getVendordataById", obj).then(function (response) {
            
            $scope.VendorInfo = response.data;
            console.log(response);
        });
        CommonAppUtilityService.CreateItem("/Vendor/getVendorDocument", obj).then(function (response) {
            console.log(response);
            $scope.VendorDoc = response.data;
        });
     
    }


    $scope.EditVendor = function (VDid) {
       // $scope.editVendor = true;
        $('#ddlCountry').on("change", function () {
            getState();
        })
        var obj = {
            VId: VDid
        }
        CommonAppUtilityService.CreateItem("/Vendor/getVendordataById", obj).then(function (response) {
            console.log(response);
            $scope.VendorInfo = response.data;

            $scope.ngVendorCompany = $scope.VendorInfo[0].VendorCompany;
            $scope.ngVendorname = $scope.VendorInfo[0].VendorName;
            $scope.ngtxtmobile = $scope.VendorInfo[0].MobileNo;
            $scope.ngContact = $scope.VendorInfo[0].VendorContact;
            $scope.ngEmail = $scope.VendorInfo[0].VendormailID;
            $scope.ngAddress = $scope.VendorInfo[0].VendorAddress;
            $scope.ngCity = $scope.VendorInfo[0].City;
            $scope.ngPANcardno = $scope.VendorInfo[0].PanCardNo;
            $scope.ngGSTno = $scope.VendorInfo[0].GstNo; 
            $scope.ngRemark = $scope.VendorInfo[0].Remark;
            $scope.ngddlState = $scope.VendorInfo[0].VendorStateId;
            $timeout(function () {
                $("#ddlCountry").val($scope.VendorInfo[0].CountryId).trigger('change');
                $("#ddlDesignation").val($scope.VendorInfo[0].DesignationId).trigger('change');
               // $("#ddlState").val($scope.VendorInfo[0].VendorStateId).trigger('change');
              
            }, 20)
           
        });
        CommonAppUtilityService.CreateItem("/Vendor/getVendorDocument", obj).then(function (response) {
            console.log(response);
            $scope.VendorDoc = response.data;
        });
    }

    //Get State
    getState = function () {
        var data = {
            'CountryNameID': angular.element("#ddlCountry").val()
        }
        CommonAppUtilityService.CreateItem("/Vendor/GetState", data).then(function (response) {
            console.log(response);
            $scope.State = response.data;
          
        });
    }

  
    

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

      //  var v = $scope.ngddlState;
     
        var Items = {
            VendorCompany: $scope.ngVendorCompany,
            VendorName: $scope.ngVendorname,
            MobileNo: $scope.ngtxtmobile,
            VendorContact: $scope.ngContact,
            VendormailID: $scope.ngEmail,
            Designation: $scope.ngddlDesignation,
            VendorAddress: $scope.ngAddress,
            City: $scope.ngCity,
            Country: $scope.ngddlCountry,
        //    VendorState: v,
            VendorState:$scope.ngddlState,
            PanCardNo: $scope.ngPANcardno,
            GstNo: $scope.ngGSTno,
            Remark: $scope.ngRemark,
        }
     
         VendorService.Postdata(Items, $scope.UploadQuotationFiles, "/Vendor/SaveInfo").then(function (response) {

        });
    } 

   /* $scope.Update = function () {
        $scope.VendorID = $scope.VendorInfo[0].ID;
        var Items = {
                    ID:$scope.VendorID,
                 VendorCompany: $scope.ngVendorCompany,
                 VendorName: $scope.ngVendorname,
                 MobileNo: $scope.ngtxtmobile,
                 VendorContact: $scope.ngContact,
                 VendormailID: $scope.ngEmail,
                 Designation: $scope.ngddlDesignation,
                 VendorAddress: $scope.ngAddress,
                 City: $scope.ngCity,
                 Country: $scope.ngddlCountry,
                 //    VendorState: v,
                 VendorState: $scope.ngddlState,
                 PanCardNo: $scope.ngPANcardno,
                 GstNo: $scope.ngGSTno,
                 Remark: $scope.ngRemark,
        }
        VendorService.Postdata(Items, $scope.UploadQuotationFiles, "/Vendor/UpdateInfo").then(function (response) {

        });
    } */

    
});