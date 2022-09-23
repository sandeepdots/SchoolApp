(function ($) {

    function StudentList() {
        var $this = this;

        function initializeGrid() {

            var gridPresenter = new Global.GridHelper('#grid-student-management', {
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
                        "sortable": true,
                        "searchable": false,
                        "data": "9",
                        "render": function (data, type, row, meta) {

                            
                            var json = {
                                type: "checkbox",
                                class: "switchBox switch-medium",
                                value: row[0],
                                'data-on': "success",
                                'data-off': "danger"
                            };

                            if (data === "Active") {
                                json.checked = true;
                            }
                            return $('<input/>', json).get(0).outerHTML;
                        }
                    },

                    {
                        "targets": 10,
                        "searchable": false,
                        "sortable": false,
                        "render": function (data, type, row, meta) {
                            var actionLink = $("<a/>", {
                                href: domain + "Students/AddEditStudent/" + row[0],
                                id: "editPresenterModal",
                                class: "btn btn-primary btn-sm",
                                'data-toggle': "modal",
                                'data-target': "#modal-add-edit-student",
                                html: $("<i/>", {
                                    class: "fa fa-pencil"
                                }),
                            }).append(" Edit").get(0).outerHTML + "&nbsp;"


                            actionLink += $("<a/>", {
                                href: domain + "Students/DeleteStudent/" + row[0],
                                id: "deleteStudent",
                                class: "btn btn-danger btn-sm",
                                'data-toggle': "modal",
                                'data-target': "#modal-delete-student",
                                html: $("<i/>", {
                                    class: "fa fa-trash-o"
                                }),
                            }).append(" Delete").get(0).outerHTML + "&nbsp;"

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
                "stateSave": false,
                "sAjaxSource": domain + "Students/GetStudentData",
                "fnServerData": function (url, data, callback) {
                   
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
                .on('switch-change', function () {
                    var switchElement = this;
                    $.get(domain + 'Students/ActivePresenter', { id: this.value }, function (result) {
                        if (!result.isSuccess) {
                            $(switchElement).bootstrapSwitch('toggleState', true);
                        }
                    }).fail(function () {

                    })
                });
        }

        function initializeModalWithForm() {

            //Add & Edit Student---------->

            $("#modal-add-edit-student").on('loaded.bs.modal', function (e) {
               
                $attendanceValue = [];
                $('.form-checkbox').bootstrapSwitch();
                formAddEditStudent = new Global.FormHelper($("#form-add-edit-student"), { updateTargetId: "validation-summary" }, function (data) {

                    //console.log(data.isSuccess);
                    if (data.isSuccess == true) {
                        $("#validation-summary").html("");
                        $("#validation-summary").hide();
                        //  window.location.href = data.redirectUrl;
                        window.location.reload();
                    }
                    else {
                        // $("#validation-summary").show();
                        $("#validation-summary").text(data.data).show().delay(5000).fadeOut(2000);
                    }
                });
                
                $('.datefield').datepicker({
                    dateFormat: 'yy-mm-dd',
                    autoclose: true,
                    minDate: new Date()
                }).on('change', function (e) {

                });








            }).on('hidden.bs.modal', function (e) {
                $("#modal-add-edit-student").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });

            //Delete Student------->

            $("#modal-delete-student").on('loaded.bs.modal', function (e) {

                formDeleteJobTitle = new Global.FormHelper($(this).find("form"), { updateTargetId: "validation-summary" }, function (data) {
                    if (data.isSuccess) {
                        alert(data.data);
                        window.location.reload();
                    }
                    else {
                        alert(data.data);
                    }
                });
            }).on('hidden.bs.modal', function (e) {
                $("#modal-delete-student").find(".modal-content").html("");
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