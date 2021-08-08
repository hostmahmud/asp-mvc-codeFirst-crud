$(function () {

    jQuery.validator.addMethod('validbirthdate', function (value, element, params) {
        var currentDate = new Date();
        if (Date.parse(value) > currentDate) {
            return false;
        }
        return true;
    }, '');

    jQuery.validator.unobtrusive.adapters.add('validbirthdate', function (options) {
        options.rules['validbirthdate'] = {};
        options.messages['validbirthdate'] = options.message;
    });

}(jQuery));