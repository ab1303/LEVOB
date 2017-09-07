
angular.module('glossaryApp').directive('levoGlossaryGrid', ['SweetAlert', '$window', '$uibModal', 'usSpinnerService',
    'glossaryRepository', 'DTOptionsBuilder', 'DTColumnDefBuilder',
    function (sweetAlert, $window, $uibModal, usSpinnerService, glossaryRepository, dTOptionsBuilder, dTColumnDefBuilder) {
        // Usage:
        // 
        // Creates:
        // 
        function levoGlossaryGrid($scope, $element, $attrs) {
            usSpinnerService.stop('shellSpinner');
            var vm = this;
            vm.dtOptions = {};
            vm.dtColumnDefs = [];



            function activate() {

                vm.dtOptions = getGridOptions();
                //vm.dtColumnDefs = getGridColumnDefs($attrs.programTypeCategory);
            }

            function getGridOptions() {
                var domPaging = "<'row'<'col-sm-3'i><'col-sm-5'><'col-sm-4' f>>" +
                    "<'row'<'col-sm-12'tr>>" +
                    "<'row'<'col-sm-5'i><'col-sm-7'>>";

                var gridoptions = dTOptionsBuilder.newOptions()
                    .withOption('bPaginate', false)
                    .withOption('ordering', false)
                    .withOption('lengthChange', false)
                    .withOption('autoWidth', false)
                    .withOption('responsive', true)
                    .withOption('aaSorting', [[0, 'desc']])
                    .withDataProp('data')
                    .withDOM(domPaging)

                ;


                return gridoptions;
            }


            activate();



            vm.editGlossaryItem = function (glossaryItem) {
                var modalParameters = {
                    openMode: 'edit',

                    glossaryItem: glossaryItem
                };

                var modalInstance = $uibModal.open({
                    animation: true,
                    size: 'md',
                    templateUrl: '/app/templates/addEditGlossaryItem.html',
                    controller: 'glossaryModalInstanceCtrl as vm',
                    resolve: {
                        modalParameters: modalParameters
                    }
                });

                modalInstance.result.then(function (updatedGlossaryItem) {
                    glossaryRepository.updateGlossary(glossaryItem.glossaryId, updatedGlossaryItem.term, updatedGlossaryItem.definition).then(function (response) {
                        glossaryItem.term = response.glossary.term;
                        glossaryItem.definition = response.glossary.definition;
                        glossaryItem.dateChanged = response.glossary.dateChanged;

                        $window.toastr.success(response.message || 'glossary item updated!', 'Success');

                    }, function (response) {
                        console.log(response);
                        $window.toastr.error(response.message || 'Server error in updating glosary item', 'Error');

                    });

                }, function () {

                });
            }

            vm.removeGlossaryItem = function (glossaryItem) {
                // just mark the row for deletion
                // instead of removing from model
                sweetAlert.swal({
                    title: "Are you sure?",
                    text: "Glossary item will be removed from the table",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Yes, delete it!",
                    closeOnConfirm: true
                }, function (isConfirmed) {

                    if (!isConfirmed) return;


                    glossaryRepository.deleteGlossary(glossaryItem.glossaryId).then(function (response) {

                        var index = vm.glossaryList.indexOf(glossaryItem);
                        vm.glossaryList.splice(index, 1);

                        $window.toastr.success(response.message || 'glossary item updated!', 'Success');

                    }, function (response) {
                        $window.toastr.error(response.message || 'Server error in updating glosary item', 'Error');
                        //sweetAlert.swal(response.message);
                    });



                });
            }





        }

        var directive = {
            templateUrl: '/app/templates/levoGlossaryGrid.html',
            link: link,
            restrict: 'E',
            scope: {
                glossaryList: '=',
                dtInstance: '='
            },
            controller: levoGlossaryGrid,
            controllerAs: 'vm',
            bindToController: true
        };
        return directive;

        function link(scope, element, attrs) {


        }
    }]);
