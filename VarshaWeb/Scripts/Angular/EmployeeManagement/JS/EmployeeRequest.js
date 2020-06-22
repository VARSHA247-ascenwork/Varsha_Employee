var EmployeeApp = angular.module('EmployeeApp', ['EmployeeServiceModule', 'CommonAppUtility'])



EmployeeApp.controller('EmployeeController', function ($scope, $timeout, EmployeeService, CommonAppUtilityService) {



    $('#datetimepicker').datetimepicker({
        minView: 2,
        format: 'dd-mm-yyyy',
        autoclose: true
    });

    $('#datetimepicker1').datetimepicker({
        minView: 2,
        format: 'dd-mm-yyyy',
        autoclose: true
    });

    $('#datetimepicker2').datetimepicker({
        minView: 2,
        format: 'dd-mm-yyyy',
        autoclose: true
    });

    

    $scope.getdataEmployee = function () {
        var ApplyAssetObj = {
            Id: 1
        }

        CommonAppUtilityService.CreateItem("/Employee/getEmpdata", ApplyAssetObj).then(function (response) {
            console.log(response);

            $scope.empdata = response.data;
           
           

        });
    }
    $scope.getdataEmployee();

  

  


   ViewEmployee = function (emId) {
        var empid = {
            EId: emId
        }

        CommonAppUtilityService.CreateItem("/Employee/getEmpdataById",empid).then(function (response) {
            console.log(response);

            $scope.EmpInfo = response.data;

         //   $scope.firstname = $scope.EmpInfo.FirstName;
        });
    }

  

    EditEmployee = function (Eid) {
        var data = { "ID": Eid }
        EmployeeService.Postdata(data,"/Employee/EditBasicInfo").then(function (response) {
            $('#ModalPopupEdit').modal('show');
            $(".modal-body").html(response.data);
            var button = '<button onclick="updateEmp()" class="btn btn-primary">Save changes</button>'+
                '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>';
           // $(".modal-title").html('Educational Details');
            $(".modal-footer").html(button);
          
        });
    }
    
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


              //  $scope.updateEmp();
                $scope.Save();

                return false;
            });

    });


  $scope.getdate = function () {


         var firstjoiningdate = moment($("#datetimepicker").val(), 'DD/MM/YYYY');
         
                 firstjoiningdate.add({ months: 6 });

                   //  var firstjoiningdate = new Date();
                  //   var firstjoiningdate = formatDate(firstjoiningdate);
                   var newdate = moment(firstjoiningdate).format('DD-MM-YYYY');
                        $("#datetimepicker2").val(newdate);


                   //     (moment("#datetimepicker2").val(),'DD/MM/YYYY');
    }



$scope.Save = function () {

        var Items = {
            FirstName: $scope.ngtxtfirstname,
            MiddleName: $scope.ngtxtMiddlename,
            LastName: $scope.ngtxtlastname,
          
            JoiningDate: moment($scope.ngtxtJoiningdate, 'DD-MM-YYYY').format("MM/DD/YYYY"),
            DOB: moment($scope.ngtxtDOB, 'DD-MM-YYYY').format("MM/DD/YYYY"),
            Gender: $scope.ngddlGender,
            MaritalStatus: $scope.ngdddlMarialstatus,
            OnProbationTill: moment($("#datetimepicker2").val(), 'DD-MM-YYYY').format("MM-DD-YYYY"),
          
            ProbationStatus: $scope.ngddlProbationstatus,
          Manager: $scope.ngddlManager,
         
            OfficeEmail: $scope.ngtxtOfficemail,
            ContactNumber: $scope.ngtxtmobileno,
            MobileNo: $scope.ngtxtmobile,
            Company: $scope.ngddlCompany,
           Designation: $scope.ngddlDesignation,
            Department: $scope.ngddlDepartment,
            Division: $scope.ngddlDivision,
            Region: $scope.ngddlRegion,
            Branch: $scope.ngddlBranch,
            DOB_Months: moment($scope.ngtxtDOB, 'DD-MM-YYYY').format("MM"),
            JoiningDate_Month: moment($scope.ngtxtJoiningdate, 'DD-MM-YYYY').format("MM"),
            User_Name: $scope.ngddluser,  
            
        }
    EmployeeService.Postdata(Items,"/Employee/SaveInfoEmp").then(function (response) {
            if (response.status == 200) {
                $("#global-loader").hide();
                $('#modaldemo4').modal('show');
            } else {
                alert("Error");
            }

        });
    }

    $scope.PerformAction = function () {
        Pageredirect("/Employee/Index");
    }

    
   
  updateEmp = function () {
      var data = {


          'FirstName': angular.element("#txtFirst").val(),
          'LastName': angular.element("#last").val(),
         // FirstName: $('#txtfirstName').val(),
          
       //   LastName: $('#last').val(),
            //  MiddleName: $('#txtMiddle_Name').val(),
       //   LastName: $('#last').val(),
            /*  JoiningDate: moment($("#txtjoiningdate").val(),'DD-MM-YYYY').format("MM-DD-YYYY"),
              DOB: moment($("#datetimepicker1").val(),'DD-MM-YYYY').format("MM-DD-YYYY"),
              Gender:$("#ddlgender option:selected").text(),
              MaritalStatus:$("#ddlMarialstatus option:selected").text(),
              OnProbationTill: moment($("#datetimepicker2").val(),'DD-MM-YYYY').format("MM-DD-YYYY"),
              ProbationStatus: $("#ddlProbationstatus option:selected").text(),
              Manager: $('#ddlManager').val(),
              OfficeEmail: $('#txtOfficemail').val(),
              ContactNumber: $('#txtContact').val(),
              MobileNo: $('#phone').val(),
              Company:$('#ddlCompany').val(),
              Designation: $('#ddlDegination').val(),
              Department: $('#ddlDepartment').val(),
              Division: $('#ddlDivision').val(),
              Region: $('#ddlRegion').val(),
              Branch: $('#ddlBranch').val(), */
            
        }
    EmployeeService.Postdata(data, "/Employee/UpdateInfo").then(function (response) {
     //     CommonAppUtilityService.CreateItem(data, "/Employee/UpdateInfo").then(function (response) {


        });
    }  



    $scope.CancelAddpage = function () {
        Pageredirect("/Employee");
    }

   /*  $scope.editmsg = function () {
        Pageredirect("/Employee/Index");
    }

   $scope.addEmployee = function () {
        Pageredirect("/Employee/Index");
    } 
    
    
     $scope.cancelEditpage = function () {
        Pageredirect("/Employee");
    }
    */


    $scope.CancelViewpage = function () {
        Pageredirect("/Employee");
    }
    

   


  /* $scope.EditEmployee = function (id) {
       
        var data = {
            'ID': id,
        }

        CommonAppUtilityService.CreateItem("/EmployeeDashboard/getEmployeeEditId", data).then(function (response) {
            Pageredirect("/EmployeeDashboard/EmployeeEdit");
        });

    }  */

    setTimeout(function () {
        $(function () {
          
            $('#Employeetable').DataTable({

                responsive: true,
                language: {
                    searchPlaceholder: 'Search...',
                    sSearch: '',
                    lengthMenu: '_MENU_ ',
                }
            });
        });
    }, 2000);

  

    
});