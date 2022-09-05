
/*global window, $*/
$.validator.addMethod(
    "regex",
    function (value, element, param) {
        var re = new RegExp(param.pattern);
        return this.optional(element) || re.test(value);
    },
    "Invalid format."
);

var Global = {
    MessageType: {
        Success: 0,
        Error: 1,
        Warning: 2,
        Info: 3
    }
};


Global.bindSorting = function (selector, handleClass, itemClass, fnDragEndCallBack, fnDragStartCallBack) {
    if (typeof Sortable != 'undefined')
        if (Sortable) {
            $(selector).each(function (index, element) {
                if (fnDragEndCallBack && fnDragStartCallBack) {
                    Sortable.create(element, {
                        sort: true,
                        handle: handleClass,
                        draggable: itemClass,
                        animation: 150,
                        easing: "cubic-bezier(1, 0, 0, 1)",
                        onEnd: fnDragEndCallBack,
                        onStart: fnDragStartCallBack
                    });
                }
                else {
                    Sortable.create(element, {
                        sort: true,
                        handle: handleClass,
                        draggable: itemClass,
                        animation: 150,
                        easing: "cubic-bezier(1, 0, 0, 1)"
                    });
                }
            });
        }
}

setTimeout(function () {
    if ($(".main-dynamically-added-container").length > 0) {
        Global.bindSorting(".toptip_container", ".card-header", ".flexible-content-box", function (event) {
            var $itemsInSections = null;
            $(event.item).parents("div.main-dynamically-added-container:first").find(".flexible-content-box").each(function (index, element) {
                var $mainCointainer = $(this).parents("div.main-dynamically-added-container:first");
                var displayOrderProperty = $mainCointainer.find("button.add-multiple-section").data("display-order-property");

                if (displayOrderProperty && displayOrderProperty != "") {
                    $(element).find("[ID$=__" + displayOrderProperty + "]").val(index);
                }
                else if ($(element).find("[ID$=__DisplayOrder]").length > 0) {
                    displayOrderProperty = "DisplayOrder";
                    $(element).find("[ID$=__" + displayOrderProperty + "]").val(index);
                }
                $itemsInSections = $mainCointainer.find(".flexible-content-box i.fa-plus").parent();
                $itemsInSections.trigger("click");
            });
        }, function (event) {
            if ($itemsInSections && $itemsInSections.length > 0) {
                $itemsInSections.parent().trigger("click");
            }
        });
    }
}, 200);

String.prototype.replaceBetween = function (start, end, value) {
    return this.substring(0, start) + value + this.substring(end);
};
function reindexLast(ele, index) {

    var i = $(ele).attr("name");
    var start = i.lastIndexOf("[");
    var end = i.lastIndexOf("]") + 1;
    i = i.replaceBetween(start, end, "[" + index + "]");
    $(ele).attr("name", i);
}

