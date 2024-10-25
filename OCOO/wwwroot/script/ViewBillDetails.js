
function ViewDailyDetail(fk_stpid, fk_invId) {
    //;
    var fk_stpid = fk_stpid;
    var fk_invId = fk_invId;
    $.ajax({
        url: '/Firm/ViewInvoiceBill', // Replace with your server endpoint
        type: 'GET', // or 'POST' depending on your server implementation
        data: { fk_stpid: fk_stpid, fk_invId: fk_invId }, // Change to the appropriate data type
        success: function (data) {
            if (data != null) {
                let str = "";
                let srno = 1;
                $("#dtList tbody").html('');
                for (var i = 0; i < data.length; i++) {
                    str += "<tr><td>" + srno + "</td><td>" + data[i].billingDate + "</td><td>" + data[i].amountOfMLDSTP + "</td><td>" + data[i].capacity + "</td><td>" + data[i].actual_Achieved + "</td><td>" + data[i].treated_for_Payement + "</td><td>" + data[i].asPerCBFixCharges_60 + "</td><td>" + data[i].asPerActual_FixCharges_60 + "</td><td>" + data[i].asPerCBFixCharges_40 + "</td><td>" + data[i].asPerActual_FixCharges_40 + "</td><td>" + data[i].bodReportedValue + "</td><td>" + data[i].bodAmount + "</td><td>" + data[i].bodldAmount + "</td><td>" + data[i].codReportedValue + "</td><td>" + data[i].codAmount + "</td><td>" + data[i].codldAmount + "</td><td>" + data[i].tssReportedValue + "</td><td>" + data[i].tssAmount + "</td><td>" + data[i].tssldAmount + "</td><td>" + data[i].fcReportedValue + "</td><td>" + data[i].fcAmount + "</td><td>" + data[i].fcldAmount + "</td><td>" + data[i].totalVerifiedAmount + "</td><td>" + data[i].totalLDAmount + "</td></tr>"

                    srno++;
                }
                $("#dtList tbody").html(str);
            }

        },
        error: function (error) {
            fShowToasterror(error);
        }
    });
    $('#txtDetails').modal('show');
}
