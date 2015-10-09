var module = angular.module('desireview', ['ngCookies', 'ngRoute']);
module.controller('homeIndexController', ['$scope', '$http', '$cookies', '$location', 'dataService', function($scope, $http, $cookies, $location, dataService) {
    $scope.isBusy = true;
    $scope.data = dataService;
    
    
    dataService.getMovies().then(function () {
        var defaultOption = $cookies.get("defaultlanguage");
        if (defaultOption != null) {
            $scope.selectedItem = defaultOption;
            dataService.selectedmovies = dataService.movies.filter(function (movie) {
                if (defaultOption === "All")
                    return true;
                else
                    return movie.MovieLanguage === defaultOption;
            });
        }
    }).then(function () {

    }).then(function () {
        $scope.isBusy = false;
    })
    
    $scope.showReviewPage = function (data) {
        //alert(data);
    };

    $scope.listOfOptions = ['All', 'Telugu', 'Hindi', 'Tamil'];

    $scope.selectedItemChanged = function () {
        dataService.selectedmovies = dataService.movies.filter(function (movie) {
            if ($scope.selectedItem === "All")
                return true;
            else
                return movie.MovieLanguage === $scope.selectedItem;
        });
    }

    $scope.makeDefaultOption = function () {
        if ($scope.selectedItem != null) {
            $cookies.put("defaultlanguage", $scope.selectedItem);
        }
    };

    $scope.go = function (path, i) {
        dataService.selectedmovie = dataService.selectedmovies.filter(function (movie) {
            return movie.Title === i;
        });
        $location.path(path);
    };
}]);

if (!Array.prototype.filter) {
    Array.prototype.filter = function (fun) {
        var len = this.length >>> 0;
        if (typeof fun != "function")
            throw new TypeError();

        var res = [];
        var thisp = arguments[1];
        for (var i = 0; i < len; i++) {
            if (i in this) {
                var val = this[i]; 
                if (fun.call(thisp, val, i, this))
                    res.push(val);
            }
        }
        return res;
    };
}

module.controller('reviewIndexController', ['$scope', 'dataService', '$routeParams', function ($scope, dataService, $routeParams) {
    alert($routeParams.movieTitle);
    $scope.name = "Review Page";
    if (dataService == null) {
        
    }
    $scope.data = dataService;

    $scope.reviewdata = [
        {
            title: "I call it a great Ripoff! Most of us would call it a Blockbuster!",
            introduction: "Introduction content. Lorem epsum. Chedsfw sdifjsifs dsfisjdf. Lorem epsuem.. This Review has also ksjdi dsfjiwsjd fsm sidfjsm sid fjsdf  isdjflsdfj isdf jsi fsjl jsldifjd sljfidsljf sdfjlsdifj sldfj dslfijdslfdsjfdslifj dslifj sdlfjds lfdslfjdslfjdslifjdslifjdsljfldsijfsdfdsf dsf dsljfdsljfdsljfdsfijdsf sdlf jdslif jdslfj dslfjdslfj dslifj dslfj dslif. dslfijdslfdsjfdslifj dslifj sdlfjds lfdslfjdslfjdslifjdslifjdsljfldsijfsdfdsf dsf dsljfdsljfdsljfdsfijdsf sdlf jdslif jdslfj dslfjdslfj dslifj dslfj dslif jdsljf dsljf ldsjf ldsj flds fjldsjfldsijfdsljfldsijfldsijf. ksjdi dsfjiwsjd fsm sidfjsm sid .",
            body: "Lorem epsuem.. This Review has also ksjdi dsfjiwsjd fsm sidfjsm sid fjsdf  isdjflsdfj isdf jsi fsjl jsldifjd sljfidsljf sdfjlsdifj sldfj dslfijdslfdsjfdslifj dslifj sdlfjds lfdslfjdslfjdslifjdslifjdsljfldsijfsdfdsf dsf dsljfdsljfdsljfdsfijdsf sdlf jdslif jdslfj dslfjdslfj dslifj dslfj dslif jdsljf dsljf ldsjf ldsj flds fjldsjfldsijfdsljfldsijfldsijf. ksjdi dsfjiwsjd fsm sidfjsm sid fjsdf  isdjflsdfj isdf jsi fsjl jsldifjd sljfidsljf sdfjlsdifj sldfj dslfijdslfdsjfdslifj dslifj sdlfjds lfdslfjdslfjdslifjdslifjdsljfldsijfsdfdsf dsf dsljfdsljfdsljfdsfijdsf sdlf jdslif jdslfj dslfjdslfj dslifj dslfj dslif jdsljf dsljf ldsjf ldsj flds fjldsjfldsijfdsljfldsijfldsijf. ksjdi dsfjiwsjd fsm sidfjsm sid fjsdf  isdjflsdfj isdf jsi fsjl jsldifjd sljfidsljf sdfjlsdifj sldfj dslfijdslfdsjfdslifj dslifj sdlfjds lfdslfjdslfjdslifjdslifjdsljfldsijfsdfdsf dsf dsljfdsljfdsljfdsfijdsf sdlf jdslif jdslfj dslfjdslfj. ksjdi dsfjiwsjd fsm sidfjsm sid fjsdf  isdjflsdfj isdf jsi fsjl jsldifjd sljfidsljf sdfjlsdifj sldfj dslfijdslfdsjfdslifj dslifj sdlfjds lfdslfjdslfjdslifjdslifjdsljfldsijfsdfdsf dsf dsljfdsljfdsljfdsfijdsf sdlf jdslif jdslfj dslfjdslfj dslifj dslfj dslif jdsljf dsljf ldsjf ldsj flds fjldsjfldsijfdsljfldsijfldsijf. ksjdi dsfjiwsjd fsm sidfjsm sid fjsdf  isdjflsdfj isdf jsi fsjl jsldifjd sljfidsljf sdfjlsdifj sldfj dslfijdslfdsjfdslifj dslifj sdlfjds lfdslfjdslfjdslifjdslifjdsljfldsijfsdfdsf dsf dsljfdsljfdsljfdsfijdsf sdlf jdslif jdslfj dslfjdslfj dslifj dslfj dslif jdsljf dsljf ldsjf ldsj flds fjldsjfldsijfdsljfldsijfldsijf.",
            vreviews: [{ title: "Check" }, { title: "Check 2" }, { title: "Check 2" }, { title: "Check 2" }]
        },
    ];

}]);




module.factory("dataService", function ($http, $q) {
    var _movies = [];
    var _selectedmovies = [];
    var _selectedmovie = [];

    var _getMovies = function () {
        var deferred = $q.defer();
        $http.get("/api/movies/get").then(function (result) {
            angular.copy(result.data, _movies);
            angular.copy(result.data, _selectedmovies);
            angular.copy(_selectedmovies.filter(function (movie) { return movie.Title === _selectedmovies[0].Title; }), _selectedmovie);

            deferred.resolve();
        }, function () {
            deferred.reject();
        });

        return deferred.promise;
    };
    return {
        movies: _movies,
        getMovies: _getMovies,
        selectedmovies: _selectedmovies,
        selectedmovie: _selectedmovie
    };

});

module.config(function ($routeProvider) {
    $routeProvider.when("/", {
        controller: "homeIndexController",
        templateUrl: "/AngularTemplates/movieView.html",
    });
    $routeProvider.when("/moviereview/:movieTitle", {
       controller: "reviewIndexController",
        templateUrl: "/AngularTemplates/reviewView.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });
});

