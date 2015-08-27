function resetPageController($scope, $http) {
    $scope.sendresetlink = function () {
        $http.post("/api/users/sendpasswordresetlink", { Email: "jayanth.sunchu@gmail.com" }).success(function () {
            alert("success");
        })
        .error(function () {
            alert("failure");
        });
    };
}

resetPageController.$inject = ['$scope', '$http'];

angular.module('desireview', []).controller("resetPageController", resetPageController);