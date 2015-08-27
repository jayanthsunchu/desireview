function homeIndexController($scope) {
    $scope.name = "Angular JS at work!";
    $scope.data = [
        {
            title: "Bahubali",
            cast: "Prabhas, Anushka",
            image: "bahu.jpg",
            width: 3,
            userrating: 4,
            desireviewrating: 3,
            director: "Rajamouli",
        },
        {
            title: "Bajrangi Bhaijaan",
            cast: "Salman, Kareena",
            image: "bbj.jpg",
            userrating: 4,
            desireviewrating: 3,
            width:
               4,
            director: "Kabir Khan",
        },
        {
            title: "Drishyam",
            cast: "Ajay Devgan, Shriya Saran",
            image: "dri.png",
            userrating: 4,
            desireviewrating: 3,
            width:
                3,
            director: "Ajay Nunan",
        },
        {
            title: "Masaan",
            cast: "Richa Chadda",
            image: "mas.jpg",
            userrating: 4,
            desireviewrating: 3,
            width:
                2,
            director: "Mara Chad",
        },
         {
             title: "Oru Vadakkan Selfie",
             cast: "Nivin Poly, SumDas",
             image: "oru.png",
             userrating: 4,
             desireviewrating: 3,
             width: 3,
             director: "BDay Direc"
         },
        {
            title: "SO Satyamurthy",
            image: "sosm.jpg",
            userrating: 4,
            desireviewrating: 3,
            cast: "Allu Arjun, Samantha Prabhu",
            width:
                2,
            director: "Trivikram",
        },
        {
            title: "Srimanthudu",
            cast: "Mahesh Babu, Shruti Hassan",
            image: "srim.jpg",
            userrating: 4,
            desireviewrating: 3,
            width:
                4,
            director: "Koritala Siva",
        },
          {
              title: "Tanu Weds Manu Returns",
              cast: "Madhavan, Kangna Ranaut",
              image: "tanu.jpg",
              userrating: 4,
              desireviewrating: 3,
              width: 2,
              director: "Anand L Rai"
          },
    ];
}
homeIndexController.$inject = ['$scope'];
angular.module('desireview', []).controller('homeIndexController', homeIndexController)