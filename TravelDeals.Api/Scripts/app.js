'use strict';

var BaseUrl = "/api/hotel/list";
var RequestParams =
    {
        ApiKey: "HD100",
        search: "",
        Sort: "DESC"
    };

var app = angular.module('hotelDeals', ['toastr']);

app.config(function (toastrConfig) {
    angular.extend(toastrConfig, {
        autoDismiss: false,
        containerId: 'toast-container',
        maxOpened: 0,
        newestOnTop: true,
        positionClass: 'toast-bottom-right',
        preventDuplicates: false,
        preventOpenDuplicates: false,
        target: 'body'
    });
});

app.controller('HotelController', function ($http, $scope, $timeout, $window, toastr) {

    var dashboardCtrl = this;

    getHotels();

    $scope.searchByCity = function () {
        RequestParams.search = $scope.searchTerm;
        getHotels();
    }

    $scope.applySort = function (option) {

        if (option == 1) {
            RequestParams.Sort = "ASC";
        } else {
            RequestParams.Sort = "DESC";
        }
        getHotels();
    }

    function getHotels() {

        $http.post("api/hotel/list", RequestParams)
            .then(function (response) {

                $scope.hotels = response.data.Hotels
                $scope.hotelCount = response.data.TotalRecords;

            }, function error(err) {
                console.log(err)
                if (err.status == 500) {
                    toastr.error('500! Internal Server Error', 'Error');
                    //$window.location.href = '/Home/Troubleshoot';
                } else if (err.status == 429) {
                    toastr.warning('Rate limit exceeded! Try again later', 'Warning');
                }

            });
    };
});


angular.bootstrap(document, ['hotelDeals']);