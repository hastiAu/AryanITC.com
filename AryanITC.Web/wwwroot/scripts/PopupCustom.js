$(function () {
      
    var PlaceHolderElement = $('#placeHolderHere');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        });
    });
    PlaceHolderElement.on('click',
        '[data-save="modal"]',
        function (event) {
            var form = $(this).parents('.modal').find('form');
            var actionUrl = form.attr('action');
            var sendData = form.serialize();
            $.post(actionUrl, sendData).done(function (data) {
                var name = $('#PersonName').value;
                if (data.length) {

                    if (data == "true") {
                        PlaceHolderElement.find('.modal').modal('hide');
                        $('div.modal-backdrop').remove();
                        ShowMessage('نوبت ثبت شد', ' مشاورین ما  در اسرع وقت با شما تماس خواهند گرفت', 'success');
                    } else if (data == "false") {
                        ShowMessage('خطا', 'امکان دریافت نوبت در حال حاضر وجود ندارد', 'error');
                    } else {
                        $('div.modal-backdrop').remove();
                            PlaceHolderElement.html(data);
                            PlaceHolderElement.find('.modal').modal('show');
                        
                    
                    }
                } else {
                    ShowMessage('خطا', 'امکان دریافت نوبت در حال حاضر وجود ندارد', 'error');
                }
               
            });
        });
});

function closeModal() {
    $('div.modal-backdrop').remove();
}

 