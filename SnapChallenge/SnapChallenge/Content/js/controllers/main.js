snapChallenge
    .controller("snapChallengeClientController", function($rootScope, $scope, $http, $timeout, growlService)
    {
        $scope.teacher = {};

        $http.get("teacher/getteacher/1")
            .success(function(result)
            {
                $scope.teacher = result;
                growlService.growl("Welkom " + $scope.teacher.FullName, "inverse");
            });

        $scope.sidebarToggle =
        {
            left: false,
            right: false
        };

        $scope.closeSidebars = function()
        {
            $rootScope.changeDashboard();
            $scope.sidebarToggle.left = false;
            $scope.sidebarToggle.right = false;
        };

        $scope.toggleSidebarLeft = function()
        {
            $scope.sidebarToggle.left = !$scope.sidebarToggle.left;
        };

        $scope.toggleSidebarRight = function()
        {
            $scope.sidebarToggle.right = !$scope.sidebarToggle.right;
        };

        $rootScope.dashboardType =
        {
            student: false,
            teacher: false,
            welcome: true
        };

        $rootScope.changeDashboard = function(type)
        {
            $rootScope.dashboardType.student = false;
            $rootScope.dashboardType.teacher = false;
            $rootScope.dashboardType.welcome = false;

            switch (type)
            {
                case "student":
                    $rootScope.dashboardType.student = true;
                    break;
                case "teacher":
                    $rootScope.dashboardType.teacher = true;
                    break;
                default:
                    $rootScope.dashboardType.welcome = true;
                    break;
            }
        };

        // Detact Mobile Browser
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent))
        {
            angular.element("html").addClass("ismobile");
        }

        $scope.refreshImportDetails = function()
        {
            $http.get("progress/getprogress")
                .success(function(result)
                {
                    $scope.importDetails = result;

                    if ($scope.importDetails.todo > 0)
                    {
                        $timeout($scope.refreshImportDetails, 2500);
                    }
                });
        };

        $scope.importDetails = {};
        $scope.refreshImportDetails();
    })
    .controller("sidebarLeftClientController", function($rootScope, $scope, $http)
    {
        $scope.selectOverview = function(overview)
        {
            $rootScope.changeDashboard("teacher");
            $rootScope.selectedOverview = overview;
        };
    })
    .controller("sidebarRightClientController", function($rootScope, $scope, $http)
    {
        $scope.students = [];

        $scope.searchStudents = function(searchStudent)
        {
            if (searchStudent === "" || searchStudent == null)
            {
                $http.get("student/getstudents")
                    .success(function(result)
                    {
                        $scope.students = result;
                    });
            }
            else
            {
                $http.get("student/findstudents/" + searchStudent)
                    .success(function(result)
                    {
                        $scope.students = result;
                    });
            }
        };

        $scope.searchStudents(null);

        $scope.selectStudent = function(student)
        {
            $rootScope.changeDashboard("student");
            $rootScope.selectedStudent = student;
        };
    })
    .controller("dashboardWelcomeClientController", function()
    {
    })
    .controller("dashboardTeacherClientController", function($rootScope, $scope, $http)
    {
        $scope.clientModels = [];

        function updateResults()
        {
            var clientModelUrl = "";

            switch ($rootScope.selectedOverview)
            {
                case "today":
                    clientModelUrl = "dashboard/gettodayresults";
                    break;
                case "yesterday":
                    clientModelUrl = "dashboard/getyesterdayresults";
                    break;
                case "all":
                    clientModelUrl = "dashboard/getallresults";
                    break;
                default:
                    clientModelUrl = "dashboard/gettodayresults";
                    break;
            }

            $http.get(clientModelUrl)
                .success(function(result)
                {
                    $scope.clientModels = result;
                });
        }

        $scope.$watch("selectedOverview", function()
        {
            updateResults();
        });

        $scope.getGridHeight = function(length)
        {
            return {
                height: (length * 30 + 60) + "px"
            };
        };
    })
    .controller("dashboardStudentClientController", function($rootScope, $scope, $http)
    {
        $scope.showType = "today";
        $scope.studentData = [];

        function updateResults()
        {
            var clientModelUrl = "";

            switch ($scope.showType)
            {
                case "today":
                    clientModelUrl = "student/getstudenttodayresults/" + $rootScope.selectedStudent.ID;
                    break;
                case "all":
                    clientModelUrl = "student/getstudentallresults/" + $rootScope.selectedStudent.ID;
                    break;
                default:
                    clientModelUrl = "student/getstudenttodayresults/" + $rootScope.selectedStudent.ID;
                    break;
            }

            $http.get(clientModelUrl)
                .success(function(result)
                {
                    $scope.studentData = result;
                });
        }

        $scope.$watch("selectedStudent", function()
        {
            updateResults();
        });

        $scope.setShowType = function(type)
        {
            $scope.showType = type;
            updateResults();
        };
    });