Global.ReIndexList = function (list) {

    if (list.length) {

        var i = 0;
        list.each(function (f, g) {
            $(g).find(":input.reindex:not(:disabled)").each(function (h, j) {
                reindexLast(j, i);
            });
            i++
        });
    }
};
Global.FormHelper = function (formElement, options, onSucccess, onError, onValidate) {
    "use strict";
    var settings = {};
    settings = $.extend({}, settings, options);
    try {
        formElement.validate(settings.validateSettings);
    } catch {

    }
    



    formElement.submit(function (e) {
        e.stopPropagation();
        e.preventDefault();
        e.stopImmediatePropagation();
        if (options && options.beforeSubmit) {
            if (!options.beforeSubmit()) {
                return false;
            }
        }
        var submitBtn = formElement.find(':submit');
        if (formElement.validate().valid()) {
            var returnOnValidate = true;
            if (onValidate !== null && onValidate !== undefined && typeof (onValidate) === "function") {
                returnOnValidate = onValidate(submitBtn);
            }
            if (returnOnValidate) {
                submitBtn.find('i').removeClass("fa fa-arrow-circle-right");
                submitBtn.find('i').addClass("fa fa-refresh");
                submitBtn.prop('disabled', true);
                submitBtn.find('span').html('Submiting..');
                $.ajax(formElement.attr("action"), {
                    type: "POST",
                    data: formElement.serializeArray(),
                    success: function (result) {
                        if (onSucccess === null || onSucccess === undefined) {
                            if (result.isSuccess) {
                                window.location.href = result.redirectUrl;
                            } else {
                                if (settings.updateTargetId) {
                                    $("#" + settings.updateTargetId).html(result);
                                }
                            }
                        } else {
                            onSucccess(result);
                        }
                    },
                    error: function (jqXHR, status, error) {
                        if (onError !== null && onError !== undefined) {
                            onError(jqXHR, status, error);
                        }
                    },
                    complete: function () {
                        submitBtn.find('i').removeClass("fa fa-refresh");
                        submitBtn.find('i').addClass("fa fa-arrow-circle-right");
                        submitBtn.find('span').html('Submit');
                        submitBtn.prop('disabled', false);
                    }
                });
            }
        }

        e.preventDefault();
    });

    return formElement;
};
Global.FormHelperWithFiles = function (formElement, options, onSucccess, onError, loadingElementId, onComplete) {
    "use strict";
    var settings = {};

    settings = $.extend({}, settings, options);
    formElement.validate(settings.validateSettings);
    formElement.submit(function (e) {

        if (options && options.beforeSubmit) {
            if (!options.beforeSubmit()) {
                return false;
            }
        }

        var formdata = new FormData();
        formElement.find('input[type="file"]:not(:disabled)').each(function (i, elem) {
            if (elem.files && elem.files.length) {
                for (var i = 0; i < elem.files.length; i++) {
                    var file = elem.files[i];
                    formdata.append(elem.getAttribute('name'), file);
                }
            }
        });

        $.each(formElement.serializeArray(), function (i, item) {
            formdata.append(item.name, item.value);
        });


        var submitBtn = formElement.find(':submit');
        if (formElement.validate().valid()) {
            submitBtn.find('i').removeClass("fa fa-arrow-circle-right");
            submitBtn.find('i').addClass("fa fa-refresh");
            submitBtn.prop('disabled', true);
            submitBtn.find('span').html('Submiting..');
            $.ajax(formElement.attr("action"), {
                type: "POST",
                data: formdata,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    if (settings.loadingElementId != null || settings.loadingElementId != undefined) {
                        $("#" + settings.loadingElementId).show();
                        submitBtn.hide();
                    }
                },
                success: function (result) {
                    if (onSucccess === null || onSucccess === undefined) {
                        if (result.isSuccess) {
                            window.location.href = result.redirectUrl;
                        } else {
                            if (settings.updateTargetId) {
                                var datatresult = (result.message == null || result.message == undefined) ? ((result.data == null || result.data == undefined) ? result : result.data) : result.message;
                                $("#" + settings.updateTargetId).html(datatresult);
                            }
                        }
                    } else {
                        onSucccess(result);
                    }
                },
                error: function (jqXHR, status, error) {
                    if (onError !== null && onError !== undefined) {
                        onError(jqXHR, status, error);
                        $("#loadingElement").hide();
                    }
                },
                complete: function (result) {
                    if (onComplete === null || onComplete === undefined) {
                        if (settings.loadingElementId != null || settings.loadingElementId != undefined) {
                            $("#" + settings.loadingElementId).hide();
                        }
                        submitBtn.find('i').removeClass("fa fa-refresh");
                        submitBtn.find('i').addClass("fa fa-arrow-circle-right");
                        submitBtn.find('span').html('Submit');
                        submitBtn.prop('disabled', false);
                    } else {
                        onComplete(result);
                    }
                }
            });
        }

        e.preventDefault();
    });

    return formElement;
};
Global.FormHelperWithCon = function (formElement, options, onSucccess, onError) {
    //"use strict";  
    var settings = {};
    settings = $.extend({}, settings, options);
    $.validator.unobtrusive.parse(formElement)
    formElement.validate(settings.validateSettings);

    formElement.submit(function (e) {
        var stop=$(this).data("stopSubmit");
        if (stop=="true")
            return true;
        var submitBtn = formElement.find(':submit');

        if (formElement.validate().valid()) {

            $.ajax(formElement.attr("action"), {
                type: "POST",
                data: formElement.serializeArray(),
                beforeSend: function (xhr) {
                    $(':input[type="submit"]').prop('disabled', true);
                },
                success: function (result) {
                    if (onSucccess === null || onSucccess === undefined) {

                        if (result.isSuccess) {

                            window.location.href = result.redirectUrl;
                        } else {

                            if (settings.updateTargetId) {
                                if (result.data == undefined) {
                                    $("#" + settings.updateTargetId).html("<span>" + result + "</span>");
                                }
                                else {
                                    $("#" + settings.updateTargetId).html("<span>" + result.data + "</span>");
                                }
                            }
                        }
                    } else {

                        onSucccess(result);
                    }
                },
                error: function (jqXHR, status, error) {
                    if (onError !== null && onError !== undefined) {
                        onError(jqXHR, status, error);
                    }
                    $(':input[type="submit"]').prop('disabled', false);
                }, complete: function () {
                    $(':input[type="submit"]').prop('disabled', false);
                }
            });
        }
        e.preventDefault();
    });

    return formElement;
};
Global.GridHelper = function (gridElement, options) {
    if ($(gridElement).find("thead tr th").length > 1) {
        var settings = {};
        settings = $.extend({}, settings, options);
        $(gridElement).dataTable(settings);
        return $(gridElement);
    }
};
Global.FormValidationReset = function (formElement, validateOption) {
    if ($(formElement).data('validator')) {
        $(formElement).data('validator', null);
    }

    $(formElement).validate(validateOption);

    return $(formElement);
};

