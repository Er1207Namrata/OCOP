let txtIsBOD;
let txtIsCOD;
let txtIsTSS;
let txtIsFC;
$(document).ready(function () {

    $('#InHouseCheck').on('change', function () {
        //
        if ($(this).is(':checked')) {
            $("#InHouseForm").css("display", "block");
        } else {
            $("#InHouseForm").css("display", "none");
            $("#InHouseBOD").val('');
            $("#InHouseCOD").val('');
            $("#InHouseTSS").val('');
            $("#InHouseFC").val('');
        }
    });

    $('#ThirdPartyCheck').on('change', function () {
        //
        if ($(this).is(':checked')) {
            $("#ThirdPartyForm").css("display", "block");
        } else {
            $("#ThirdPartyForm").css("display", "none");
            $("#ThirdPartyBOD").val('');
            $("#ThirdPartyCOD").val('');
            $("#ThirdPartyTSS").val('');
            $("#ThirdPartyFC").val('');
        }
    });

    $('#UPPCBCheck').on('change', function () {
        //
        if ($(this).is(':checked')) {
            $("#UPPCBForm").css("display", "block");
        } else {
            $("#UPPCBForm").css("display", "none");
            $("#UPPCBCOD").val('');
            $("#UPPCBBOD").val('');
            $("#UPPCBTSS").val('');
            $("#UPPCBFC").val('');
        }
    });

});
// $('#InHouseCheck').on('change', function () {
//
//     if ($(this).is(':checked')) {
//         $("#InHouseStatus").prop("disabled", false);
//     } else {
//         $("#InHouseStatus").prop("disabled", true);
//     }
// });

// $('#ThirdPartyCheck').on('change', function () {
//
//     if ($(this).is(':checked')) {
//         $("#ThirdPartyStatus").prop("disabled", false);
//     } else {
//         $("#ThirdPartyStatus").prop("disabled", true);
//     }
// });

// $('#UPPCBCheck').on('change', function () {
//
//     if ($(this).is(':checked')) {
//         $("#UPPCBStatus").prop("disabled", false);
//     } else {
//         $("#UPPCBStatus").prop("disabled", true);
//     }
// });

// function fPumpStationForm() {

//     var table = $('#tblPoSale').DataTable({ retrieve: true, "paging": false, bFilter: false, bInfo: false, "ordering": false, }).clear();
//     let count = 0;
//     for (var i = 1; i <= txtPumpStation; i++) {

//         var RowsData = $('<tr>' +

//             '<td style="width:80px"> <div class=""> <input type="checkbox" id="Checkbox_' + i + '" /> <label for="Checkbox_' + i + '">  </label></div > </td>' +

//             '<td> <div class="input-group"> <select id="Agency_' + i + '" style="width:150px" name="Agency_' + i + '" class="ddlAgency form-control ddlItem select2"> </select></div></td>' +

//             '<td> <div class="input-group"> <select id="Status_' + i + '" style="width:150px" name="Status_' + i + '" class="form-control ddlItem select2"> <option value="0"> --select-- </option> <option value="Pass"> Pass </option> <option value="Failed"> Failed </option></select></div></td>' +

//             '</tr>');
//         count++;
//         table.row.add(RowsData).draw(false);
//     }
//     $("#Count").val(count);

// }



function ShowPopup() {
    $('#Submit').val('Submit');
    $('#txtDetails').modal('show');
    $("#InHouseForm").css("display", "none");
    $("#InHouseCheck").prop("checked", false);
    $("#ThirdPartyForm").css("display", "none");
    $("#ThirdPartyCheck").prop("checked", false);
    $("#UPPCBForm").css("display", "none");
    $("#UPPCBCheck").prop("checked", false);
    $("#Pk_BillingId").val('');
    $("#BillingDate").val('');
    $("#Pk_STPId").val('0');
    $("#WaterDischarge").val('');
    $("#InHouseBOD").val('');
    $("#InHouseCOD").val('');
    $("#InHouseTSS").val('');
    $("#InHouseFC").val('');
    $("#ThirdPartyBOD").val('');
    $("#ThirdPartyCOD").val('');
    $("#ThirdPartyTSS").val('');
    $("#ThirdPartyFC").val('');
    $("#UPPCBCOD").val('');
    $("#UPPCBBOD").val('');
    $("#UPPCBTSS").val('');
    $("#UPPCBFC").val('');
    $("#ComplaintReceived").val('');
    $("#ComplaintResolved").val('');
    $("#Submit").html('<i class="fa fa-save"> Save</i>');

    $('#msg').text('')
    $('#Submit').attr('disabled', false);

    $('#Submit').css('cursor', 'pointer');
    $('#capcityExceedmsg').text('');
    $('#capcityExceedmsg1').text('');
}

