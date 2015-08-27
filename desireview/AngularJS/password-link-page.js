function passwordResetLinkController($scope, $http, $location) {
    $scope.changepassword = function () {
        alert($location.search()[1]);
    };
}

passwordResetLinkController.$inject = ['$scope', '$http', '$location'];
angular.module('desireview', []).controller("passwordResetLinkController", passwordResetLinkController);