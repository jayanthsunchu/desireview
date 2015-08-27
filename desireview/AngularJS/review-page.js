function reviewIndexController($scope) {
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

}

reviewIndexController.$inject = ['$scope'];
angular.module('desireview', []).controller("reviewIndexController", reviewIndexController);

