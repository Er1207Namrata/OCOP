
var RptFilter_Clmn_indx = 0;
var ItemMasterLst = null;
var Doc_ImagePath = "";
$(document).ready(function () {


    ////fGetSalesCustomerDet();
    //GetItemName();


    ////  $("#CustomerId").select2();

    //var PK_IndentMstId = $("#PK_IndentMstId").val();
    //if (PK_IndentMstId == null || PK_IndentMstId == undefined || PK_IndentMstId == "" || PK_IndentMstId == NaN || PK_IndentMstId == "0") {
    //    PK_IndentMstId = 0;
    //}
    //if (parseInt(PK_IndentMstId) > 0) {
    //    fEditIndentd(PK_IndentMstId);
    //} else {
    //    $("#ProcurementDate").val(GblDisplayDate());
    //    LocGetFilterList();
    //}

   


   




});





function GetItemName() {
    $.ajax({
        url: '/ULB/GetItemName', type: 'post', dataType: 'json',
        data: { 'CompanyName': null },
        async: false,
        success: function (data) {
            // 
            ItemMasterLst = data.lstItem;

            // console.log(data);
            //$("#Item").html(""); // clear before appending new list
            //$("#Item").empty();
            //$("#Item").append(
            //    $('<option></option>').val(0).html('Select Item'));
            //$.each(data.lstItem, function(index, ItemCount) {
            //    $("#Item").append(
            //        $('<option></option>').val(ItemCount.value).html(ItemCount.text));
            //});

        }

    });
}





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





function LocGetFilterList() {
    
    var table = $('#tblPoSale').DataTable({ retrieve: true, "paging": false, bFilter: false, bInfo: false, "ordering": false, fixedHeader: true });
    table.clear().draw();
    RptFilter_Clmn_indx = 0;
    for (var i = 0; i < 1; i++) {
        var RowsData = $('<tr>' +
            //'<td>' + parseInt(i + 1) + '</td>' +

            '<td>   <select id="Item' + i + '" style="width:250px" class="form-control ddlItem select2"></select></td>' +



            '<td> <div class="input-group"> <input type="Text"  style="width:100px" class="form-control Quantity Quantity' + i + '" onkeypress="return isNumberKey(event)" onchange="return fgridItemDetails(' + i + ',' + '0' + ')"   id="Quantity' + i + '" /> </td >' +

            '<td> <div class="input-group"> <input type="Text"  style="width:200px" class="form-control GrossRate GrossRate' + i + '" onkeypress="return isNumberKey(event)" onchange="return fcalcFinalAMount(' + i + ')"  id="GrossRate' + i + '" /> </td >' +



            '<td> <div class="input-group"> <input type="Text" disabled style="width:200px" class="form-control GrossTotal GrossTotal' + i + '"  id="GrossTotal' + i + '" /> </td >' +

            '<td> <div class="input-group"> <input type="Text"  style="width:330px" class="form-control txtReason txtReason' + i + '"  id="txtReason' + i + '" /> </td >' +


            '<td>      <button type="button" style="width:40px" onclick="AddNewFilterRow()" class="btn btn-primary"><i class="fa fa-plus"></i></button> </td>' +

            '<td> <button type="button" style="width:40px"  onclick="RemoveFilterRow()" disabled class="btn btn-danger btndelete"><i class="fa fa-trash"></i></button></td>' +

            '</tr>');
        table.row.add(RowsData).draw(false);
        BindLocItemCode(i, 0);

    }

}

//function AddNewFilterRow() {
//    RptFilter_Clmn_indx = (parseInt(RptFilter_Clmn_indx) + 1);
//    var table = $('#tblPoSale').DataTable({ retrieve: true, "paging": false, bFilter: false, bInfo: false, "ordering": false, });
//    var tableindex = 0;// ($('#tblPoSale >tbody >tr').length);
//    //if (tableindex == undefined || tableindex == NaN || tableindex == "")
//    //{
//    tableindex = RptFilter_Clmn_indx;
//    //}

//    var RowsData = $('<tr>' +
//        //'<td>' + parseInt(i + 1) + '</td>' +

//        '<td>   <select id="Item' + tableindex + '" style="width:250px" class="form-control ddlItem select2"     ></select></td>' +

//        '<td> <div class="input-group"> <input type="Text"  style="width:100px"  onchange="return fgridItemDetails(' + tableindex + ',' + '0' + ')" class="form-control Quantity  Quantity' + tableindex + '"  onkeypress="return isNumberKey(event)"  id="Quantity' + tableindex + '" /> </td >' +

