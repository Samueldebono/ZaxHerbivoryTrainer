
function GreetingsMessage(finished, phase) {
    const swalConsent = Swal.mixin({
        customClass: {
            confirmButton: 'myButton'
        },
        buttonsStyling: false
    });

    var html = "<div>" +
        "<img src='/images/Asset4.png' class='instuction-img' >" +
        "<h2>Thank you for completing our trainer!</h2>" +
        "<p>If you would like to claim your $20 Woolworths E-giftcard, please submit your email address into the textbox (make sure there are no typos!)." +
        "<br> Please allow 48 hours for the E-giftcard to arrive in your inbox.<br><br> If you have any issues, contact Zoe at <a href='mailto:z.xirocostas@unsw.edu.au'>z.xirocostas@unsw.edu.au</a></p></div > ";

    if (finished === 1) {
        html = "<div>" +
            "<img src = '/images/Asset3.png' class= 'instuction-img'>" +
            "<h2>Thank you for participating in our study!</h2>" +
            "<p>This app works in 3 phases and shouldn't take longer than 30 minutes to completely finish.<br><br>" +
            "A pop-up box will appear at the start of each phase to explain what to do.<br><br>" +
            "When all three phases are complete, you will be prompted to submit your email address to claim your $20" +
            " Woolworths E-giftcard reward.</p></div>";


        swalConsent.fire({
            html: html,
            showCancelButton: false,
            customClass: 'swal-wide-3',
            confirmButtonText: 'OK'
        }).then((result) => {
            if (result.value) {
                PhaseMessage(phase);
            }
        });
    } else {
        {
            swalConsent.fire({
                html: html,
                showCancelButton: false,
                customClass: 'swal-wide-3',
                confirmButtonText: 'OK'
            }).then((result) => {
                if (result.value) {
                }
            });
        }
    }
}