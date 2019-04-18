(function () {
    function kloudcoreApiService($http, $q) {

        var apiUrl = "http://codingtest.kloud.com.au/";
        //var apiUrl = "http://localhost:9912/";
        var self = this;

        this.setUrl = function (url) {
            apiUrl = url;
        };
        //get method 
        this.get = function (url, queryStringParamsObj) {

            var params = '';
            if (queryStringParamsObj) {
                for (var i in queryStringParamsObj) {
                    params += (params.length > 0 ? '&' : '?') + encodeURIComponent(i) + '=' + encodeURIComponent(queryStringParamsObj[i]);
                }
            }
            var x = apiUrl + url + params;
            var deferred = $q.defer();
            $http.get(apiUrl + url + params).then(function successCallback(response) {
                var reject = false;
                if (response.data && response.data.Info === "403") {

                    reject = true;
                }
                if (reject)
                    deferred.reject(response);
                else
                    deferred.resolve(response);

            }, function (response) {

                if (response.status == 500) {
                    alert("An unexpected error occurred.", "Please contact your system administrator.", "error");
                }

                deferred.resolve(response);
            });

            return deferred.promise;
        };

        this.buildUrl = function (url, queryStringParamsObj) {

            var params = '';
            if (queryStringParamsObj) {
                for (var i in queryStringParamsObj) {
                    params += (params.length > 0 ? '&' : '?') + encodeURIComponent(i) + '=' + encodeURIComponent(queryStringParamsObj[i]);
                }
            }

            return apiUrl + url + params;
        };


    }
angular
    .module('app')
    .service('KloudCoreAPIService', kloudcoreApiService);

}) ();