function NewLetterPostSuccess(result) {
    swal('اعلام', result.text, 'info');
    $('#NewsLetterEmail').val('');
}

function ShowInformationMessage(text) {
    swal('اعلام', text, 'info');
}

function CreateArticleCommentSuccess(result) {

    if (result.statusCode == 200) {
        swal('توجه', result.text, 'success').then((result) => {
            if (result.value) {

                window.location.reload();

            }
        });
    } else {
        ShowInformationMessage(result.text);
    }
}

function ChangeCommentPage(pageId) {

    $.get('/Article/ShowComments', { pageId: pageId })
        .done(function (data) {
            var commentsList = $('#comments-list');

            commentsList.empty();
            console.log(data);
            commentsList.html(data);
        });

}

$('[article-vote]').on('click',
    function (e) {

        e.preventDefault();
        var url = $(this).attr('href');

        $.post(url,
            function (data, status) {
                ShowInformationMessage(data.text);
                $('#articlvote-count').empty();
                $('#articlvote-count').html(data.articleVoteCount);
            });

    });

$('[replay-comment]').on('click',
    function (e) {

        e.preventDefault();

        var commentId = $(this).attr('replay-comment');
        $('#Comment-ParentId').val(commentId);

        $('html, body').animate({
            scrollTop: $("#comment-form").offset().top - 200
        }, 1500);

    });

 
