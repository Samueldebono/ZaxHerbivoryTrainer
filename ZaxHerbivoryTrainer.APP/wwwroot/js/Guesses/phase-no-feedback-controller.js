
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

    var startoverall = new Date;
    

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

                $('#imageCount').text(data.Count + "/50 image estimations");

                    //if reach percentage or time limit
                if (data.Count >= finishingImageNumber) {
                    

                    swalWithBootstrapButtons.fire({
                        html:
                            '<div id="success-box"> ' +
                            '<div class="face">' +
                            '<div class="eye"></div>' +
                            '<div class="eye right"></div>' +
                            '<div class="mouth happy"></div>' +
                            '</div>' +
                            '<div class="shadow scale"></div>' +
                            '<div class="message"><h1 class="alert">Well Done!</h1><p>Your average accuracy for this phase was ' + data.Total+'%</p>' +
                            '</div>' +
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
