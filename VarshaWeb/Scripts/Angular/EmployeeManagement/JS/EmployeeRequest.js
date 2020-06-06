var EmployeeApp = angular.module('EmployeeApp', ['EmployeeServiceModule'])



EmployeeApp.controller('EmployeeController', function ($scope, EmployeeService) {


    // validate function
    $(function () {
        'use strict'
        //validation
        $('#AddEmployeeDetailForm').parsley().on('field:validated', function () {
            var ok = $('.parsley-error').length === 0;
            $('.bs-callout-info').toggleClass('hidden', !ok);
            $('.bs-callout-warning').toggleClass('hidden', ok);
        })
            .on('form:submit', function () {
                $scope.Save();
               // $scope.updateEmp();
                return false;
            });
    });

    $scope.Save = function () {
        var Items = {
            FirstName: $scope.ngtxtfirstname,
            MiddleName: $scope.ngtxtMiddlename,
            LastName: $scope.ngtxtlastname,
            EmpCode: $scope.ngtxtEmpcode,
            JoiningDate: moment($scope.ngtxtJoiningdate, 'DD-MM-YYYY').format("MM/DD/YYYY"),
            DOB: moment($scope.ngtxtDOB, 'DD-MM-YYYY').format("MM/DD/YYYY"),
            Gender: $scope.ngddlGender,
            MaritalStatus: $scope.ngdddlMarialstatus,
            OnProbationTill: moment($scope.ngtxtProbationtill, 'DD-MM-YYYY').format("MM/DD/YYYY"),
            ProbationStatus: $scope.ngddlProbationstatus,
            Manager: $scope.ngddlManager,
          //  Manager_Code: $scope.managerempcode,
            OfficeEmail: $scope.ngtxtOfficemail,
            ContactNumber: $scope.ngtxtmobileno,
           // EmpStatus: $scope.ngEmpStatus,

            Company: $scope.ngddlCompany,
            Designation: $scope.ngddlDesignation,
            Department: $scope.ngddlDepartment,
            Division: $scope.ngddlDivision,
            Region: $scope.ngddlRegion,
            Branch: $scope.ngddlBranch,
            User_Name: $scope.ngddluser,
            
        }
        EmployeeService.Postdata(Items).then(function (response) {


        });
    }


  /*  $scope.updateEmp = function () {
        var Items = {
            ID: $scope.ID,
            FirstName: $scope.ngtxtfirstname,
            MiddleName: $scope.ngtxtMiddlename,
            LastName: $scope.ngtxtlastname,
            EmpCode: $scope.ngtxtEmpcode,
            

        }
        EmployeeService.EmpPostdata(Items, ID).then(function (response) {


        });
    }*/
   
    $scope.addEmployee = function () {
        Pageredirect("/EmployeeDashboard/AddEmployeeBasicInfo");
    }

  /* $scope.EditEmployee = function (id) {
       
        var data = {
            'ID': id,
        }

        CommonAppUtilityService.CreateItem("/EmployeeDashboard/getEmployeeEditId", data).then(function (response) {
            Pageredirect("/EmployeeDashboard/EmployeeEdit");
        });

    }  */


  

    
});