function fGetSTPBillDetails() {
    FClear_onSTP();
    var txtId = $("#Pk_STPId").val();

    if (txtId == null || txtId == '' || txtId == '0') {
        //$("#Pk_STPId").focus()
        //alert("Please select STP !")
        return false;
    }

    $.ajax({
        url: '/Firm/GetSTPBillDetails',
        type: 'GET',
        data: { Id: txtId },
        dataType: 'json',
        success: function (data) {

            txtIsBOD = data.isBOD;
            txtIsCOD = data.isCOD;
            txtIsTSS = data.isTSS;
            txtIsFC = data.isFC;

            if (txtIsFC === true) {
                $(".IsFc").removeClass("d-none");
            }
        },
        error: function (error) {
            fShowToasterror(error);
        }
    });
    $('#WaterDischarge').val('');
}

function fGetCallCenterLog() {
    //debugger
    $('#msg').text('')
    $('#Submit').attr('disabled', false);
    //
    let txtId = $("#Pk_STPId").val();
    let txtDate = $("#BillingDate").val();
    if (txtId == null || txtId == '' || txtId == '0') {
        $("#Pk_STPId").focus()
        fShowToastalert("Please select STP !")
        return false;
    }
    $.ajax({
        url: '/Firm/CheckEntryBill',
        type: 'GET',
        data: { billDate: txtDate, Fk_STPId: txtId },
        success: function (response) {

            if (response !== null) {
                if (response.status === '1') {
                    $('#msg').text(response.message);
                    $('#Submit').attr('disabled', true);
                    $('#Submit').css('cursor', 'not-allowed');
                    $("#BillingDate").val('')
                    fShowToastalert(response.message);
                }
                else if (response.status === '2') {
                    $('#msg').text(response.message);
                    $('#Submit').attr('disabled', true);
                    $('#Submit').css('cursor', 'not-allowed');
                    fShowToastalert(response.message);
                    $("#BillingDate").val('')
                    return false;
                }
                else {

                    $('#msg').text('');
                    if ($("#WaterDischarge").val() != null && $("#WaterDischarge").val() != undefined && $("#WaterDischarge").val() != NaN && $("#WaterDischarge").val() != "" && $("#WaterDischarge").val() != "0") {
                        fGetDischargeBillDetails()
                    }
                }
            }
        },
        error: function (error) {
            fShowToasterror(error);
        }
    })

    //$.ajax({
    //    url: '/Firm/GetSTPCallLog',
    //    type: 'GET',
    //    dataType: 'json',
    //    data: { stpId: txtId, BillingDate: txtDate },
    //    success: function (response) {
    //        if (response != null) {
    //            $("#ComplaintReceived").val(response.complaintReceived);
    //            $("#ComplaintResolved").val(response.complaintResolved);
    //        }
    //    },
    //    error: function () {

    //    }
    //});
}


