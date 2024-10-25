
var TPIReportsCount = 0;
function ValidateGenerateForm() {
    if ($("#CoveringLetter").val() == null || $("#CoveringLetter").val() == '') {
        $("#CoveringLetter").focus();
        return false;
    }

    //if ($("#TPIReports").val() == null || $("#TPIReports").val() == '') {
    //    $("#TPIReports").focus();
    //    return false;
    //}
    //if ($("#LDSheet").val() == null || $("#LDSheet").val() == '') {
    //    $("#LDSheet").focus();
    //    return false;
    //}

    //if ($("#OtherDocuments").val() == null || $("#OtherDocuments").val() == '') {
    //    $("#OtherDocuments").focus();
    //    return false;
    //}
    if ($("#BillingDate").val() == null || $("#BillingDate").val() == '') {
        $("#BillingDate").focus();
        return false;
    }

    if (parseInt(TPIReportsCount) == 0) {
        $("#TPIReports").focus();
      return false;
    }

    if ($("#ComplaintsReceived24").val() == null || $("#ComplaintsReceived24").val() == '') {
        $("#ComplaintsReceived24").focus();
        return false;
    } if ($("#ComplaintsResolved24").val() == null || $("#ComplaintsResolved24").val() == '') {
        $("#ComplaintsResolved24").focus();
        return false;
    } if ($("#ComplaintsReceivedInBillingMonth").val() == null || $("#ComplaintsReceivedInBillingMonth").val() == '') {
        $("#ComplaintsReceivedInBillingMonth").focus();
        return false;
    } if ($("#ComplaintsResolvedInBillingMonth").val() == null || $("#ComplaintsResolvedInBillingMonth").val() == '') {
        $("#ComplaintsResolvedInBillingMonth").focus();
        return false;
    } if ($("#LDBondedComplaints").val() == null || $("#LDBondedComplaints").val() == '') {
        $("#LDBondedComplaints").focus();
        return false;
    }
    if ($("#BalanceComplaints").val() == null || $("#BalanceComplaints").val() == '') {
        $("#BalanceComplaints").focus();
        return false;
    } if ($("#Remarks").val() == null || $("#Remarks").val() == '') {
        $("#Remarks").focus();
        return false;
    }

    if ($("#Isdeclaration").is(":checked") == false) {
        fShowToastalert("Please! check verify condition checkbox")
        $("#Isdeclaration").focus();
        return false;
    }
    $("#IsFinalSave").val(true);
    var result = window.confirm("If you submit once, you will not be able to edit again.");
    if (result == false) {
        $("#IsFinalSave").val(false);
        return false;
    }
}

function ValidateForm() {
    if ($("#CoveringLetter").val() == null || $("#CoveringLetter").val() == '') {
        $("#CoveringLetter").focus();
        return false;
    }
    if ($("#TPIReports").val() == null || $("#TPIReports").val() == '') {
        $("#TPIReports").focus();
        return false;
    }
    if ($("#LDSheet").val() == null || $("#LDSheet").val() == '') {
        $("#LDSheet").focus();
        return false;
    }
    if ($("#OtherDocuments").val() == null || $("#OtherDocuments").val() == '') {
        $("#OtherDocuments").focus();
        return false;
    }
    $("#IsFinalSave").val(false);
}

function fCheckGeneratedBill() {
    $('input[type="submit"]').attr('disabled', false);
    $('input[type="submit"]').css('cursor', 'pointer');
    $('#msg').text('');
    let txtDate = $("#BillingDate").val();
    let txtFk_STPId = $("#Fk_STPId").val();

    $.ajax({
        url: '/Firm/CheckGeneratedBill',
        type: 'GET',
        data: { billDate: txtDate, Fk_STPId: txtFk_STPId },
        success: function (response) {
            if (response !== null) {
                if (response.status === '1') {
                    $('input[type="submit"]').attr('disabled', 'disabled');
                    $('input[type="submit"]').css('cursor', 'not-allowed');
                    $('#msg').text(response.message);
                    fShowToastalert(response.message);
                }
                else if (response.status === '2') {
                    $('input[type="submit"]').attr('disabled', 'disabled');
                    $('input[type="submit"]').css('cursor', 'not-allowed');
                    $('#msg').text(response.message);
                    fShowToastalert(response.message);
                }
                else {

                }
            }
        },
        error: function (error) {
            fShowToasterror(error);
        }
    })
}

function fComplaintBalance() {
    debugger;
    let txtCompReceived = $("#ComplaintsReceivedInBillingMonth").val();
    let txtCompResolved = $("#ComplaintsResolvedInBillingMonth").val();
    let result = txtCompReceived - txtCompResolved;
    if (result < 0) {
        result = 0;
    }
    $("#BalanceComplaints").val(result)

}

