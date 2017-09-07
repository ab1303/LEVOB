(function () {
    'use strict';

    angular
        .module('glossaryApp')
        .controller('glossaryModalInstanceCtrl', glossaryModalInstanceCtrl);

    glossaryModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'glossaryRepository', 'modalParameters'];

    function glossaryModalInstanceCtrl($scope, $uibModalInstance, glossaryRepository, modalParameters) {
        /* jshint validthis:true */
        var vm = this;
        vm.submitFormText = '';
        vm.isEditMode = modalParameters.openMode !== 'add';
        vm.alert = {
            type: '',
            msg: '',
            isVisible: false

        };
       

        activate();

        function activate() {
            vm.submitFormText = (modalParameters.openMode === 'add') ? 'Add Glossary' : 'Update';

            if (modalParameters.openMode === 'add') {
                vm.glossaryItem = {
                    id: -1,
                    term: '',
                    definition:''
                };
            } else {
                vm.glossaryItem = angular.copy(modalParameters.glossaryItem);
            } 

        }


        vm.submitForm = function (isFormValid) {
            if (isFormValid) {
                $uibModalInstance.close(vm.glossaryItem);
            }

        };
       
       
        vm.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

      

    }


})();
