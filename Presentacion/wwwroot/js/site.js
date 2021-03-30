


///Modal de confirmacion generico
function confirmModal(heading, question, cancelButtonTxt, okButtonTxt, callback, param = false) {

    var confirmModal =
        $('<div class="modal fade">' +
            '<div class="modal-dialog">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<a class="close" data-dismiss="modal" >&times;</a>' +
            '<h3>' + heading + '</h3>' +
            '</div>' +

            '<div class="modal-body">' +
            '<p>' + question + '</p>' +
            '</div>' +

            '<div class="modal-footer">' +
            '<a href="#!" id="okButton" class="btn btn-success">' + okButtonTxt + '</a>' +

            '<a href="#!" class="btn  btn-danger" data-dismiss="modal">' + cancelButtonTxt + '</a>' +

            '</div>' +
            '</div>' +
            '</div>' +
            '</div>');

    confirmModal.find('#okButton').click(function (event) {
        if (param)
            callback(param);
        else
            callback();

        confirmModal.modal('hide');
    });

    confirmModal.modal('show');
}




///Agregamos la funcion de loading para los botones
(function ($) {
    $.fn.button = function (action) {
        if (action === 'loading' && this.data('loading-text')) {
            this.data('original-text', this.html()).html(this.data('loading-text')).prop('disabled', true);
        }
        if (action === 'reset' && this.data('original-text')) {
            this.html(this.data('original-text')).prop('disabled', false);
        }
    };
}(jQuery));