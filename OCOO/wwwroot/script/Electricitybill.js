function checkdate() {
    debugger;
    const dateToCheck11 = $("#MonthStartDate").val();
    const dateToCheck12 = $("#MonthEndDate").val();
    // Split the date string into day, month, and year components
    const [day, month, year] = dateToCheck11.split('/');
    const [day1, month1, year1] = dateToCheck12.split('/');

    // Create a Date object (Note: Months are zero-based in JavaScript Date)
    const dateToCheck = new Date(`${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`);
    const dateToCheck1 = new Date(`${year1}-${month1.padStart(2, '0')}-${day1.padStart(2, '0')}`);


    // Get the current date
    const currentDate = new Date();
    // Compare the dates
    if (dateToCheck > currentDate) {

        fShowToastalert('Please select Past date!');
        $("#MonthStartDate").focus();
        return false;
    }
    if (dateToCheck1 > currentDate) {
        fShowToastalert('Please select Past date!');
        $("#MonthEndDate").focus();
        return false;
    }
    
    if (dateToCheck >= dateToCheck1) {
        fShowToastalert('Month end date must be greater than month start date.');
        $("#MonthEndDate").focus();
        return false;
    }

}

$(document).ready(function () {
    
    if ($("#update").val() == null || $("#update").val() == 'Save'|| $("#update").val() == '') {
        FBinddate()
        $('#StartMeterReading').prop('readonly', false);
        $('.stpreadonly').css('pointer-events', 'fill')
        $('.SPSreadonly').css('pointer-events', 'fill')
        $('#MonthStartDate').css('pointer-events', 'fill');
    }
    else {
        
        $('#StartMeterReading').prop('readonly',true);
        $('.stpreadonly').css('pointer-events', 'none').css('background-color', '#e9ecef');
        $('.SPSreadonly').css('pointer-events', 'none').css('background-color', '#e9ecef');
        $('#MonthStartDate').css('pointer-events', 'none');
    }
})

function fbindIPS(val) {
    
    var PK_ID = $("#Pk_STPId").val()

    if (PK_ID != 0) {
        $.ajax({
            url: '/STP/PumpingStationDropDown?Data=' + PK_ID,
            method: 'GET',
            success: function (data) {
                var dropdown = $('#PK_IPS');
                dropdown.empty();
                $.each(data, function (index, item) {
                    dropdown.append($('<option>', {
                        value: item.value,
                        text: item.text
                    }));
                });
                if (parseInt(val) > 0) {
                    $('#PK_IPS').val(val);
                } else {
                    FBinddate();
                }
            },
            error: function (error) {
                fShowToasterror(error);
            }
        });
    }
}