Global.IsNull = function (o) { return typeof o === "undefined" || typeof o === "unknown" || o == null || o == '' };
Global.IsNotNull = function (o) { return !Global.IsNull(o); };
Global.IsNullOrEmptyString = function (str) {
    return Global.IsNull(str) || typeof str === "string" && $.trim(str).length == 0
};


Global.IsNotNullOrEmptyString = function (str) { return !Global.IsNullOrEmptyString(str); };
Global.IsNotNullResult = function (results) { return Global.IsNotNull(results) && results.length > 0; };

Global.ValidateInput = function (str) {
    return (str != undefined && str != null && str != '');
}
Global.showsPartial = function ($url, $divId, fnCallBack) {
    $.ajax({
        url: $url,
        type: 'GET',
        async: false,
        crossDomain: true,
        cache: false,
        success: function (htmlElement) {

            $('#' + $divId).empty().html(htmlElement).promise().done(function () {
                if (fnCallBack && typeof (fnCallBack) === "function") {
                    fnCallBack(htmlElement);
                }
            });
        }
    });
}
/******************************************************************************************************************************/
Global.Alert = function (title, message, callback) {
    alertify.alert(title, message, function () {
        if (callback)
            callback();
    }).set({
        transition: 'fade',
        'movable': false
    })
};


Global.Confirm = function (title, message, okCallback, cancelCallback) {
    return alertify.confirm(title, message, function () {
        if (okCallback)
            okCallback();
    }, function () {
        if (cancelCallback)
            cancelCallback();
    }).set({ transition: 'fade', 'closable': false, 'movable': false });
};
Global.ConfirmWith = function (title, message, okCallback, cancelCallback) {
    return alertify.confirm(title, message, function () {
        if (okCallback)
            okCallback();
    }, function () {
        if (cancelCallback)
            cancelCallback();
        }).set({ transition: 'fade', 'closable': false, 'movable': false }).set('labels', { ok: ' Return to add the batch number', cancel: 'Continue to submit without coa data' });
};

Global.ConfirmWithCustomLable = function (title, message,okbuttontext,cancelText, okCallback, cancelCallback) {
    return alertify.confirm(title, message, function () {
        if (okCallback)
            okCallback();
    }, function () {
        if (cancelCallback)
                cancelCallback();
        }).set({ transition: 'fade', 'closable': false, 'movable': false }).set('labels', { ok: okbuttontext != undefined && okbuttontext != null && okbuttontext != '' ? okbuttontext : 'OK', cancel: cancelText != undefined && cancelText != null && cancelText != '' ? cancelText : 'Cancel' });
};

