var EmployeeApp = angular.module('EmployeeApp', ['EmployeeServiceModule', 'CommonAppUtility'])



EmployeeApp.controller('EmployeeController', function ($scope, $timeout, EmployeeService, CommonAppUtilityService) {

    
    $(function () {

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

       



        $('#datetimepicker').datetimepicker({
            minView: 2,
            format: 'dd-mm-yyyy',
            autoclose: true
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

    })
   
   
    

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

       CommonAppUtilityService.CreateItem("/Employee/getEmpdataByIdEdit",empid).then(function (response) {
            console.log(response);
          
            $scope.EmpInfo = response.data;
            
         //   $scope.firstname = $scope.EmpInfo.FirstName;
        });
    }

  
/*   EditEmployee = function (Eid) {
        var data = { "ID": Eid }
       EmployeeService.Postdata(data, "/Employee/EditBasicInfo").then(function (response) {

           $scope.eeee = response.data;

        //  $('#ModalPopupEdit').modal('show');
      //  $(".modal-body").html(response.data);

         //   $scope.ngtxtname = $scope.FirstName;
         // var button = '<button type="button" onclick="updateEmp()" class="btn btn-primary">Save changes</button>'+
           //    '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>';
           // $(".modal-title").html('Educational Details');
         // $(".modal-footer").html(button);
          
       });
    }   */
    
  


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

    
    $scope.empid = "0";

    $scope.updateEmp = function () {

        $scope.empid = $scope.EmpInfo[0].ID;

      var data = {

          ID : $scope.empid,

        //  'FirstName': angular.element("#txtFirst").val(),
          //  'LastName': angular.element("#txtlast").val(),
          FirstName:$scope.ngtxtfirstnameedit,
          Company: $scope.ngddlcompanyedit,
      //    LastName: $("#txtlast").val(),
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
    EmployeeService.Postdata(data,"/Employee/UpdateInfo").then(function (response) {
     //     CommonAppUtilityService.CreateItem(data, "/Employee/UpdateInfo").then(function (response) {


        });
    }  



    $scope.CancelAddpage = function () {
        Pageredirect("/Employee");
    }

   


    $scope.CancelViewpage = function () {
        Pageredirect("/Employee");
    }
    

    
    $scope.getcompany = function () {
        var aaa = {
            Id: 1
        }

        CommonAppUtilityService.CreateItem("/Employee/getcompany", aaa).then(function (response) {
            console.log(response);

            $scope.CompanyMaster = response.data;
        });
    }
    $scope.getcompany();
    $scope.getDesignation = function () {
        var aaa = {
            Id: 1
        }

        CommonAppUtilityService.CreateItem("/Employee/getDesignation", aaa).then(function (response) {
            console.log(response);

            $scope.DesignationMaster = response.data;
        });
    }
    $scope.getDesignation();
    $scope.AddEmployee = function () {

        $scope.ngddlcompanyedit = "";
    }




   EditEmployee = function (emId) {
       
        $scope.ngddlcompanyedit = "";
        $scope.ngddlDesination = "";
        var empid = {
            EId: emId

        }

        CommonAppUtilityService.CreateItem("/Employee/getEmpdataById", empid).then(function (response) {
            console.log(response);

            $scope.EmpInfo = response.data;

            //   $scope.empid = $scope.EmpInfo.ID;

            $scope.ngtxtfirstnameedit = $scope.EmpInfo[0].FirstName;
            $scope.ngddlDesignation = $scope.EmpInfo[0].DesignationId;
            //  $("#txtFirst_NameEdit").val($scope.EmpInfo[0].FirstName);
            //  $("#ngddlcompanyedit").val($scope.EmpInfo[0].CompanyId);
            //  $("#ngddlcompanyedit").val($scope.EmpInfo[0].CompanyId).trigger('change');

            for (var i = 0; i < $scope.CompanyMaster.length; i++) {
                if ($scope.EmpInfo[0].CompanyId == $scope.CompanyMaster[i].ID) {
                    $scope.ngddlcompanyedit = $scope.EmpInfo[0].CompanyId;
                      }
                    }
                for (var j = 0; j < $scope.DesignationMaster.length; j++) {
                    if ($scope.EmpInfo[0].DesignationId == $scope.DesignationMaster[j].ID) {
                        $scope.ngddlDesination = $scope.EmpInfo[0].DesignationId;
                    }
                }
          //  $scope.ngddlDepartment = $scope.EmpInfo[0].DepartmentId;

            for (var k = 0; k < $scope.EmpInfo.length; k++) {
                if ($scope.EmpInfo[k].ID == $scope.EmpInfo[k].ManagerId) {
                    $scope.ngddlManager = $scope.EmpInfo[k].ManagerId;
                }
            }
            });
  

       
    

    }   

  

   

  

    
});