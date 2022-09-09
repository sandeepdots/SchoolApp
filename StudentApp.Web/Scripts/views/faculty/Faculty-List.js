(function ($) {


    function FacultyList() {
        var $this = this;

        function initializeGrid() {
            
            var gridPresenter = new Global.GridHelper('#grid-faculty-management', {
                "columnDefs": [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    },
                    {
                        "targets": [1],
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
                        "sortable": false,
                        "searchable": false
                    },
                    {
                        "targets": [4],
                        "visible": true,
                        "sortable": false,
                        "searchable": false
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
                                href: domain + "/Faculty/AddEditFaculty/" + row[0],
                                id: "editPresenterModal",
                                class: "btn btn-primary btn-sm",
                                'data-toggle': "modal",
                                'data-target': "#modal-add-edit-faculty",
                                html: $("<i/>", {
                                    class: "fa fa-pencil"
                                }),
                            }).append(" Edit").get(0).outerHTML + "&nbsp;"


                            actionLink += $("<a/>", {
                                href: domain + "/Faculty/DeleteFaculty/" + row[0],
                                id: "deletePresenter",
                                class: "btn btn-danger btn-sm",
                                'data-toggle': "modal",
                                'data-target': "#modal-delete-faculty",
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
                "sAjaxSource": domain + "Faculty/FacultyIndex",   //some changes here //

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
        ///chh

        function initializeModalWithForm() {
            $("#modal-add-edit-faculty").on('loaded.bs.modal', function (e) {            
                $attendanceValue = [];
                formAddEditPresenter = new Global.FormHelper($("#form-Add-Edit-Faculty"),
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
            }).on('hidden.bs.modal', function (e) {
                $("#modal-add-edit-faculty").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });


            $("#modal-delete-faculty").on('loaded.bs.modal', function (e) {

                $attendanceValue = [];
                formDeleteFaculty = new Global.FormHelper($("#formDeleteFaculty"),
                    { updateTargetId: "validation-summary" }, function onSuccess(result) {
                        window.location.reload();
                    });

            }).on('hidden.bs.modal', function (e) {
                $("#modal-delete-faculty").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });


        }



        $this.init = function () {
            initializeGrid();
            initializeModalWithForm();
        };
    }
    $(function () {
        var self = new FacultyList();
        self.init();
    });

}(jQuery));