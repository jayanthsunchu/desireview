var module = angular.module('desireview', ['ngCookies', 'ngRoute']);
module.controller('homeIndexController', ['$scope', '$http', '$cookies', '$location', function($scope, $http, $cookies, $location, dataService) {
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

        //alert(data);
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
    $scope.go = function (path) {
        $location.path(path);
    };
}]);

module.factory("dataService", function ($http, $q) {
    var movies = [];

    var deferred = $q.defer();

    var getMovies = function () {
        $http.get("/api/movies/getbylanguage/" + $scope.selectedItem).then(function (result) {
            angular.copy(result.data, movies);
        }, function () {

        }).then(function () {
            $scope.isBusy = false;
        });
    };
    return {

    };

});

module.config(function ($routeProvider) {
    $routeProvider.when("/", {
        controller: "homeIndexController",
        templateUrl: "/AngularTemplates/movieView.html",
    });
    $routeProvider.when("/moviereview", {
       controller: "reviewIndexController",
        templateUrl: "/AngularTemplates/reviewView.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });
});

    module.controller('reviewIndexController', ['$scope', function($scope){
        $scope.name = "Review Page";
        $scope.data = [
            {
                image: "bahu.jpg",
                cast: "Anushka, Prabhas",
                director: "Rajamouli",
                producer: "Shobhu Yarlagadda",
                userrating: 4,
                desireviewrating: 3
            }
        ];

        $scope.reviewdata = [
            {
                title: "I call it a great Ripoff! Most of us would call it a Blockbuster!",
                introduction: "Introduction content. Lorem epsum. Chedsfw sdifjsifs dsfisjdf. Lorem epsuem.. This Review has also ksjdi dsfjiwsjd fsm sidfjsm sid fjsdf  isdjflsdfj isdf jsi fsjl jsldifjd sljfidsljf sdfjlsdifj sldfj dslfijdslfdsjfdslifj dslifj sdlfjds lfdslfjdslfjdslifjdslifjdsljfldsijfsdfdsf dsf dsljfdsljfdsljfdsfijdsf sdlf jdslif jdslfj dslfjdslfj dslifj dslfj dslif. dslfijdslfdsjfdslifj dslifj sdlfjds lfdslfjdslfjdslifjdslifjdsljfldsijfsdfdsf dsf dsljfdsljfdsljfdsfijdsf sdlf jdslif jdslfj dslfjdslfj dslifj dslfj dslif jdsljf dsljf ldsjf ldsj flds fjldsjfldsijfdsljfldsijfldsijf. ksjdi dsfjiwsjd fsm sidfjsm sid .",
                body: "Lorem epsuem.. This Review has also ksjdi dsfjiwsjd fsm sidfjsm sid fjsdf  isdjflsdfj isdf jsi fsjl jsldifjd sljfidsljf sdfjlsdifj sldfj dslfijdslfdsjfdslifj dslifj sdlfjds lfdslfjdslfjdslifjdslifjdsljfldsijfsdfdsf dsf dsljfdsljfdsljfdsfijdsf sdlf jdslif jdslfj dslfjdslfj dslifj dslfj dslif jdsljf dsljf ldsjf ldsj flds fjldsjfldsijfdsljfldsijfldsijf. ksjdi dsfjiwsjd fsm sidfjsm sid fjsdf  isdjflsdfj isdf jsi fsjl jsldifjd sljfidsljf sdfjlsdifj sldfj dslfijdslfdsjfdslifj dslifj sdlfjds lfdslfjdslfjdslifjdslifjdsljfldsijfsdfdsf dsf dsljfdsljfdsljfdsfijdsf sdlf jdslif jdslfj dslfjdslfj dslifj dslfj dslif jdsljf dsljf ldsjf ldsj flds fjldsjfldsijfdsljfldsijfldsijf. ksjdi dsfjiwsjd fsm sidfjsm sid fjsdf  isdjflsdfj isdf jsi fsjl jsldifjd sljfidsljf sdfjlsdifj sldfj dslfijdslfdsjfdslifj dslifj sdlfjds lfdslfjdslfjdslifjdslifjdsljfldsijfsdfdsf dsf dsljfdsljfdsljfdsfijdsf sdlf jdslif jdslfj dslfjdslfj. ksjdi dsfjiwsjd fsm sidfjsm sid fjsdf  isdjflsdfj isdf jsi fsjl jsldifjd sljfidsljf sdfjlsdifj sldfj dslfijdslfdsjfdslifj dslifj sdlfjds lfdslfjdslfjdslifjdslifjdsljfldsijfsdfdsf dsf dsljfdsljfdsljfdsfijdsf sdlf jdslif jdslfj dslfjdslfj dslifj dslfj dslif jdsljf dsljf ldsjf ldsj flds fjldsjfldsijfdsljfldsijfldsijf. ksjdi dsfjiwsjd fsm sidfjsm sid fjsdf  isdjflsdfj isdf jsi fsjl jsldifjd sljfidsljf sdfjlsdifj sldfj dslfijdslfdsjfdslifj dslifj sdlfjds lfdslfjdslfjdslifjdslifjdsljfldsijfsdfdsf dsf dsljfdsljfdsljfdsfijdsf sdlf jdslif jdslfj dslfjdslfj dslifj dslfj dslif jdsljf dsljf ldsjf ldsj flds fjldsjfldsijfdsljfldsijfldsijf.",
                vreviews: [{ title: "Check" }, { title: "Check 2" }, { title: "Check 2" }, { title: "Check 2" }]
            },
        ];

    }]);

