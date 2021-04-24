const $element = $('input[type="range"]');

$(document).ready(function () {
    const $element = $('input[type="range"]');

    const $tooltip = $('#range-tooltip');
    const sliderStates = [
        { name: "low", tooltip: "Signs of herbivory are low", range: _.range(0, 26) },
        { name: "med", tooltip: "Signs of herbivory are medium.", range: _.range(26, 51) },
        { name: "high", tooltip: "Signs of herbivory are high", range: _.range(51, 100) }
    ];
    var currentState;
    var $handle;
    $element
        .rangeslider({
            polyfill: false,
            onInit: function () {
                $handle = $('.rangeslider__handle', this.$range);
                updateHandle($handle[0], this.value);
                updateState($handle[0], this.value);
            }
        })
        .on('input',
            function () {
                updateHandle($handle[0], this.value);
                checkState($handle[0], this.value);
            });

    // Update the value inside the slider handle
    function updateHandle(el, val) {
        el.textContent = val;
    }

    // Check if the slider state has changed
    function checkState(el, val) {
        // if the value does not fall in the range of the current state, update that shit.
        if (!_.contains(currentState.range, parseInt(val))) {
            updateState(el, val);
        }
    }

    // Change the state of the slider
    function updateState(el, val) {
        for (var j = 0; j < sliderStates.length; j++) {
            if (_.contains(sliderStates[j].range, parseInt(val))) {
                currentState = sliderStates[j];
                // updateSlider();
            }
        }

        // Update handle color
        $handle
            .removeClass(function (index, css) {
                return (css.match(/(^|\s)js-\S+/g) || []).join(' ');
            })
            .addClass("js-" + currentState.name);
        // Update tooltip
        $tooltip.html(currentState.tooltip);
    }
});