$("#PK_IPS").on('change', function () {
    debugger;
    FBinddate()
})
function FBinddate() {
    debugger
    var PK_STPID = $("#Pk_STPId").val()
    var PK_IPS = $("#PK_IPS").val()
    $.ajax({
        url: '/DeductionRelease/GetSTPMonthDate',
        type: 'Post',
        data: { Pk_STPId: PK_STPID, PK_IPS: PK_IPS },
        success: function (response) {
            
            if (response != null) {
                if (response.status == "1") {
                    if (response.monthEndDate == null) {
                        var day = response.months
                        const date = new Date();
                        const month = (date.getMonth() + 1).toString().padStart(2, '0');
                        const year = date.getFullYear();
                        var date1 = `${day}/${month}/${year}`;
                        $("#MonthStartDate").val(date1);
                        debugger
                        addOneMonth()

                    }
                    if (response.endMeterReading != null) {
                        $('#MonthStartDate').val(response.monthEndDate)
                        $('#StartMeterReading').val(response.endMeterReading)
                        $('#StartMeterReading').prop('readonly', true);
                      //  $('#MonthStartDate').css('pointer-events', 'none');
                        debugger
                        addOneMonth()

                    }
                    else {
                        $('#StartMeterReading').val('')
                        $('#StartMeterReading').prop('readonly', false);
                        $('#MonthStartDate').css('pointer-events', 'fill');
                    }
                }
            }
        },
        error: function (error) {
            fShowToasterror(error);
        }
    });
}
function addOneMonth() {
    debugger
    const dateInput = $('#MonthStartDate').val();
    var PK_IPS = $("#PK_IPS").val()
    var PK_STPID = $("#Pk_STPId").val()
    if (PK_STPID == null || PK_STPID == "0" || PK_STPID == "") {
        fShowToastalert('Please Select STP !')
        $("#MonthStartDate").val('')
        $("#Pk_STPId").focus();
        return false;
    } else {
        if ($("#update").val() == null || $("#update").val() == '') {
            debugger
            $.ajax({
                url: '/DeductionRelease/CheckValiddate',
                type: 'Post',
                data: { Pk_STPId: PK_STPID, PK_IPS: PK_IPS, MonthStartDate: dateInput },
                success: function (response) {

                    if (response != null) {
                        if (response.status == "0") {
                            fShowToastalert(response.message)
                            $('#MonthStartDate').val('');
                            return false;
                        }
                    }
                },
                error: function (error) {
                    fShowToasterror(error);
                }
            });
        }
        if ($("#update").val() == "Save" && 1==2) {
            debugger
            const dateParts = dateInput.split('/');

            // Check if the input format is valid (dd/mm/yyyy)
            if (dateParts.length === 3) {
                const day = parseInt(dateParts[0], 10);
                const month = parseInt(dateParts[1], 10) - 1; // Adjust month to 0-based index
                const year = parseInt(dateParts[2], 10);

                // Create a new Date object using the specified date
                const selectedDate = new Date(year, month, day);

                if (!isNaN(selectedDate.getTime())) {
                    // Add one month
                    selectedDate.setMonth(selectedDate.getMonth() + 1);

                    // Subtract one day
                    selectedDate.setDate(selectedDate.getDate() - 1);

                    // Get the new month, year, and day after adding one month and subtracting one day
                    const newMonth = selectedDate.getMonth() + 1;
                    const newYear = selectedDate.getFullYear();
                    const newDay = selectedDate.getDate();

                    // Format the new date as 'dd/mm/yyyy'
                    const formattedDay = newDay.toString().padStart(2, '0');
                    const formattedMonth = newMonth.toString().padStart(2, '0');
                    const formattedDate = `${formattedDay}/${formattedMonth}/${newYear}`;

                    // Update the input value with the new date using jQuery
                    $('#MonthEndDate').val(formattedDate);
                } else {
                    fShowToastalert('Please enter a valid date.');
                }
            } else {
                fShowToastalert('Please enter the date in dd/mm/yyyy format.');
            }
        }
    }
}
function fTotal() {

    let Start = $("#StartMeterReading").val();
    let End = $("#EndMeterReading").val();
    if ($("#EndMeterReading").val() == NaN || $("#EndMeterReading").val() == undefined || $("#EndMeterReading").val() == "") {
        End = 0;
    }
    if ($("#StartMeterReading").val() == NaN || $("#StartMeterReading").val() == undefined || $("#StartMeterReading").val() == "") {
        Start = 0;
    }
    var Total = (parseFloat(End) - parseFloat(Start));
    $("#ElectricityUnit").val(Total);
    if (parseFloat(End) < parseFloat(Start)) {
        $("#EndMeterReading").val('')
        $("#Showmsgmeter").html('Please Fill Valid Meter Reading !')
        return false;
    } else {
        $("#Showmsgmeter").html('')

    }
}

function ValidateForm() {
    debugger;
   
    if ($("#Pk_STPId").val() == '' || $("#Pk_STPId").val() == null || $("#Pk_STPId").val() == 0) {
        $("#Pk_STPId").focus();
        return false;
    }

    if (!$("#MonthStartDate").val()) {
        $("#MonthStartDate").focus();
        return false;
    }
    //if (!$("#MonthEndDate").val()) {
    //    $("#MonthEndDate").focus();
    //    return false;
    //}
    //    var date = checkdate();
    //    if (date == false) {
    //        return false;
    //    }
    
    if (!$("#StartMeterReading").val()) {
        $("#StartMeterReading").focus();
        return false;
    }
    if (!$("#EndMeterReading").val()) {
        $("#EndMeterReading").focus();
        return false;
    }
    if ($("#ElectricityUnit").val() < 0) {
        $("#ElectricityUnit").focus();
        fShowToastalert('Please Fill Valid Meter Reading!')
        return false;
    }
    if (!$("#UnitRate").val()) {
        $("#UnitRate").focus();
        return false;
    }
    if (!$("#FixCharges").val()) {
        $("#FixCharges").focus();
        return false;
    }
    if (!$("#OtherCharges").val()) {
        $("#OtherCharges").focus();
        return false;
    }
    if (!$("#Penalty").val()) {
        $("#Penalty").focus();
        return false;
    }
    if ($("#txtRemark").val() == '' || $("#txtRemark").val() == null) {
        $("#txtRemark").focus();
        return false;
    }
}