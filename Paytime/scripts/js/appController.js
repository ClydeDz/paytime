/*
    List controller
*/
angular.module("paytime")
    .controller("ListController", ['$scope', '$http', function ($scope, $http) {
        $scope.loading = true;
        $scope.ajaxStatus = true;
        $scope.urlList = {};
        $scope.isAdmin = true;
        $scope.TestFooter = "G";
        //$http.get("api/Identity/isAdmin")
        //    .then(function (response) {
        //        $scope.isAdmin = response;
        //    },
        //    function (error) {
        //        //console.log(error);
        //        $scope.isAdmin = false;
        //    });

        $http.get("/api/Events")
            .then(function (response) {
                //console.log(response);
                $scope.urlList = response.data;
                $scope.ajaxStatus = true;
                $scope.loading = false;

            },
            function (error) {
                $scope.userTabFlag = false;
                //console.log("error HERE");
                $scope.ajaxStatus = false;
                $scope.loading = false;
            });      

    }]);

/*
    Add controller
*/
angular.module("paytime")
    .controller("AddController", ['$scope', '$http', function ($scope, $http) {
        $scope.pageType = "Add";

        $scope.title = "";
        $scope.shortDescription = "";
        $scope.longDescription = "";
        $scope.recurrenceRule = "";
        $scope.startDate = new Date(new Date().toISOString().substring(0, new Date().toISOString().length - 1))
        $scope.endDate = new Date(new Date().toISOString().substring(0, new Date().toISOString().length-1))
        $scope.reminderMode = "Mobile Only";
        $scope.submitStatus = {
            'display': false,
            'flag': true,
            'message': ""
        };
        $scope.isAdmin = true;
        //$http.get("api/Identity/isAdmin")
        //    .then(function (response) {
        //        $scope.isAdmin = response.data;
        //    },
        //    function (error) {
        //        //console.log(error);
        //        $scope.isAdmin = false;
        //    });

        $scope.addRecord = function (x) {
            if (x == $scope.pageType && $scope.isUnique == true && $scope.isAdmin == true) {
                $scope.postData = {
                    'Id':0,
                    'Title':$scope.title,
                    'ShortDescription':$scope.shortDescription,
                    'LongDescription':$scope.longDescription,
                    'RecurrenceRule':$scope.recurrenceRule,
                    'StartDate':$scope.startDate,
                    'EndDate':$scope.endDate,
                    'ReminderMode': $scope.reminderMode,
                    'LastModifiedOn': '',
                    'CreatedOn': ''
                };
                $http.post("/api/Events", $scope.postData)
                    .then(function (response) {
                        //console.log(response);
                        //console.log("post done");
                        $scope.title = "";
                        $scope.shortDescription = "";
                        $scope.longDescription = "";
                        $scope.recurrenceRule = "";
                        $scope.startDate = "";
                        $scope.endDate = "";
                        $scope.reminderMode = "";                       
                        $scope.submitStatus.display = true;
                        $scope.submitStatus.flag = true;
                        $scope.submitStatus.message = "You have successfully added a new url mapping record.";
                        $scope.uno.$setPristine();
                        $scope.shortDescriptionInput.$setPristine();
                        $scope.longDescriptionInput.$setPristine();
                        $scope.startDateInput.$setPristine();
                        $scope.endDateInput.$setPristine();
                        $scope.recurrenceRuleInput.$setPristine();

                    },
                    function (error) {
                        //console.log(error);
                        //console.log("error HERE");
                        $scope.submitStatus.display = true;
                        $scope.submitStatus.flag = false;
                        $scope.submitStatus.message = "There have been errors in posting this url mapping. Maybe try again later?";
                    });
            }
        };
        $scope.stateChanged = function (x) {
            $scope.blocked = x;
        };
        $scope.closeSubmitStatusAlert = function () {
            $scope.submitStatus = {
                'display': false,
                'flag': true,
                'message': ""
            };
        };
        $scope.isUnique = true;
        $scope.checkUnique = function (x) {
            ////console.log("at leasy here");
            //$http.get("/api/UrlMappings?shorturl=" + x)
            //.then(function (response) {
            //    //console.log(response.data);                
            //    $scope.isUnique = false;
            //    if (response.data.LongUrl == null)
            //        $scope.isUnique = true;
            //},
            //function (error) {
            //    $scope.isUnique = true;
            //});
        };

    }]);