//        '<td> <div class="input-group"> <input type="Text"  style="width:100px" class="form-control GrossRate  GrossRate' + tableindex + '" onchange="return fcalcFinalAMount(' + tableindex + ')" onkeypress="return isNumberKey(event)" id="GrossRate' + tableindex + '" /> </td >' +

//        //'<td> <div class="input-group"> <input type="Text" disabled style="width:100px" class="form-control  GrossTotal' + tableindex + '"  id="GrossTotal' + tableindex + '" /> </td >' +

//        //'<td> <div class="input-group"> <input type="Text"  style="width:300px" class="form-control txtReason txtReason' + tableindex + '"  id="txtReason' + tableindex + '" /> </td >' +

//        //'<td>      <button type="button" style="width:40px"  onclick="AddNewFilterRow()" class="btn btn-primary"> <i class="fa fa-plus"></i></button> </td>' +
//        //'<td><button type="button" style="width:40px" onclick="RemoveFilterRow()"  class="btn btn-danger btndelete"> <i class="fa fa-trash"></i></button></td>' +

//        '</tr>');
//    table.row.add(RowsData).draw(false);
//    BindLocItemCode(tableindex, 0);
//}

//function fPumpStationForm() {
        
//        RptFilter_Clmn_indx = (parseInt(RptFilter_Clmn_indx) + 1);
//        var table = $('#tblPoSale').DataTable({ retrieve: true, "paging": false, bFilter: false, bInfo: false, "ordering": false, });
//        var tableindex = 0;// ($('#tblPoSale >tbody >tr').length);
//        //if (tableindex == undefined || tableindex == NaN || tableindex == "")
//        //{
//        tableindex = RptFilter_Clmn_indx;
//        //}
//    
//    for (var i = 1; i < 5; i++) {
//        
//        alert(i)
//        var RowsData = $('<tr>' +
//            //'<td>' + parseInt(i + 1) + '</td>' +

//            '<td>   <select id="Item' + tableindex + '" style="width:250px" class="form-control ddlItem select2" ></select></td>' +

//            '<td> <div class="input-group"> <input type="Text"  style="width:100px"  onchange="return fgridItemDetails(' + tableindex + ',' + '0' + ')" class="form-control Quantity  Quantity' + tableindex + '"  onkeypress="return isNumberKey(event)"  id="Quantity' + tableindex + '" /> </td >' +

//            '<td> <div class="input-group"> <input type="Text"  style="width:100px" class="form-control GrossRate  GrossRate' + tableindex + '" onchange="return fcalcFinalAMount(' + tableindex + ')" onkeypress="return isNumberKey(event)" id="GrossRate' + tableindex + '" /> </td >' +

//            '<td> <div class="input-group"> <input type="Text" disabled style="width:100px" class="form-control  GrossTotal' + tableindex + '"  id="GrossTotal' + tableindex + '" /> </td >' +

//            '<td> <div class="input-group"> <input type="Text"  style="width:300px" class="form-control txtReason txtReason' + tableindex + '"  id="txtReason' + tableindex + '" /> </td >' +

//            '<td>      <button type="button" style="width:40px"  onclick="AddNewFilterRow()" class="btn btn-primary"> <i class="fa fa-plus"></i></button> </td>' +
//            '<td><button type="button" style="width:40px" onclick="RemoveFilterRow()"  class="btn btn-danger btndelete"> <i class="fa fa-trash"></i></button></td>' +

//            '</tr>');
//        table.row.add(RowsData).draw(false);
//       // BindLocItemCode(tableindex, 0);
//    }
//}

function RemoveFilterRow() {

    var TableName = 'tblPoSale';
    var className = 'btndelete';
    var Count = 0;
    var table = $('#' + TableName).DataTable();
    $('#' + TableName + ' tbody').on('click', '.' + className, function () {

        if (Count == 0) {
            table
                .row($(this).parents('tr'))
                .remove()
                .draw();


            Count = Count + 1;
        }
        else {
            return;
        }
    });
    fcalculateItemAmt();
}


function BindLocItemCode(val, value) {
    //
    $("#Item" + val).empty();

    $("#Item" + val).append("<option value=''>---Please Select---</option>");
    $.each(ItemMasterLst, function (i) {
        // 
        $('#Item' + val).append($('<option></option>').val(ItemMasterLst[i].value).html(ItemMasterLst[i].text));

    });
    if (parseInt(value) > 0) {
        $('#Item' + val).val(value);
    }
    $('#Item' + val).val();
    // $('#Item' + val).select2();

}

