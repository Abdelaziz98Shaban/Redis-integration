$(function () {
    var changeDirection = function (dir, align) {
        // Wrap block tags.
        this.selection.save();
        this.html.wrap(true, true, true, true);
        this.selection.restore();

        // Get blocks.
        var elements = this.selection.blocks();

        // Save selection to restore it later.
        this.selection.save();

        for (var i = 0; i < elements.length; i++) {
            var element = elements[i];
            if (element !== this.$el.get(0)) {
                $(element)
                    .css('direction', dir)
                    .css('text-align', align)
                    .removeClass('fr-temp-div');
            }
        }

        // Unwrap temp divs.
        this.html.unwrap();
        // Restore selection.
        this.selection.restore();
    }

    $.FroalaEditor.DefineIcon('rightToLeft', { NAME: 'long-arrow-left' });
    $.FroalaEditor.RegisterCommand('rightToLeft',
        {
            title: 'RTL',
            focus: true,
            undo: true,
            refreshAfterCallback: true,
            callback: function () {
                changeDirection.apply(this, ['rtl', 'right']);
            }
        });

    $.FroalaEditor.DefineIcon('leftToRight', { NAME: 'long-arrow-right' });
    $.FroalaEditor.RegisterCommand('leftToRight',
        {
            title: 'LTR',
            focus: true,
            undo: true,
            refreshAfterCallback: true,
            callback: function () {
                changeDirection.apply(this, ['ltr', 'left']);
            }
        });
    var deleteUrl = '/api/FroalaApi/DeleteImage';
    var imageUploadUrl = '/api/FroalaApi/UploadImageResize';
    var fileUploadUrl = '/api/FroalaApi/UploadFileValidation';
    var imageManagerLoadUrl = '/api/FroalaApi/LoadImages';

    var fullToolbar = [
        'bold', 'italic', 'underline', 'strikeThrough', 'subscript', 'superscript', '|',
        'fontFamily', 'fontSize', 'color', 'paragraphStyle', '|', 'paragraphFormat', 'align',
        'formatOL', 'formatUL', 'outdent', 'indent', '|', 'rightToLeft', 'leftToRight', '-', 'undo',
        'redo', '|', 'insertLink', 'insertImage',
        'insertVideo', 'insertFile', 'insertTable', '|', 'emoticons', 'specialCharacters', 'insertHR',
        'selectAll', 'clearFormatting', '|', 'print', 'html', 'fullscreen'
    ];

    var miniToolbar = [
        'bold', 'italic', 'underline', '|',  'align',
        '|', 'formatOL', 'formatUL', '|',
        'selectAll', 'clearFormatting', 'html'
    ];
    var quickInsertButtons = ['image', 'video', 'embedly', 'table', 'ul', 'ol', 'hr'];
    var miniRemoveTag= ['script', 'style', 'base'];
    var fullRemoveTag = ['script','base'];

    $('textarea.froala').each(
        function (i, obj) {
            var m = -1;
            var toolbar = fullToolbar;
            var htmlRemoveTags = fullRemoveTag;
            var allowDelete = false;
            if (obj.hasAttribute('charcountmax') === true) {
                m = $(obj).attr('charcountmax');
            }
            if (obj.hasAttribute('allowdelete') === true) {
                allowDelete = $(obj).attr('allowdelete');
            }
            if ($(obj).hasClass('mini') === true) {
                toolbar = miniToolbar;
                htmlRemoveTags = miniRemoveTag;
                quickInsertButtons = ['ul', 'ol', 'hr'];
            }
            $(obj).froalaEditor({
                imageUploadURL: imageUploadUrl,
                fileUploadURL: fileUploadUrl,
                fileMaxSize: 1024 * 1024 * 50,
                imageManagerLoadURL: imageManagerLoadUrl,
                imageManagerDeleteMethod: "POST",
                heightMin: 100,
                heightMax: 300,
                language: 'ar',
                quickInsertButtons: quickInsertButtons,
                toolbarButtons: toolbar,
                imageEditButtons: ['imageReplace', 'imageAlign', 'imageCaption', 'imageRemove', '|',
                    'imageLink', 'linkOpen', 'linkEdit', 'linkRemove', '-', 'imageDisplay', 'imageStyle', 'imageAlt', 'imageSize'],
                pluginsEnabled: null,
                charCounterMax: m,
                imageManagerDeleteURL: allowDelete ? deleteUrl : "",
                htmlRemoveTags: htmlRemoveTags
            });
        });
});

