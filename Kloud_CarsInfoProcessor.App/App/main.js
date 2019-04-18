(function () {

    angular
        .module('app')
        .controller('Main', main);
    main.$inject = ['$q', 'KloudCoreAPIService'];

    function main($q, KloudCoreAPIService) {
        var self = this;
        self.init = init;
        self.json = "";
        self.displaylist = "";
        self.carsfulllist = [];
        self.orderedBrandList = [];
        self.getCarOwnerList = getCarOwnerList;
        self.GetPersonNameByBrand = GetPersonNameByBrand;
        //initiate method to get the list on load
        function init() {

            $q.all({
                types: getCarOwnerList()
            }).then(function (values) {
                self.owners = values.types;
            });
        }
        //Method to get data from API
        function getCarOwnerList() {
            var owners = [];
            var deferred = $q.defer();
            //calling the API
            KloudCoreAPIService.get('api/cars/').then(function (response) {
                if (response !== null && response.statusText === "OK") {
                    if (response.data.length > 0) {
                        //Process response data from server
                        self.json = response.data;
                        processobjects(self.json);
                        self.busy = false;
                    }
                }
                deferred.resolve(owners);
            }, function () { self._busy = false; deferred.resolve(); });
            return deferred.promise;

        }
        //Method to list data as per requiremtn
        function processobjects(array) {
            //Response data from server is grouped by person, hence ungroup and convert to plain json
            array.forEach(function (person) {
                person["cars"].forEach(function (car) {
                    self.carsfulllist.push({ name: person["name"], brand: car["brand"], colour: car["colour"] });
                });
            });

            //Group by brand using linq.js
            var brandlist = Enumerable.From(self.carsfulllist)
                .GroupBy(function (x){return x.brand})
                .Select(function (x){return x.First().brand})
                .ToArray();
            //Order list based on brand alphabetically
            self.orderedBrandList = Enumerable.From(brandlist)
                .OrderBy(function (x) {return x})
                .Select()
                .ToArray();
        }
        //Method to get person list based on selected brand
        function GetPersonNameByBrand(brand)
            {
            var personlist = Enumerable.From(self.carsfulllist)

                .OrderBy(function (x) { return x.colour})
                .Where(function (x) {
                    //Getting person list with selected brand and omiting the null and empty person names
                    return x.brand === brand && x.name !== null && x.name !== ""
                })
                .Select(function (x) { return x.name })
                .Distinct()
                .ToArray();
            return personlist;
            }
    }
})();