function fcalculateItemAmt() {
    var i = 0;
    var Flag = false;
    var Itemcount = 0;
    var indentAmt = 0;
    var TotaindentAmt = 0;

    $('#tblPoSale tr').each(function () {

        //
        if ($('#Item' + i).val() != undefined && $('#Item' + i).val() != "" && $('#Item' + i).val() != NaN && $('#Item' + i).val() != "0") {
            //


            indentAmt = 0;
            indentAmt = $('#GrossTotal' + i).val();

            TotaindentAmt = parseFloat(parseFloat(TotaindentAmt) + parseFloat(indentAmt)).toFixed(2);


        }
        i = i + 1;
    });
    if (TotaindentAmt == "" || TotaindentAmt == undefined || TotaindentAmt == isNaN || TotaindentAmt == null || TotaindentAmt == NaN || TotaindentAmt == "NaN") {
        TotaindentAmt = 0;
    }
    $("#RequestedAmount").val(TotaindentAmt);
}

var checkbox1 = 0;
function Check() {
    // ;
    if ($("#md_checkbox_34").prop('checked') == true) {
        //let text = "Are you sure ? You have uploaded all documents. Once save you can not update It !";
        //if (confirm(text) == true) {
        checkbox1 = 1;
    }
    else {
        checkbox1 = 0;
        document.getElementById("md_checkbox_34").checked = false;
    }
}



function PreSave() {
    fvalidate()
    if ($("#UploadDocument").val() != "" && $("#UploadDocument").val() != NaN && $("#UploadDocument").val() != null && $("#UploadDocument").val() != undefined) {
        UploadImage();
    }

    else {
        fsavePo(Doc_ImagePath);
    }


}
function UploadImage() {
    //
    var file_data = $("#UploadDocument").prop("files")[0];
    var form_data = new FormData();
    form_data.append("file", file_data);


    $.ajax({
        url: '/ULB/UploadIndentreqDoc',
        cache: false,
        contentType: false,
        processData: false,
        dataType: 'json',
        data: form_data,
        type: 'post',
        async: false,
        success: function (result) {
            if (result != null) {
                Doc_ImagePath = result.uploadDocument;
                fsavePo(result.uploadDocument);

            }
            else {
                alert('Some Thing wrong!');
            }

        },
        error: function () {
            alert('error');
        }
    });
}