function ValidateForm() {
    //debugger
    if ($("#Pk_STPId").val() == null || $("#Pk_STPId").val() == '' || $("#Pk_STPId").val() <= 0) {
        $("#Pk_STPId").focus();
        return false;
    }
    if ($("#BillingDate").val() == null || $("#BillingDate").val() == '') {
        $("#BillingDate").focus();
        return false;
    }
    if ($("#WaterDischarge").val() == null || $("#WaterDischarge").val() == '' || $("#WaterDischarge").val() <= 0) {
        $("#WaterDischarge").focus();
        return false;
    }
    if ($("#InHouseCheck").is(":checked")) {
        if ($("#InHouseBOD").val() == null || $("#InHouseBOD").val() == '' || $("#InHouseBOD").val() == 0) {
            $("#InHouseBOD").focus()
            fShowToastalert('BOD Should be greater than zero!')
            return false;
        }
        if ($("#InHouseCOD").val() == null || $("#InHouseCOD").val() == '' || $("#InHouseCOD").val() == 0) {
            $("#InHouseCOD").focus()
            fShowToastalert('COD Should be greater than zero!')
            return false;
        }
        if ($("#InHouseTSS").val() == null || $("#InHouseTSS").val() == '') {
            $("#InHouseTSS").focus()
            fShowToastalert('TSS Should be greater than zero!')
            return false;
        }
        if (txtIsFC == true) {
            if ($("#InHouseFC").val() == null || $("#InHouseFC").val() == '') {
                $("#InHouseFC").focus()
                fShowToastalert("Please fill In House FC Reported Value")
                return false;
            }
        }

    }
    if ($("#ThirdPartyCheck").is(":checked")) {
        if ($("#ThirdPartyBOD").val() == null || $("#ThirdPartyBOD").val() == '' || $("#ThirdPartyBOD").val() == 0) {
            $("#ThirdPartyBOD").focus()
            fShowToastalert('BOD Should be greater than zero!')
            return false;
        }
        if ($("#ThirdPartyCOD").val() == null || $("#ThirdPartyCOD").val() == '' || $("#ThirdPartyCOD").val() == 0) {
            $("#ThirdPartyCOD").focus()
            fShowToastalert('COD Should be greater than zero!')
            return false;
        }
        if ($("#ThirdPartyTSS").val() == null || $("#ThirdPartyTSS").val() == '') {
            $("#ThirdPartyTSS").focus()
            fShowToastalert('TSS Should be greater than zero!')
            return false;
        }
        if (txtIsFC == true) {
            if ($("#ThirdPartyFC").val() == null || $("#ThirdPartyFC").val() == '') {
                $("#ThirdPartyFC").focus()
                fShowToastalert("Please fill In Third Party FC Reported Value")
                return false;
            }
        }
    }
    if ($("#UPPCBCheck").is(":checked")) {
        if ($("#UPPCBBOD").val() == null || $("#UPPCBBOD").val() == '' || $("#UPPCBBOD").val() == 0) {
            $("#UPPCBBOD").focus()
            fShowToastalert('BOD Should be greater than zero!')
            return false;
        }
        if ($("#UPPCBCOD").val() == null || $("#UPPCBCOD").val() == '' || $("#UPPCBCOD").val() == 0) {
            $("#UPPCBCOD").focus()
            fShowToastalert('COD Should be greater than zero!')
            return false;
        }
        if ($("#UPPCBTSS").val() == null || $("#UPPCBTSS").val() == '') {
            $("#UPPCBTSS").focus()
            fShowToastalert('TSS Should be greater than zero!')
            return false;
        }
        if (txtIsFC == true) {
            if ($("#UPPCBFC").val() == null || $("#UPPCBFC").val() == '') {
                $("#UPPCBFC").focus()
                fShowToastalert("Please fill In UPP CB FC Reported Value")
                return false;
            }
        }
    }

    if ($("#ComplaintReceived").val() == null || $("#ComplaintReceived").val() == '' || $("#ComplaintReceived").val() == undefined || $("#ComplaintReceived").val() == NaN) {
        $("#ComplaintReceived").focus();
        return false;
    }
    if ($("#ComplaintResolved").val() == null || $("#ComplaintResolved").val() == '' || $("#ComplaintResolved").val() == undefined) {
        $("#ComplaintResolved").focus();
        return false;
    }
    if (!$("#InHouseCheck").is(":checked") && !$("#ThirdPartyCheck").is(":checked") && !$("#UPPCBCheck").is(":checked")) {
        fShowToastalert('Please select minimum 1 agency!')
        return false;
    }

    if (!$("#InHouseCheck").is(":checked")) {
        fShowToastalert('Please select In House testing agency!')
        return false;
    }
    var date = checkdate();
    if (date == false) {
        return false;
    }
    if (!checkBillingDates()) {
        return false;
    }
    // fGetDischargeBillDetails();

    //debugger
    let txtDischarge = $("#WaterDischarge").val();
    let oldvalue = $("#checksamecap").val();

    if (txtDischarge == oldvalue) {
        $("#IsSame").val(true)
    }
    else {
        $("#IsSame").val(false)

    }
}