Global.ShowMessage = function (message, type) {
    if (type == Global.MessageType.Success) {
        alertify.success(message);
    }
    else if (type == Global.MessageType.Error) {
        alertify.error(message);
    }
    else if (type == Global.MessageType.Warning) {
        alertify.warning(message);
    }
    else if (type == Global.MessageType.Info) {
        alertify.message(message);
    }
}

Global.SetDatePicker = function (selector, endDate = null) {

    var todayDate = new Date();
    $(selector).each(function () {
        try {
            if (endDate == null) {
                $(selector).datetimepicker({
                    defaultDate: new Date(),
                    format: "MM/DD/YYYY"
                }).on('dp.change', function (e) {
                    $(selector).removeClass('error').next('label').remove();
                    $('#PersonalInfoTabContent .dobError').html('');
                });
            }
            else {
                $(selector).datetimepicker({
                    defaultDate: new Date(),
                    format: "MM/DD/YYYY",
                    maxDate: endDate
                }).on('dp.change', function (e) {
                    $(selector).removeClass('error').next('label').remove();
                    $('#PersonalInfoTabContent .dobError').html('');
                });
            }
        } catch (e) {
            console.log(e);
        }
    })

}
Global.manageDependentCtrlVisibility = function ($item) {
    var dependentCtrl = $item.data("dependent-ctrl");
    var $collapseIcon = $item.parents("div:first").find("button[data-widget='collapse'] i");
    if ($item.is(":checked")) {
        //$('#' + dependentCtrl).removeClass("hidden");
        $('#' + dependentCtrl).slideDown("fast", function () {
            $collapseIcon.removeClass("fa-minus").addClass("fa-minus");
        });
    }
    else {
        //$('#' + dependentCtrl).addClass("hidden");        
        $('#' + dependentCtrl).slideUp("fast", function () {
            $("#" + dependentCtrl).find("input[type=text]").each(function () {
                $(this).val("");
            })
            $collapseIcon.removeClass("fa-minus").addClass("fa-plus");
        });
    }
};

/**
* Get the value of a querystring
* @param  {String} field The field to get the value of
* @param  {String} url   The URL to get the value from (optional)
* @return {String}       The field value
*/
var getQueryString = function (field, url) {
    var href = url ? url : window.location.href;
    var reg = new RegExp('[?&]' + field + '=([^&#]*)', 'i');
    var string = reg.exec(href);
    return string ? string[1] : null;
};
/******************************************************************************************************************************/

function manageDependent() {
    $("input[type=checkbox][data-dependent-ctrl!='']").each(function () {
        Global.manageDependentCtrlVisibility($(this));
    });
}
$(document).ready(function () {
    manageDependent();
    $(document).on("change", "input[type=checkbox][data-dependent-ctrl!='']", function () {
        Global.manageDependentCtrlVisibility($(this));
    });
});
//var diagnosisItemIndex = [];
var randeredItems = [];

