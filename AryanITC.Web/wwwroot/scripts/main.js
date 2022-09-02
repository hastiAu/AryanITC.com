$(document).ready(function () {
    console.log('running main js')

    // vars
    const hamberger = document.querySelector('#hambergerBtn')
    const mobileOverlay = document.querySelector('#mobileOverlay')
    const mobileSlidingMenu = document.querySelector('#mobileSlidingMenu')
    const mobileCallMenuBtn = document.querySelector('#mobile-call-menu__btn')
   
    let mobileSlidingMenu_open = false

    // funcs
    const handle_hamberger_click = () => {
        console.log(handle_hamberger_click)
        mobileSlidingMenu_open = true
        mobileSlidingMenu.classList.add('show')
        mobileOverlay.classList.add('show')
    }
    const handle_outsideSlidingMenu_click = (e) => {
        if (!mobileSlidingMenu_open) return

        console.log(handle_outsideSlidingMenu_click)
        console.log(mobileSlidingMenu_open)
        const inside = mobileSlidingMenu.contains(e.target) || hamberger.contains(e.target)
        if (!inside) {
            mobileSlidingMenu.classList.remove('show')
            mobileOverlay.classList.remove('show')
            mobileSlidingMenu_open = false

        }
    }
    const handle_mobileCallMenuBtn_click = () => {
        console.log(handle_mobileCallMenuBtn_click)
        const mobileCallMenuHidden = document.querySelector('#mobile-call-menu__hidden')
        mobileCallMenuHidden.classList.toggle('show')
    }

    // events
    hamberger.addEventListener('click', handle_hamberger_click)
    document.addEventListener('click', handle_outsideSlidingMenu_click)
    mobileCallMenuBtn.addEventListener('click', handle_mobileCallMenuBtn_click)

});


var isOpenSearch = false;
$(document).ready(function () {
    $("#front").attr('checked', 'checked');
    $("#Man").attr('checked', 'checked');
    changepicture('F');
    // Add smooth scrolling to all links
    var _haveClass= false;
    $(".form-search").click(function () {
        if (!$(".form-search").hasClass("form-search-open")) {
            isOpenSearch = false;
        } else {
            isOpenSearch=true;
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

    $(".btn-reservation").click(function () {
        $('#RegisterCustumer').modal('show');
        $('#RegisterCustumer').removeClass('d-none');
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
    if(path=="/") path= "/" +window.location.hash.replace('2', '')
    switch (path) {
        case "./log in.html":
            Home="Login";
            $('.bottom-menu li').eq(0).addClass('active');
            break;

        case "/#services":
             Home="Services";
            $('.bottom-menu li').eq(1).addClass('active');
            $('.bottom-menu li').eq(2).removeClass('active');
            break;
        case "/#clubsuccess":
             Home="ClubSuccess";
            $('.bottom-menu li').eq(2).addClass('active');
             $('.bottom-menu li').eq(1).removeClass('active');
            break;
        case "./workOut-list.html":
             Home="Exersices";
             $('.bottom-menu li').eq(3).addClass('active');
            break;
        case "./articles-list.html":
             Home="Articles";
             $('.bottom-menu li').eq(4).addClass('active');
            break;
        default:
             Home="Home";
            break;
    }
}
$('.form-search').submit(function (e) {


    if ($('[name="txtSearch"]').val() == '' && $('.txtSearch2').val() == '' || !isOpenSearch ) {
        e.preventDefault();

    }


    // your code here
});
$('.form-search2').submit(function (e) {
    if ($('[name="txtSearch"]').val() == '' && $('.txtSearch2').val() == '' || !isOpenSearch ) {
        e.preventDefault();

    }

    // your code here
});

function changepicture(position) {
    if (position == 'F') {
    $(".bg").css("display", "block");
    $(".bg--back").css("display", "none");
    }
    else {
    $(".bg").css("display", "none");
    $(".bg--back").css("display", "block");
    }
    }