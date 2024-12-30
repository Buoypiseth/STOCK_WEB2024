function round(value, exp) {
    if (typeof exp === 'undefined' || +exp === 0)
        return Math.round(value);

    value = +value;
    exp = +exp;

    if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0))
        return NaN;

    // Shift
    value = value.toString().split('e');
    value = Math.round(+(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp)));

    // Shift back
    value = value.toString().split('e');
    return +(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp));
}

function spinner(e, status = 'loading', cl = '.spinner') {
    var _spinner = e.find(cl);
    if (status == 'loading') {
        e.attr("disabled", true);
        _spinner.find('i').addClass('d-none');
        _spinner.find('span').removeClass('d-none');
        return true;
    }
    e.attr("disabled", false);
    _spinner.find('i').removeClass('d-none');
    _spinner.find('span').addClass('d-none');
    //var _html = '<span class="spinner-border spinner-border-sm" role="status"></span>';
    //_spinner.html(_html);
}