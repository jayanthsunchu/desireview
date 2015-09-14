function homeIndexController($scope, $http, $cookies) {
    
    var defaultOption = $cookies.get("defaultlanguage");
    if (defaultOption != null) {
        $scope.selectedItem = defaultOption;
        $http.get("/api/movies/getbylanguage/" + $scope.selectedItem).success(function (data) {
            $scope.data = data;
        }).error(function (data) {
            alert("Error");
        });
    }
    else {
    $http.get("/api/movies/get").success(function (data) {
        $scope.data = data;
    }).error(function (data) {
        alert("error" + data);
    });
    }

    $scope.listOfOptions = ['All', 'Telugu', 'Hindi', 'Tamil'];
    
    $scope.selectedItemChanged = function () {
        $http.get("/api/movies/getbylanguage/" + $scope.selectedItem).success(function (data) {
            $scope.data = data;
        }).error(function (data) {
            alert("Error");
        });
    }

    $scope.makeDefaultOption = function () {
        if ($scope.selectedItem != null) {
            $cookies.put("defaultlanguage", $scope.selectedItem);
        }
        
    };

}
homeIndexController.$inject = ['$scope', '$http', '$cookies'];
angular.module('desireview', ['ngCookies']).controller('homeIndexController', homeIndexController)