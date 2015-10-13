var module = angular.module('desireview', ['ngCookies', 'ngRoute']);
module.controller('homeIndexController', ['$scope', '$http', '$cookies', '$location', 'dataService', function ($scope, $http, $cookies, $location, dataService) {
    $scope.isBusy = false;
    $scope.data = dataService;

    if (dataService.isReady() == false) {
        $scope.isBusy = true;
        dataService.getMovies("").then(function () {
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
    }

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

module.controller('loginController', ['$scope', 'loginService', '$cookies', '$location', function ($scope, loginService, $cookies, $location) {
    $scope.existingUser = {};
    $scope.newUser = {};
    $scope.rememberme = 'NO';
                           
    $scope.checkUserName = function () {
        
    };
    $scope.logIn = function () {
        loginService.validateUser($scope.existingUser).then(function () {
            $cookies.put("UserName", loginService.validatedUser.UserName);
            $location.path("/#");
            window.location.reload();
        }, function () {
            //show error message
        }).then(function () {
          
        });
    };

    $scope.registerUser = function () {

    };
}]);

module.controller('contactController', function () {
});

module.controller('reviewIndexController', ['$scope', 'dataService', '$routeParams', '$cookies', function ($scope, dataService, $routeParams, $cookies) {
    $scope.name = "Review Page";
    if (dataService.isReady()) {
        $scope.data = dataService;
        dataService.getReviewById(dataService.selectedmovie.filter(function (movie) { return true; })[0].Id)
    .then(function () {
    }).
    then(function () { });

    }
    else {
        $scope.data = dataService;
        if ($routeParams.movieTitle != null || $routeParams.movieTitle != "") {
            dataService.getMovies($routeParams.movieTitle).then(function () {
                dataService.getReviewById(dataService.selectedmovie.filter(function (movie) { return true; })[0].Id)
                .then(function () {
                }).
                then(function () { });

            }).then(function () { });
        }

    }
}]);

module.factory("loginService", function ($http, $q) {
    var _validatedUser = {};
    var _validateUser = function (existingUser) {
        var deferred = $q.defer();
       $http.post("api/users/validateexistinguser", existingUser)
       .then(function (result) {
           //alert(data.UserName);
           angular.copy(result.data, _validatedUser);
           deferred.resolve();
       }, function () {
           //alert(data);
           angular.copy({ StatusCode: 401 }, _validatedUser);
           deferred.reject();
       });
       return deferred.promise;
    };
    return {
        validateUser: _validateUser,
        validatedUser: _validatedUser
    };
});

module.factory("dataService", function ($http, $q) {
    var _movies = [];
    var _selectedmovies = [];
    var _selectedmovie = [];
    var _reviewContent = [];

    var _IsInit = false;
    var _IsReady = function () {
        return _IsInit;
    };

    var _getReviewById = function (movieId) {
        var deferred = $q.defer();
        $http.get("/api/reviews/getreviewbyid?movieId=" + movieId).then(function (result) {
            angular.copy(result.data, _reviewContent);
            deferred.resolve();
        }, function () {
            deferred.reject();
        });
        return deferred.promise;
    };
    var _getMovies = function (selectedReview) {
        var deferred = $q.defer();
        $http.get("/api/movies/get").then(function (result) {
            angular.copy(result.data, _movies);
            angular.copy(result.data, _selectedmovies);
            if (selectedReview != "")
                angular.copy(_selectedmovies.filter(function (movie) { return movie.Title === selectedReview; }), _selectedmovie);
            else
                angular.copy(_selectedmovies.filter(function (movie) { return movie.Title === _selectedmovies[0].Title; }), _selectedmovie);
            _IsInit = true;
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
        selectedmovie: _selectedmovie,
        isReady: _IsReady,
        getReviewById: _getReviewById,
        reviewContent: _reviewContent
    };

});

module.config(function ($routeProvider) {
    $routeProvider.when("/", {
        controller: "homeIndexController",
        templateUrl: "/AngularTemplates/movieView.html",
    })
    .when("/moviereview/:movieTitle", {
        controller: "reviewIndexController",
        templateUrl: "/AngularTemplates/reviewView.html"
    })
    .when("/loginorregister", {
        controller: "loginController",
        templateUrl: "/AngularTemplates/loginView.html"
    })
    .when("/contact", {
        controller: "contactController",
        templateUrl: "/AngularTemplates/contactView.html"
    })
    .otherwise({ redirectTo: "/" });
});

