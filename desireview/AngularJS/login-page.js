function loginPageController($scope, $http) {
    $scope.registernewuser = function () {
        $http.post("api/users/registernewuser", { UserName: "jsunchu1", Password: "graduate", Email: "jayanth.sunchu@gmail.com" })
            .success(function (data) {
                alert(data.UserName);
            }).error(function (data) {
                alert("Failure");
            });
    };

    $scope.validateuser = function () {
        $http.post("api/users/validateexistinguser", { UserName: "jsunchu", Password: "graduat" })
        .success(function (data) {
            alert(data.UserName);
        })
        .error(function (data) {
            alert(data);
        });
    };
}

loginPageController.$inject = ['$scope', '$http'];
angular.module('desireview', []).controller("loginPageController", loginPageController);