(function () {
    angular
      .module('validation.rule', ['validation'])
      .config(['$validationProvider', function ($validationProvider) {
          var expression = {
              required: function (value) {
                  return !!value;
              },
              requiredSelect: function (value) {
                  if (value==null) {
                      return false;
                  }

                  if (angular.isDefined(value.value)) {
                      return !!value.value;
                  } else
                      return !!value;
              },
              url: /((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)/,
              //email: /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/,
              email: /^null$|^$|^(\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([;, ]+)?)+/,
              number: /^\d+$/,
              numberGt0: function (value) {
                  return value > 0;
              },
              minlength: function (value, scope, element, attrs, param) {
                  if (!value) {
                      return true; // Don't validate if empty
                  }

                  return value.length >= param;
              },
              maxlength: function (value, scope, element, attrs, param) {
                  if (!value) {
                      return true; // Don't validate if empty
                  }

                  return value.length <= param;
              },
              validateBasedOnState: function (value, scope, element, attrs, param) {
                  var vm = scope.vm;

                  if (!vm.address.state)
                      return true;

                  var testValue = false;
                  var stateRegExp;
                  switch (vm.address.state.trim().toUpperCase()) {

                      case "NSW":
                          stateRegExp = new RegExp("^(1[0-9][0-9][0-9]|2[0-4][0-9][0-9]|25[0-9][0-9]|262[0-9]|26[3-9][0-9]|2[7-8][0-9][0-9]|292[1-9]|29[3-9][0-9])$");
                          break;
                      case "VIC":
                          stateRegExp = new RegExp("^(3[0-9][0-9][0-9]|8[0-9][0-9][0-9])$");
                          break;
                      case "QLD":
                          stateRegExp = new RegExp("^(4[0-9][0-9][0-9]|9[0-9][0-9][0-9])$");
                          break;
                      case "SA":
                          stateRegExp = new RegExp("^(5[0-9][0-9][0-9])$");
                          break;
                      case "WA":
                          stateRegExp = new RegExp("^(6[0-9][0-9][0-9])$");
                          break;
                      case "TAS":
                          stateRegExp = new RegExp("^(7[0-9][0-9][0-9])$");
                          break;
                      case "ACT":
                          stateRegExp = new RegExp("^(02[0-9][0-9]|26[0-1][0-9]|29[0-1][0-9]|2920)$");
                          break;
                      case "NT":
                          stateRegExp = new RegExp("^(0[8-9][0-9][0-9])$");
                          break;
                      default:
                          return true;
                  }
                  if (stateRegExp != undefined)
                      testValue = stateRegExp.test(value);
                  else
                      testValue = false;

                  return testValue;
              },
              name: function (value) {
                  var nameRegExp = new RegExp("^$|^[a-zA-Z]+([a-zA-Z0-9\-'\\s]*)$");
                  return nameRegExp.test(value);
              },
              printable: function (value) {
                  var printableRegExp = new RegExp("^[\u0020-\u007E]*$");
                  return printableRegExp.test(value);
              },
              requiredPhone: function (value) {
                  if (!value) return false;
                  var vm = scope.vm;
                  return vm.phoneInstance.isValidNumber();
              },
              intlPhone: function (value, scope, element, attrs, param) {
                  if (!value) return true;
                  var vm = scope.vm;
                  if (value === '+' + vm.dialCode) return true; //don't validate if its just dial code

                  return vm.phoneInstance.isValidNumber();
              },
              noPoBoxNumber: function (value) {
                  var poBoxRegExp = new RegExp("^((?![Pp](.?)[Oo](.?)( ?)[Bb][Oo][Xx]).)*$");
                  return poBoxRegExp.test(value);
              }
          };

          var defaultMsg = {
              required: {
                  error: 'This field is required.',
                  success: 'Valid'
              },
              requiredSelect: {
                  error: 'This field is required.',
                  success: 'It\'s Required'
              },
              url: {
                  error: 'This should be Url',
                  success: 'It\'s Url'
              },
              email: {
                  error: 'This should be Email',
                  success: 'It\'s Email'
              },
              number: {
                  error: 'This should be Number',
                  success: 'It\'s Number'
              },
              numberGt0: {
                  error: 'should be greater than zero',
                  success: 'It\'s greater than zero'
              },
              minlength: {
                  error: 'This should be longer',
                  success: 'Long enough!'
              },
              maxlength: {
                  error: 'This should be shorter',
                  success: 'Short enough!'
              },
              validateBasedOnState: {
                  error: 'Post Code doesn\'t match selected state',
                  success: 'Post Code matches state!'
              },
              name: {
                  error: 'Name allows only alphanumeric characters and can start with alphabet only',
                  success: 'Name is valid'
              },
              printable: {
                  error: 'Field contains non printable character(s)',
                  success: 'Field is valid'
              },
              requiredPhone: {
                  error: 'Phone number is required',
                  success: 'Phone is valid'
              },
              intlPhone: {
                  error: 'The phone number is invalid',
                  success: 'Phone is valid'
              },
              noPoBoxNumber: {
                  error: 'Please note: we do not ship to PO Boxes',
                  success: ''
              }
          };
          $validationProvider.setExpression(expression).setDefaultMsg(defaultMsg);
      }]);
}).call(this);
