(function () {
    angular.module('LayoutApp').controller('Dashboard_v1_Controller', function ($scope, $http) {
        $scope.EmployeeZoneList = [];
        $scope.STPDropdownList = [];
        $scope.InspectionAgencyDropdownList = [];
        $scope.FormSubmitted = false;
        $scope.BusinessStartedYear = "2020-01-01";
        $scope.Calendar = "M";
        $scope.YearsList = [];
        $scope.QuartersList = [];
        $scope.MonthsList = [];
        $scope.DaysList = [];
        $scope.PerformanceDashboardData = [];

        var config = {
            headers: {
                'Content-Type': 'application/json'
            }
        };
        $scope.GetEmployeeZoneDropdown = function () {
            $http.get("/Master/GetZoneDropdownByEmployeeId", null).then(function (response) {
                $scope.EmployeeZoneList = response.data;
                $scope.GetSTPDropdown();
            }).catch(function (error) {
            });
        }
        $scope.GetEmployeeZoneDropdown();

        $scope.GetSTPDropdown = function () {
            $scope.ZoneIds = [];
            $scope.ZoneIds = $scope.Zones == undefined || $scope.Zones == '' ? $scope.EmployeeZoneList.map(x => x.zoneId) : $scope.Zones;
            var postData = {
                ZoneIds: $scope.ZoneIds,
            };
            $http.post("/Master/GetSTP", postData).then(function (response) {
                $scope.STPDropdownList = response.data;
                $scope.InspectionAgency();
            }).catch(function (error) {
                console.error('Error fetching data:', error);
            });
        }
        $scope.InspectionAgency = function () {
            $scope.ZoneIds = $scope.Zones == undefined || $scope.Zones == '' ? $scope.EmployeeZoneList.map(x => x.zoneId) : $scope.Zones;
            var postData = {
                ZoneIds: $scope.ZoneIds
            };

            $http.post("/Master/GetInspectionAgency", postData).then(function (response) {
                $scope.InspectionAgencyDropdownList = response.data;
                $scope.FilterData();
            }).catch(function (error) {
            });
        };

        $scope.CalendarChange = function () {
            $scope.YearsList = [];
            $scope.QuartersList = [];
            $scope.MonthsList = [];
            $scope.DaysList = [];
            $scope.FilterData();
        }

        $scope.DaysChange = function () {
            $scope.FilterData();
        }
        $scope.MonthsChange = function () {
            $scope.FilterData();
        }
        $scope.QuartersChange = function () {
            $scope.FilterData();
        }
        $scope.YearsChange = function () {
            $scope.FilterData();
        }
        $scope.STPChange = function () {
            $scope.FilterData();
        }
        $scope.InspectionAgencyChange = function () {
            $scope.FilterData();
        }

        $scope.IsDataLoading = false;
        $scope.FilterData = function () {
            $scope.resetChart();
            $scope.YearsList = [];
            $scope.QuartersList = [];
            $scope.MonthsList = [];
            $scope.DaysList = [];
            $scope.IsDataLoading = true;
            $scope.PerformanceDashboardData = [];
            if ($scope.Calendar == 'Y') {
                $(".year-list li input[type='checkbox']").each(function (i, v) {
                    if ($(this).prop("checked")) {
                        $scope.YearsList.push($(this).val())
                    }
                })
            }
            else if ($scope.Calendar == 'Q') {
                $(".quarters-list li input[type='checkbox']").each(function (i, v) {
                    var parentListItem = $(this).closest('.quarters-li');
                    var quarter = parentListItem.find('small').text().trim();
                    var year = parentListItem.closest('.dashboard-li').find('small').text().trim();
                    if ($(this).prop("checked")) {
                        $scope.QuartersList.push($(this).val() + '/' + $(this).attr("year"))
                    }
                })
            }
            else if ($scope.Calendar == 'M') {
                $(".months-list li input[type='checkbox']").each(function (i, v) {
                    if ($(this).prop("checked")) {
                        $scope.MonthsList.push($(this).val() + '/' + $(this).attr("year"))
                    }
                })
            }
            else if ($scope.Calendar == 'D') {
                $(".days-list li input[type='checkbox']").each(function (i, v) {
                    if ($(this).prop("checked")) {
                        $scope.DaysList.push($(this).val() + '/' + $(this).attr("month") + '/' + $(this).attr("year"))
                    }
                })
            }
            $scope.ZoneIds = $scope.Zones == undefined || $scope.Zones == '' ? $scope.EmployeeZoneList.map(x => x.zoneId) : $scope.Zones;
            $scope.STPIds = $scope.STPs == undefined || $scope.STPs == '' ? $scope.STPDropdownList.map(x => x.id) : $scope.STPs;
            $scope.TestingAgencyIds = $scope.InspectionAgencys == undefined || $scope.InspectionAgencys == '' ? $scope.InspectionAgencyDropdownList.map(x => x.id) : $scope.InspectionAgencys;
            var postData = {
                DateFilterType: $scope.Calendar,
                Years: $scope.YearsList,
                Quarters: $scope.QuartersList,
                Months: $scope.MonthsList,
                Days: $scope.DaysList,
                ZoneIds: $scope.ZoneIds,
                STPIds: $scope.STPIds,
                TestingAgencyIds: $scope.TestingAgencyIds,
            };

            $http.post("/Master/GetPerformanceDashboard", postData).then(function (response) {
                if (response.status) {
                    $scope.PerformanceDashboardData = response.data.data;
                } else {
                    $scope.PerformanceDashboardData = [];
                }
                $scope.IsDataLoading = false;
            }).catch(function (error) {
                $scope.IsDataLoading = false;
            });
            $scope.GetDashboardChartData(postData);
        }

        $scope.ActiveColor = "";
        $scope.ctrlKey = true;
        $scope.AllowSTPMultipleSelect = function () {
            $scope.FilterData();
            if ($scope.ctrlKey) {
                $scope.ctrlKey = false;
                $scope.ActiveColor = "";
            }
            else {
                $scope.ctrlKey = true;
                $scope.ActiveColor = "#eb8c23";
            }
        }

        $scope.toggleSelection = function (event, optionId) {
            if ($scope.ctrlKey) {               
            }
        };

        $scope.GetMonthFullName = function (monthIndex) {
            var monthNames = [
                'January', 'February', 'March', 'April', 'May', 'June',
                'July', 'August', 'September', 'October', 'November', 'December'
            ];
            return monthNames[monthIndex];
        }
        $scope.GetMonthSortName = function (monthIndex) {
            var monthNames = [
                'JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN',
                'JUL', 'AUG', 'SEP', 'OCT', 'NOV', 'DEC'
            ];
            return monthNames[monthIndex];
        }

        $scope.generateDays = function (startDate) {
            var yearsArray = [];
            var currentDate = new Date(startDate);
            while (currentDate.getFullYear() <= new Date().getFullYear()) {
                var year = currentDate.getFullYear();
                var monthsArray = [];
                while (currentDate.getFullYear() === year) {
                    var month = currentDate.getMonth();
                    var monthName = $scope.GetMonthSortName(month);
                    var daysArray = [];
                    var lastDay = new Date(year, month + 1, 0).getDate();
                    for (var day = 1; day <= lastDay; day++) {
                        daysArray.push(new Date(year, month, day));
                    }
                    monthsArray.push({ name: monthName, month: month + 1, days: daysArray });
                    currentDate.setMonth(month + 1);
                }
                yearsArray.push({ year: year, months: monthsArray });
            }
            $scope.yearsArray = yearsArray;
        };
        $scope.generateDays($scope.BusinessStartedYear);
        $scope.generateQuarters = function (startDate) {
            var yearsArray = [];
            var currentDate = new Date(startDate);
            function generateQuartersForYear(year) {
                var quartersArray = [];
                for (var quarter = 1; quarter <= 4; quarter++) {
                    var startMonth = (quarter - 1) * 3;
                    var endMonth = quarter * 3 - 1;
                    var monthsArray = [];
                    for (var month = startMonth; month <= endMonth; month++) {
                        var monthName = $scope.GetMonthSortName(month);
                        var daysArray = generateDaysForMonth(year, month);
                        monthsArray.push({ name: monthName, days: daysArray });
                    }
                    quartersArray.push({ quarter: 'Q' + quarter, months: monthsArray });
                }
                return quartersArray;
            }
            function generateDaysForMonth(year, month) {
                var daysArray = [];
                var lastDay = new Date(year, month + 1, 0).getDate();
                for (var day = 1; day <= lastDay; day++) {
                    daysArray.push(new Date(year, month, day));
                }
                return daysArray;
            }

            while (currentDate.getFullYear() <= new Date().getFullYear()) {
                var year = currentDate.getFullYear();
                var quartersArray = generateQuartersForYear(year);
                yearsArray.push({ year: year, quarters: quartersArray });
                currentDate.setFullYear(year + 1);
            }
            $scope.QuartersyearsArray = yearsArray;
        };
        $scope.generateQuarters($scope.BusinessStartedYear);

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
        };
        $scope.ClearCalendarFilter = function () {
            $scope.Calendar = "M";
            $(".datesSection input[type=checkbox]").each(function () {
                $(this).prop("checked", false);
            });
            $scope.FilterData();
        }
        $scope.ClearSTPFilter = function () {
            $('#Zones').prop('selectedIndex', 0);
            $('#STPs').prop('selectedIndex', 0);
            $('#InspectionAgencys').prop('selectedIndex', 0);
            $scope.Zones = "";
            $scope.STPs = "";
            $scope.InspectionAgencys = "";
            $scope.GetSTPDropdown();
            $scope.FilterData();
        }
        //----------------------------------------------------Added by SL000143(Maqsood)---------------------------------------------------------------------

        $scope.DashboardChartData = [];
        $scope.GetDashboardChartData = function (FilterData) {
            $http.post("/Admin/GetDashboardChartData", FilterData).then(function (response) {              
                $scope.DashboardChartData = response.data;
                $scope.GenerateCharts();
            }).catch(function (error) {
               
            });
        }
        //$scope.GetDashboardChartData();
        $scope.myChart = undefined;
        $scope.GenerateCharts = function () {

            if ($scope.DashboardChartData.length > 0) {
                angular.forEach($scope.DashboardChartData, function (Data, index) {
                    var convertedData = [];
                    angular.forEach(Data.yearlyData, function (yearlyData) {
                        var convertedYearData = {
                            Year: yearlyData.year,
                            Months: [],
                            Values: []
                        };
                        angular.forEach(yearlyData.months, function (month, i) {
                            var abbreviatedMonth = month.substring(0, 3);
                            convertedYearData.Months.push(abbreviatedMonth);
                            convertedYearData.Values.push(yearlyData.value[i]);
                        });
                        convertedData.push(convertedYearData);
                    });
                    var data = {
                        ChartData: convertedData
                    };
                    $scope.DrawChart(data, Data.chartName, index);
                });
            }
            else {

                $scope.DashboardChartData = [];
                $scope.DashboardChartData = [
                    {
                        chartName: "BODTrendChart",
                        yearlyData: [
                            {
                                year: '',
                                months: [],
                                value: []
                            }]
                    },
                    {
                        chartName: "TSSTrendChart",
                        yearlyData: [
                            {
                                year: '',
                                months: [],
                                value: []
                            }
                        ]
                    }
                    ,
                    {
                        chartName: "FCTrendChart",
                        yearlyData: [
                            {
                                year: '',
                                months: [],
                                value: []
                            }
                        ]
                    }
                    ,
                    {
                        chartName: "CODTrendChart",
                        yearlyData: [
                            {
                                year: '',
                                months: [],
                                value: []
                            }
                        ]
                    }
                    ,
                    {
                        chartName: "FlowTrendChart",
                        yearlyData: [
                            {
                                year: '',
                                months: [],
                                value: []
                            }
                        ]
                    }

                ];
                angular.forEach($scope.DashboardChartData, function (Data, index) {
                    var convertedData = [];
                    angular.forEach(Data.yearlyData, function (yearlyData) {
                        var convertedYearData = {
                            Year: yearlyData.year,
                            Months: [],
                            Values: []
                        };
                        angular.forEach(yearlyData.months, function (month, i) {
                            var abbreviatedMonth = month.substring(0, 3);
                            convertedYearData.Months.push(abbreviatedMonth);
                            convertedYearData.Values.push(yearlyData.value[i]);
                        });
                        convertedData.push(convertedYearData);
                    });
                    var data = {
                        ChartData: convertedData
                    };
                    $scope.DrawChart(data, Data.chartName, index);
                });
            }
        }      
        var colors = ["#fd7e14", "#21aee4", "#ffb366", "#6ad1ff", "#17a2b8"];

        $scope.DrawChart = function (chartdata, chartName, i) {
            var data = chartdata;
            var labels = [];
            let values = [];
            var datasets = [];
            data.ChartData.forEach((group, groupIndex) => {
                group.Months.forEach((month, monthIndex) => {
                    if ((monthIndex === Math.floor(group.Months.length / 2)))
                        labels.push((monthIndex === Math.floor(group.Months.length / 2)) ? group.Year + '\n\n\n\n\n\n\n\n\n\n\n' : month);
                    else {
                        labels.push(month);
                    }
                    values.push(group.Values[monthIndex]);
                });
            });

            var dataset = {
                label: "",
                data: values,
                borderDash: [0, 0],
                fill: false,
                pointStyle: 'circle',
                borderColor: '#000',
                backgroundColor: 'transparent',
                borderWidth: 1,
                pointRadius: 2,
                borderRadius: 5,
            }
            dataset.borderColor = colors[i];
            var ctx = document.getElementById(chartName);
            $scope.myChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: labels,
                    datasets: [dataset],
                },
                options: {
                    legend: {
                        display: false,
                    },
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true,
                            },
                            border: false
                        }],
                        yAxes: [{
                            ticks: {
                                 fontSize: values.length > 12 ? 8 : 10,
                            },
                            gridLines: {
                                display: true,
                            },
                        }],
                        xAxes: [{
                            ticks: {
                                fontSize: values.length > 12 ? 8 : 10,
                                maxRotation: 90,
                                 minRotation: 0
                            },
                            gridLines: {
                                display: false,
                                color: 'rgba(0, 0, 0, 0)'
                            },
                            border: false
                        }]
                    }
                }
            });
        }

        $scope.resetChart = function () {
            if ($scope.myChart) {
                $scope.myChart.destroy();
                var canvas = document.getElementById("FlowTrendChart");
                if (canvas) {
                    canvas.parentNode.removeChild(canvas);
                }
                var newCanvas = document.createElement("canvas");
                newCanvas.id = "FlowTrendChart";
                newCanvas.height = 67;
                document.getElementById("FlowTrendChartchartContainer").appendChild(newCanvas);

                var canvas = document.getElementById("BODTrendChart");
                if (canvas) {
                    canvas.parentNode.removeChild(canvas);
                }
                var newCanvas = document.createElement("canvas");
                newCanvas.id = "BODTrendChart";
                document.getElementById("BODTrendChartchartContainer").appendChild(newCanvas);

                var canvas = document.getElementById("CODTrendChart");
                if (canvas) {
                    canvas.parentNode.removeChild(canvas);
                }
                var newCanvas = document.createElement("canvas");
                newCanvas.id = "CODTrendChart";
                document.getElementById("CODTrendChartContainer").appendChild(newCanvas);

                var canvas = document.getElementById("TSSTrendChart");
                if (canvas) {
                    canvas.parentNode.removeChild(canvas);
                }
                var newCanvas = document.createElement("canvas");
                newCanvas.id = "TSSTrendChart";
                document.getElementById("TSSTrendChartchartContainer").appendChild(newCanvas);

                var canvas = document.getElementById("FCTrendChart");
                if (canvas) {
                    canvas.parentNode.removeChild(canvas);
                }
                var newCanvas = document.createElement("canvas");
                newCanvas.id = "FCTrendChart";
                document.getElementById("FCTrendChartchartContainer").appendChild(newCanvas);
            }
        }
    });
})();
