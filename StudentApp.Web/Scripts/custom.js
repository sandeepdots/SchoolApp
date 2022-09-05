// Click to toggle the hamburger-icon animation and add the fullscreen-menu overlay.
$(document).ready(function(){
    $('#nav-icon').click(function(){
        $(this).toggleClass('animate-icon');
    });
});

//sameheight
$(document).ready(function () {
    $('.equal-height').matchHeight();
});

//toggle
$(function () {
    $('.dash-mobile-nav-toggle').click(function () {
        $('.dash-left').toggleClass('active');
        document.querySelector('body').classList.toggle('overflow');
        return false;
    });
});

//toggle
$(function () {
    $('.profile-toggle').click(function () {
        $('.profile-detail').slideToggle('slow');
        return false;
    });
});
//tooltip js
$(document).ready(function () {
    try {
        $('.js-btn-tooltip').tooltip();
        $('.js-btn-tooltip--custom').tooltip({
            customClass: 'tooltip-custom'
        });
        $('.js-btn-tooltip--custom-alt').tooltip({
            customClass: 'tooltip-custom-alt'
        });
    }
    catch (ex) {

    }
  

  $('.js-btn-popover').popover();
  $('.js-btn-popover--custom').popover({
    customClass: 'popover-custom'
  });
  $('.js-btn-popover--custom-alt').popover({
    customClass: 'popover-custom-alt'
  });

});

//scroll active class
$(document).on('click', '.scroll-nav ul li a', function (event) {
    "use strict";
    event.preventDefault();

    $('html, body').animate({
        scrollTop: $($.attr(this, 'href')).offset().top
    }, 100);

});

//scroll nav
$(function () {
    "use strict";
    $(window).bind('scroll', function () {
        $('.anchor').each(function () {
            var anchored = $(this).attr("id"),
                position = $(this).position().top - $(window).scrollTop(),
                targetOffset = $(this).offset().top - 50;

            if ($(window).scrollTop() > targetOffset) {
                $('.scroll-nav ul').find('a').removeClass("active");
                $('.scroll-nav ul').find(('*[data-anchor="' + anchored + '"]')).addClass("active");
            }
        });
    });
});

//Scroll Window Animation JS//
$(function () {
	$('.anim-btn').click(function () {
		if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
			var target = $(this.hash);
			target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
			if (target.length) {
				$('html,body').animate({
					scrollTop: target.offset().top - ($(window).width() > 767 ? 84 : 100)
				}, 1000);
				return false;
			}
		}
	});
});

//hide msg screen
window.onload = function () {
    var isFirstLogin = localStorage.getItem("betamsg");
    if (localStorage.getItem("betamsg") != null) {
        if (isFirstLogin != 1) {
            localStorage.setItem("betamsg", 1);
            document.getElementById('beta-msg').classList.remove('hide');
        }
        else {
            try {
                document.getElementById('beta-msg').classList.add('hide');
            }
            catch (exp) {

            }
            
        }
    } else {
        localStorage.setItem("betamsg", 1);
        document.getElementById('beta-msg').classList.remove('hide');
    }
};

//msg screen hide show
$(function () {
    $('.beta-msg-icon').click(function () {
        $('.beta-msg-box').toggleClass('hide');
        return false;
    });
    $('.close-msg').click(function () {
        $('.beta-msg-box').addClass('hide');
        return false;
    });
});

//responsive tabs
//$('#VerticalTab').easyResponsiveTabs({
//    type: 'vertical', //Types: default, vertical, accordion
//    width: 'auto', //auto or any width like 600px
//    fit: true, // 100% fit in a container
//    closed: 'accordion', // Start closed if in accordion view
//    tabidentify: 'hor_1', // The tab groups identifier
//    activate: function(event) { // Callback function if tab is switched
//        var $tab = $(this);
//        var $info = $('#nested-tabInfo2');
//        var $name = $('span', $info);
//        $name.text($tab.text());
//        $info.show();
//    }
//});

