angular
    .module('glossaryApp')
    .config(function config(usSpinnerConfigProvider) {

        usSpinnerConfigProvider.setTheme('bigBlue', { color: 'blue', radius: 20 });
        usSpinnerConfigProvider.setTheme('smallBlue', { color: '#18a689', radius: 6 });
        usSpinnerConfigProvider.setTheme('smallRed', { color: 'red', radius: 6 });



    })
    .run(['$rootScope', '$window', function ($rootScope, $window) {



    }]);
