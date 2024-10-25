$(document).ready(function () {

    GetVisitors();
 
});



function GetVisitors() {
   

   
    $.ajax({
        url: '/Home/GetVisitorCount',
        type: 'post',
        dataType: 'json',
        data: {},
        async: false,
        success: function (data) {
           
            $("#SpnVisitors").text(data);

          
        }

    });
}