/*
    Update controller
*/
angular.module("paytime")
    .controller("EditController", ['$scope', '$routeParams', '$http', '$window', function ($scope, $routeParams, $http, $window) {
        $scope.pageType = "Edit";
        $scope.ID = $routeParams.ID;
        $scope.title = "";
        $scope.shortDescription = "";
        $scope.longDescription = "";
        $scope.recurrenceRule = "";
        $scope.startDate = "";
        $scope.endDate = "";
        $scope.reminderMode = "";
        $scope.urlList = {};
        $scope.blocked = false;
        $scope.submitStatus = {
            'display': false,
            'flag': true,
            'message': ""
        };
        $scope.isAdmin = true;
        //$http.get("api/Identity/isAdmin")
        //    .then(function (response) {
        //        $scope.isAdmin = response.data;
        //    },
        //    function (error) {
        //        //console.log(error);
        //        $scope.isAdmin = false;
        //    });

        $http.get("/api/Events/" + $routeParams.ID)
            .then(function (response) {
                //console.log("edit fetching");
                console.log(response.data);
                $scope.urlList = response.data;
                $scope.title = response.data.Title;
                $scope.shortDescription = response.data.ShortDescription;
                $scope.longDescription = response.data.LongDescription;
                $scope.recurrenceRule = response.data.RecurrenceRule;
                $scope.startDate = response.data.StartDate;
                $scope.endDate = response.data.EndDate;
                $scope.reminderMode = response.data.ReminderMode;
                //angular.forEach($scope.urlList, function (todo) {
                //    if (todo.ID == $scope.ID) {
                //        $scope.shortUrl = todo.ShortUrl;
                //        $scope.longUrl = todo.LongUrl;
                //        $scope.blocked = todo.Blocked;
                //    }
                //});
            },
            function (error) {
                $scope.userTabFlag = false;
                //console.log("error HERE");
            });

        $scope.deleteRecord = function () {
            if ($scope.isAdmin) {
                $http.delete("/api/Events/" + $scope.ID)
                    .then(function (response) {
                        //console.log(response);
                        //console.log("deleted" + $scope.ID);
                        $window.location = "#/";
                    },
                    function (error) {
                        $scope.userTabFlag = false;
                        //console.log("error HERE");
                        $scope.submitStatus.display = true;
                        $scope.submitStatus.flag = false;
                        $scope.submitStatus.message = "There have been errors in deleting this url mapping. Maybe try again later?";
                    });
            }

        };
        $scope.addRecord = function (x) {
            if (x == $scope.pageType && $scope.isUnique == true && $scope.isAdmin) {
                //console.log($scope.blocked);
                $scope.postData = {
                    'Id': $scope.ID,
                    'Title': $scope.title,
                    'ShortDescription': $scope.shortDescription,
                    'LongDescription': $scope.longDescription,
                    'RecurrenceRule': $scope.recurrenceRule,
                    'StartDate': $scope.startDate,
                    'EndDate': $scope.endDate,
                    'ReminderMode': $scope.reminderMode,
                    'LastModifiedOn': '',
                    'CreatedOn': ''
                };
                $http.put("/api/Events/" + $scope.ID, $scope.postData)
                    .then(function (response) {
                        //console.log(response);
                        //console.log("put done");
                        $scope.submitStatus.display = true;
                        $scope.submitStatus.flag = true;
                        $scope.submitStatus.message = "You have successfully updated the url mapping record.";
                        $scope.uno.$setPristine();
                        $scope.shortDescriptionInput.$setPristine();
                        $scope.longDescriptionInput.$setPristine();
                        $scope.startDateInput.$setPristine();
                        $scope.endDateInput.$setPristine();
                        $scope.recurrenceRuleInput.$setPristine();
                    },
                    function (error) {
                        //console.log("error HERE");
                        $scope.submitStatus.display = true;
                        $scope.submitStatus.flag = false;
                        $scope.submitStatus.message = "There have been errors in posting this url mapping. Maybe try again later?";
                    });
            }
        };
        $scope.stateChanged = function (x) {
            $scope.blocked = x;
        };
        $scope.closeSubmitStatusAlert = function () {
            $scope.submitStatus = {
                'display': false,
                'flag': true,
                'message': ""
            };
        };
        $scope.isUnique = true;
        $scope.checkUnique = function (x) {
            ////console.log("at leasy here");
            //$http.get("/api/UrlMappings?shorturl=" + x)
            //.then(function (response) {
            //    //console.log(response);
            //    //console.log(x);
            //    $scope.isUnique = false;
            //},
            //function (error) {
            //    $scope.isUnique = true;
            //});
        };
    }]);


