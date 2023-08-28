var updated;


function onModalBegin() {
    $('body :submit').attr('disabled', 'disabled').attr('data-kt-indicator', 'on');
}

function showSuccessMessage(message = "Saved Successfully !") {
    Swal.fire({
        icon: 'success',
        title: 'Success',
        text: message,
    })
}

function showErrorMessage(message = "Something went wrong!") {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: message,
    })
}

function onModalComplete() {
    $('body :submit').removeAttr('disabled').removeAttr('data-kt-indicator');
}

function onModalSuccess(item) {
    showSuccessMessage();
    $('#Modal').modal('hide');
    if (updated === undefined) {
        $('tbody').append(item);
    }
    else {
        $(updated).replaceWith(item);
        updated = undefined;
    }

}

$(document).ready(function () {

    // DataTable
    datatable = $('table').DataTable({
        "info": false,
        'pageLength': 4,
    });

    //Handle Modal
    $('body').delegate('.js-render-modal', 'click', function () {
        var btn = $(this);
        var Modal = $('#Modal');
        Modal.find('#ModalLabel').text(btn.data('title'));

        if (btn.data('updated') !== undefined) {
            updated = btn.parents('tr');
        }

        $.get({
            url: btn.data('url'),
            success: function (form) {
                Modal.find('.modal-body').html(form);
                $.validator.unobtrusive.parse(Modal);
            },
            error: function () {
                showErrorMessage();
            }
        });


        Modal.modal('show');
    });

    //Handle Toggle Status
    $('body').delegate('.js-toggle-status', 'click', function () {
        var btn = $(this);
        bootbox.confirm({
            message: 'Are you sure that you wanna toggle this item status ?',
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-warning'
                }
            },
            callback: function (result) {
                if (result) {
                    $.post({
                        url: btn.data('url'),
                        data: {
                            '__RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                        },
                        success: function (lastupdatedon) {
                            var row = btn.parents('tr');
                            console.log(row);
                            var status = row.find('.js-status');
                            var newstatus = status.text().trim() === 'Deleted' ? 'Avilable' : 'Deleted';
                            status.text(newstatus).toggleClass('text-bg-danger text-bg-primary');
                            row.find('.js-updated-on').html(lastupdatedon);

                            showSuccessMessage('Toggled Successfully');

                            row.addClass('animate__animated animate__flash');

                        },
                        error: function () {
                            showErrorMessage();
                        }
                    });
                }

            }
        });
    });
});