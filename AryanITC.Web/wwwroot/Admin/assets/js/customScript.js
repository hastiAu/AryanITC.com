
$("#filterServiceBtn").on("click", function () {
    $("#PageId").val("1");
});
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});
$(document).ready(function () {
    var usersData = [];
    $("#PhoneNumberInput").autocomplete({
        source: function (request, response) {
            $.getJSON("/AdminPanel/Specialist/SpecialistAutoComplete",
                request,
                function (data) {
                    console.log(data);
                    usersData = data;
                    response($.map(data,
                        function (item) {
                            return {
                                label: item.userId + " - " + item.fullName,
                                value: item.fullName
                            }
                        }));
                });
        },
        select: function (event, ui) {
            //console.log(event, ui);
            var user = usersData.filter(s => s.fullName === ui.item.value)[0];
            $('#UserIdInput').val(user.userId);
            $("#phoneNumberHiddenInput").val(user.mobile);
            /*$('#PhoneNumberInput').val(user.fullname);*/
        }
    });

});
$(".ArticleCategory").on('click', function () {
    var elementId = $(this).attr('value');
    var categoryElement = $('#subcategory-' + elementId);
    if ($(this).is(':checked')) {
        console.log(elementId);
        categoryElement.removeClass('d-none');
    } else {
        $(categoryElement)
        categoryElement.addClass('d-none');
        var inputs = $('#subcategory-' + elementId + ' input:checkbox');
        $(inputs).each(function (index, value) {
            console.log(value);
            $(value).prop('checked', false);
        })
    }
});
$(".articleSubCategory").on('click', function () {
    var elementId = $(this).attr('value');
    var categoryElement = $('#subsetCheckBox-' + elementId);
    if ($(this).is(':checked')) {
        console.log(elementId);
        categoryElement.removeClass('d-none');
    } else {
        $(categoryElement)
        categoryElement.addClass('d-none');
        var inputs = $('#subsetCheckBox-' + elementId + ' input:checkbox');
        $(inputs).each(function (index, value) {
            console.log(value);
            $(value).prop('checked', false);
        })
    }
});
// Related Articles Autocomplete
$(document).ready(function () {

    var relatedArticleData = [];
    $("#ArticleTitleInput").autocomplete({
        source: function (request, response) {
            $.getJSON("/AdminPanel/RelatedArticle/RelatedArticleAutoComplete",
                request,
                function (data) {
                    console.log(data);
                    relatedArticleData = data;
                    response($.map(data,
                        function (item) {
                            console.log(item);
                            return {

                                label: item.articleTitle,
                                value: item.secondArticleId
                            }
                        }));
                });
        },
        select: function (event, ui) {
            console.log(event, ui);

            var article = relatedArticleData.filter(a => a.articleTitle === ui.item.value)[0];
            $('#SecondArticleIdInput').val(article.articleId);

        }

    });
});


function CreateNotification() {
    var checkBox = document.getElementById("checkbox");
    var text = document.getElementById("myClass");
    if (checkBox.checked == true) {

        text.style.display = "none";
    }
    else {
        text.style.display = "block";
    }
}


$(document).ready(function () {
    var usersData = [];
    $("#PhoneNumberInput").autocomplete({
        source: function (request, response) {
            $.getJSON("/AdminPanel/Notification/NotificationListAutoComplete",
                request,
                function (data) {
                    console.log(data);
                    usersData = data;
                    response($.map(data,
                        function (item) {
                            return {
                                label: item.userId + " - " + item.fullName,
                                value: item.fullName
                            }
                        }));
                });
        },
        select: function (event, ui) {
            //console.log(event, ui);
            var user = usersData.filter(s => s.fullName === ui.item.value)[0];
            $('#UserIdInput').val(user.userId);
            $("#phoneNumberHiddenInput").val(user.mobile);
            /*$('#PhoneNumberInput').val(user.fullname);*/
        }
    });

});
