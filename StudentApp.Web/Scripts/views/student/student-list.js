(function ($) {


    function StudentList() {
        var $this = this;

        function initializeGrid() {

            var gridPresenter = new Global.GridHelper('#grid-student', {
                "columnDefs": [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    },
                    {
                        "targets": [1],
                        "visible": true,
                        "sortable": false,
                        "searchable": false
                    },
                    {
                        "targets": [2],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [2],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [3],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [4],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [5],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [6],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [7],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [8],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [9],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": 10,
                        "searchable": false,
                        "sortable": false,
                        "data": "0",
                        "render": function (data, type, row, meta) {
                            var actionLink = $("<a/>", {
                                href: 'http://localhost:44315/' + "Office/AddEditTeamLeader/" + row[0],
                                id: "editPresenterModal",
                                class: "btn btn-primary btn-sm",
                                'data-toggle': "modal",
                                'data-target': "#modal-add-edit-presenter",
                                html: $("<i/>", {
                                    class: "fa fa-pencil"
                                }),
                            }).append(" Edit").get(0).outerHTML + "&nbsp;"


                            actionLink += $("<a/>", {
                                href: 'http://localhost:51483/' + "Office/DeleteTeamLeader/" + row[0],
                                id: "deletePresenter",
                                class: "btn btn-danger btn-sm",
                                'data-toggle': "modal",
                                'data-target': "#modal-delete-presenter",
                                html: $("<i/>", {
                                    class: "fa fa-trash-o"
                                }),
                            }).append(" Delete").get(0).outerHTML + "&nbsp;"

                            actionLink += $("<a/>", {
                                href: 'http://localhost:51483/' + "Office/DetailsTeamLead/" + row[0],
                                id: "detailsPresenter",
                                class: "btn btn-info btn-sm",
                                'data-toggle': "modal",
                                'data-target': "#modal-details-presenter",
                                html: $("<i/>", {
                                    class: "fa fa-align-justify"
                                }),
                            }).append(" Details").get(0).outerHTML + "&nbsp;"


                            return actionLink;
                        }
                    }
                ],
                "direction": "rtl",
                "bPaginate": true,
                "sPaginationType": "simple_numbers",
                "bProcessing": true,
                "bServerSide": true,
                "bAutoWidth": false,
                "stateSave": true,
                "sAjaxSource": 'http://localhost:44315/' +"Students/GetStudentData",
                "fnServerData": function (url, data, callback) {
                    debugger;
                    $.ajax({
                        "url": url,
                        "data": data,
                        "success": callback,
                        "contentType": "application/x-www-form-urlencoded; charset=utf-8",
                        "dataType": "json",
                        "type": "POST",
                        "cache": false,
                        "error": function () {

                        }
                    });


                },


                "fnDrawCallback": function (oSettings) {
                    initGridControlsWithEvents();


                    if (oSettings._iDisplayLength > oSettings.fnRecordsDisplay()) {
                        $(oSettings.nTableWrapper).find('.dataTables_paginate').hide();
                    }
                    else {
                        $(oSettings.nTableWrapper).find('.dataTables_paginate').show();
                    }
                },
                "stateSave": true,
                "stateSaveCallback": function (settings, data) {
                    localStorage.setItem('DataTables_' + settings.sInstance, JSON.stringify(data))
                },
                "stateLoadCallback": function (settings) {
                    return JSON.parse(localStorage.getItem('DataTables_' + settings.sInstance))
                }
            });
            table = gridPresenter.DataTable();



          

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
        var self = new StudentList();
        self.init();
    });

}(jQuery));