

function PhaseMessage(phase) {
    const swalConsent = Swal.mixin({
        customClass: {
            confirmButton: 'myButton'
        },
        buttonsStyling: false
    });

    var html = "<div>" +
        "<img src='/images/Asset1.png' class='instuction-img' >" +
        "<h2>Phase 1 of 3</h2>" +
        "<p>You will now estimate the percentage of leaf damage on 50 leaf images. " +
        "You will receive no feedback on how accurate your individual estimates are.<br><br>" +
        "Happy estimating!</p></div>";

    if (phase === '1') {
        html = "<div>" +
            "<img src = '/images/Asset2.png' class= 'instuction-img'>" +
            "<h2>Phase 2 of 3</h2>" +
            "<p>You will now estimate the percentage of leaf damage on leaf images until you achieve a running " +
            "average of greater or equal to 98% accuracy on your " +
            "10 most recent estimates. You will receive feedback on how accurate your individual estimates are.<br><br>" +
            "Happy training!</p></div>";
    }
    else if (phase === '2') {
        html = "<div>" +
            "<img src = '/images/Asset3.png' class= 'instuction-img'>" +
            "<h2>Phase 3 of 3</h2>" +
            "<p>You will now estimate the percentage of leaf damage on 50 leaf images.... again! " +
            "You will receive no feedback on how accurate your individual estimates are.<br><br>" +
            "Happy estimating!</p></div>";
    }

    swalConsent.fire({
        html: html,
        showCancelButton: false,
        customClass: 'swal-wide-3',
        confirmButtonText: 'NEXT'
    }).then((result) => {
        if (result.value) {

        }
    });
}