Global.Select2Tab = function (controlId, mainCollectionId, otherCollectionId, placeholderText, multiSelectData, onChangeCallback) {
   
    if (controlId != null) {
        function format(state, element) {
            var isExistsInmultiSelectData = function (textToSearch) {
                var result = multiSelectData.filter(function (e, i) {
                    return e.text.trim().replace(/ /g, '').toLowerCase() == textToSearch.trim().replace(/ /g, '').toLowerCase();
                });
                return result.length >= 1;
            }

            if (!state.id) return state.text; // optgroup
            var isOtherItem = state.id.indexOf("O_") >= 0;
            var isMainItem = !isOtherItem && state.id.indexOf("M_") >= 0 && isExistsInmultiSelectData(state.text);
            var isNewItem = !isMainItem && !isOtherItem;
            var itemId = !isNewItem ? state.id.substring(2, state.id.length) : state.id;
            var itemControlId = isNewItem ? otherCollectionId + "_New" : isOtherItem ? otherCollectionId : mainCollectionId;

            if (!isMainItem) {
                $(element).addClass("select2-other-item");
            }

            var newIndex = 0;
            if ((otherCollectionId == "SelectedOtherAllergy" && itemControlId.includes("SelectedOtherAllergy")) || (otherCollectionId == "SelectedOtherMedications" && itemControlId.includes("SelectedOtherMedications")) ||
                (otherCollectionId == "SelectedOtherSymptom" && itemControlId.includes("SelectedOtherSymptom"))) {
                
                var newItemToBeRandered = $('<input id="' + itemControlId + '_' + newIndex + '__Value" name="' + itemControlId + '[' + newIndex + '].Value" data-type-value="' + state.id + '" type="hidden" class="' + itemControlId + '" value="' + state.text + '">'
                    + '&nbsp; &nbsp; &nbsp;<label> ' + state.text + ' </label>');

                return newItemToBeRandered;

            }
            else {
                var newItemToBeRandered = $('<input id="' + itemControlId + '_' + newIndex + '__Value" name="' + itemControlId + '[' + newIndex + '].Value" data-type-value="' + state.id + '" type="hidden" class="' + itemControlId + '" value="' + itemId + '">'
                    + '&nbsp; &nbsp; &nbsp;<label> ' + state.text + ' </label>');

                return newItemToBeRandered;
            }
          
        }
        var resetRanderedItemIndexing = function () {
            randeredItems = [];
            if (mainCollectionId != undefined && mainCollectionId != '') {
                $("input[type=hidden]." + mainCollectionId).each(function (i, e) {
                    $(this).attr("id", mainCollectionId + "_" + i + "__Value").attr("name", mainCollectionId + "[" + i + "].Value");
                });
            }
            if (otherCollectionId != undefined && otherCollectionId != '') {
                
                $("input[type=hidden]." + otherCollectionId).each(function (i, e) {
                    $(this).attr("id", otherCollectionId + "_" + i + "__Value").attr("name", otherCollectionId + "[" + i + "].Value");
                });
            }

            if (otherCollectionId != undefined && otherCollectionId != '') {
                $("input[type=hidden]." + otherCollectionId + "_New").each(function (i, e) {
                    var newOtherControlId = otherCollectionId + "_New";
                    $(this).attr("id", newOtherControlId + "_" + i + "__Value").attr("name", newOtherControlId + "[" + i + "].Value");
                });
            }
            
        };

        if ($("#" + controlId).hasClass("select2-hidden-accessible")) {
            $("#" + controlId).select2('destroy');
        }
        if (controlId === "MedicalProfileSecondaryDiagnoses" || controlId ==='AVSDiagnose') {
            $("#" + controlId).select2({
                placeholder: placeholderText,
                tags: true,
                templateSelection: format,
                createSearchChoice: function (term, data) { if ($(data).filter(function () { return this.text.localeCompare(term) === 0; }).length === 0) { return { id: term, text: term }; } },
                multiple: true,
                width: "100%",
                closeOnSelect: true,
                createTag: function (params) {
                    return undefined;
                },
                //maximumSelectionLength: 10,
                //allowClear: true,
                //minimumInputLength: 1,
                data: multiSelectData
            }).on("change", function (e) {
                resetRanderedItemIndexing();
                if (typeof (onChangeCallback) === "function") {
                    setTimeout(function () {
                        onChangeCallback(e);
                    }, 100);
                }
            });
        }
        else {
            $("#" + controlId).select2({
                placeholder: placeholderText,
                tags: true,
                templateSelection: format,
                createSearchChoice: function (term, data) { if ($(data).filter(function () { return this.text.localeCompare(term) === 0; }).length === 0) { return { id: term, text: term }; } },
                multiple: true,
                width: "100%",
                closeOnSelect: true,
                //maximumSelectionLength: 10,
                //allowClear: true,
                //minimumInputLength: 1,
                data: multiSelectData
            }).on("change", function (e) {
                resetRanderedItemIndexing();
                if (typeof (onChangeCallback) === "function") {
                    setTimeout(function () {
                        onChangeCallback(e);
                    }, 100);
                }
            });
        }
        setTimeout(function () {
            resetRanderedItemIndexing();
        }, 500);
       
    }
};
//$(document).off("click", ".paginate_button").on("click", ".paginate_button", function (event) {
//    var targetId = '#' + $(this).parent().parent().attr('id').replace('_paginate', '_wrapper');

