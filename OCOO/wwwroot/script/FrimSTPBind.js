function fGetFirmList(abc) {
    
    debugger;
    let txtFk_ZoneId = $("#Pk_ZoneId").val();
    $.ajax({
        url: '/Admin/GetFirmDDl',
        type: 'GET',
        data: { Id: txtFk_ZoneId },
        success: function (response) {
            
            if (response != null) {
                debugger;
                $("#Fk_FirmId").html('');
                let dd = ''; // Initialize dd as an empty string
                for (let i = 0; i < response.length; i++) { // Fix the loop condition
                    if (abc == response[i].value) {
                        dd += '<option value="' + response[i].value + '"selected>' + response[i].text + '</option>';
                    }
                    else {
                        dd += '<option value="' + response[i].value + '">' + response[i].text + '</option>';
                    }
                    
                }
                $("#Fk_FirmId").html(dd);
            }
        },
        error: function (error) {
            fShowToasterror(error);
        }
    });
}

function fGetSTPList(firm,stp) {
    debugger;
    let txtFk_FirmId = 0;
    if (firm == '0' || firm == '' || firm == undefined) {
        txtFk_FirmId = $("#Fk_FirmId").val();
    } else {
        txtFk_FirmId = firm;
    }
          
    $.ajax({
        url: '/Admin/GetSTPDDl',
        type: 'GET',
        data: { Id: txtFk_FirmId },
        success: function (response) {
            if (response != null) {
                
                //console.log(response);
                $("#Fk_STPId").html('');
                let dd = '';
                for (let i = 0; i < response.length; i++) {
                    if (stp == response[i].value) {
                        dd += '<option value="' + response[i].value + '"selected>' + response[i].text + '</option>';
                    }
                    else {
                        dd += '<option value="' + response[i].value + '">' + response[i].text + '</option>';
                    }
                    
                }
                //console.log(dd)
                $("#Fk_STPId").html(dd);
            }
        },
        error: function (error) {
            fShowToasterror(error);
        }
    })
}
function fGetSTPList12(firm, stp) {
    debugger;
    let txtFk_FirmId = 0;
    if (firm == '0' || firm == '' || firm == undefined) {
        txtFk_FirmId = $("#Fk_FirmId").val();
    } else {
        txtFk_FirmId = firm;
    }
          
    $.ajax({
        url: '/STP/GetSTPDDl2',
        type: 'GET',
        data: { Id: txtFk_FirmId },
        success: function (response) {
            if (response != null) {
                
                //console.log(response);
                $("#Fk_STPId").html('');
                let dd = '';
                for (let i = 0; i < response.length; i++) {
                    if (stp == response[i].value) {
                        dd += '<option value="' + response[i].value + '"selected>' + response[i].text + '</option>';
                    }
                    else {
                        dd += '<option value="' + response[i].value + '">' + response[i].text + '</option>';
                    }
                    
                }
                //console.log(dd)
                $("#Fk_STPId").html(dd);
            }
        },
        error: function (error) {
            fShowToasterror(error);
        }
    })
}