function fsavePo(ImgPath) {
     //
    var isvalid = fvalidate();
    if (isvalid == false) {
        return false;
    }

    var FltrClmnArr = { itemId: [], Qty: [], rate: [], NetAmount: [], Reason: [] };

    var i = 0;
    var Flag = false;
    var Itemcount = 0;
    var indentAmt = 0;
    var TotaindentAmt = 0;
    $('#tblPoSale tr').each(function () {

        //
        if ($('#Item' + i).val() != undefined && $('#Item' + i).val() != "" && $('#Item' + i).val() != NaN && $('#Item' + i).val() != "0") {
           //

            if ($('#Item' + i).val() == "" || $('#Item' + i).val() == undefined || $('#Item' + i).val() == NaN) {
                $('#Item' + i).val().focus();
                $('#Item' + i).val().css("border-color", 'red');
                Flag = true;
                return false;
            }
            if ($('#Quantity' + i).val() == "" || $('#Quantity' + i).val() == undefined || $('#Quantity' + i).val() == NaN) {
                $('#Quantity' + i).focus();
                $('#Quantity' + i).css("border-color", 'red');
                Flag = true;
                return false;
            }
            if ($('#GrossRate' + i).val() == "" || $('#GrossRate' + i).val() == undefined || $('#GrossRate' + i).val() == NaN) {
                $('#GrossRate' + i).focus();
                $('#GrossRate' + i).css("border-color", 'red');
                Flag = true;
                return false;
            }
            indentAmt = 0;
            indentAmt = $('#GrossTotal' + i).val();

            TotaindentAmt = parseFloat(parseFloat(TotaindentAmt) + parseFloat(indentAmt)).toFixed(2);
            FltrClmnArr.itemId.push($('#Item' + i).val());
            // var s = $('#GrossRate' + i).val();
           //
            FltrClmnArr.Qty.push($('#Quantity' + i).val());
            FltrClmnArr.rate.push($('#GrossRate' + i).val());
            FltrClmnArr.NetAmount.push($('#GrossTotal' + i).val());
            FltrClmnArr.Reason.push($('#txtReason' + i).val());
            Itemcount = parseInt(Itemcount + 1);

        }
        i = i + 1;
    });
    if (Flag == true) {
        alert("Please check all details & try again");
        return false;
    }
    if (Itemcount == "0") {
        alert("Please add atleast one item & try again");
        return false;

    }
    //alert(checkbox1)
    //;

    //var PK_IndentMstId = $("#PK_IndentMstId").val();
    //if (PK_IndentMstId == null || PK_IndentMstId == undefined || PK_IndentMstId == "" || PK_IndentMstId == NaN || PK_IndentMstId == "0") {
    //    PK_IndentMstId = 0;
    //}

    var masterDTO = {};
    masterDTO.itemList = FltrClmnArr;
    masterDTO.SBMContact = $("#SBMContact").val();
    /* masterDTO.CustomerId = $("#CustomerId").val();*/
    masterDTO.Fk_FundingSourceId = $("#Fk_FundSourceId").val();
    masterDTO.Indentdate = $("#Indentdate").val();
    masterDTO.MobileNo = $("#MobileNo").val();
    masterDTO.IsFinalSubmited1 = checkbox1;

    masterDTO.DocUrl = ImgPath;

    masterDTO.RequestorName = $("#RequestorName").val();
    if ($("#PK_IndentMstId").val() == "" || $("#PK_IndentMstId").val() == undefined || $("#PK_IndentMstId").val() == isNaN || $("#PK_IndentMstId").val() == null) {
        masterDTO.PK_IndentMstId = "";
    }
    else {
        masterDTO.PK_IndentMstId = $("#PK_IndentMstId").val();
    }

     //;
    if (checkbox1 == 1) {
        let text = "Are you sure ? You have uploaded all documents. Once save you can not update It !";
        if (confirm(text) == true) {


            //masterDTO.RequestedAmount = TotaindentAmt;// $("#RequestedAmount").val();
            // masterDTO.FK_ULBId = 0;
            //for document upload

            //

            $.ajax({
                type: "POST",
                url: "/ULB/saveIndentMaster/",
                data: JSON.stringify(masterDTO),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                   // 
                    var msg = "Something went wrong. please reresh the page & try again";
                    if (data == "1") {
                        msg = "Indent saved successfully";
                    }
                    else if (data == "2") {
                        msg = "Indent Updated successfully";
                    }

                    alert(msg);
                    RedirectSame();
                },
                error: function (response, y, z) {

                    alert("Please refresh the page & try again");
                }
            });

        }
        else
        {
            return false;
        }
    }
   

    else {
        $.ajax({
            type: "POST",
            url: "/ULB/saveIndentMaster/",
            data: JSON.stringify(masterDTO),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //
                var msg = "Something went wrong. please reresh the page & try again";
                if (data == "1") {
                    msg = "Indent saved successfully";
                }
                else if (data == "2") {
                    msg = "Indent Updated successfully";
                }

                alert(msg);
                RedirectSame();
            },
            error: function (response, y, z) {

                alert("Please refresh the page & try again");
            }
        });
    }
}

function RedirectSame() {

    window.location.href = "/ULB/Indents";
}

function fgridItemDetails(ind, value) {
    
    var ItemId = 0;
    var Qty = 0;
    if (parseInt(value) == "0") {
        if ($('#Item' + ind).val() == "" || $('#Item' + ind).val() == NaN || $('#Item' + ind).val() == null || $('#Item' + ind).val() == undefined || $('#Item' + ind).val() == 0) {

            $('#Quantity' + ind).val(0);
            $('#GrossRate' + ind).val(0);
            $('#GrossTotal' + ind).val(0);  
            $('#Item' + ind).css("border-color", "red");
            return false;
        }
        if ($('#Quantity' + ind).val() == "" || $('#Quantity' + ind).val() == NaN || $('#Quantity' + ind).val() == null || $('#Quantity' + ind).val() == undefined || $('#Quantity' + ind).val() == 0) {

            $('#Quantity' + ind).val(0);
            $('#GrossRate' + ind).val(0);
            $('#GrossTotal' + ind).val(0);
            $('#Quantity' + ind).css("border-color", "red");
            return false;
        }
        ItemId = $('#Item' + ind).val();
        Qty = $('#Quantity' + ind).val();
    }
    else {
        ItemId = value;
    }
    $.ajax({
        url: '/ULB/ValidateItemRequest', type: 'post', dataType: 'json',
        data: { 'ItemId': ItemId, 'Qty': Qty },
        async: false,
        success: function (data) {
           
            if (data == "1") {
                $('#Quantity' + ind).val('');
                $('#Quantity' + ind).focus();
                alert("You have enough quantity by the population.So You can not request");
            } 
            fcalcFinalAMount(ind);
        }

    });


    //if (parseInt(value) > 0) {
    //    $('#Item' + val).val(value);
    //}

    fcalculateItemAmt();
}

