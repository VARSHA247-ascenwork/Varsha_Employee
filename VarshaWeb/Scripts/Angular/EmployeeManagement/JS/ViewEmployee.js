var EmployeeDashboardApp = angular.module('EmployeeDashboardApp', ['ViewRequestServiceModule'])

EmployeeDashboardApp.controller('EmployeeDashboardController', function ($scope, $http, ViewEmployeeService) {





    $scope.GetHistory = function () {

        var url = "/NewRequest/GetCurrentUserHistory?SPHostUrl=https://ascenworktech.sharepoint.com/sites/Developer/";
        $http.get(url)
            .then(function (response) {
                $scope.LastHistory = response.data;
              
                var ht = "";
                for (var i = 0; i < $scope.LastHistory.length; i++) {


                    if (i == 0) {

                        ht += "<div class='card'>";
                        ht += "<div class='card-header' role='tab' id='heading-4'>";
                        ht += "<h6 class='mb-0'>";
                        ht += "<a data-toggle='collapse' href='#collapse-4' aria-expanded='false' aria-controls='collapse-4'>1-2-2020 To 5-2-2020";
                        ht += "</a></h6>";
                        ht += "</div>";
                        ht += "<div id='collapse-4' class='collapse' role='tabpanel' aria-labelledby='heading-4' data-parent='#accordion-2'>";
                        ht += "<div class='card-body'>";
                        ht += "You can pay for the product you have purchased using credit cards, debit cards, or via online banking. We also provide cash-on-delivery services for most of our products. You can also use your account wallet for payment.";
                        ht += "</div></div></div>";

                    }
                    if (i == 1) {

                        ht += "<div class='card'>";
                        ht += " <div class='card-header' role='tab' id='heading-5'>";
                        ht += "<h6 class='mb-0'>";
                        ht += "<a data-toggle='collapse' href='#collapse-5' aria-expanded='false' aria-controls='collapse-5'>1-2-2020 To 5-2-2020"
                        ht += "</a></h6>";
                        ht += "</div>";
                        ht += "<div id='collapse-5' class='collapse' role='tabpanel' aria-labelledby='heading-4' data-parent='#accordion-2'>"
                        ht += "<div class='card-body'>";
                        ht += "      You can pay for the product you have purchased using credit cards, debit cards, or via online banking. We also provide cash-on-delivery services for most of our products. You can also use your account wallet for payment.";
                        ht += "</div ></div ></div >";

                    }
                    if (i == 2) {

                        ht += "<div class='card'>";
                        ht += " <div class='card-header' role='tab' id='heading-6'>";
                        ht += "<h6 class='mb-0'>";
                        ht += "<a data-toggle='collapse' href='#collapse-6' aria-expanded='false' aria-controls='collapse-6'>1-2-2020 To 5-2-2020"
                        ht += "</a></h6>";
                        ht += "</div>";
                        ht += "<div id='collapse-6' class='collapse' role='tabpanel' aria-labelledby='heading-4' data-parent='#accordion-2'>"
                        ht += "<div class='card-body'>";
                        ht += "      You can pay for the product you have purchased using credit cards, debit cards, or via online banking. We also provide cash-on-delivery services for most of our products. You can also use your account wallet for payment.";
                        ht += "</div ></div ></div >";

                    }

                }

                //
                $("#accordion-2").append(ht);





            });

    }
    $scope.GetHistory();


    

    $scope.UpdateAction = function (Action, Status) {

        var data = {
            'ActionType': Action,
            'comment': $scope.ngtxtComment,
            'Status': Status
        }
       
        ViewRequestService.UpdateRequest(data).then(function (response) {
            console.log(response);
            if (response.status == 200) {
                alert("done");
                Pageredirect("/Approve");
            } else {

                alert("Error");
            }

        });
    }


    $scope.Cancal = function () {
        Pageredirect("/Approve");
    }
});