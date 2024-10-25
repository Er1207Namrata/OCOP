app.directive('loadingButton', function () {
    //debugger
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var spinner = angular.element('<div class="loading-spinner">Processing...</div>');
            element.addClass('data-text-processing');
            element.append(spinner);
            scope.$watch(attrs.ngDisabled, function (value) {
                if (value) {
                    element.attr('disabled', 'disabled');
                    element.addClass('disabled');
                } else {
                    element.removeAttr('disabled');
                    element.removeClass('disabled');
                }
            });

            scope.$watch(attrs.loadingButton, function (value) {
                if (value) {
                    element.find('.loading-spinner').css('display', 'block');
                } else {
                    element.find('.loading-spinner').css('display', 'none');
                }
            });
        }
    };
});