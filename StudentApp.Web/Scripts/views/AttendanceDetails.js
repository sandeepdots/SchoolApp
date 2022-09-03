(function ($) {


    function Attendance() {
        var $this = this;

        function initializeGrid() {
            $(document).off("click", ".btn-attendance").on("click", ".btn-attendance", function () {
                debugger;
                $('.dynamicLinkSlotAddAppointment').remove();

                $('body').append('<a id="linkAppointmentAdd" class="dynamicLinkSlotAddAppointment" href="https://localhost:44325/Student/AddEditAttendance" data-toggle="modal"  data-target="#modal-add-edit-attendance"  data-backdrop="static" >& nbsp;</a >');
                $('#linkAppointmentAdd')[0].click();

            });
            $(document).off("click", ".btnSearch").on("click", ".btnSearch", function () {
                debugger;
                var studentId = 0;
                if ($('#StudentId').val() == undefined || $('#StudentId').val() == null || $('#StudentId').val() == '') {
                    studentId = 0;
                }
                else {
                    studentId = parseInt($('#StudentId').val());

                }
                $.get("https://localhost:44325/Student/GetTableData?StartDate=" + $('#FromDate').val() + '&EndDate=' + $('#ToDate').val() + '&StudentId=' + studentId, function (result) {
                    debugger;
                    $('#divAttendanceData').html(result);

                })

            });

        }
        function initGridControlsWithEvents() {
            if ($('.switchBox').data('bootstrapSwitch')) {
                $('.switchBox').off('switchChange.bootstrapSwitch');
                $('.switchBox').bootstrapSwitch('destroy');
            }

            $('.switchBox').bootstrapSwitch()
                .on('switchChange.bootstrapSwitch', function () {
                    var switchElement = this;

                    $.post(domain + 'Admin/Brand/UpdateStatus', { brandId: this.value },
                        function (result) {
                            alertify.dismissAll();
                            alertify.success(result.message)
                        })
                });
        }

        function initializeModalWithForm() {
            $("#modal-add-edit-attendance").on('loaded.bs.modal', function (e) {
                $attendanceValue = [];
                formAddEditPresenter = new Global.FormHelper($("#form-Add-Edit-Attendance"),
                    { updateTargetId: "validation-summary" }, function onSuccess(result) {
                        window.location.reload();
                    });
                $('.form-checkbox').bootstrapSwitch(
                );
                $('.datefield').datepicker({
                    dateFormat: 'yy-mm-dd',
                    autoclose: true,
                    minDate: new Date()
                }).on('change', function (e) {

                });
                $(document).off("click", "#submitAttendance").on("click", '#submitAttendance', function () {
                    // New array
                    $attendanceValue = [];
                    $(".form-checkbox").each(function (e) {
                        var id = $(this).attr('id');
                        var studentId = $(this).attr('name');
                        if ($(this).is(':checked')) {
                            var isExist = false;
                            $.each($attendanceValue, function (index, value) {
                                if (value.Id == studentId) {
                                    isExist = true;
                                    if (id.includes('First')) {
                                        value.SessionFirstAttendance = true;
                                    }
                                    else if (id.includes('Second')) {
                                        value.SessionSecondAttendance = true;
                                    }
                                }
                            })
                            if (!isExist) {
                                var attendanceData = {
                                    StudentId: studentId,
                                    SessionFirstAttendance: (id).includes('First') ? true : false,
                                    SessionSecondAttendance: (id).includes('Second') ? true : false,
                                }
                                $attendanceValue.push(attendanceData);
                            }

                        }

                    });
                    $('#HdnAttendanceData').val(JSON.stringify($attendanceValue));

                    $('#formAddEditAttendance').submit();
                });


            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });

        }
        $this.init = function () {
            initializeGrid();
            initializeModalWithForm();
        };
    }
    $(function () {
        var self = new Attendance();
        self.init();
    });

}(jQuery));