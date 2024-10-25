(function () {
    angular.module('LayoutApp', []).controller('layoutController', function ($scope, $http) {
        $scope.getFileExtension = function (fileName) {
            return fileName.split('.').pop().toLowerCase();
        };
        $scope.getFileIconClass = function (fileName) {            
            var extension = $scope.getFileExtension(fileName);
            if (extension === 'pdf') {
                return 'fa fa-file-pdf-o text-danger';
            } else if (extension === 'xls' || extension === 'xlsx') {
                return 'fa fa-file-excel-o text-success';
            } else if (extension === 'doc' || extension === 'docx') {
                return 'fa fa-file-word-o text-primary';
            } else if (extension === 'txt') {
                return 'file-icon-text fa fa-file-alt';
            } else if (['jpg', 'jpeg', 'png', 'gif', 'bmp'].includes(extension)) {
                return 'fa fa-file-image-o text-default';
            } else {
                return 'fa fa-file text-info';
            }
        };

    });
})();