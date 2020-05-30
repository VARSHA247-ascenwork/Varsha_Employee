﻿var EmployeeApp = angular.module('EmployeeApp', ['EmployeeServiceModule'])



EmployeeApp.controller('EmployeeController', function ($scope, EmployeeService) {

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
           // Manager: $scope.ngddlManager,
            OfficeEmail: $scope.ngtxtOfficemail,
            ContactNumber: $scope.ngtxtmobileno,
            EmpStatus: $scope.ngEmpStatus,

            Company: $scope.ngddlCompany,
            Designation: $scope.ngddlDesignation,
            Department: $scope.ngddlDepartment,
            Division: $scope.ngddlDivision,
            Region: $scope.ngddlRegion,
            Branch: $scope.ngddlBranch,
            User_Name: $scope.ngUser_Name
            
        }
        EmployeeService.Postdata(Items).then(function (response) {


        });
    }


    
});