//    $('.dynamicscrollLink').remove();
//    $("body").append('<a class="dynamicscrollLink" id="linkscroll" href="' + targetId + '"> &nbsp</a>')
//    $('#linkscroll')[0].click();

//});

//function for google calendar sync
Global.StartSyncWithGoogle = function (eventId) {
    $.get(domain + "Users/ManageAppointment/GetEventsForSync?eventId=" + eventId, function (data) {
        if (data != undefined && data != null && data != '' && data != '[]') {
            return gapi.auth2.getAuthInstance()
                .signIn({ scope: "https://www.googleapis.com/auth/calendar https://www.googleapis.com/auth/calendar.events" })
                .then(function (daa) {
                    alertify.success("Appointment schedules are synced successfully with google calendar");
                    loadClient(data);
                },
                    function (err) { console.error("Error signing in", err);  });
        }

    }).always(function () {
    });



}
Global.StartSecretarySyncWithGoogle = function (eventId, doctorId) {
    $.get(domain + "Users/ManageAppointment/GetEventsForSync?eventId=" + eventId + "&doctorId=" + doctorId, function (data) {
        if (data != undefined && data != null && data != '' && data != '[]') {
            return gapi.auth2.getAuthInstance()
                .signIn({ scope: "https://www.googleapis.com/auth/calendar https://www.googleapis.com/auth/calendar.events" })
                .then(function (daa) {
                    alertify.success("Appointment schedules are synced successfully with google calendar");
                    loadClient(data, eventId);
                },
                function (err) {
                    console.error("Error signing in", err);
                });
        }

    }).always(function () {
    });



}

Global.ValidatePhoneNumber = function (value) {
    var onlyInteger = /^\d{0,12}$/.test(value);
    var IndianNumber = /^[+]?\d{0,12}$/.test(value);
    var us = /^[+]?\d{0,1}?\d{0,3}[-]?\d{0,3}[-]?\d{0,4}?$/.test(value);
    return (onlyInteger || IndianNumber || us);


}

Global.ValidateDecimal = function (value) {
    return /^\d*[.]?\d{0,2}$/.test(value);
}
Global.ValidateInteger = function (value) {
    return /^\d*$/.test(value)
}

Global.isGuid = function (stringToTest) {
    if (stringToTest[0] === "{") {
        stringToTest = stringToTest.substring(1, stringToTest.length - 1);
    }
    var regexGuid = /^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$/gi;
    return regexGuid.test(stringToTest);
}

function loadmultipledata(data) {

    const eventdata = data;
    gapi.client.setApiKey("AIzaSyCGMs_DHq7D_1aOf5A36XtScAsGN1YhSK0");
    return gapi.client.load("https://content.googleapis.com/discovery/v1/apis/calendar/v3/rest")
        .then(function () {

            console.log("GAPI client loaded for API");
            for (var j = 0; j < eventdata.length; j++) {

                googleAppointmentId = ''
                const events = eventdata[j];
                var request;

                //googleAppointmentId = data.htmlLink;
                request = function (resource) {

                    return gapi.client.calendar.events.insert({
                        'calendarId': 'primary',
                        'resource': resource
                    });

                    // Function that returns a request.

                }(events);
                setTimeout(function () {
                    // Bind to the current event.
                    request.execute(function (resp) {

                        $.ajax({
                            type: 'POST',
                            url: domain + "Users/ManageAppointment/UpdateEventId",
                            data: { eventId: resp.id, appointmentId: resp.extendedProperties.shared.appointmentId },
                            async: true,
                            success: function (result) {

                            }, error: function (xhr) {

                            }
                        });


                    });
                }, 500);
                


            }
        },
            function (err) { console.error("Error loading GAPI client for API", err); });


}



