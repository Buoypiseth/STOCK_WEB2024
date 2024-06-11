$(function () {
    $('.nav-item').on('click', function (e) {
        var url = window.location.pathname;
        var lastUrl = $(this).find('a').attr("href");
        if (url == lastUrl ) {
            e.preventDefault();
        }
    });
});