function fvalidateCoveringletter(input) {
    debugger

    var path = input.value;
    var ext = path.slice(path.lastIndexOf('.') + 1).toUpperCase()
    if (ext == "JPG" || ext == "JPEG" || ext == "PNG" || ext == "PDF") { }
    else {
        fShowToastalert('Invalid Format');
        $("#CoveringLetter").val('');
        $("#CoveringLetter").focus();
        $("#" + input.id).val('');
        $("#" + input.id).focus();

        return false;
    }
    var file = input.files[0];
    debugger
    if (file) {
        var fileSize = file.size; // File size in bytes
        //var maxSizeInBytes = 1024 * 1024; // 1MB in bytes
        var maxSizeInBytes = 5 * 1024 * 1024; // 5MB in bytes
        if (fileSize > maxSizeInBytes) {
            //fShowToastalert('Selected image exceeds 1MB. Please select a smaller image!');
            fShowToastalert('Selected image exceeds 5MB. Please select a smaller image!');
            $('#CoveringLetter').val('');
            $("#" + input.id).val('');
            $("#" + input.id).focus();
        }
    }
}
function fvalidateTPIReport(input) {
    debugger
    var files = input.files;
    var validExtensions = ['jpg', 'jpeg', 'png', 'pdf']; // Allowed extensions
    //var maxSizeInBytes = 1024 * 1024; // 1MB in bytes
    var maxSizeInBytes = 5 * 1024 * 1024; // 5MB in bytes
    for (var i = 0; i < files.length; i++) {
        var file = files[i];
        var fileSize = file.size;
        var fileName = file.name;
        var fileExtension = fileName.split('.').pop().toLowerCase();

        if (fileSize > maxSizeInBytes) {
            //fShowToastalert('File ' + fileName + ' exceeds 1MB. Please select a smaller file.');
            fShowToastalert('File ' + fileName + ' exceeds 5MB. Please select a smaller file.');
            input.value = ''; // Clear the input to remove invalid files
            $("#TPIReports").val('');
            $("#TPIReports").focus();
            return false;
        }

        if (!validExtensions.includes(fileExtension)) {
            fShowToastalert('File ' + fileName + ' has an invalid extension. Please select a valid file (jpg, jpeg, png, pdf)!');
            input.value = ''; // Clear the input to remove invalid files 
            $("#TPIReports").val('');
            $("#TPIReports").focus();
            return false;
        }
        $("#TPIReportsCount").val(files.length)
    }
}


function ValidateFiles(input) {
    debugger

    var path = input.value;
    var ext = path.slice(path.lastIndexOf('.') + 1).toUpperCase()
    if (ext == "JPG" || ext == "JPEG" || ext == "PNG" || ext == "PDF") { }
    else {
        fShowToastalert('Invalid Format');
        $("#CoveringLetter").val('');
        $("#CoveringLetter").focus();
        $("#" + input.id).val('');
        $("#" + input.id).focus();

        return false;
    }
    var file = input.files[0];
    debugger
    if (file) {
        var fileSize = file.size; // File size in bytes
        //var maxSizeInBytes = 1024 * 1024; // 1MB in bytes
        var maxSizeInBytes = 5 * 1024 * 1024; // 5MB in bytes
        if (fileSize > maxSizeInBytes) {
            //fShowToastalert('Selected image exceeds 1MB. Please select a smaller image!');
            fShowToastalert('Selected image exceeds 5MB. Please select a smaller image!');
            $('#CoveringLetter').val('');
            $("#" + input.id).val('');
            $("#" + input.id).focus();
        }
    }
}
function fvalidateLDSheet(input) {
    debugger

    var files = input.files;
    var validExtensions = ['jpg', 'jpeg', 'png', 'pdf']; // Allowed extensions
    //var maxSizeInBytes = 1024 * 1024; // 1MB in bytes
    var maxSizeInBytes = 5 * 1024 * 1024; // 5MB in bytes
    for (var i = 0; i < files.length; i++) {
        var file = files[i];
        var fileSize = file.size;
        var fileName = file.name;
        var fileExtension = fileName.split('.').pop().toLowerCase();

        if (fileSize > maxSizeInBytes) {
            fShowToastalert('File ' + fileName + ' exceeds 5MB. Please select a smaller file.');
            input.value = ''; // Clear the input to remove invalid files
            $("#LDSheet").val('');
            $("#LDSheet").focus();
            return false;
        }

        if (!validExtensions.includes(fileExtension)) {
            fShowToastalert('File ' + fileName + ' has an invalid extension. Please select a valid file (jpg, jpeg, png, pdf)!');
            input.value = ''; // Clear the input to remove invalid files 
            $("#LDSheet").val('');
            $("#LDSheet").focus();
            return false;
        }
        $("#LDReportCount").val(files.length)
    }
}
function fvalidateOtherDoc(input) {
    debugger
    var files = input.files;
    var validExtensions = ['jpg', 'jpeg', 'png', 'pdf']; // Allowed extensions
    //var maxSizeInBytes = 1024 * 1024; // 1MB in bytes
    var maxSizeInBytes = 5 * 1024 * 1024; // 5MB in bytes

    for (var i = 0; i < files.length; i++) {
        var file = files[i];
        var fileSize = file.size;
        var fileName = file.name;
        var fileExtension = fileName.split('.').pop().toLowerCase();

        if (fileSize > maxSizeInBytes) {
            fShowToastalert('File ' + fileName + ' exceeds 5MB. Please select a smaller file.');
            input.value = ''; // Clear the input to remove invalid files
            $("#OtherDocuments").val('');
            $("#OtherDocuments").focus();
            return false;
        }

        if (!validExtensions.includes(fileExtension)) {
            fShowToastalert('File ' + fileName + ' has an invalid extension. Please select a valid file (jpg, jpeg, png, pdf)!');
            input.value = ''; // Clear the input to remove invalid files 
            $("#OtherDocuments").val('');
            $("#OtherDocuments").focus();
            return false;
        }
        $("#OtherDocumentsCount").val(files.length)
    }
}

