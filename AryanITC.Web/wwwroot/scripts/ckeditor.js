//ClassicEditor
//    .create(document.querySelector('#editor'))
//    .catch (error => {

//		console.error(error);
//    });
CKEDITOR.disableAutoInline = true;

CKEDITOR.inline('editor2', {
    extraPlugins: 'sourcedialog',
    removePlugins: 'sourcearea'
});
