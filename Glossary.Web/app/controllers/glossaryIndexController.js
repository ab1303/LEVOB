(function () {
    'use strict';

    angular
        .module('glossaryApp')
        .controller('glossaryIndexController', glossaryIndexController);

    glossaryIndexController.$inject = ['$window', '$scope', '$uibModal', 'usSpinnerService', 'glossaryRepository'];

    function glossaryIndexController($window, $scope, $uibModal, usSpinnerService, glossaryRepository) {
        /* jshint validthis:true */
        var vm = this;
        vm.glossaryDtInstance = {};

        vm.alert = {
            type: '',
            msg: '',
            isVisible: false

        };
        vm.glossaryList = [];

        activate();

        function activate() {
            $scope.showSpinner = true;
            queryGlossaries();
        }


        function queryGlossaries() {
            glossaryRepository.getGlossaries().then(function (response) {
                vm.glossaryList = response.glossaries;
            }, function (response) {
                $window.toastr.error(response.message || 'Server error in fetching glossary items', 'Error');
            });
        }

        vm.addGlossaryItem = function () {

            var modalParameters = {
                openMode: 'add',
                glossaryId: 0
            }


            var modalInstance = $uibModal.open({
                animation: true,
                size: 'md',
                templateUrl: '/app/templates/addEditGlossaryItem.html',
                controller: 'glossaryModalInstanceCtrl as vm',
                resolve: {
                    modalParameters: modalParameters
                }
            });

            modalInstance.result.then(function (glossaryItem) {
                glossaryRepository.addGlossary(glossaryItem.term, glossaryItem.definition).then(function (response) {
                    $window.toastr.success(response.message || 'glossary item added!', 'Success');
                    queryGlossaries();
                }, function (response) {
                    $window.toastr.error(response.message || 'Server error in adding glossary items', 'Error');
                });
            }, function () {

            });
        }

    }


})();