function fTPIDocvalidation() {
    debugger
    if ($("#TPIReports").val() == "" || $("#TPIReports").val() == null) {
        $("#TPIReports").focus()
        return false;
    }
    // if ($("#Remarks").val() == "0" || $("#Remarks").val() == "" || $("#Remarks").val() == null) {
    //     $("#Remarks").focus()
    //     return false;
    // }
    var file_data = $("#TPIReports").prop("files")[0];
    var form_data = new FormData();
    form_data.append("file", file_data);
    form_data.append("TPIReports", $('#TPIReports').val());
    form_data.append("Remarks", $('#Remark').val());
    form_data.append("Fk_BillId", $('#Fk_BillId').val());
    form_data.append("tokenNo", $('#tokenNo').val());

    form_data.append("save", $('#Save').val());
    $.ajax({
        url: '/FirmBilling/AddTpiDocument',
        cache: false,
        contentType: false,
        processData: false,
        dataType: 'json',
        data: form_data,
        type: 'post',
        success: function (result) {
            if (result != null) {

                fDoc_Details();
            }
            else {
                alert('Some Thing wrong1!');
            }
        },
        error: function () {
            alert('error');
        }
    });
}
function fDoc_Details() {
    debugger;
   

    var Token_id = $('#tokenNo').val()
    var Remark = $('#Remarks').val()
    $.ajax({
        url: '/FirmBilling/TpiDocumentDetails',
        data: { Fk_BillId: $('#Fk_BillId').val(), tokenNo: Token_id },
        type: 'post',
        async: false,
        success: function (result) {
           

            if (result.tpidetail != null) {

                var str = "";
                var icon = "";
                $("#Tpi_Description tbody").html('');
                for (var i = 0; i < result.tpidetail.length; i++) {
                    TPIReportsCount = 1;
                    debugger
                    if (result.tpidetail[i].docFormat == "Image") {
                        icon = " <i class='fa fa-file-image-o'></i >";
                    }
                    else {
                        icon = "<i class='fa fa-file-pdf-o'></i>";
                    }
                    str += "<tr><td>" + icon + "</td><td style='width: 58%;'>" + result.tpidetail[i].remark + "</td><td style='text - align: end; width: 14%;'><a href=/FirmBilling/DownloadImage?filename=" + result.tpidetail[i].files + "><i class='ti-import mr-2 imp'></i></a> <a href='#' data-original-title='Delete' onclick='return DeleteTpiDocument(" + result.tpidetail[i].pK_FTDId + ")'> <i class='ti-trash mr-2 del'></i > </a></td></tr > ";
                }
                $("#Tpi_Description tbody").html(str);
                $('#PK_FTDId').val(result.tpidetail[0].PK_FTDId);
                $('#TPIReports').val('');
                $('#Remark').val('');
            }
        },
        error: function () {
            alert('error');
        }
    });
}
function DeleteTpiDocument(PK_FTDId) {
    var result;
    var r = confirm("Are you sure want to delete Documents ?");
    if (r == true) {
        $.ajax({
            url: '/FirmBilling/DeleteTpiDetails',
            data: { PK_FTDId: PK_FTDId },
            type: 'post',
            success: function (result) {
                if (result != null) {
                    fDoc_Details();
                }
                else {
                    alert('Documents Not Deleted !');
                }
            },
            error: function () {
                alert('error');
            }
        });
    }
}