function fcalcFinalAMount(ind) {
    

    //var finalamt = Number($('#Quantity').val()) * Number($('#GrossRate').val());
    //finalamt = finalamt.toFixed(2);
    //$('#FinalAmount').val(finalamt);
    var GrosTotal = Number($('#Quantity' + ind).val()) * Number($('#GrossRate' + ind).val());
    GrosTotal = parseFloat(GrosTotal).toFixed(2);
    $('#GrossTotal' + ind).val(GrosTotal);


    fcalculateItemAmt();

}



function fEditIndentd(IndentId) {
    
   // Reset();

    $.ajax({
        url: '/ULB/IndentReq_Edit',
        type: 'post',
        dataType: 'json',
        data: { 'IndentId': IndentId },
        async: false,
        success: function (data) {
            //$("#Save").css({ 'display': 'none' });
            //$("#Update").css({ 'display': 'inline-block' });

          

          
            $('#RequestorName').val(data[0].requestorName);
            $('#MobileNo').val(data[0].mobileNo);
            $('#Indentdate').val(data[0].indentdate);

            $('#Fk_FundSourceId').val(data[0].fk_FundingSourceId);

            Doc_ImagePath = data[0].docUrl;
          



            /// Bind Item data from data base

            var table = $('#tblPoSale').DataTable({ retrieve: true, "paging": false, bFilter: false, bInfo: false, "ordering": false, fixedHeader: true });
            table.clear().draw();
            RptFilter_Clmn_indx = 0;
            for (var i = 0; i < data.length; i++) {
                var RowsData = $('<tr>' +



                    '<td>   <select id="Item' + i + '" style="width:250px" class="form-control ddlItem select2" onchange="return fgridItemDetails(' + i + ',' + '0' + ')"   ></select></td>' +



                    '<td> <div class="input-group"> <input type="Text"  style="width:100px" class="form-control Quantity Quantity' + i + '" onchange="return fcalcFinalAMount(' + i + ')" onkeypress="return isNumberKey(event)"  id="Quantity' + i + '"  value="' + data[i].quantity + '" /> </td >' +

                    '<td> <div class="input-group"> <input type="Text"  style="width:200px" class="form-control GrossRate GrossRate' + i + '" onchange="return fcalcFinalAMount(' + i + ')" onkeypress="return isNumberKey(event)"  id="GrossRate' + i + '" value="' + data[i].unitCost + '" /> </td >' +



                    '<td> <div class="input-group"> <input type="Text" disabled style="width:200px" class="form-control GrossTotal GrossTotal' + i + '"  id="GrossTotal' + i + '" value="' + data[i].totalCost + '"  /> </td >' +

                    '<td> <div class="input-group"> <input type="Text"  style="width:330px" class="form-control txtReason txtReason' + i + '"  id="txtReason' + i + '" value="' + data[i].reason + '" /> </td >' +


                  

                    '<td>      <button type="button" style="width:40px"  onclick="AddNewFilterRow()" class="btn btn-primary"> <i class="fa fa-plus"></i></button> </td>' +
              
                    '<td><button type="button" style="width:40px" onclick="RemoveFilterRow()"  class="btn btn-danger btndelete"> <i class="fa fa-trash"></i></button></td>' +
                  

                    //'<td>' + parseInt(i + 1) + '</td>' +


                    '</tr>');
                table.row.add(RowsData).draw(false);
                RptFilter_Clmn_indx = (parseInt(RptFilter_Clmn_indx) + 1);
                BindLocItemCode(i, data[i].fk_ProductId);
                fcalcFinalAMount(i);

                if (i == 0) {
                    $(".btndelete").prop("disabled", true);
                }
            }
            // end binding

        }

    });
}