var ClientApp = angular.module('ClientApp', ['ClientServiceModule', 'CommonAppUtility'])

ClientApp.controller('ClientController', function ($scope, $timeout,ClientService, CommonAppUtilityService) {
    
    $(function () {
        $('#ddlCountry').on("change", function () {
            getState();
        })
        //validation
        $('#AddClientDetailForm').parsley().on('field:validated', function () {
            var ok = $('.parsley-error').length === 0;
            $('.bs-callout-info').toggleClass('hidden', !ok);
            $('.bs-callout-warning').toggleClass('hidden', ok);
        })
            .on('form:submit', function () {

                if ($scope.addClient == true) {
                    $scope.Save();

                } else if ($scope.editClient == true) {
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
    $scope.getClientdata = function () {
        var obj = {
            Id: 1
        }
        CommonAppUtilityService.CreateItem("/Client/getClientdata", obj).then(function (response) {
            console.log(response);
            $scope.Clientdata = response.data;
          /*  setTimeout(function () {
                $('#Clienttable').DataTable({
                    responsive: true,
                    bDestroy: true,
                    language: {
                        searchPlaceholder: 'Search...',
                        sSearch: '',
                        lengthMenu: '_MENU_ ',
                    }

                });
            }, 200); */

            setTimeout(function () {
                table = $('#Clienttable').DataTable({
                    lengthChange: true,
                    responsive: true,
                    bDestroy: true,
                    language: {
                        searchPlaceholder: 'Search...',
                        sSearch: '',
                        lengthMenu: '_MENU_ ',
                    }
                });
               // table.buttons().container()
                // .appendTo('#tblProject_wrapper .col-md-6:eq(0)');
            }, 2);




        });
    }
    $scope.getClientdata();  

    getState = function () {
        var data = {
            'CountryNameID': angular.element("#ddlCountry").val()
        }
        CommonAppUtilityService.CreateItem("/Client/GetState", data).then(function (response) {
            console.log(response);
            $scope.State = response.data;

        });
    }
    //File Upload
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
    //Save Cient
    $scope.Save = function () {
        var data = {
            ClientName: $scope.ngClientName,
            ClientContact: $scope.ngClientContact,
            ClientAddress: $scope.ngClientAddress,
            ClientCity: $scope.ngClientCity,
            ClientState: $scope.ngddlClientState,
            ClientCountry: $scope.ngddlClientCountry,
            ClientDesignation: $scope.ngddlClientDesignation,
            ClientMailID: $scope.ngClientEmail,
            ClientGSTNO: $scope.ngClientGSTno,
            ClientPanCardNo: $scope.ngClientPANcardno,
            ClientRemark: $scope.ngClientRemark,
            MobileNo: $scope.ngtxtClientmobile,
        }

        ClientService.Postdata(data, $scope.UploadQuotationFiles, "/Client/SaveInfo").then(function (response) {

            $("#global-loader").hide();
            $('#modaldemo4').modal('show');

        });
    }

    $scope.Update = function () {
        $scope.ClientID = $scope.ClientInfo[0].ID;
            var data = {
                ID: $scope.ClientID,
                ClientName: $scope.ngClientName,
                ClientContact: $scope.ngClientContact,
                ClientAddress: $scope.ngClientAddress,
                ClientCity: $scope.ngClientCity,
                ClientState: $scope.ngddlClientState,
                ClientCountry: $scope.ngddlClientCountry,
                ClientDesignation: $scope.ngddlClientDesignation,
                ClientMailID: $scope.ngClientEmail,
                ClientGSTNO: $scope.ngClientGSTno,
                ClientPanCardNo: $scope.ngClientPANcardno,
                ClientRemark: $scope.ngClientRemark,
                MobileNo: $scope.ngtxtClientmobile,
        }
      ClientService.Postdata(data, $scope.UploadQuotationFiles, "/Client/UpdateInfo").then(function (response) {
            $("#global-loader").hide();
            $('#modaldemo4').modal('show');
        });AddDetail
    } 

    $scope.AddClient = function () {
       $scope.UploadQuotationFiles.length = 0;
            $scope.viewdocument = false;
            $scope.editClient = false;
            $scope.addClient = true;
            $scope.AddDetail = true;
            $scope.UpdateDetail = false;
            $scope.ngClientName = "",
            $scope.ngClientContact = "",
            $scope.ngClientAddress = "",
            $scope.ngClientCity = "",
            $("#ddlDesignation")[0].selectedIndex = 0;
            $("#ddlCountry")[0].selectedIndex = 0;
            $("#ddlState")[0].selectedIndex = 0;
            $scope.ngClientEmail = "",
            $scope.ngClientGSTno = "",
            $scope.ngClientPANcardno = "",
            $scope.ngClientRemark = "",
            $scope.ngtxtClientmobile = "",
           
        $('#ddlCountry').on("change", function () {
            getState();
        })
        $('.select2').select2({
            placeholder: 'Choose one',
            searchInputPlaceholder: 'Search',
            width: '100%',
            dropdownParent: $("#ModalPopUp")

        });

    }
    $scope.EditClient = function (Did) {
        $scope.UploadQuotationFiles.length = 0;
        $scope.editClient = true;
        $scope.UpdateDetail = true;
        
        $scope.addClient = false;
        $scope.AddDetail = false;
        $scope.viewdocument = true;
        $('#ddlCountry').on("change", function () {
            getState();
        })
        var obj = {
            CId: Did
        }
        CommonAppUtilityService.CreateItem("/Client/getClientdataById", obj).then(function (response) {
            console.log(response);
            $scope.ClientInfo = response.data;
            $scope.ngClientName = $scope.ClientInfo[0].ClientName;
            $scope.ngClientContact = $scope.ClientInfo[0].ClientContact;
            $scope.ngClientAddress = $scope.ClientInfo[0].ClientAddress;
            $scope.ngClientCity = $scope.ClientInfo[0].ClientCity;
            $scope.ngddlClientState = $scope.ClientInfo[0].ClientStateId;
            $scope.ngClientEmail = $scope.ClientInfo[0].ClientMailID;
            $scope.ngClientGSTno = $scope.ClientInfo[0].ClientGSTNO;
            $scope.ngClientPANcardno = $scope.ClientInfo[0].ClientPanCardNo;
            $scope.ngClientRemark = $scope.ClientInfo[0].ClientRemark;
            $scope.ngtxtClientmobile = $scope.ClientInfo[0].MobileNo;
            $timeout(function () {
                $("#ddlCountry").val($scope.ClientInfo[0].ClientCountryId).trigger('change');
                $("#ddlDesignation").val($scope.ClientInfo[0].ClientDesignationId).trigger('change');
               
            }, 20)

        });
        CommonAppUtilityService.CreateItem("/Client/getClientDocument", obj).then(function (response) {
            console.log(response);
            $scope.ClientDoc = response.data;
        });
    }
    $scope.ViewClient = function (Did) {
        var obj = {
            CId: Did
        }
        CommonAppUtilityService.CreateItem("/Client/getClientdataById", obj).then(function (response) {

            $scope.ClientInfo = response.data;
            console.log(response);
        });
        CommonAppUtilityService.CreateItem("/Client/getClientDocument", obj).then(function (response) {
            console.log(response);
            $scope.ClientDoc = response.data;
        });

    }

    $scope.DeleteDocument = function (did) {
        if (confirm("Are you sure you want to delete this?")) {
            var obj = {
                DId: did
            }
            CommonAppUtilityService.CreateItem("/Client/DeleteDocument", obj).then(function (response) {
                $scope.ClientDoc = response.data;
            });
        }
        else {
            false;
        }
    }
    $scope.PerformAction = function () {
        Pageredirect("/Client");
    }

});