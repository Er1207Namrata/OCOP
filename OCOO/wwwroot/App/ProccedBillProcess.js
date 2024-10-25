(function () {
    angular.module('LayoutApp').controller('ProccedBillProcessController', function ($scope, $http) {
        $scope.MarkEmployeeList = [];
        $scope.ToForwardEmployeeList = [];
        $scope.EmployeeZoneList = [];
        $scope.CheckedMarkEmployees = [];
        $scope.CheckedForwardEmployees = [];
        $scope.ToDeclineEmployeeList = [];
        $scope.ReforwardEmployeeList = [];
        $scope.FormSubmitted = false;
        $scope.ForwardFormSubmitted = false;
        $scope.Remarks = "Please proceed to bill.";
        $scope.DeclineRemarks = "Please proceed to bill.";
        var config = {
            headers: {
                'Content-Type': 'application/json'
            }
        };

        $scope.GetReforwardEmployees = function () {
            $scope.BillId = $("#BillId").val();
            $http.get("/Master/GetReforwardEmployeeDropdown/" + $scope.BillId, null).then(function (response) {
                $scope.ReforwardEmployeeList = response.data;
                console.log($scope.ReforwardEmployeeList);
            }).catch(function (error) {
            });
        }
        $scope.GetReforwardEmployees();

        $scope.GetEmployeeDropdown = function () {
            $scope.BillId = $("#BillId").val();
            $http.get("/Master/GetMarkedAndForwardedEmployeeDropdown/" + $scope.BillId, null).then(function (response) {   
                $scope.MarkEmployeeList = response.data.markedEmployees;
                $scope.ToForwardEmployeeList = response.data.forwardedEmployees;
                $scope.ToDeclineEmployeeList = response.data.billForwardedEmployees;
            }).catch(function (error) {
            });
        }
        $scope.GetEmployeeDropdown();

        $scope.BillDetails = [];
        $scope.GetBillsDetails = function () {
            $scope.BillId = $("#BillId").val();
            $http.get("/Bill/GetBillsDetails?id=" + $scope.BillId, null).then(function (response) {
                $scope.BillDetails = response.data;
                console.log(response.data);
                angular.forEach(response.data.billDocuments, function (_, index) {
                    if (_.status == 'Pending')
                        _.remarks = '';
                });
            }).catch(function (error) {
            });
        }
        $scope.GetBillsDetails();
        $scope.MarkedEmployees = {};
        $scope.ForwardedEmployees = {};
        $scope.CheckedMarkEmployees = [];
        $scope.CheckedForwardEmployees = [];
        $scope.IsUploadCoveringLetterAttached = true;
        $scope.IsForwardUploadCoveringLetterAttached = true;
        $scope.CheckFile = function () {
            $scope.IsUploadCoveringLetterAttached = $('#UploadCoveringLetter')[0].files.length > 0 ? true : false;
        }

        $scope.ChangeForwardFormStatus = function (status) {
            $(".Forward-employee-card input[type=checkbox]").each(function () {
                if (status == 'Paid') {
                    $(this).prop("checked", false);
                    $(this).prop("disabled", true);
                }
                else {
                    $(this).prop("disabled", false);
                }
            });

        }
        $scope.Save = function () {
            $scope.FormSubmitted = true;
            $scope.IsUploadCoveringLetterAttached = $('#UploadCoveringLetter')[0].files.length > 0 ? true : false;
            $scope.CheckedEmployees = [];
            $(".mark-employee-card input[type=checkbox]").each(function (i, v) {
                if ($(this).is(":checked"))
                    $scope.CheckedEmployees.push(parseInt($(this).attr("employee-id")));
            });
            if ($scope.MarkedForm.$valid && $scope.IsUploadCoveringLetterAttached && $scope.CheckedEmployees.length > 0) {
                var formData = new FormData();
                formData.append('EmployeesId', $scope.CheckedEmployees.join(','));
                formData.append('Remarks', $scope.Remarks);
                formData.append('CoveringLetter', $('#UploadCoveringLetter')[0].files[0]);
                formData.append('LDCoveringLetter', $('#LDUploadLetter')[0].files.length > 0 ? $('#LDUploadLetter')[0].files[0] : null);
                formData.append('BillId', $("#BillId").val());
                formData.append('ProcessType', '1');
                $http.post('/Bill/MarkedSave', formData, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                }).then(function (response) {
                    if (response.data.status) {
                        window.location.href = "/ApproveBilling/BillApprovalList";
                    }
                    alert(response.data.message);
                }).catch(function (error) {
                    // Handle error, if needed
                    console.error('Error sending form data:', error);
                });
            }
            else {
                $scope.MarkedForm.$submitted = true;
            }
        };
        $scope.Status = "Approved";
        $scope.SaveForwardForm = function () {
            debugger
            $scope.ForwardFormSubmitted = true;

            $scope.IsForwardUploadCoveringLetterAttached = $('#ForwardUploadCoveringLetter')[0].files.length > 0 ? true : false;
            $scope.CheckedForwardEmployeeList = [];
            $(".Forward-employee-card input[type=checkbox]").each(function (i, v) {
                if ($(this).is(":checked"))
                    $scope.CheckedForwardEmployeeList.push(parseInt($(this).attr("employee-id")));
            });


            $(".ReForward-employee-card input[type=checkbox]").each(function (i, v) {
                if ($(this).is(":checked"))
                    $scope.CheckedForwardEmployeeList.push(parseInt($(this).attr("employee-id")));
            });


            if ($scope.ForwardForm.$valid) {
                debugger
                var valid = $("#IsVerified").prop("checked");
                //if (valid == undefined) {
                //    $("#firmVerify").show();
                //    return false;
                //}
                //else if (valid == false) {
                //    $("#firmVerifydec").show();
                //    return false;
                //}
                
                var formData = new FormData();
                formData.append('EmployeesId', $scope.CheckedForwardEmployeeList.join(','));
                formData.append('Status', $("#DesignationId").val() == 1 && $("#IsPrimary").val()==1 && !$("#IsVerified").prop("checked") ? 'Pending' : ($scope.Status == "Approved" ? "Verified" : $scope.Status));
                formData.append('Remarks', $scope.Remarks);
                formData.append('CoveringLetter', $('#ForwardUploadCoveringLetter')[0].files[0]);
                formData.append('LDCoveringLetter', $('#ForwardLDUploadLetter')[0].files.length > 0 ? $('#ForwardLDUploadLetter')[0].files[0] : null);
                formData.append('BillId', $("#BillId").val());
                formData.append('ProcessType', '2');
                formData.append('IsVerified', $("#IsVerified").prop("checked"));
                $http.post('/Bill/MarkedSave', formData, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                }).then(function (response) {
                    if (response.data.status) {
                        window.location.href = "/ApproveBilling/BillApprovalList";
                    }
                    alert(response.data.message);
                }).catch(function (error) {
                    // Handle error, if needed
                    console.error('Error sending form data:', error);
                });

            }
            else {
                $scope.ForwardForm.$submitted = true;
            }
        };

        $scope.DeclineFormSave = function () {
            $scope.DeclineFormSubmitted = true;
            $scope.IsdeclineUploadCoveringLetterAttached = $('#declineUploadCoveringLetter')[0].files.length > 0 ? true : false;
            $scope.CheckedEmployees = [];
            $(".decline-employee-card input[type=checkbox]").each(function (i, v) {
                if ($(this).is(":checked"))
                    $scope.CheckedEmployees.push(parseInt($(this).attr("employee-id")));
            });

            if ($scope.DeclineForm.$valid && $scope.IsdeclineUploadCoveringLetterAttached && $scope.CheckedEmployees.length > 0) {
                var formData = new FormData();
                formData.append('EmployeesId', $scope.CheckedEmployees.join(','));
                formData.append('Remarks', $scope.DeclineRemarks);
                formData.append('CoveringLetter', $('#declineUploadCoveringLetter')[0].files[0]);
                formData.append('BillId', $("#BillId").val());
                formData.append('ProcessType', '3');
                formData.append('Status', 'Decline')

                $http.post('/Bill/MarkedSave', formData, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                }).then(function (response) {
                    if (response.data.status) {
                        window.location.href = "/ApproveBilling/BillApprovalList";
                    }
                    alert(response.data.message);
                }).catch(function (error) {
                    // Handle error, if needed
                    console.error('Error sending form data:', error);
                });
            }
            else {
                $scope.MarkedForm.$submitted = true;
            }
        };


        $scope.CheckedEmployees = [];
        $scope.MarkedcheckboxChanged = function (key) {
            if (key == "M") {
                var MarkEmployeeAllcheckedCount = 0;
                $(".mark-employee-card input[type=checkbox]").each(function () {
                    if ($(this).is(":checked"))
                        $scope.CheckedEmployees.push(parseInt($(this).attr("employee-id")));
                });
            }
        }
        $scope.CheckedForwardEmployeeList = [];
        $scope.ForwardcheckboxChanged = function (key) {
            if (key == "M") {
                var MarkEmployeeAllcheckedCount = 0;
                $(".Forward-employee-card input[type=checkbox]").each(function () {
                    if ($(this).is(":checked"))
                        $scope.CheckedForwardEmployeeList.push(parseInt($(this).attr("employee-id")));
                });
            }
        }
        $scope.resetForm = function () {
            $scope.Form.$setPristine();
            $scope.Form.$setUntouched();
            $scope.FormSubmitted = false;
            $scope.employee = [];
            $scope.UploadCoveringLetter = null;
            $scope.LDUploadLetter = null;
        };


        //--------------Document approval proccess--------------------------------------

        $scope.ProcceedFileVerification = function (status, id, remark, index) {
            var rowForm = $scope.BillDocumentVerificationForm['rowForm_' + index];
            if (id > 0 && remark != undefined && remark != '' && remark != null) {
                if (window.confirm('Are you sure you want to ' + (status == "Approved" ? 'approve' : 'decline') + '?')) {
                    var formData = new FormData();
                    formData.append('Id', id);
                    formData.append('Remark', remark);
                    formData.append('Status', status)
                    $http.post('/Bill/BillDocumentVerification', formData, {
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined }
                    }).then(function (response) {
                        if (response.data.status) {
                            $scope.GetBillsDetails();
                            // window.location.href = "/ApproveBilling/BillApprovalList";
                        }
                        fShowToast(response.data.message);
                    }).catch(function (error) {
                        console.error('Error sending form data:', error);
                    });
                }
            } else {
                fShowToastalert('Please enter remark.');
            }
        };


        $scope.BillApprovalAttachmentList = [];

        $scope.AddBillApprovalAttachment = function () {
            var fileInput = document.getElementById("DocumentAttachment");
            var documentTypeElement = document.getElementById("DocumentType");
            var remarksElement = document.getElementById("Remarks");
            if (!fileInput.files.length || !documentTypeElement.value.trim()) {
                alert("Document type and attachment is required fields.");
                return;
            }


            var file = fileInput.files[0];
            var documentTypeText = documentTypeElement.options[documentTypeElement.selectedIndex].textContent.trim();

            var allowedFileTypes = ['image/jpeg', 'image/png', 'application/pdf'];
            if (!allowedFileTypes.includes(file.type)) {
                alert("Please upload only JPG, JPEG, PNG, or PDF files.");
                return;
            }

            var maxFileSize = 1024 * 1024;
            if (file.size > maxFileSize) {
                alert("File size exceeds the limit. Please upload a file smaller than 1MB.");
                return;
            }

            var file = fileInput.files[0];
            var documentTypeText = documentTypeElement.options[documentTypeElement.selectedIndex].textContent.trim();
            var isDuplicate = $scope.BillApprovalAttachmentList.some(function (attachment) {
                return attachment.DocumentType === documentTypeText && attachment.DocumentAttachment.name === file.name && attachment.Remarks === remarksElement.value;
            });

            if (!isDuplicate) {
                $scope.BillApprovalAttachmentList.push({
                    DocumentTypeId: $("#DocumentType").val(),
                    DocumentType: documentTypeText,
                    DocumentAttachment: file,
                    Remarks: remarksElement.value,
                });
                $scope.BillApprovalAttachmentList.forEach(function (attachment) {
                    if (attachment.DocumentAttachment.type === 'application/pdf') {
                        attachment.previewUrl = attachment.DocumentAttachment.name;
                    } else {
                        var reader = new FileReader();
                        reader.onload = function (event) {
                            $scope.$apply(function () {
                                attachment.previewUrl = event.target.result;
                            });
                        };
                        reader.readAsDataURL(attachment.DocumentAttachment);
                    }
                });
                $('#BillApprovalAttachmentForm')[0].reset();
            } else {
                alert("This attachment already exists.");
            }
        };

        $scope.RemoveAttachment = function (index) {
            var confirmDelete = confirm("Are you sure you want to delete this attachment?");
            if (confirmDelete) {
                $scope.BillApprovalAttachmentList.splice(index, 1);
            }
        };

        $scope.SubmitAttachments = function () {
            var formData = new FormData();
            $scope.BillApprovalAttachmentList.forEach(function (attachment) {
                formData.append('AttachmentTypeId', attachment.DocumentTypeId);
            });
            $scope.BillApprovalAttachmentList.forEach(function (attachment) {
                formData.append('attachments', attachment.DocumentAttachment);
            });
            $scope.BillApprovalAttachmentList.forEach(function (attachment) {
                formData.append('Remarks', attachment.Remarks);
            });

            $http({
                method: 'POST',
                url: '/Bill/AddAttachments/' + $("#BillId").val(),
                headers: { 'Content-Type': undefined },
                data: formData,
                transformRequest: angular.identity
            }).then(function (response) {
                console.log(response.data.message);
                alert(response.data.message);
                if (response.data.status) {
                    window.location = "/Bill/ProcessBill?id=" + $("#Bill_id").val();
                }

            }, function (error) {
                console.log(error);
                alert(error.data.message);
            });
        };
        $scope.getImageUrl = function (file) {
            return URL.createObjectURL(file);
        };
        $scope.openPDF = function (filePath) {
            window.open(filePath, '_blank');
        };
        $scope.openImage = function (filePath) {
            $(".file-download-button").attr("href", filePath)
            $(".file-download-button").attr("download", filePath)
            $("#ImageViewTag").attr("src", filePath)
            $('#openImageModal').modal('show');
        };
        $scope.AttachedList = [];
        $scope.GetAttachedFiles = function () {
            $http({
                method: 'GET',
                url: '/Bill/FetchBillApprovalDocument?xyz=' + $("#Bill_id").val(),
            }).then(function (response) {
                console.log(response.data);
                $scope.AttachedList = response.data;
            }, function (error) {
                console.log(error);
                alert(error.data.message);
            });
        }
        $scope.DeleteAttachedFiles = function (id) {
            var confirmDelete = confirm("Are you sure you want to delete this attachment?");
            if (confirmDelete) {
                $http({
                    method: 'GET',
                    url: '/Bill/DeleteApprovalDocument/' + id,
                }).then(function (response) {
                    console.log(response);
                    if (response.data.status) {
                        $scope.GetAttachedFiles();
                    }
                    alert(response.data.message);

                }, function (error) {
                    console.log(error);
                });
            }
        }
        $scope.isPDF = function (url) {
            return url.toLowerCase().endsWith('.pdf');
        };
        $scope.AssignToFirmRemark = "Please proceed to bill.";
        $scope.AssignToFirm = function () {

            if ($("#AssignToFirmDeclineFormIsdeclaration").prop("checked")) {
                var formData = new FormData();
                formData.append('billId', $("#BillId").val());
                formData.append('Remark', $scope.AssignToFirmRemark);
                $http.post('/Bill/BillAssignToFirm', formData, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                }).then(function (response) {
                    if (response.data.status) {
                        fShowToast(response.data.message);
                        location.reload();
                        $("#AssignToFirmModal").modal("hide");
                    }
                    else {
                        alert(response.data.message);
                    }

                }).catch(function (error) {
                    console.error('Error sending form data:', error);
                });
            } else {
                fShowToast("Declaration is required.");

            }
        }


    });
})();
