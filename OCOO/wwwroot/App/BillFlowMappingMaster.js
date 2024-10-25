(function () {
    angular.module('LayoutApp').controller('BillFlowMappingMasterController', function ($scope, $http) {
        $scope.MarkEmployeeList = [];
        $scope.ToForwardEmployeeList = [];
        $scope.EmployeeZoneList = [];
        $scope.CheckedMarkEmployees = [];
        $scope.CheckedForwardEmployees = [];
        $scope.FormSubmitted = false;

        $scope.DepartmentList = [];
        $scope.DesignationList = [];
        var config = {
            headers: {
                'Content-Type': 'application/json'
            }
        };

        $scope.GetDepartmentDropdown = function () {
            $http.get("/Master/GetDepartmentDropdown/").then(function (response) {
                $scope.DepartmentList = response.data;
                $scope.GetDesignationDropdown();

            }).catch(function (error) {
            });
        }
        $scope.GetDepartmentDropdown();

        $scope.GetDesignationDropdown = function (DepartmentId) {
            DepartmentId = DepartmentId == undefined ? 0 : DepartmentId;
            $http.get("/Master/GetDesignationDropdown/" + DepartmentId, null).then(function (response) {
                $scope.DesignationList = response.data;
            }).catch(function (error) {
            });
        }
        
        $scope.GetEmployeeZoneDropdown = function () {
            $http.get("/Master/GetZoneDropdownByEmployeeId", null).then(function (response) {
                $scope.EmployeeZoneList = response.data;
            }).catch(function (error) {
                console.error('Error fetching data:', error);
            });
        }
        $scope.GetEmployeeZoneDropdown();

        $scope.GetEmployeeDropdown = function () {
            var _filterData = {
                ZoneId: $scope.ZoneId,
                DepartmentId: $scope.DepartmentId,
                DesignationId: $scope.DesignationId

            };
            $http.post("/Master/GetEmployeeDropdown", _filterData).then(function (response) {
                $scope.MarkEmployeeList = response.data;
                $scope.ToForwardEmployeeList = response.data;
                $scope.GetBillFlowMapping();
            }).catch(function (error) {
                console.error('Error fetching data:', error);
            });
        }

        //$scope.GetEmployeeDropdown();
        $scope.MarkedEmployees = {};
        $scope.ForwardedEmployees = {};
        $scope.CheckedMarkEmployees = [];
        $scope.CheckedForwardEmployees = [];

        $scope.Save = function () {
            debugger
            $scope.FormSubmitted = true;
            $scope.CheckedMarkEmployees = [];
            $scope.CheckedForwardEmployees = [];
            $scope._BillFlowMappedEmployees = [];
            $(".bill-flow-mapping > tbody > tr").each(function (i, v) {
                debugger
                var _Employee = $(this).find("td:eq(5) input[type='checkbox']");
                if (_Employee.is(":checked")) {
                    $scope._BillFlowMappedEmployees.push({
                        EmployeeId: parseInt(_Employee.attr("employee-id")),
                        Forword: $(this).find("td:eq(5) input[type='checkbox']").is(":checked"),
                        DocumentVerification: $(this).find("td:eq(6) input[type='checkbox']").is(":checked"),
                        FinalApproval: $(this).find("td:eq(7) input[type='checkbox']").is(":checked")
                    });
                    $scope.CheckedMarkEmployees.push(parseInt(_Employee.attr("employee-id")));
                }
            });
            $(".mark-employee-card input[type=checkbox]").each(function (i, v) {
                if ($(this).is(":checked"))
                    $scope.CheckedMarkEmployees.push(parseInt($(this).attr("employee-id")));
            });
            $(".forward-employee-card input[type=checkbox]").each(function (i, v) {
                if ($(this).is(":checked"))
                    $scope.CheckedForwardEmployees.push(parseInt($(this).attr("employee-id")));
            });
            if ($scope.billflowmappingmasterForm.$valid && ($scope.CheckedMarkEmployees.length > 0 || $scope.CheckedForwardEmployees.length > 0 || $scope._BillFlowMappedEmployees.length > 0)) {

                var postData = {
                    ZoneId: $scope.ZoneId,
                    MarkedEmployeeIds: $scope.CheckedMarkEmployees,
                    ForwardedEmployeeIds: $scope.CheckedForwardEmployees,
                    BillFlowMappedEmployees: $scope._BillFlowMappedEmployees
                };
                $http.post("/Master/SaveBillFlowMapping", postData).then(function (response) {
                    fShowToast(response.data.message);
                }).catch(function (error) {
                    console.error('Error fetching data:', error);
                });
            }
            else {
                $scope.billflowmappingmasterForm.$submitted = true;
            }
        };
        $scope.GetBillFlowMapping = function () {
            if ($scope.ZoneId > 0) {

                $("#ForwardEmployeeAll").prop("checked", false);
                $("#MarkEmployeeAll").prop("checked", false);
                $scope.ResetCheckboxes();
                $http.get("/Master/GetBillFlowMapping?id=" + $scope.ZoneId, null).then(function (response) {
                    if (response.data != null && response.data.zoneId > 0) {
                        if (1 == 2) {
                            if (response.data.markedEmployeeIds.length == $scope.MarkEmployeeList.length)
                                $("#MarkEmployeeAll").prop("checked", true);
                            if (response.data.forwardedEmployeeIds.length == $scope.ToForwardEmployeeList.length)
                                $("#ForwardEmployeeAll").prop("checked", true);
                            $(".mark-employee-card input[type=checkbox]").each(function () {
                                var employeeId = parseInt($(this).attr("employee-id"));
                                if (response.data.markedEmployeeIds.includes(employeeId)) {
                                    $(this).prop("checked", true);
                                }
                            });
                            $(".forward-employee-card input[type=checkbox]").each(function () {
                                var employeeId = parseInt($(this).attr("employee-id"));
                                if (response.data.forwardedEmployeeIds.includes(employeeId)) {
                                    $(this).prop("checked", true);
                                }
                            });
                        }
                        console.clear();
                        angular.forEach(response.data.billFlowMappedEmployees, function (_, index) {                            
                            $("#forward" + _.employeeId).prop("checked", true);
                            if (_.documentVerification) {
                                $("#document" + _.employeeId).prop("checked", true);
                                $("#document" + _.employeeId).prop("disabled", false);
                            }
                            if (_.finalApproval) {
                                $("#final" + _.employeeId).prop("checked", true);
                                $("#final" + _.employeeId).prop("disabled", false);
                            }
                           
                        });

                    }
                    else {
                        $scope.ResetCheckboxes();
                    }
                }).catch(function (error) {
                    console.error('Error fetching data:', error);
                });
            }
            else {
                $scope.ResetCheckboxes();
            }
        }


        $scope.forwardcheckboxChanged = function (id) {
            debugger
            if ($("#forward" + id).is(":checked")) {
                $("#document" + id).prop("disabled", false);
                $("#final" + id).prop("disabled", false);
            }
            else {
                $("#document" + id).prop("disabled", true);
                $("#final" + id).prop("disabled", true);
                $("#document" + id).prop("checked", false);
                $("#final" + id).prop("checked", false);
            }
        }
        $scope.CheckUncheckAll = function (key) {
            if (key == "M") {
                if ($("#MarkEmployeeAll").is(":checked")) {
                    $(".mark-employee-card input[type=checkbox]").each(function () {
                        $(this).prop("checked", true);
                    });
                }
                else {
                    $(".mark-employee-card input[type=checkbox]").each(function () {
                        $(this).prop("checked", false);
                    });
                }
            }
            else if (key == "F") {
                if ($("#ForwardEmployeeAll").is(":checked")) {
                    $(".forward-employee-card input[type=checkbox]").each(function () {
                        $(this).prop("checked", true);
                    });
                } else {
                    $(".forward-employee-card input[type=checkbox]").each(function () {
                        $(this).prop("checked", false);
                    });
                }
            }
        }

        $scope.checkboxChanged = function (key) {
            if (key == "M") {
                var MarkEmployeeAllcheckedCount = 0;
                $(".mark-employee-card input[type=checkbox]").each(function () {
                    if ($(this).is(":checked"))
                        MarkEmployeeAllcheckedCount++;
                });
            }
            else if (key == "F") {
                var ForwardEmployeeAllcheckedCount = 0;
                $(".forward-employee-card input[type=checkbox]").each(function () {
                    if ($(this).is(":checked"))
                        ForwardEmployeeAllcheckedCount++;
                });
            }

            if (MarkEmployeeAllcheckedCount == $scope.MarkEmployeeList.length)
                $("#MarkEmployeeAll").prop("checked", true);
            else
                $("#MarkEmployeeAll").prop("checked", false);
            if (ForwardEmployeeAllcheckedCount == $scope.ToForwardEmployeeList.length)
                $("#ForwardEmployeeAll").prop("checked", true);
            else
                $("#ForwardEmployeeAll").prop("checked", false);

        }

        $scope.ResetCheckboxes = function () {
            $(".mark-employee-card input[type=checkbox]").each(function () {
                $(this).prop("checked", false);
            });
            $(".forward-employee-card input[type=checkbox]").each(function (i, v) {
                $(this).prop("checked", false);
            });
            $("#ForwardEmployeeAll").prop("checked", false);
            $("#MarkEmployeeAll").prop("checked", false);
        }
        $scope.Reset = function () {
            $scope.ZoneId = "";
            $(".mark-employee-card input[type=checkbox]").each(function () {
                if ($(this).is(":checked"))
                    $(this).prop("checked", false);
            });
            $(".forward-employee-card input[type=checkbox]").each(function (i, v) {
                if ($(this).is(":checked"))
                    $(this).prop("checked", false);
            });
            $scope.FormSubmitted = false;
            $("#ForwardEmployeeAll").prop("checked", false);
            $("#MarkEmployeeAll").prop("checked", false);
        };

    });
})();