function loadClient(data, googleAppointmentId, googleEventId) {

    gapi.client.setApiKey("AIzaSyCGMs_DHq7D_1aOf5A36XtScAsGN1YhSK0");

    return gapi.client.load("https://content.googleapis.com/discovery/v1/apis/calendar/v3/rest")
        .then(function () { console.log("GAPI client loaded for API"); execute(data, googleAppointmentId, googleEventId); },
            function (err) { console.error("Error loading GAPI client for API", err); });
}
// Make sure the client is loaded and sign-in is complete before calling this method.
function execute(data, googleAppointmentId, googleEventId) {


    const events = data;
    var request;
    for (var j = 0; j < events.length; j++) {
        request = function (resource) {
            if (googleEventId != undefined && googleEventId != null && googleEventId != '') {
                return gapi.client.calendar.events.update({
                    'calendarId': 'primary',
                    'eventId': googleEventId,
                    'resource': resource
                });

            }
            else {
                return gapi.client.calendar.events.insert({
                    'calendarId': 'primary',
                    'resource': resource
                });
            }
            // Function that returns a request.

        }(events[j]);
        // Bind to the current event.
        request.execute(function (resp) {
            if (googleEventId != undefined && googleEventId != null && googleEventId != '') {
                console.log(resp);
            }
            else {
                $.post(domain + "Users/ManageAppointment/UpdateEventId", { eventId: resp.id, appointmentId: resp.extendedProperties.shared.appointmentId }, function (data) {

                });
            }


        });
    }


}
function SyncAppointmentWithGoogleCalendar(googleAppointmentId, googleEventId) {

    $.get(domain + "Users/ManageAppointment/GetEventsForSync?eventId=" + googleAppointmentId, function (data) {
        if (data != undefined && data != null && data != '' && data != '[]') {

            try {
                if (gapi == undefined) {
                    Global.Alert("Network problem. Please check your internet connection.");
                    return false;
                }
            } catch{
                Global.Alert("Network problem. Please check your internet connection.");
                return false;
            }

            if (gapi == undefined) {
                Global.Alert("Network problem. Please check your internet connection.");
                return false;
            }
            try {
                if (gapi.auth2.getAuthInstance().isSignedIn.get()) {
                    loadClient(data, googleAppointmentId, googleEventId);
                }
                else {
                    return gapi.auth2.getAuthInstance()
                        .signIn({ scope: "https://www.googleapis.com/auth/calendar https://www.googleapis.com/auth/calendar.events" })
                        .then(function (daa) {
                            alertify.success("Appointment schedules are synced successfully with google calendar");
                            loadClient(data, googleAppointmentId, googleEventId);
                        },
                        function (err) {
                            console.error("Error signing in", err); 
                        });
                }
            } catch{
                Global.Alert("Network problem. Please check your internet connection.");
                return false;
            }
        }
    });

}

function PreventCharcter(obj, key) {
    if (key.keyCode < 48 || key.keyCode > 57) {
        return false;
    }
    var txtDOB = $('.DOBValidation').val()
    var DOB = txtDOB.split('/')
    var year = parseInt(DOB[2], 10);
    if (year.toString().length >= 4) {
        return false;
    }
}

function ValidateDOB(obj) {
    var txtDOB = $('.DOBValidation').val()
    var DOB = txtDOB.split('/')
    var day = parseInt(DOB[1], 10);
    var month = parseInt(DOB[0], 10);
    var year = parseInt(DOB[2], 10);
    if (year < 1000 || year > 3000 || month == 0 || month > 12) {
        $('.dobError').text("Please enter DOB in MM/DD/YYYY format.");
        $(obj).find('.DOBValidation').focus();
        return false;
    }
    $('.dobError').text("");

}
//$(".alert-success,alert-danger").fadeTo(5000, 1000).slideUp(1000, function () {
//    $(".alert-success,alert-danger").slideUp(1000);
//});
//================================================================

