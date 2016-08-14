/*
   Creating a new module
*/
angular.module("paytime", ['ngRoute', 'kendo.directives']);

/*
    Configuration
*/
angular.module("paytime")
    .config(['$routeProvider', '$compileProvider', function ($routeProvider, $compileProvider) {
        $routeProvider
            .when("/", { templateUrl: "../../Partials/List.html", controller: "ListController" })
            .when("/add", { templateUrl: "../../Partials/Edit.html", controller: "AddController" })
            .when("/edit/:ID", { templateUrl: "../../Partials/Edit.html", controller: "EditController" })
            .otherwise({ templateUrl: "" });
        $compileProvider.debugInfoEnabled(false);
    }]);
