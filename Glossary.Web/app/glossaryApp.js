
(function () {
    'use strict';

    window.app = angular.module('glossaryApp', [
        // Angular modules         
        'angularSpinner',
        'oitozero.ngSweetAlert',
        'ui.bootstrap',
        'datatables',                   // Angular-datatables
        'datatables.fixedheader'

    ])
    .constant("appSettings", {
        baseUri: 'http://localhost:55920/',
        apiSuccessCode: 70003,
        resources: {
            glossaryApi: 'api/glossary/'
        }
    })

    ;
})();
