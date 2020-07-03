var VendorApp = angular.module('VendorApp', ['VendorServiceModule', 'CommonAppUtility'])

VendorApp.controller('VendorController', function ($scope, $timeout, VendorService, CommonAppUtilityService) {

    $(function () {

        //validation
        $('#AddVendorDetailForm').parsley().on('field:validated', function () {
            var ok = $('.parsley-error').length === 0;
            $('.bs-callout-info').toggleClass('hidden', !ok);
            $('.bs-callout-warning').toggleClass('hidden', ok);
        })
            .on('form:submit', function () {
               
                if ($scope.addVendor == true) {
                    $scope.Save();

                } else if ($scope.editVendor == true) {
                    $scope.Update();
                }
               
                return false;
            });
        
        //Dropdown
        $('.select2').select2({
            placeholder: 'Choose one',
            searchInputPlaceholder: 'Search',
            width: '100%',
            dropdownParent: $("#ModalPopUp")

        }); 
    })

    $scope.AddVendor = function () {
        $("#ddlDesignation")[0].selectedIndex = 0;
        $("#ddlCountry")[0].selectedIndex = 0;
        $("#ddlState")[0].selectedIndex = 0;
        $scope.UploadQuotationFiles.length = 0;
        $scope.editVendor = false;
        $scope.addVendor = true;
        $scope.viewdocument = false;
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
        $('.select2').select2({
            placeholder: 'Choose one',
            searchInputPlaceholder: 'Search',
            width: '100%',
            dropdownParent: $("#ModalPopUp")

        });
    }

    $scope.getVendordata = function () {
        var obj = {
            Id: 1
        }
        CommonAppUtilityService.CreateItem("/Vendor/getVendordata", obj).then(function (response) {
            console.log(response);
            $scope.Vendordata = response.data;
            setTimeout(function () {
                $('#Vendortable').DataTable({
                    responsive: true,
                    bDestroy: true,
                    language: {
                        searchPlaceholder: 'Search...',
                        sSearch: '',
                        lengthMenu: '_MENU_ ',
                    }

                });
            }, 1000);
        });
    }
    $scope.getVendordata();  
    
    $scope.DeleteDocument = function (did) {
        if (confirm("Are you sure you want to delete this?")) {
            var obj = {
                DId: did
            }
            CommonAppUtilityService.CreateItem("/Vendor/DeleteDocument", obj).then(function (response) {
                $scope.VendorDoc = response.data;
            });
        }
        else {
            false;
        }
    }
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
        $scope.UploadQuotationFiles.length = 0;
        $scope.editVendor = true;
        $scope.addVendor = false;
        $scope.viewdocument = true;
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
            
                $("#global-loader").hide();
                $('#modaldemo4').modal('show');
           
        });
    } 

    $scope.Update = function () {
      
        $scope.VendorID = $scope.VendorInfo[0].ID;
        var data = {
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
        VendorService.Postdata(data, $scope.UploadQuotationFiles, "/Vendor/UpdateInfo").then(function (response) {
            
                $("#global-loader").hide();
                $('#modaldemo4').modal('show');
           

        });
    } 
    $scope.PerformAction = function () {
        Pageredirect("/Vendor");
    }
    
});