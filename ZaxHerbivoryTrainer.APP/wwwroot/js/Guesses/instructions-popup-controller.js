
function PopupMessage(isSuccess) {
    var html = '<div id="error-box">' +
        '<div class="face2">' +
        '<div class="eye"></div>' +
        '<div class="eye right"></div>' +
        '<div class="mouth sad"></div></div>' +
        '<div class="shadow move"></div>' +
        '<div class="message"><h1 class="alert">Whoops!</h1><p>That is not the right answer</p><p> Your answer was: ' +
        ($('.rangeslider__handle')[0].textContent) + "%" +
        '</p><p> The correct herbivory percentage is: ' +
        data.CorrectPercent + "%" +
        '</p></div>' +
        '</div>';

    if (isSuccess) {
        html = '<div id="success-box"> ' +
            '<div class="face">' +
            '<div class="eye"></div>' +
            '<div class="eye right"></div>' +
            '<div class="mouth happy"></div>' +
            '</div>' +
            '<div class="shadow scale"></div>' +
            '<div class="message"><h1 class="alert">Awesome!</h1><p>' +
            message +
            '</p>' +
            '<p> Your answer was: ' +
            ($('.rangeslider__handle')[0].textContent) + "%" +
            '</p>' +
            '<p> The herbivory percentage is: ' +
            data.CorrectPercent + "%" +
            '</p></div>' +
            '</div>';
    }


    swalWithBootstrapButtons.fire({
        html: html,
        showCloseButton: false,
        showCancelButton: false,
        allowOutsideClick: false,
        confirmButtonText: 'Next image',
        confirmButtonAriaLabel: 'Next image'

    });
}