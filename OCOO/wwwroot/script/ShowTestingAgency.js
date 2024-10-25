$(document).ready(function () {
    document.getElementById('export-excel').addEventListener('click', function () {
        // Get table data
     
       
        var table = document.getElementById('data-table');
        var data = XLSX.utils.table_to_sheet(table);

        // Create a workbook
        var wb = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, data, 'Sheet1');

        // Export the workbook as a downloadable Excel file
        XLSX.writeFile(wb, 'Billing_Report.xlsx');
    });
})
function fViewDetails(txtId) {
    $.ajax({
        url: '/Home/BillingDetails',
        type: 'GET',
        data: { Id: txtId },
        success: function (response) {
            if (response != null) {
                $('#Details').modal('show');
                $("#ShowSTPName").html(response.stpName);
                $("#ShowBillingDate").html(response.billingDate);
                $("#ShowDischarge").html(response.waterDischarge);
                if (response.isInhouse == true) {
                    $("#InHouseForm").css("display", "block");
                    $("#showInHouseBOD").html(response.inHouseBOD);
                    $("#showInHouseCOD").html(response.inHouseCOD);
                    $("#showInHouseTSS").html(response.inHouseTSS);
                    $("#showInHouseFC").html(response.inHouseFC);
                    $('#showInHouseBOD').css('color', response.inHouseBODColor);
                    $('#showInHouseCOD').css('color', response.inHouseCODColor);
                    $('#showInHouseTSS').css('color', response.inHouseFCColor);
                    $('#showInHouseFC').css('color', response.inHouseTSSColor);
                }
                else {
                    $("#InHouseForm").css("display", "none");
                }
                if (response.isTPI == true) {
                    $("#ThirdPartyForm").css("display", "block");
                    $("#showThirdPartyBOD").html(response.thirdPartyBOD);
                    $("#showThirdPartyCOD").html(response.thirdPartyCOD);
                    $("#showThirdPartyTSS").html(response.thirdPartyTSS);
                    $("#showThirdPartyFC").html(response.thirdPartyFC);
                    $('#showThirdPartyBOD').css('color', response.tpibodColor);
                    $('#showThirdPartyCOD').css('color', response.tpicodColor);
                    $('#showThirdPartyTSS').css('color', response.tpifcColor);
                    $('#showThirdPartyFC').css('color', response.tpitssColor);
                } else {
                    $("#ThirdPartyForm").css("display", "none");
                }
                if (response.isUPPCB == true) {
                    $("#UPPCBForm").css("display", "block");
                    $("#showUPPCBBOD").html(response.uppcbbod);
                    $("#showUPPCBCOD").html(response.uppcbcod);
                    $("#showUPPCBTSS").html(response.uppcbtss);
                    $("#showUPPCBFC").html(response.uppcbfc);
                    $('#showUPPCBBOD').css('color', response.uppcbbodColor);
                    $('#showUPPCBCOD').css('color', response.uppcbcodColor);
                    $('#showUPPCBTSS').css('color', response.uppcbtssColor);
                    $('#showUPPCBFC').css('color', response.uppcbfcColor);
                } else {
                    $("#UPPCBForm").css("display", "none");
                }
            }
        },
        error: function (error) {
            fShowToasterror(error);
        }
    });
}