var EmployeeApp = angular.module('EmployeeApp', ['EmployeeServiceModule', 'CommonAppUtility'])

EmployeeApp.controller('EmployeeController', function ($scope, $timeout, EmployeeService, CommonAppUtilityService) {

    
    $(function () {

        $('.select2').select2({
            placeholder: 'Choose one',
            searchInputPlaceholder: 'Search',
            width: '100%',
            dropdownParent: $("#ModalPopUp")

        }); 

        $('#txtjoiningDate').datetimepicker({
            minView: 2,
            format: 'dd-mm-yyyy',
            autoclose: true
        });

        $('#txtProbationaltill').datetimepicker({
            minView: 2,
            format: 'dd-mm-yyyy',
            autoclose: true
        });
        $("#txtProbationaltill").on('change', function () {
            var start = moment(angular.element("#txtjoiningDate").val(), 'DD/MM/YYYY');
            var end = moment($(this).val(), 'DD/MM/YYYY');
            if (end < start) {
                alert('Probation Date greater than Joining Date ');
                $(this).val('');
            }
        });
        setTimeout(function () {


            $('#Employeetable').DataTable({

                responsive: true,
                language: {
                    searchPlaceholder: 'Search...',
                    sSearch: '',
                    lengthMenu: '_MENU_ ',
                }

            });
        }, 2000);
        //Validation
        $('#AddEmployeeDetailForm').parsley().on('field:validated', function () {
            var ok = $('.parsley-error').length === 0;
            $('.bs-callout-info').toggleClass('hidden', !ok);
            $('.bs-callout-warning').toggleClass('hidden', ok);
        })
            .on('form:submit', function () {
                $scope.Save();
                $scope.updateEmp();
               

                return false;
            });

      //  $scope.ViewEmployee();
      //  $scope.EditEmployee();
    })
   
   
    $scope.getdate = function () {
        var firstjoiningdate = moment($("#txtjoiningDate").val(), 'DD/MM/YYYY');
        firstjoiningdate.add({ months: 6 });
        var newdate = moment(firstjoiningdate).format('DD-MM-YYYY');
        $("#txtProbationaltill").val(newdate);
    }

    $scope.getdataEmployee = function () {
        var ApplyAssetObj = {
            Id: 1
        }
        CommonAppUtilityService.CreateItem("/EmployeeNew/getEmpdata", ApplyAssetObj).then(function (response) {
            console.log(response);

            $scope.empdata = response.data;
        });
    }
        $scope.getdataEmployee();  

    $scope.ViewEmployee = function (emId) {
        var empid = {
            EId: emId
        }
        CommonAppUtilityService.CreateItem("/EmployeeNew/getEmpdataById", empid).then(function (response) {
            console.log(response);
            $scope.EmpInfo = response.data;
        });
    }

    $scope.Save = function () {
        var data = {

            FirstName: $scope.ngtxtfirstname,
            MiddleName: $scope.ngtxtMiddlename,
            LastName: $scope.ngtxtlastnameadd,
            JoiningDate: moment($scope.ngtxtJoiningdate, 'DD-MM-YYYY').format("MM/DD/YYYY"),
            DOB: moment($scope.ngtxtDOB, 'DD-MM-YYYY').format("MM/DD/YYYY"),
            Gender: $scope.ngddlGender,
            MaritalStatus: $scope.ngdddlMarialstatus,
            OnProbationTill: moment($("#txtProbationaltill").val(), 'DD-MM-YYYY').format("MM-DD-YYYY"),
           // OnProbationTill: moment($scope.ngtxtProbationtill, 'DD-MM-YYYY').format("MM/DD/YYYY"),
            ProbationStatus: $scope.ngddlProbationstatus,
            Manager: $scope.ngddlManagerAdd,
            OfficeEmail: $scope.ngtxtOfficemail,
            ContactNumber: $scope.ngtxtContact,
            MobileNo: $scope.ngtxtmobile,
            Company: $scope.ngddlcompany,
            Designation: $scope.ngddlDesignation,
            Department: $scope.ngddlDepartment,
            Division: $scope.ngddlDivision,
            Region: $scope.ngddlRegion,
            Branch: $scope.ngddlBranch,
            DOB_Months: moment($scope.ngtxtDOB, 'DD-MM-YYYY').format("MM"),
            JoiningDate_Month: moment($scope.ngtxtJoiningdate, 'DD-MM-YYYY').format("MM"),
            User_Name: $scope.ngddluser, 
        }
        EmployeeService.Postdata(data, "/EmployeeNew/SaveInfo").then(function (response) {
            if (response.status == 200) {
                $("#global-loader").hide();
                $('#modaldemo4').modal('show');
            } else {
                alert("Error");
            }

        });
    }
    $scope.PerformAction = function () {
        Pageredirect("/EmployeeNew");
    }

    $scope.updateEmp = function () {
       // document.getElementById("#txtLast_Name").required = false;
     //   $('#txtLast_Name').removeAttr('required'),
      //  $('#ddlUsername').removeAttr('required'),
      //  $('#ddlManageradd').removeAttr('required'),
        $scope.empid = $scope.EmpInfo[0].ID;
        var data = {
            ID: $scope.empid,
            //FirstName: $scope.ngtxtfirstname,
            //MiddleName: $scope.ngtxtMiddlename,
            //LastName: $scope.ngtxtlastnameadd,
            JoiningDate: moment($scope.ngtxtJoiningdate, 'DD-MM-YYYY').format("MM/DD/YYYY"),
            DOB: moment($scope.ngtxtDOB, 'DD-MM-YYYY').format("MM/DD/YYYY"),
            Gender: $scope.ngddlGender,
            MaritalStatus: $scope.ngdddlMarialstatus,
            OnProbationTill:moment($scope.ngtxtProbationtill, 'DD-MM-YYYY').format("MM/DD/YYYY"),
            ProbationStatus: $scope.ngddlProbationstatus,
            Manager: $scope.ngddlManager,
            OfficeEmail: $scope.ngtxtOfficemail,
            ContactNumber: $scope.ngtxtContact,
            MobileNo: $scope.ngtxtmobile,
            Company: $scope.ngddlcompany,
            Designation: $scope.ngddlDesignation,
            Department: $scope.ngddlDepartment,
            Division: $scope.ngddlDivision,
            Region: $scope.ngddlRegion,
            Branch: $scope.ngddlBranch, 
            DOB_Months: moment($scope.ngtxtDOB, 'DD-MM-YYYY').format("MM"),
            JoiningDate_Month: moment($scope.ngtxtJoiningdate, 'DD-MM-YYYY').format("MM"),
           // User_Name: $scope.ngddluser, 
        }
        EmployeeService.Postdata(data, "/EmployeeNew/UpdateInfo").then(function (response) {
            if (response.status == 200) {
                $("#global-loader").hide();
                $('#modaldemo4').modal('show');
            } else {
                alert("Error");
            }
           
        });
    }
    
    $scope.EditEmployee = function (emId) {
        $scope.makeReadOnly = true;  
        $scope.addMiddlename = false;
        $scope.addLastname = false;
        $scope.viewlastname = true;
        $scope.viewEmpcode = true;
        $scope.editEmployee = true;
        $scope.addEmployee = false;
        $scope.UpdateDetail = true;
        $scope.AddDetail = false;
        $scope.AddManager = false;
        $scope.viewmanager = true;
        $scope.ViewUsername = true;
        $scope.Username = false;
        $scope.ngddlcompany = "";
        $scope.ngddlDesignation = "";
        $scope.ngddlDepartment = "";
        $scope.ngddlRegion = "";
        $scope.ngddlDivision = "";
        $scope.ngddlBranch = "";
        $scope.ngddlGender = "";
        $scope.ngdddlMarialstatus = "";
        $scope.ngddlProbationstatus = "";
        $scope.ngddlManager = "";
        
        var empid = {
            EId: emId
        }

        CommonAppUtilityService.CreateItem("/EmployeeNew/getEmpdataById", empid).then(function (response) {
            console.log(response);

            $scope.EmpInfo = response.data;

            $scope.ngtxtfirstname = $scope.EmpInfo[0].FirstName;
            $scope.ngtxtMiddlename = $scope.EmpInfo[0].MiddleName;
            $scope.ngtxtlastname = $scope.EmpInfo[0].LastName;
            $scope.ngtxtEmpcode = $scope.EmpInfo[0].EmpCode;
            $scope.ngtxtDOB = $scope.EmpInfo[0].DOB;
            $scope.ngtxtJoiningdate = $scope.EmpInfo[0].JoiningDate;
            $scope.ngtxtProbationtill = $scope.EmpInfo[0].OnProbationTill;
            $scope.ngtxtusername = $scope.EmpInfo[0].User_Name;
            $scope.ngtxtmobile = $scope.EmpInfo[0].MobileNo;
            $scope.ngtxtContact = $scope.EmpInfo[0].ContactNumber;
            $scope.ngtxtOfficemail = $scope.EmpInfo[0].OfficeEmail;
           
            $timeout(function () {
                $("#ddlCompany").val($scope.EmpInfo[0].CompanyId).trigger('change');
                $("#ddlDesignation").val($scope.EmpInfo[0].DesignationId).trigger('change');
                $("#ddlDepartment").val($scope.EmpInfo[0].DepartmentId).trigger('change');
                $("#ddlDivision").val($scope.EmpInfo[0].DivisionId).trigger('change');
                $("#ddlRegion").val($scope.EmpInfo[0].RegionId).trigger('change');
                $("#ddlBranch").val($scope.EmpInfo[0].BranchId).trigger('change');
                $("#ddlGender").val($scope.EmpInfo[0].Gender).trigger('change');
                $("#dddlMarialStatus").val($scope.EmpInfo[0].MaritalStatus).trigger('change');
                $("#ddlProbationStatus").val($scope.EmpInfo[0].ProbationStatus).trigger('change');
                $("#ddlManager").val($scope.EmpInfo[0].ManagerId).trigger('change');
            },20)
        });
    }

    $scope.AddEmployee = function () {
        $scope.makeReadOnly = false;
        $scope.viewlastname = false;
        $scope.viewEmpcode = false;
        $scope.addMiddlename = true;
        $scope.addLastname = true;
        $scope.UpdateDetail = false;
        $scope.AddDetail = true;
        $scope.editEmployee = false;
        $scope.addEmployee = true;
        $scope.AddManager = true;
        $scope.viewmanager = false;
        $scope.ViewUsername = false;
        $scope.Username = true;
        $scope.ngddlcompany = "";
        $scope.ngddlDesignation = "";
        $scope.ngddlDepartment = "";
        $scope.ngddlRegion = "";
        $scope.ngddlDivision = "";
        $scope.ngddlBranch = "";
        $scope.ngddlGender = "";
        $scope.ngdddlMarialstatus = "";
        $scope.ngddlProbationstatus = "";
        $scope.ngddlManager = "";
        $scope.ngtxtfirstname = "";
        $scope.ngtxtMiddlename ="";
        $scope.ngtxtlastname = "";
        $scope.ngtxtDOB ="";
        $scope.ngtxtJoiningdate ="";
        $scope.ngtxtProbationtill = "";
        $scope.ngtxtmobile = "";
        $scope.ngtxtContact ="";
        $scope.ngtxtOfficemail = "";
    }
});