function EditForm(txtId) {
    $.ajax({
        url: '/Firm/BillingDetails',
        type: 'GET',
        data: { Id: txtId },
        success: function (response) {
            if (response != null) {
                $('#txtDetails').modal('show');
                $("#Pk_BillingId").val(response.pk_BillingId);
                $("#BillNo").val(response.billNo);
                $("#BillingDate").val(response.billingDate);
                $("#Pk_STPId").val(response.pk_STPId);
                $("#WaterDischarge").val(response.waterDischarge);

                if (response.inHouseBOD != null || response.inHouseCOD != null || response.inHouseTSS != null || response.inHouseFC != null) {
                    $("#InHouseForm").css("display", "block");
                    $("#InHouseCheck").prop("checked", true);
                    $("#InHouseBOD").val(response.inHouseBOD);
                    $("#InHouseCOD").val(response.inHouseCOD);
                    $("#InHouseTSS").val(response.inHouseTSS);
                    $("#InHouseFC").val(response.inHouseFC);
                }
                if (response.thirdPartyBOD != null || response.thirdPartyCOD != null || response.thirdPartyTSS != null || response.thirdPartyFC != null) {
                    $("#ThirdPartyForm").css("display", "block");
                    $("#ThirdPartyCheck").prop("checked", true);
                    $("#ThirdPartyBOD").val(response.thirdPartyBOD);
                    $("#ThirdPartyCOD").val(response.thirdPartyCOD);
                    $("#ThirdPartyTSS").val(response.thirdPartyTSS);
                    $("#ThirdPartyFC").val(response.thirdPartyFC);
                }
                if (response.uppcbcod != null || response.uppcbbod != null || response.uppcbtss != null || response.uppcbfc != null) {
                    $("#UPPCBForm").css("display", "block");
                    $("#UPPCBCheck").prop("checked", true);
                    $("#UPPCBCOD").val(response.uppcbcod);
                    $("#UPPCBBOD").val(response.uppcbbod);
                    $("#UPPCBTSS").val(response.uppcbtss);
                    $("#UPPCBFC").val(response.uppcbfc);
                }
                $("#ComplaintReceived").val(response.complaintReceived);
                $("#ComplaintResolved").val(response.complaintResolved);
                $('#Submit').val('Update');
                $('#Submit').html('<i class="fa fa-pencil" > Update < /i>');


            }
        },
        error: function (error) {
            fShowToasterror(error);
        }
    });
}

function fGetDischargeBillDetails() {
    //debugger
    $('#msg').text('')
    $('#Submit').attr('disabled', false);
    let txtId = $("#Pk_STPId").val();
    let txtDischarge = $("#WaterDischarge").val();
    if (txtDischarge == null || txtDischarge == '' || txtDischarge == '0') {
        $("#WaterDischarge").focus()
        fShowToastalert("Please Enter Water Discharge !")
        return false;
    }
    if (txtId == null || txtId == '' || txtId == '0') {
        $("#Pk_STPId").focus()
        fShowToastalert("Please select STP !")
        return false;
    }
    $.ajax({
        url: '/Firm/CheckDischargeEntryBill',
        type: 'GET',
        data: { WaterDischarge: txtDischarge, Fk_STPId: txtId },
        success: function (response) {
            if (response !== null) {
                debugger
                if (response.status === '1') {
                    // $('#capcityExceedmsg').text(response.message);
                    //$('#Submit').attr('disabled', false);
                    //$('#Submit').css('cursor', 'pointer');
                    // alert(response.message);
                    $('#capcityExceedmsg').text('');
                }
                else if (response.status === '0') {
                    $('#capcityExceedmsg').text(response.message);
                    //$('#Submit').attr('disabled', true);
                    // $('#Submit').css('cursor', 'not-allowed');
                    // alert(response.message);
                }
                else {
                    $('#capcityExceedmsg').text('');

                }
                fGetSamecapacityDetails()
            }
        },
        error: function (error) {
            fGetSamecapacityDetails()
            fShowToasterror(error);
        }
    })
}

