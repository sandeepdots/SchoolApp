(function ($) {


    function StudentList() {
        var $this = this;

        function initializeGrid() {

            var gridPresenter = new Global.GridHelper('#grid-allocation-management', {
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
                        "sortable": false,
                        "searchable": false
                    },
                    {
                        "targets": [6],
                        "visible": true,
                        "sortable": false,
                        "searchable": false
                    },


                    {
                        "targets": [7],
                        "sortable": false,
                        "searchable": false,
                        "data": "7",
                        "render": function (data, type, row, meta) {
                     
                            var json = {
                                type: "checkbox",
                                class: "switchBox switch-medium",
                                value: row[0],
                                'data-on': "success",
                                'data-off': "danger"
                            };

                            if (data == "Active") {
                                json.checked = true;
                            }
                            return $('<input/>', json).get(0).outerHTML;
                        }
                    },
                    {
                        "targets": 8,
                        "searchable": false,
                        "sortable": false,
                        "data": "0",
                        "render": function (data, type, row, meta) {
                            var actionLink = $("<a/>", {
                                href: domain + "FacultyAllocation/AddUpdateAllocation/" + row[0],
                                id: "editPresenterModal",
                                class: "btn btn-primary btn-sm",
                                'data-toggle': "modal",
                                'data-target': "#modal-add-edit-allocation",
                                html: $("<i/>", {
                                    class: "fa fa-pencil"
                                }),
                            }).append(" Edit").get(0).outerHTML + "&nbsp;"


                            actionLink += $("<a/>", {
                                href: domain + "FacultyAllocation/DeleteFacultyAllocation/" + row[0],
                                id: "deleteAllocation",
                                class: "btn btn-danger btn-sm",
                                'data-toggle': "modal",
                                'data-target': "#modal-delete-allocation",
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
                "sAjaxSource": domain + "FacultyAllocation/AllocationIndex",
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
          
            $("#modal-add-edit-allocation").on('loaded.bs.modal', function (e) {
                $attendanceValue = [];
                formAddEditDepartment = new Global.FormHelper($(this).find("form"),
                    { updateTargetId: "validation-summary" }, function onSuccess(data) {
                        if (data.isSuccess) {
                          //  alertify.alert(data.data); 

                                window.location.href = data.redirectUrl;
                            
                        }
                        else {
                            alert(data.data);
                        }
                    });


                $('.form-checkbox').bootstrapSwitch();         
          
                $('.datefield').datepicker({             
                    dateFormat: 'yy-mm-dd',
                    autoclose: true,
                    changeMonth: true,
                    changeYear: true,
                    minDate: new Date()
                }).on('change', function (e) {

                });


                // for method timepicker

                $('#timefield').timepicker({
                    timeFormat: 'HH:mm',
                    autoclose: true,
                   
                }).on('change', function (e) {

                });

            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });

            $("#modal-delete-allocation").on('loaded.bs.modal', function (e) {

                formDeleteJobTitle = new Global.FormHelper($(this).find("form"), { updateTargetId: "validation-summary" }, function (data) {
                    if (data.isSuccess) {
                       alert(data.data);
                       // window.location.reload();
                    }
                    else {
                        alert(data.data);
                    }
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











