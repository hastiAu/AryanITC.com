﻿//Pagination
$('[Pagination]').on("click", function () {
    var pageId = $(this).attr("Pagination");
    $("#PageId").val(pageId);
    var pformid = $(this).attr("PFormId");
    $("#" + pformid + "").submit();
});



$("[remove-ajax]").on('click', function (e) {
    e.preventDefault();
    var itemId = $(this).attr("remove-ajax-item-id");
    var itemUrl = $(this).attr("href");

    swal({
        title: 'اخطار',
        text: "آیا از انجام عملیات مورد نظر اطمینان دارید؟",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "بله",
        cancelButtonText: "خیر",
        closeOnConfirm: false,
        closeOnCancel: false
    }).then((result) => {
        if (result.value) { 

            $.post(itemUrl).then(function (resultMessage) {

                if (resultMessage != null) {
                    
                    swal({
                        title: "توجه",
                        text: resultMessage.text,
                        type: "info"
                    }).then(resultCommand => {


                        if (resultMessage.statusCode == 200) {

                            if (itemId !== null && itemId !== "" && itemId !== undefined) {

                                $('[remove-ajax-item="' + itemId + '"]').hide(300);

                            } else {
                                window.location.reload();
                            }

                        }


                    });

                }

            });

        } else if (result.dismiss === swal.DismissReason.cancel) {
            swal('اعلام', 'عملیات لغو شد', 'error');
        }
    });

});

var editors = $("[editor]");
if (editors.length > 0) {

    $.getScript('/scripts/ckeditor.js',
        function () {

            $(editors).each(function (index, value) {

                var editorId = $(value).attr('editor');

                ClassicEditor.create(document.querySelector('[editor="' + editorId + '"]'),
                    {
                        toolbar: {
                            items: [
                                'heading',
                                '|',
                                'italic',
                                'bold',
                                'underline',
                                'highlight',
                                'fontSize',
                                'fontColor',
                                'fontFamily',
                                'fontBackgroundColor',
                                '|',
                                'imageUpload',
                                'imageInsert',
                                'insertTable',
                                'link',
                                'blockQuote',
                                'htmlEmbed',
                                'codeBlock',
                                'code',
                                'undo',
                                'redo',
                                'alignment',
                                'horizontalLine',
                                'specialCharacters',
                                'strikethrough',
                                'subscript',
                                'superscript',
                                'todoList',
                                'indent',
                                'outdent',
                                'numberedList',
                                'bulletedList',
                                'mediaEmbed',
                                'removeFormat',
                                'pageBreak'
                            ]
                        },
                        language: 'fa',
                        table: {
                            contentToolbar: [
                                'tableColumn',
                                'tableRow',
                                'mergeTableCells'
                            ]
                        },
                        licenseKey: '',
                        simpleUpload: {
                            // The URL that the images are uploaded to.
                            uploadUrl: '/file-upload'
                        }

                    }).then(editor => {
                        window.editor = editor;
                    }).catch(error => {
                        console.error(error);
                    });
            });

        });

}

function ShowMessage(title, text, theme) {
    window.createNotification({
        closeOnClick: true,
        displayCloseButton: false,
        positionClass: 'nfc-bottom-right',
        showDuration: 5000,
        theme: theme !== '' ? theme : 'success',
    })({
        title: title !== '' ? title : 'اعلان',
        message: text
    });
}

function Resettimespan(seconds) {
    setTimeout(function () {
        $("#ReSendCode").show();
        $("#hideAlert").hide();

    }, seconds);
}

function sendNewActivationCode(mobile) {
    $.get("/Account/SendOtpCode/"  +"0" + mobile,

        function (res) {
            location.reload();
        });
}

function MyNotifications(e) {
    $.get("/Home/VisitNotifications/" + e)

    $("#title-" + e).click(function () {
        $("#description-" + e).toggle();
    });
}


$(document).ready(function () {
    $('#demo').hide();
    $('#picker').farbtastic('#color');
});