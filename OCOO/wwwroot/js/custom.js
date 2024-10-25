function fNotification() {
    $.ajax({
        url: '/Home/GetNotification',
        type: 'GET',
        success: function (response) {
            
            var dynamicData = response;
            var dynamicList = $('#dynamicList');
            var notificationsTitle = $('<li>' +
                '<div class="drop-title">Notification</div>' +
                '</li>');
            dynamicList.append(notificationsTitle);
            dynamicData.forEach(function (dataItem) {
                if (dynamicData.length > 0) {
                    var listItem = $('<li>' +
                        '<div class="message-center">' +
                        '<a href="#">' +
                        '<div class="mnotification">' +
                        '<small>' + dataItem.firm + '</small>' +
                        '</div>' +
                        '<div class="mnotification">' +
                        '<small>' + dataItem.zone + '</small>' +
                        '</div>' +
                        '<div class="mnotification">' +
                        '<small class="fwc-9">' + dataItem.stp + '</small>' +
                        '</div>' +
                        '<div class="mnotification" ' + (dataItem.fk_userTypeId != "2" ? '' : 'style="display: none;"') + '>' +
                        '<small class="fwc-9"><span class="text-success">Design-</span>' + dataItem.designCapacity + ' MLD</small>' +
                        '</div>' +
                        '<div class="mnotification" ' + (dataItem.fk_userTypeId != "2" ? '' : 'style="display: none;"') + '>' +
                        '<small class="fwc-9">Discharge-' + dataItem.capacity + ' MLD</small>' +
                        '</div>' +
                        '<div class="mnotification">' +
                        '<small class="mail-desc fwc-9">' + dataItem.message + '</small>' +
                        '</div>' +
                        '<div class="mnotification">' +
                        '<small class="time fwc-9">' + dataItem.time + '</small>' +
                        '</div>' +
                        '</a>' +
                        '</div>' +
                        '</li>'
                    );
                    dynamicList.append(listItem);
                    $('.badge-1').text(dataItem.row_count);
                }
            });

            if (response.length == 0) {
                var listItem1 = $('<li><div class="text-center text-danger mt-4">Empty</div></li>')
                $('.mailbox').css('height', '150px');
                $('.mailbox').css('overflow', 'hidden');
                dynamicList.append(listItem1);
            }
            if (response.length == 1) {
                if (dynamicData[0].fk_userTypeId == "2") {
                    $('.mailbox').css('height', '200px');
                }
                else {
                    $('.mailbox').css('height', '240px');
                }
                $('.mailbox').css('overflow', 'hidden');
            }
            if (response.length == 2) {
                if (dynamicData[0].fk_userTypeId == "2") {
                    $('.mailbox').css('height', '350px');
                }
                else {
                    $('.mailbox').css('height', '365px');
                }
                $('.mailbox').css('overflow', 'hidden');
            } if (response.length >= 1) {
                  if (dynamicData[0].fk_userTypeId == "2") {

                    var checkAllLink = $('<li>' +
                        '<a class="nav-link text-center" href="/FirmReport/PendingBillReport">' +
                        '<strong>Check all notifications</strong> <i class="fa fa-angle-right"></i>' +
                        '</a>' +
                        '</li>');
                }
                else {
                    var checkAllLink = $('<li>' +
                        '<a class="nav-link text-center" href="/AdminMasters/Notification">' +
                        '<strong>Check all notifications</strong> <i class="fa fa-angle-right"></i>' +
                        '</a>' +
                        '</li>');
                }
                dynamicList.append(checkAllLink);
            }
        },
        error: function (error) {
            fShowToasterror(error);
        }
    });
}