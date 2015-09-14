function homeIndexController($scope, $http) {
    $scope.name = "Angular JS at work!";
    $http.get("api/movies/get").success(function (data) {
        $scope.data = data;
    }).error(function (data) {
        alert("error");
    });

}
homeIndexController.$inject = ['$scope', '$http'];
angular.module('desireview', []).controller('homeIndexController', homeIndexController)