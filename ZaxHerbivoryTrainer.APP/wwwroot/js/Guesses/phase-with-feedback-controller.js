
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

$.removeCookie = function (key, options) {
    if ($.cookie(key) === undefined) {
        return false;
    }
    // Must not alter options, thus extending a fresh object...
    $.cookie(key, '', $.extend({}, options, { expires: -1 }));
    return !$.cookie(key);
};


const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: 'myButton'
    },
    buttonsStyling: false
});
$(document).ready(function () {

    timer = window.setInterval(function () {
        secondsoverall = Math.round((new Date - startoverall) / 1000, 0);
        $('#Timer').val(secondsoverall);
    }, 1000);

    $('#btnGuess-finished').click(function () {
        $.removeCookie('userHash', { path: '/' });
    });

    $('#btnGuess').click(function () {
        $.ajax({
            url: guessCheckUrl,
            type: 'POST',
            data: {
                imageId: $('#CurrentImageId').val(),
                guessPercent: $('.rangeslider__handle')[0].textContent,
                userId: modelId,
                currentTime: $('#Timer').val(),
                phase: phase
            },
            success: function (data) {

                $('#finalResult').val(data.Total);

                if (data.Count >= 10) {
                    $('#accuracyArea').removeClass("hide");
                    $('#accuracyResult').removeClass("hide");
                    $('#actIcon').removeClass("hide");
                    $('#accuracyResult').text(data.Total + "% accuracy from the last 10 images");
                }
                //if reach percentage or time limit
                if (data.Total > finishPercentage) {
                    var reason = "final percentage";



                    swalWithBootstrapButtons.fire({
                        html:
                            '<div id="success-box"> ' +
                            '<div class="face">' +
                            '<div class="eye"></div>' +
                            '<div class="eye right"></div>' +
                            '<div class="mouth happy"></div>' +
                            '</div>' +
                            '<div class="shadow scale"></div>' +
                            '<div class="message"><h1 class="alert">Well Done!</h1><p>You have reached the ' + reason + ', click finished to see your results! </p>' +
                            '<p>Your average accuracy for this phase was ' + data.Total + '%</p></div>' +
                            '</div>',
                        showCloseButton: false,
                        showCancelButton: false,
                        allowOutsideClick: false,
                        confirmButtonText: 'Done',
                        confirmButtonAriaLabel: 'Done'
                    });
                    window.clearInterval(timer);
                    $('#btnGuess-finished').removeClass("hide");
                    $('#btnGuess').addClass("hide");
                    $('#FinalPercentage').val(data.Total);
                    $('#ImagesUsed').val(usedImages.length);

                }
                else {
                    if (data.Answer) {

                        var idMessageBox = '<div id="almost-box">';
                        var title = '<h1 class="alert">So close!</h1>';
                        var message = "That's close enough to be correct!";

                        if (data.CorrectPercent == ($('.rangeslider__handle')[0].textContent)) {
                            message = "PERFECT percentage guess!!";
                            idMessageBox = '<div id="success-box">';
                            title = '<h1 class="alert">Awesome!</h1>';
                        }
                        swalWithBootstrapButtons.fire({
                            html:
                                idMessageBox +
                                '<div class="face">' +
                                '<div class="eye"></div>' +
                                '<div class="eye right"></div>' +
                                '<div class="mouth happy"></div>' +
                                '</div>' +
                                '<div class="shadow scale"></div>' +
                                '<div class="message">' + title + '<p>' +
                                message +
                                '</p>' +
                                '<p> Your answer was: ' +
                                ($('.rangeslider__handle')[0].textContent) + "%" +
                                '</p>' +
                                '<p> The herbivory percentage is: ' +
                                data.CorrectPercent + "%" +
                                '</p></div>' +
                                '</div>',
                            showCloseButton: false,
                            showCancelButton: false,
                            allowOutsideClick: false,
                            confirmButtonText: 'Next image',
                            confirmButtonAriaLabel: 'Next image'
                        });


                    } else {
                        swalWithBootstrapButtons.fire({
                            html:
                                '<div id="error-box">' +
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
                                '</div>',
                            showCloseButton: false,
                            showCancelButton: false,
                            allowOutsideClick: false,
                            confirmButtonText: 'Next image',
                            confirmButtonAriaLabel: 'Next image'

                        });
                    }
                }
                $.ajax({
                    url: getNewImageUrl,
                    type: 'POST',
                    data:
                        { preImageId: usedImages } //array
                    ,
                    success: function (data) {
                        usedImages.push(data.ImageId);
                        ResetSlider();
                        $('#CurrentImageId').val(data.ImageId);
                        $('#GuessResult').val(0);
                        $('#image_Id').attr("src", data.ImageUrl);
                    }
                });
            }
        });
    });
});

function ResetSlider() {
    $('.rangeslider__handle').text(0);
    $('.rangeslider__handle').css("left", 0);
    $('.rangeslider__handle').removeClass("js-med");
    $('.rangeslider__handle').removeClass("js-high");
    $('.rangeslider__handle').addClass("js-low");
    $('.rangeslider__tooltip').text("Signs of herbivory are low");
}