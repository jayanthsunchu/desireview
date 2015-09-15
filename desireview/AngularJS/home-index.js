var module = angular.module('desireview', ['ngCookies', 'ngRoute']);
module.controller('homeIndexController', ['$scope', '$http', '$cookies', function($scope, $http, $cookies) {
    $scope.isBusy = true;
    $scope.data = [];
    var defaultOption = $cookies.get("defaultlanguage");
    if (defaultOption != null) {
        $scope.selectedItem = defaultOption;
        $http.get("/api/movies/getbylanguage/" + $scope.selectedItem).then(function (result) {
            angular.copy(result.data, $scope.data);
        }, function () {
            alert("Error");
        }).then(function () {
            $scope.isBusy = false;
        });
    }
    else {
        $http.get("/api/movies/get").then(function (result) {
            angular.copy(result.data, $scope.data);
        }, function () {
            alert("Error");
        }).then(function () {
            $scope.isBusy = false;
        });
    }

    $scope.showReviewPage = function (data) {
        alert(data);
    };

    $scope.listOfOptions = ['All', 'Telugu', 'Hindi', 'Tamil'];

    $scope.selectedItemChanged = function () {
        $http.get("/api/movies/getbylanguage/" + $scope.selectedItem).then(function (result) {
            angular.copy(result.data, $scope.data);
        }, function () {
            alert("Error");
        }).then(function () {
            $scope.isBusy = false;
        });
    }

    $scope.makeDefaultOption = function () {
        if ($scope.selectedItem != null) {
            $cookies.put("defaultlanguage", $scope.selectedItem);
        }

    };

}]);

module.config(function ($routeProvider) {
    $routeProvider.when("/", {
        controller: "homeIndexController",
        templateUrl: "/AngularTemplates/movieView.html",
    });

    $routeProvider.when("/moviereview", {
        
        controller: "reviewPageController",
        templateUrl: "/AngularTemplates/reviewView.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });
});
