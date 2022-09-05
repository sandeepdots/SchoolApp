//$(document).ajaxStart(function () {   
//    if(!DisableAjaxLoader)
//    blockUI();
//});

//$(document).ajaxStop(function () {
//    if (!DisableAjaxLoader)
//    $.unblockUI();
//});

//$(document).ajaxError(function () {    
//    $.unblockUI();
//});
//$(document).ajaxComplete(function () {
//    $.unblockUI();
//});

//function blockUI() {
   
//    $.blockUI({
    
//        message: "<div id='loader'></div>",
//        centerX: true,
//        centerY: true,
//        css: {
//            border: 'none',
//            padding: '15px',
//            backgroundColor: 'none',
//            '-webkit-border-radius': '10px',
//            '-moz-border-radius': '10px',
//            opacity: 1,
//            color: '#fff',
//            'z-index': 9999999999,
//        }
//    });
//}

//function UnblockUI() {
//    if (!DisableAjaxLoader)
//        $.unblockUI();
//}