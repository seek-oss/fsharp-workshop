var profileApp = angular.module('profileApp', []);

profileApp.controller('ProfileCtrl', function ($scope, $http) {
    $scope.profile = {
        firstName : "Foo",
        lastName : "Bar",
        description: "",
        postcode: ""
    };

    $scope.saveProfile = function(profile){
        $http.put("/profile", profile)
            .success(function(data, status, headers, config) {
                $scope.result = data;
                $scope.errors = null;
            }).
            error(function(data, status, headers, config) {
                $scope.result = null;
                $scope.errors = data;
            });
    };
});
