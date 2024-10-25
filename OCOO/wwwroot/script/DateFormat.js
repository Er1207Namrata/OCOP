$(document).ready(function () {
   

   
    //debugger
    //$('.fdatepicker').datepicker({
    //    dateFormat: 'dd/mm/yy',
    //    maxDate: 0,
    //    changeYear: true,
    //    changeMonth: true,


    //});
    jQuery('.fdatepicker').datepicker({
        format: 'dd/mm/yyyy',
        todayHighlight: true,
    });
    //$('.datepicker').datepicker({
    //    format: "dd-mm-yyyy",
    //    autoclose: true,
    //    endDate: "today",
    //    maxDate: 0,
    //    showOn: "off"
    //});
    jQuery('.mydatepicker, #datepicker,.datepicker').datepicker({
        format: 'dd/mm/yyyy',
        endDate: new Date(),
        todayHighlight: true,
    });

    /* $('.footable').DataTable({ "Info": false });*/
});
$(document).ready(function () {
    var table = $('.footable').DataTable({
        responsive: true
    });

    new $.fn.dataTable.FixedHeader(table);

    fglbcheck_UserPermission();
});

// Global Date Picker
function GblDisplayDate() {
    //
    /**/
    var dateTime = new Date()
    var resultDate = null;
    var FullDate = new Date(dateTime);
    var day = FullDate.getDate();

    //var months = ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"];
    var months = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
    resultDate = (day < 10 ? '0' : '') + day + "/" + months[FullDate.getMonth()] + "/" + FullDate.getFullYear();
    return resultDate;
}

// for number to convert in  word


var a = ['', 'one ', 'two ', 'three ', 'four ', 'five ', 'six ', 'seven ', 'eight ', 'nine ', 'ten ', 'eleven ', 'twelve ', 'thirteen ', 'fourteen ', 'fifteen ', 'sixteen ', 'seventeen ', 'eighteen ', 'nineteen '];
var b = ['', '', 'twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];

function inWords(num) {
    if ((num = num.toString()).length > 9) return 'overflow';
    n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
    if (!n) return; var str = '';
    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'crore ' : '';
    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'lakh ' : '';
    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'thousand ' : '';
    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'hundred ' : '';
    str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
    return str;
}


function fglbcheck_UserPermission() {

    var Isdisabled = $(".hddnUserPermission").val();
    if (Isdisabled == "" || Isdisabled == undefined || Isdisabled == NaN) {
        Isdisabled = 0;
    }
    if (parseInt(Isdisabled) == 1) {
        $('.fUserPermission').hide();
       /* $('.fUserPermission').prop('readonly', true);*/
    } else {
        $('.fUserPermission').show();
       // $('.fUserPermission').prop('readonly', false);
    }

    var Isenabled = $(".fUserPermission").val();



}