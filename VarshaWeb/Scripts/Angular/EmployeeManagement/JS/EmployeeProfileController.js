var EmployeeProfileApp = angular.module('EmployeeProfileApp', ['EmployeeProfileServiceModule'])

EmployeeProfileApp.controller('EmployeeProfileController', function ($scope, EmployeeProfileService) {

    //Get State
    getState = function () {
        var data = {
            'CountryNameID': angular.element(".getCountryId").val()
        }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/GetState").then(function (response) {
            //$scope.State = response.data;
            var states = response.data;

            $('.getStates').html("");
            $.each(states, function (i, state) {
                $(".getStates").append('<option value="' + state.ID + '">' +
                    state.StateName + '</option>');
            });  
        });
    }
    //end


    //Education Details
    SaveEducation = function () {
       
        var data = {
            'UniversityInstitute': angular.element("#Institute").val(),
            'Degree': angular.element("#Degree").val(),
            'From': moment(angular.element("#txtEducatinalfromdate").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
            'To': moment(angular.element("#txtEducatinalTodate").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
            'MarksInPercentage': angular.element("#Percentage").val(),
            'Subject': angular.element("#txtSubject").val()
        }

        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/SaveEducationDetails").then(function (response)
        {
            if (response.status == 200) {
                $('#ModalPopUp').modal('hide');
                $('#EducationalDetailsModel').html(response.data);
                notif({
                    msg: "<b>Success:</b> Well done Details Submitted Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
        
    }

    UpdateEducationDetails = function () {
        var data = {
            'UniversityInstitute': angular.element("#Institute").val(),
            'Degree': angular.element("#Degree").val(),
            'From': moment(angular.element("#txtEducatinalfromdate").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
            'To': moment(angular.element("#txtEducatinalTodate").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
            'MarksInPercentage': angular.element("#Percentage").val(),
            'Subject': angular.element("#txtSubject").val()
        }

        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/UpdateEducationDetails").then(function (response) {
            if (response.status == 200) {
                $('#ModalPopUp').modal('hide');
                $('#EducationalDetailsModel').html(response.data);
                notif({
                    msg: "<b>Success:</b> Well done Details Updated Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
    }

    DeleteEducationDetails = function (Eid) {
        var data = {'ID':Eid}
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/DeleteEducationDetails").then(function (response) {
            if (response.status == 200) {
                $('#EducationalDetailsModel').html(response.data);
                notif({
                    msg: "<b>Success:</b> Delete Record Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
    }

    EditEducation = function (Eid) {
        var data = { "ID": Eid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/EditEducationDetails").then(function (response) {
            $('#ModalPopUp').modal('show');
            $(".modal-body").html(response.data);
            var button = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>' +
                '<button type="submit" onclick="UpdateEducationDetails()" class="btn btn-primary">Save changes</button>';
            $(".modal-title").html('Educational Details');
            $(".modal-footer").html(button);
            $('#txtEducatinalfromdate').datetimepicker({
                minView: 4,
                format: 'dd-mm-yyyy',
                gotoCurrent: true,
                autoclose: true
            });
            $('#txtEducatinalTodate').datetimepicker({
                minView: 2,
                format: 'dd-mm-yyyy',
                autoclose: true
            });
            $("#txtEducatinalTodate").on('change', function () {
                var start = moment(angular.element("#txtEducatinalfromdate").val(), 'DD/MM/YYYY');
                var end = moment($(this).val(), 'DD/MM/YYYY');
                if (end < start) {
                    alert('Please should ');
                    $(this).val('');
                }
            });
        });
    }

    //End

    //WorkExperience
    SaveWorkExperience= function() {
        var data = {
            'OrganizationName': angular.element('#Organazation_name').val(),
            'Designation': angular.element('#Designation').val(),
            'FromDate': moment(angular.element("#txtfromdate").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
            'ToDate': moment(angular.element("#To_Date").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
            'Notes': angular.element('#txtWorkNotes').val(),
            'ContactNumber': angular.element('#txtContact').val(),
            'AnnualSalary': angular.element('#txtAnnual_Salary').val(),
            'Industry': angular.element('#ddlIndustry').val()
        }

        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/SaveWorkExperience").then(function (response) {
            if (response.status == 200) {
                $('#ModalPopUp').modal('hide');
                $('#WorkExperienceModel').html(response.data);
                notif({
                    msg: "<b>Success:</b> Well done Details Submitted Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
    }

    UpdateWorkExperience = function () {
        var data = {
            'OrganizationName': angular.element('#Organazation_name').val(),
            'Designation': angular.element('#Designation').val(),
            'FromDate': moment(angular.element("#txtfromdate").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
            'ToDate': moment(angular.element("#To_Date").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
            'Notes': angular.element('#txtWorkNotes').val(),
            'ContactNumber': angular.element('#txtContact').val(),
            'AnnualSalary': angular.element('#txtAnnual_Salary').val(),
            'Industry': angular.element('#ddlIndustry').val()
        }

        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/UpdateWorkExperiences").then(function (response) {
            if (response.status == 200) {
                $('#ModalPopUp').modal('hide');
                $('#WorkExperienceModel').html(response.data);
                notif({
                    msg: "<b>Success:</b> Well done Details Updated Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
    }

    DeleteWorkExperience = function (Wid) {
        var data = { 'ID': Wid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/DeleteWorkExperience").then(function (response) {
            if (response.status == 200) {
                $('#WorkExperienceModel').html(response.data);
                notif({
                    msg: "<b>Success:</b> Delete Record Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
    }

    EditWorkExperience = function (Wid) {
        var data = { "ID": Wid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/EditWorkExperience").then(function (response) {
            $('#ModalPopUp').modal('show');
            $(".modal-body").html(response.data);
            var button = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>' +
                '<button type="submit" onclick="UpdateWorkExperience()" class="btn btn-primary">Save changes</button>';
            $(".modal-title").html('Work Experience');
            $(".modal-footer").html(button);
            $('#txtfromdate').datetimepicker({
                minView: 4,
                format: 'dd-mm-yyyy',
                gotoCurrent: true,
                autoclose: true
            });
            $('#To_Date').datetimepicker({
                minView: 2,
                format: 'dd-mm-yyyy',
                autoclose: true
            });
            $("#To_Date").on('change', function () {
                var start = moment(angular.element("#txtfromdate").val(), 'DD/MM/YYYY');
                var end = moment($(this).val(), 'DD/MM/YYYY');
                if (end < start) {
                    alert('Please should ');
                    $(this).val('');
                }
            });
        });
    }

    DetailsWorkExperience = function (Wid) {
        var data = { "ID": Wid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/DetailsWorkExperience").then(function (response) {
            $('#ModalPopUp').modal('show');
            $(".modal-body").html(response.data);
            var button = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>';
            $(".modal-title").html('Work Experience');
            $(".modal-footer").html(button);
        })
    }

    //End



    //Award Details
    SaveAwardDetails = function(){
        var data = {
            'Award': angular.element("#Award").val(),
            'AwardedBy': angular.element("#txtAwarded_By").val(),
            'AwardedOn': moment(angular.element("#txtAwarded_on").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
            'Description': angular.element("#Description").val(),
        }

        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/SaveAwardDetails").then(function (response) {
            if (response.status == 200) {
                $('#ModalPopUp').modal('hide');
                $('#AwardDetails').html(response.data);
                notif({
                    msg: "<b>Success:</b> Well done Details Submitted Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
    }

    DeleteAwardDetails = function (Aid) {
        var data = { 'ID': Aid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/DeleteAwardDetails").then(function (response) {
            if (response.status == 200) {
                $('#AwardDetails').html(response.data);
                notif({
                    msg: "<b>Success:</b> Delete Record Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
    }

    EditAwardDetails= function (Aid) {
        var data = { "ID": Aid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/EditAwardDetails").then(function (response) {
            $('#ModalPopUp').modal('show');
            $(".modal-body").html(response.data);
            var button = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>' +
                '<button type="submit" onclick="UpdateAwardDetails()" class="btn btn-primary">Save changes</button>';
            $(".modal-title").html('Award Details');
            $(".modal-footer").html(button);
            $('#txtAwarded_on').datetimepicker({
                minView: 2,
                format: 'dd-mm-yyyy',
                gotoCurrent: true,
                autoclose: true
            });
        });
    }

    UpdateAwardDetails = function () {
        var data = {
            'Award': angular.element("#Award").val(),
            'AwardedBy': angular.element("#txtAwarded_By").val(),
            'AwardedOn': moment(angular.element("#txtAwarded_on").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
            'Description': angular.element("#Description").val(),
        }

        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/UpdateAwardDetails").then(function (response) {
            if (response.status == 200) {
                $('#ModalPopUp').modal('hide');
                $('#AwardDetails').html(response.data);
                notif({
                    msg: "<b>Success:</b> Well done Details Updated Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
    }

    //End




    //Publication Details

    SavePublication = function () {
        var data = {
            'PublishedOn': moment(angular.element("#txt_Publication_date").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
            'Publication': angular.element("#txtPublication_on").val(),
            'Description': angular.element("#Description").val(),
            'Url': angular.element("#url").val(),
        }

        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/SavePublication").then(function (response) {
            if (response.status == 200) {
                $('#ModalPopUp').modal('hide');
                $('#emp_PublicationDetails').html(response.data);
                notif({
                    msg: "<b>Success:</b> Well done Details Submitted Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
    }

    DeletePublication = function (Pid) {
        var data = { 'ID': Pid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/DeletePublication").then(function (response) {
            if (response.status == 200) {
                $('#emp_PublicationDetails').html(response.data);
                notif({
                    msg: "<b>Success:</b> Delete Record Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
    }

    EditPublication = function (Pid) {
        var data = { "ID": Pid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/EditPublication").then(function (response) {
            $('#ModalPopUp').modal('show');
            $(".modal-body").html(response.data);
            var button = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>' +
                '<button type="submit" onclick="UpdatePublication()" class="btn btn-primary">Save changes</button>';
            $(".modal-title").html('Publication Details');
            $(".modal-footer").html(button);
            $('#txt_Publication_date').datetimepicker({
                minView: 2,
                format: 'dd-mm-yyyy',
                gotoCurrent: true,
                autoclose: true
            });
        });
    }

    UpdatePublication = function () {
        var data = {
            'PublishedOn': moment(angular.element("#txt_Publication_date").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
            'Publication': angular.element("#txtPublication_on").val(),
            'Description': angular.element("#Description").val(),
            'Url': angular.element("#url").val(),
        }

        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/UpdatePublication").then(function (response) {
            if (response.status == 200) {
                $('#ModalPopUp').modal('hide');
                $('#emp_PublicationDetails').html(response.data);
                notif({
                    msg: "<b>Success:</b> Well done Details Updated Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
    }

    //End

    //Reference Details

    SaveReference = function () {
        var data = {
            'Person': angular.element("#txtPerson").val(),
            'Designation': angular.element("#Designation").val(),
            'Company': angular.element("#txtCompany").val(),
            'Email': angular.element("#Email").val(),
            'Phone': angular.element("#Phone").val(),
            'HowDoYouKnowPerson': angular.element("#txtknowPerson").val(),
            'Notes': angular.element("#txtNotes").val(),
            'Address': angular.element("#txtAddress").val(),
        }

        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/SaveReferenceDetails").then(function (response) {
            if (response.status == 200) {
                $('#ModalPopUp').modal('hide');
                $('#Reference').html(response.data);
                notif({
                    msg: "<b>Success:</b> Well done Details Submitted Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
    }

    DeleteReferenceDetails = function (Rid) {
        var data = { 'ID': Rid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/DeleteReferenceDetails").then(function (response) {
            if (response.status == 200) {
                $('#Reference').html(response.data);
                notif({
                    msg: "<b>Success:</b> Delete Record Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
    }

    UpdateReference = function () {
        var data = {
            'Person': angular.element("#txtPerson").val(),
            'Designation': angular.element("#Designation").val(),
            'Company': angular.element("#txtCompany").val(),
            'Email': angular.element("#Email").val(),
            'Phone': angular.element("#Phone").val(),
            'HowDoYouKnowPerson': angular.element("#txtknowPerson").val(),
            'Notes': angular.element("#txtNotes").val(),
            'Address': angular.element("#txtAddress").val(),
        }

        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/UpdateReferenceDetails").then(function (response) {
            if (response.status == 200) {
                $('#ModalPopUp').modal('hide');
                $('#Reference').html(response.data);
                notif({
                    msg: "<b>Success:</b> Well done Details Submitted Successfully",
                    type: "success"
                });
            } else {

                alert("Error");
            }
        });
    }

    EditReference = function (Rid) {
        var data = { "ID": Rid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/EditReferenceDetails").then(function (response) {
            $('#ModalPopUp').modal('show');
            $(".modal-body").html(response.data);
            var button = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>' +
                '<button type="submit" onclick="UpdateReference()" class="btn btn-primary">Save changes</button>';
            $(".modal-title").html('Reference Details');
            $(".modal-footer").html(button);
        });
    }

    ReferenceDetails = function (Rid) {
        var data = { "ID": Rid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/ReferenceDetails").then(function (response) {
            $('#ModalPopUp').modal('show');
            $(".modal-body").html(response.data);
            var button = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>';
            $(".modal-title").html('Reference Details');
            $(".modal-footer").html(button);
        });
    }

    //End

    //Disciplinary Details
    DisciplinaryDetailsAction = function (Action) {
        var path = "";
        
        var data = {
            'ActionOn': moment(angular.element("#txt_Displinary_date").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
            'Description': angular.element("#Description").val()
        }
        if (Action == "Update") {
            path = "/Employeeprofile/UpdateDisciplinaryDetails";
        }
        else {
            path = "/Employeeprofile/SaveDisciplinaryDetails";
        }

        if (path != "") {
            EmployeeProfileService.SaveRequest(data, path).then(function (response) {
                if (response.status == 200) {
                    $('#ModalPopUp').modal('hide');
                    notif({
                        msg: "<b>Success:</b> Well done Details Submitted Successfully",
                        type: "success"
                    });
                    $('#Disciplinary').html(response.data);
                } else {

                    alert("Error");
                }
            });
        }
    }

    DeleteDisciplinaryDetails = function (Did) {
        var data = { 'ID': Did }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/DeleteDisciplinaryDetails").then(function (response) {
            if (response.status == 200) {
                notif({
                    msg: "<b>Success:</b> Delete Record Successfully",
                    type: "success"
                });
                $('#Disciplinary').html(response.data);
            } else {

                alert("Error");
            }
        });
    }

    DisciplinaryAddEdit = function (action,Did) {
        var data = { "ID": Did }
        var path = "";
        var button = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>';
        if (action == "Edit") {
            path = "/Employeeprofile/EditDisciplinaryDetails"
            button += '<button type="submit" onclick="DisciplinaryDetailsAction(' + "'Update'" +')" class="btn btn-primary">Save changes</button>';
        }
        else {
            path = "/Employeeprofile/DisciplinaryAddView"
            button += '<button type="submit" onclick="DisciplinaryDetailsAction(' + "'Save'" + ')" class="btn btn-primary">Save changes</button>';
        }
        if (path != "") {
            EmployeeProfileService.SaveRequest(data, path).then(function (response) {
                $('#ModalPopUp').modal('show');
                $(".modal-body").html(response.data);
                $(".modal-title").html('Disciplinary Details');
                $(".modal-footer").html(button);
                $('#txt_Displinary_date').datetimepicker({
                    minView: 2,
                    format: 'dd-mm-yyyy',
                    gotoCurrent: true,
                    autoclose: true
                });
            });
        }
    }

    //End


    //Visa Details
    VisaDetailsAction = function (Action) {
        var data = {
            'VisaType': angular.element("#txtVisaType").val(),
            'Country': angular.element("#ddlVisaCountry").val(),
            'ValidUntil': moment(angular.element("#txt_Visa_date").val(), 'DD-MM-YYYY').format("MM/DD/YYYY")
        }
        var path = "";
        if (Action == "Update") {
            path = "/Employeeprofile/UpdateVisaDetails";
        }
        else {
            path = "/Employeeprofile/SaveVisaDetails";
        }

        EmployeeProfileService.SaveRequest(data, path).then(function (response) {
            if (response.status == 200) {
                $('#ModalPopUp').modal('hide');
               // $('#VisaDetails').load('Index?SPHostUrl=' + getUrlVars()["SPHostUrl"] + ' #VisaDetails');
                notif({
                    msg: "<b>Success:</b> Well done Details Submitted Successfully",
                    type: "success"
                });
                $('#VisaDetails').html(response.data);
            } else {

                alert("Error");
            }
        });
    }

    DeleteVisaDetails = function (Vid) {
        var data = { 'ID': Vid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/DeleteVisaDetails").then(function (response) {
            if (response.status == 200) {
                //$('#VisaDetails').load('Index?SPHostUrl=' + getUrlVars()["SPHostUrl"] + ' #VisaDetails');
                notif({
                    msg: "<b>Success:</b> Delete Record Successfully",
                    type: "success"
                });
                $('#VisaDetails').html(response.data);
            } else {

                alert("Error");
            }
        });
    }

    VisaAddEdit = function (Action, Vid) {
        var data = { "ID": Vid }
        var path = "";
        var button = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>';
        if (Action == "Edit") {
            path = "/Employeeprofile/EditVisaDetails"
            button += '<button type="submit" onclick="VisaDetailsAction(' + "'Update'" + ')" class="btn btn-primary">Save changes</button>';
        }
        else {
            path = "/Employeeprofile/VisaDetailsAddView"
            button += '<button type="submit" onclick="VisaDetailsAction(' + "'Save'" + ')" class="btn btn-primary">Save changes</button>';
        }
        if (path != "") {
            EmployeeProfileService.SaveRequest(data, path).then(function (response) {
                $('#ModalPopUp').modal('show');
                $(".modal-body").html(response.data);
                $(".modal-title").html('Visa Details');
                $(".modal-footer").html(button);
                $('#ddlVisaCountry').select2({
                    dropdownParent: $('#ModalPopUp'),
                    placeholder: 'Choose one',
                    searchInputPlaceholder: 'Search',
                    width: '100%'
                });
                $('#txt_Visa_date').datetimepicker({
                    minView: 2,
                    format: 'dd-mm-yyyy',
                    gotoCurrent: true,
                    autoclose: true
                });
            });
        }
    }

    //End


    //Banking Details
    BankingDetailsAction = function (action) {
        var data = {
            'BankName': angular.element("#Bankname").val(),
            'Branch': angular.element("#txtBranch").val(),
            'Country': angular.element("#ddlbankCountry").val(),
            'State': angular.element("#ddlbankState").val(),
            'City': angular.element("#txtCity").val(),
            'AccountNumber': angular.element("#txtAccount_Number").val(),
            'AccountType': angular.element("#ddlAccountType").val(),
            'RoutingNumber': angular.element("#txtRouting_Number").val(),
        }

        var path = "";
        if (action == "Update") {
            path = "/Employeeprofile/UpdateBankingDetails";
        }
        else {
            path = "/Employeeprofile/SaveBankingDetails";
        }
        if (path != "") {
            EmployeeProfileService.SaveRequest(data, path).then(function (response) {
                if (response.status == 200) {
                    $('#ModalPopUp').modal('hide');
                    notif({
                        msg: "<b>Success:</b> Well done Details Submitted Successfully",
                        type: "success"
                    });
                    $('#emp_BankDetails').html(response.data);
                } else {

                    alert("Error");
                }
            });
        }
    }

    DeleteBankingDetails = function (Bid) {
        var data = { 'ID': Bid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/DeleteBankingDetails").then(function (response) {
            if (response.status == 200) {
                notif({
                    msg: "<b>Success:</b> Delete Record Successfully",
                    type: "success"
                });
                $('#emp_BankDetails').html(response.data);
            } else {

                alert("Error");
            }
        });
    }

    BankingAddEdit = function (action, Bid) {
        var data = { "ID": Bid }
        var path = "";
        var button = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>';
        if (action == "Edit") {
            path = "/Employeeprofile/EditBankingDetails"
            button += '<button type="submit" onclick="BankingDetailsAction(' + "'Update'" + ')" class="btn btn-primary">Save changes</button>';
        }
        else if (action == "Add") {
            path = "/Employeeprofile/BankingDetailsAddView"
            button += '<button type="submit" onclick="BankingDetailsAction(' + "'Save'" + ')" class="btn btn-primary">Save changes</button>';
        }
        else {
            path = "/Employeeprofile/DetailsBankingDetails";
        }
        if (path != "") {
            EmployeeProfileService.SaveRequest(data, path).then(function (response) {
                $('#ModalPopUp').modal('show');
                $(".modal-body").html(response.data);
                $(".modal-title").html('Banking Details');
                $(".modal-footer").html(button);
                $('#ddlbankCountry').select2({
                    placeholder: 'Choose One',
                    dropdownParent: $('#ModalPopUp'),
                    width: '100%'
                });
                $('#ddlbankState').select2({
                    placeholder: 'Choose One',
                    dropdownParent: $('#ModalPopUp'),
                    width: '100%'
                });
                $('#ddlAccountType').select2({
                    placeholder: 'Choose One',
                    dropdownParent: $('#ModalPopUp'),
                    width: '100%'
                });
                $('#ddlbankCountry').on("change", function () {
                    getState();
                })
            });
        }
    }

    //End


    //Emergency Contact Details

    EmergencyContactAction = function (action) {
        var data = {
            'PersonName': angular.element("#txtEMRPersonname").val(),
            'Relationship': angular.element("#ddlRelationship").val(),
            'PrimaryPhoneNumber': angular.element("#txtPrimary_Phone").val(),
            'AlternatePhoneNumber': angular.element("#txtAlternate_Phone").val(),
            'Address': angular.element("#txtAddress_in_Detail").val()
        }

        var path = "";
        if (action == "Update") {
            path = "/Employeeprofile/UpdateEmergencyContact";
        }
        else {
            path = "/Employeeprofile/SaveEmergencyContact";
        }
        if (path != "") {
            EmployeeProfileService.SaveRequest(data, path).then(function (response) {
                if (response.status == 200) {
                    $('#ModalPopUp').modal('hide');
                    notif({
                        msg: "<b>Success:</b> Well done Details Submitted Successfully",
                        type: "success"
                    });
                    $('#emp_emergencyContactModels').html(response.data);
                } else {

                    alert("Error");
                }
            });
        }
    }

    DeleteEmergencyContact = function (Eid) {
        var data = { 'ID': Eid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/DeleteEmergencyContact").then(function (response) {
            if (response.status == 200) {
                notif({
                    msg: "<b>Success:</b> Delete Record Successfully",
                    type: "success"
                });
                $('#emp_emergencyContactModels').html(response.data);
            } else {

                alert("Error");
            }
        });
    }

    EmergencyContactAddEdit = function (action, Eid) {
        var data = { "ID": Eid }
        var path = "";
        var button = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>';
        if (action == "Edit") {
            path = "/Employeeprofile/EditEmergencyContact"
            button += '<button type="submit" onclick="EmergencyContactAction(' + "'Update'" + ')" class="btn btn-primary">Save changes</button>';
        }
        else if (action == "Add") {
            path = "/Employeeprofile/EmergencyContactAddView"
            button += '<button type="submit" onclick="EmergencyContactAction(' + "'Save'" + ')" class="btn btn-primary">Save changes</button>';
        }
        else {
            path = "/Employeeprofile/EmergencyContactDetails";
        }
        if (path != "") {
            EmployeeProfileService.SaveRequest(data, path).then(function (response) {
                $('#ModalPopUp').modal('show');
                $(".modal-body").html(response.data);
                $(".modal-title").html('Emergency Contact');
                $(".modal-footer").html(button);
                $('#ddlRelationship').select2({
                    placeholder: 'Choose One',
                    dropdownParent: $('#ModalPopUp'),
                    width: '100%'
                });
            });
        }
    }

    //End




    //Address 

    AddressAction = function (action) {
        var data = {
            'AddressType': angular.element("#ddlType").val(),
            'Country': angular.element("#ddlHomeCountry").val(),
            'State': angular.element("#ddlHomeState").val(),
            'City': angular.element("#ddlHomeCity").val(),
            'Address': angular.element("#txtAddress").val(),
            'Landmark': angular.element("#txtHomeLandmark").val(),
            'PostalCode': angular.element("#txtPostalcode").val(),
            'ResidingSince': moment(angular.element("#txtHomeResidingSince").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
        }

        var path = "";
        if (action == "Update") {
            path = "/Employeeprofile/UpdateAddress";
        }
        else {
            path = "/Employeeprofile/SaveAddress";
        }
        if (path != "") {
            EmployeeProfileService.SaveRequest(data, path).then(function (response) {
                if (response.status == 200) {
                    $('#ModalPopUp').modal('hide');
                    notif({
                        msg: "<b>Success:</b> Well done Details Submitted Successfully",
                        type: "success"
                    });
                    $('#Address').html(response.data);
                } else {

                    alert("Error");
                }
            });
        }
    }

    DeleteAddress = function (Aid) {
        var data = { 'ID': Aid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/DeleteAddress").then(function (response) {
            if (response.status == 200) {
                notif({
                    msg: "<b>Success:</b> Delete Record Successfully",
                    type: "success"
                });
                $('#Address').html(response.data);
            } else {

                alert("Error");
            }
        });
    }

    AddressView = function (action, Eid) {

        var data = {
            "ID": Eid
        }
        var path = "";
        var button = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>';
        if (action == "Edit") {
            path = "/Employeeprofile/EditAddress"
            button += '<button type="submit" onclick="AddressAction(' + "'Update'" + ')" class="btn btn-primary">Save changes</button>';
        }
        else if (action == "Add") {
            path = "/Employeeprofile/AddressAddView"
            button += '<button type="submit" onclick="AddressAction(' + "'Save'" + ')" class="btn btn-primary">Save changes</button>';
        }
        else {
            path = "/Employeeprofile/AddressDetails";
        }
        if (path != "") {
            EmployeeProfileService.SaveRequest(data, path).then(function (response) {
                $('#ModalPopUp').modal('show');
                $(".modal-body").html(response.data);
                $(".modal-title").html('Address Contact');
                $(".modal-footer").html(button);
                $('#ddlHomeCountry').select2({
                    placeholder: 'Choose One',
                    dropdownParent: $('#ModalPopUp'),
                    width: '100%'
                });
                $('#ddlHomeState').select2({
                    placeholder: 'Choose One',
                    dropdownParent: $('#ModalPopUp'),
                    width: '100%'
                });
                $('#ddlType').select2({
                    placeholder: 'Choose One',
                    dropdownParent: $('#ModalPopUp'),
                    width: '100%'
                });
                $('#ddlHomeCountry').on("change", function () {
                    getState();
                })
                $('#txtHomeResidingSince').datetimepicker({
                    minView: 2,
                    format: 'dd-mm-yyyy',
                    gotoCurrent: true,
                    autoclose: true
                });
            });
        }
    }

    //End


    //Family Details 

    FamilyAction = function (action) {
        var data = {
            'PersonName': angular.element("#txtFamilyPersonname").val(),
            'Relationship': angular.element("#ddlRelationship").val(),
            'BirthPlace': angular.element("#txtBirth_Place").val(),
            'Age': angular.element("#txtAge").val(),
            'Gender': angular.element("#ddlgender").val(),
            'MediclaimCovered': angular.element("#ddlMediclaim").val(),
            'ContactNumber': angular.element("#txtContact_Number").val(),
            'BirthDate': moment(angular.element("#txtBirthDate").val(), 'DD-MM-YYYY').format("MM/DD/YYYY"),
        }

        var path = "";
        if (action == "Update") {
            path = "/Employeeprofile/UpdateFamilyDetails";
        }
        else {
            path = "/Employeeprofile/SaveFamilyDetails";
        }
        if (path != "") {
            EmployeeProfileService.SaveRequest(data, path).then(function (response) {
                if (response.status == 200) {
                    $('#ModalPopUp').modal('hide');
                    notif({
                        msg: "<b>Success:</b> Well done Details Submitted Successfully",
                        type: "success"
                    });
                    $('#emp_FamilyDetails').html(response.data);
                } else {

                    alert("Error");
                }
            });
        }
    }

    DeleteFamily = function (Fid) {
        var data = { 'ID': Fid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/DeleteFamilyDetails").then(function (response) {
            if (response.status == 200) {
                notif({
                    msg: "<b>Success:</b> Delete Record Successfully",
                    type: "success"
                });
                $('#emp_FamilyDetails').html(response.data);
            } else {

                alert("Error");
            }
        });
    }

    FamilyView = function (action, Fid) {

        var data = {
            "ID": Fid
        }
        var path = "";
        var button = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>';
        if (action == "Edit") {
            path = "/Employeeprofile/EditFamilyDetails"
            button += '<button type="submit" onclick="FamilyAction(' + "'Update'" + ')" class="btn btn-primary">Save changes</button>';
        }
        else if (action == "Add") {
            path = "/Employeeprofile/FamilyDetailsAddView"
            button += '<button type="submit" onclick="FamilyAction(' + "'Save'" + ')" class="btn btn-primary">Save changes</button>';
        }
        else {
            path = "/Employeeprofile/FamilyDetails";
        }
        if (path != "") {
            EmployeeProfileService.SaveRequest(data, path).then(function (response) {
                $('#ModalPopUp').modal('show');
                $(".modal-body").html(response.data);
                $(".modal-title").html('Family Details');
                $(".modal-footer").html(button);
                $('#ddlRelationship').select2({
                    placeholder: 'Choose One',
                    dropdownParent: $('#ModalPopUp'),
                    width: '100%'
                });
                $('#ddlgender').select2({
                    placeholder: 'Choose One',
                    dropdownParent: $('#ModalPopUp'),
                    width: '100%'
                });
                $('#ddlMediclaim').select2({
                    placeholder: 'Choose One',
                    dropdownParent: $('#ModalPopUp'),
                    width: '100%'
                });
                $('#txtBirthDate').datetimepicker({
                    minView: 2,
                    format: 'dd-mm-yyyy',
                    gotoCurrent: true,
                    autoclose: true
                });
                $('#txtBirthDate').on('change', function () {
                    var end = moment($(this).val(), 'DD/MM/YYYY');
                    var today = moment(new Date(), 'DD/MM/YYYY');
                    var age = Math.floor((today - end) / (365.25 * 24 * 60 * 60 * 1000));
                    $('#txtAge').val(age);
                });
            });
        }
    }

    //End


    //Medical Information

    MedicalInformationAction = function (action) {
        var data = {
            'MedicalConditionName': angular.element("#ddlDiseaseName").val(),
            'MedicalConditionFrom': angular.element("#ddlDisease").val(),
            'CurrentlyActive': $("input[name='CurrentlyActive']:checked").val(),
            'NeedSpecialAttention': $("input[name='NeedSpecialAttention']:checked").val(),
            'Notes': angular.element("#txtMedicalNotes").val()
        }

        var path = "";
        if (action == "Update") {
            path = "/Employeeprofile/UpdateMedicalInformation";
        }
        else {
            path = "/Employeeprofile/SaveMedicalInformation";
        }
        if (path != "") {
            EmployeeProfileService.SaveRequest(data, path).then(function (response) {
                if (response.status == 200) {
                    $('#ModalPopUp').modal('hide');
                    notif({
                        msg: "<b>Success:</b> Well done Details Submitted Successfully",
                        type: "success"
                    });
                    $('#emp_MedicalInformation').html(response.data);
                } else {

                    alert("Error");
                }
            });
        }
    }

    DeleteMedicalInformation = function (Fid) {
        var data = { 'ID': Fid }
        EmployeeProfileService.SaveRequest(data, "/Employeeprofile/DeleteMedicalInformation").then(function (response) {
            if (response.status == 200) {
                notif({
                    msg: "<b>Success:</b> Delete Record Successfully",
                    type: "success"
                });
                $('#emp_MedicalInformation').html(response.data);
            } else {

                alert("Error");
            }
        });
    }

    MedicalInformationView = function (action, Fid) {

        var data = {
            "ID": Fid
        }
        var path = "";
        var button = '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>';
        if (action == "Edit") {
            path = "/Employeeprofile/EditMedicalInformation"
            button += '<button type="submit" onclick="MedicalInformationAction(' + "'Update'" + ')" class="btn btn-primary">Save changes</button>';
        }
        else if (action == "Add") {
            path = "/Employeeprofile/MedicalInformationAddView"
            button += '<button type="submit" onclick="MedicalInformationAction(' + "'Save'" + ')" class="btn btn-primary">Save changes</button>';
        }
        else {
            path = "/Employeeprofile/MedicalInformation";
        }
        if (path != "") {
            EmployeeProfileService.SaveRequest(data, path).then(function (response) {
                $('#ModalPopUp').modal('show');
                $(".modal-body").html(response.data);
                $(".modal-title").html('Medical Information');
                $(".modal-footer").html(button);
                $('#ddlDiseaseName').select2({
                    placeholder: 'Choose One',
                    dropdownParent: $('#ModalPopUp'),
                    width: '100%'
                });
                $('#ddlDisease').select2({
                    placeholder: 'Choose One',
                    dropdownParent: $('#ModalPopUp'),
                    width: '100%'
                });
            });
        }
    }

    //End




});