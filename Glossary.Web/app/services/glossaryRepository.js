

(function () {
    'use strict';

    angular
      .module('glossaryApp')
      .factory('glossaryRepository', glossaryRepository);

    glossaryRepository.$inject = ['appSettings', '$http', '$q', 'usSpinnerService'];

    function glossaryRepository(appSettings, $http, $q, usSpinnerService) {
        var service = {
            getGlossaries: getGlossaries,
            addGlossary: addGlossary,
            updateGlossary: updateGlossary,
            deleteGlossary: deleteGlossary
        };

        return service;

        function getGlossaries() {

            usSpinnerService.spin('shellSpinner');
            var url = appSettings.baseUri + appSettings.resources.glossaryApi;
            var deferred = $q.defer();

            $http.get(url, {

            })
               .then(function (response) {
                   usSpinnerService.stop('shellSpinner');

                   if (response.data.glossaries && response.data.code === appSettings.apiSuccessCode) {
                       deferred.resolve({
                           status: 'success',
                           message: response.data.message || '',
                           glossaries: response.data.glossaries
                       });

                   } else {
                       deferred.reject({
                           status: 'error',
                           message: response.data.message || ''
                       });
                   }
               }, function (response) {
                   usSpinnerService.stop('shellSpinner');

                   deferred.reject({
                       httpStatusCode: response.status,
                       status: 'error',
                       message: response.statusText
                   });
               });

            return deferred.promise;

        }

        function addGlossary(term, definition) {

            usSpinnerService.spin('shellSpinner');
            var deferred = $q.defer();
            var url = appSettings.baseUri + appSettings.resources.glossaryApi;


            if (!term) {
                usSpinnerService.stop('shellSpinner');
                deferred.reject('term is not provided');
                return deferred.promise;
            }

            if (!definition) {
                usSpinnerService.stop('shellSpinner');
                deferred.reject('definition is not provided');
                return deferred.promise;
            }

            $http.post(url, {
                term: term,
                definition: definition
            })
                .success(function (response) {
                    usSpinnerService.stop('shellSpinner');

                    if (response.glossary && response.code === appSettings.apiSuccessCode) {
                        deferred.resolve({
                            status: 'success',
                            message: response.message || '',
                        });
                    } else {
                        deferred.reject({
                            status: 'error',
                            message: response.message || ''
                        });
                    }


                }).error(function (msg, code) {
                    usSpinnerService.stop('shellSpinner');
                    deferred.reject({
                        httpStatusCode: code,
                        status: 'error',
                        message: msg || 'Server Error, Please contact Administrator'
                    });
                });


            return deferred.promise;
        }

        function updateGlossary(id, term, definition) {

            usSpinnerService.spin('shellSpinner');
            var deferred = $q.defer();
            var url = appSettings.baseUri + appSettings.resources.glossaryApi + id;


            if (!term) {
                usSpinnerService.stop('shellSpinner');
                deferred.reject('term is not provided');
                return deferred.promise;
            }

            if (!definition) {
                usSpinnerService.stop('shellSpinner');
                deferred.reject('definition is not provided');
                return deferred.promise;
            }


            $http.put(url, {
                term: term,
                definition: definition
            })
                .success(function (response) {
                    usSpinnerService.stop('shellSpinner');

                    if (response.glossary && response.code === appSettings.apiSuccessCode) {
                        deferred.resolve({
                            glossary:response.glossary,
                            status: 'success',
                            message: response.message || '',
                        });
                    } else {
                        deferred.reject({
                            status: 'error',
                            message: response.message || ''
                        });
                    }


                }).error(function (msg, code) {
                    usSpinnerService.stop('shellSpinner');
                    deferred.reject({
                        httpStatusCode: code,
                        status: 'error',
                        message: msg || 'Server Error, Please contact Administrator'
                    });
                });


            return deferred.promise;


        }

        function deleteGlossary(id) {
            usSpinnerService.spin('shellSpinner');

            var deferred = $q.defer();
            if (!id) {
                deferred.reject({
                    status: 'error',
                    message: 'Invalid parameter'
                });
            }

            var url = appSettings.baseUri + appSettings.resources.glossaryApi + id;
            $http.delete(url)
               .then(function (response) {
                   usSpinnerService.stop('shellSpinner');

                   if (response.data.code === appSettings.apiSuccessCode) {
                       deferred.resolve({
                           status: 'success',
                           message: response.data.message || ''
                       });

                   } else {
                       deferred.reject({
                           status: 'error',
                           message: response.data.message || ''
                       });
                   }
               }, function (response) {
                   usSpinnerService.stop('shellSpinner');
                   deferred.reject({
                       httpStatusCode: response.status,
                       status: 'error',
                       message: msg || response.statusText
                   });
               });

            return deferred.promise;
        }

    }

})();