function fGetSamecapacityDetails() {
    //debugger
    let txtId = $("#Pk_STPId").val();
    let txtDischarge = $("#WaterDischarge").val();
    var BillingDate = $("#BillingDate").val();
    if (txtId == '0') {
        $("#Pk_STPId").focus()
        fShowToastalert("Please Select STP !")
        return false;
    } if (txtDischarge == '') {
        $("#WaterDischarge").focus()
        fShowToastalert("Please Enter Water Discharge !")
        return false;
    } if (BillingDate == '' || BillingDate == '0' || BillingDate == "") {
        $("#BillingDate").focus()
        fShowToastalert("Please Enter Billing Date !")
        return false;
    }
    else {
        $.ajax({
            url: '/Firm/ChecksameDischarge',
            type: 'GET',
            data: { WaterDischarge: txtDischarge, Fk_STPId: txtId, BillingDate: BillingDate },
            success: function (response) {
                if (response !== null) {
                    if (response.status === '1') {
                        $('#capcityExceedmsg1').text(response.message);
                        $("#checksamecap").val(txtDischarge);
                    }
                    else {
                        $('#capcityExceedmsg1').text('');
                        //return false;
                    }
                }
            },
            error: function (error) {
                fShowToasterror(error);
            }
        })
    }
}

function checkdate() {
    const dateToCheck11 = $("#BillingDate").val();
    // Split the date string into day, month, and year components
    const [day, month, year] = dateToCheck11.split('/');

    // Create a Date object (Note: Months are zero-based in JavaScript Date)
    const dateToCheck = new Date(`${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`);


    // Get the current date
    const currentDate = new Date();
    // Compare the dates
    if (dateToCheck > currentDate) {
        fShowToastalert('Future date is not allowed!');
        $("#BillingDate").val('');
        $("#BillingDate").focus();
        return false;
    }
}

function checkBillingDates() {
    const startDate = $("#LastBillingDate").val();
    const endDate = $('#BillingDate').value;  
    var start_Dateparts = startDate.split('/');
    var parts = endDate.split('/');
    var _startdate = new Date(start_Dateparts[2], start_Dateparts[1] - 1, start_Dateparts[0]);
    var date = new Date(parts[2], parts[1] - 1, parts[0]);

    if (date < _startdate) {
        alert("Sorry, you cannot make an entry for a previous date.");
        document.querySelector('.datepicker').value = startDate;
        return false;
    }
    else {
        return true;
    }
}
function FClear_onSTP() {

    $("#InHouseForm").css("display", "none");
    $("#InHouseCheck").prop("checked", false);
    $("#ThirdPartyForm").css("display", "none");
    $("#ThirdPartyCheck").prop("checked", false);
    $("#UPPCBForm").css("display", "none");
    $("#UPPCBCheck").prop("checked", false);
    // $("#Pk_BillingId").val('');
    $("#BillingDate").val('');
    //$("#Pk_STPId").val('0');
    $("#WaterDischarge").val('');
    $("#InHouseBOD").val('');
    $("#InHouseCOD").val('');
    $("#InHouseTSS").val('');
    $("#InHouseFC").val('');
    $("#ThirdPartyBOD").val('');
    $("#ThirdPartyCOD").val('');
    $("#ThirdPartyTSS").val('');
    $("#ThirdPartyFC").val('');
    $("#UPPCBCOD").val('');
    $("#UPPCBBOD").val('');
    $("#UPPCBTSS").val('');
    $("#UPPCBFC").val('');
    $("#ComplaintReceived").val('');
    $("#ComplaintResolved").val('');
    $('#msg').text('');
    $('#capcityExceedmsg').text('');
    $('#capcityExceedmsg1').text('');
}
