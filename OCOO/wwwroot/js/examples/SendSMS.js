

$(document).ready(function () {



    fSendEnquiryMail();






});





function fvalidate() {
    //
    var IsValid = true;
    if ($("#RequestorName").val() == "" || $("#RequestorName").val() == NaN || $("#RequestorName").val() == null || $("#RequestorName").val() == undefined) {
        IsValid = false;

        $('#RequestorName').css("border-color", "red");
    }
    else {
        $('#RequestorName').css("border-color", "green");
    }
    if ($("#MobileNo").val() == "" || $("#MobileNo").val() == NaN || $("#MobileNo").val() == null || $("#MobileNo").val() == undefined) {
        IsValid = false;

        $('#MobileNo').css("border-color", "red");
    }
    else {
        $('#MobileNo').css("border-color", "green");
    }
    if ($("#Fk_FundSourceId").val() == "" || $("#Fk_FundSourceId").val() == NaN || $("#Fk_FundSourceId").val() == null || $("#Fk_FundSourceId").val() == undefined || $("#Fk_FundSourceId").val() == 0) {
        IsValid = false;

        $('#Fk_FundSourceId').css("border-color", "red");
    } else {
        $('#Fk_FundSourceId').css("border-color", "green");
    }

    if ($("#Indentdate").val() == "" || $("#Indentdate").val() == NaN || $("#Indentdate").val() == null || $("#Indentdate").val() == undefined) {
        IsValid = false;

        $('#Indentdate').css("border-color", "red");
    }
    else {
        $('#Indentdate').css("border-color", "green");
    }
    //if ($("#UploadDocument").val() == "" || $("#UploadDocument").val() == NaN || $("#UploadDocument").val() == null || $("#UploadDocument").val() == undefined) {
    //    IsValid = false;

    //    $('#UploadDocument').css("border-color", "red");
    //    return false;
    //}
    //else {
    //    $('#UploadDocument').css("border-color", "green");
    //}


    if (IsValid == false) {
        return false;
    } else {
        return true;
    }





}



//function Reset() {
//    $('#SaleId').val("");

//    $("#CustomerId").select2("val", "0");
//    $('#PONo').val("");
//    $("#PoDate").val(GblDisplayDate());
//    $('#Address').val("");
//    $('#Balance').val("");
//    $('#GSTNo').val("");
//    $('#CINNo').val("");
//    $('#ECCNo').val("");

//    $('#QtyLimit').val("");



//    LocGetFilterList();

//    $("#Update").css({ 'display': 'none' });
//    $("#Save").css({ 'display': 'inline-block' });

//}











function fSendEnquiryMail() {
    //var isvalid = fvalidate();
    //if (isvalid == false) {
    //    return false;
    //}




    //var masterDTO = {};
    //masterDTO.itemList = FltrClmnArr;
    //masterDTO.SBMContact = $("#SBMContact").val();

    //masterDTO.Fk_FundingSourceId = $("#Fk_FundSourceId").val();
    //masterDTO.Indentdate = $("#Indentdate").val();
    //masterDTO.MobileNo = $("#MobileNo").val();
    //masterDTO.IsFinalSubmited1 = checkbox1;

    //masterDTO.DocUrl = ImgPath;

    //masterDTO.RequestorName = $("#RequestorName").val();
    //if ($("#PK_IndentMstId").val() == "" || $("#PK_IndentMstId").val() == undefined || $("#PK_IndentMstId").val() == isNaN || $("#PK_IndentMstId").val() == null) {
    //    masterDTO.PK_IndentMstId = "";
    //}
    //else {
    //    masterDTO.PK_IndentMstId = $("#PK_IndentMstId").val();
    //}

    //;

    /// Mail Send Message

    //"ProductName": "Essential Oil Distillation Plants",
    //"Name": "Ramu Verma",
    //"MobileNo": "6306395918",
    //"emailaddress": "sileo@the.com",
    //"Quantity": "1",
    //"PorposeRequairement": "Raw Material",
    //"RequirmentDetail": "Hello"

    var sndDTO = {};

    sndDTO.productName = "Essential Oil Distillation Plants";
    sndDTO.name = "Ramu Verma";
    sndDTO.mobileNo = "6306395918";
    sndDTO.emailaddress = "sileo@the.com";
    sndDTO.quantity = "1";
    sndDTO.unitType = "1";
    sndDTO.porposeRequairement = "Raw Material";
    sndDTO.requirmentDetail = "Hello";

    //"productName": "string",
    //    "name": "string",
    //        "emailAddress": "string",
    //            "mobileNo": "string",
    //                "quantity": "string",
    //                    "unitType": "string",
    //                        "porposeRequairement": "string",
    //                            "requirmentDetail": "string"

    /// End Sending mail

    $.ajax({
        type: "POST",
        url: "http://localhost:5290/api/Email/EmailRequest",
        // url: "http://api.swarajindia.com/api/Email/EmailRequest",
        data: JSON.stringify(sndDTO),
        contentType: "application/json;",
        dataType: "json",
        success: function (data) {
            var msg = "Something went wrong. please reresh the page & try again";
            if (data == "1") {
                msg = "Indent saved successfully";
            }
            else if (data == "2") {
                msg = "Indent Updated successfully";
            }

            fShowToast(msg);
            RedirectSame();
        },
        error: function (error) {
            fShowToasterror(error);
        }
    });

}

function RedirectSame() {

    window.location.reload();
    // window.location.href = "/ULB/Indents";
}
