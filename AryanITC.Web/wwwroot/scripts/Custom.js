

var isOpenSearch = false;
$(document).ready(function () {
    // Add smooth scrolling to all links
    var _haveClass = false;
    $(".form-search").click(function () {
        if (!$(".form-search").hasClass("form-search-open")) {
            isOpenSearch = false;
        } else {
            isOpenSearch = true;
        }
        $(".form-search").addClass("form-search-open");
        _haveClass = true;
    });
    $("body").click(function () {
        if (!_haveClass) {
            $(".form-search").removeClass("form-search-open");

        }
        _haveClass = false;
    });
    $(".scrolled").on('click', function (event) {

        // Make sure this.hash has a value before overriding default behavior
        if (this.hash !== "") {


            //data-isredirect
            // Prevent default anchor click behavior
            event.preventDefault();

            // Store hash
            var hash = this.hash;
            var ddfdsf = window.IsHomePage;// $(this).data("isredirect");
            if (ddfdsf != true) {
                window.location = "/" + hash;
                return false;
            }
            // Using jQuery's animate() method to add smooth page scroll
            // The optional number (800) specifies the number of milliseconds it takes to scroll to the specified area
            $('html, body').animate({
                scrollTop: $(hash.replace('2', '')).offset().top
            }, 800, function () {

                // Add hash (#) to URL when done scrolling (default click behavior)
                window.location.hash = hash.replace('2', '');

                ActiveMobileNav();
            });
        } // End if
    });
    ActiveMobileNav();
});
function ActiveMobileNav() {
    var path = window.location.pathname;
    if (path == "/") path = "/" + window.location.hash.replace('2', '')
    switch (path) {
        case "/FrontCMS/User/Login":
            Home = "Login";
            $('.bottom-menu li').eq(0).addClass('active');
            break;

        case "/#services":
            Home = "Services";
            $('.bottom-menu li').eq(1).addClass('active');
            $('.bottom-menu li').eq(2).removeClass('active');
            break;
        case "/#clubsuccess":
            Home = "ClubSuccess";
            $('.bottom-menu li').eq(2).addClass('active');
            $('.bottom-menu li').eq(1).removeClass('active');
            break;
        case "/FrontCMS/Post/Exercises":
            Home = "Exersices";
            $('.bottom-menu li').eq(3).addClass('active');
            break;
        case "/Posts/Articles":
            Home = "Articles";
            $('.bottom-menu li').eq(4).addClass('active');
            break;
        default:
            Home = "Home";
            break;
    }
}
$('.form-search').submit(function (e) {


    if ($('[name="txtSearch"]').val() == '' && $('.txtSearch2').val() == '' || !isOpenSearch) {
        e.preventDefault();

    }


    // your code here
});
$('.form-search2').submit(function (e) {
    if ($('[name="txtSearch"]').val() == '' && $('.txtSearch2').val() == '' || !isOpenSearch) {
        e.preventDefault();

    }

    // your code here
});




