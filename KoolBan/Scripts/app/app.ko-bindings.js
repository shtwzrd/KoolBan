ko.bindingHandlers.buttonSet = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var value = valueAccessor();
        console.log(valueAccessor());
        value($(element).find('button.active').data('value'));
        $(element).find('button').on('click', function () {
            $(element).find('button.active').removeClass('active');
            value($(this).data("value"));
            $(this).addClass('active');
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = valueAccessor();
        $(element).find('button.active').removeClass('active');
        $(element).find('button[data-value=' + value().toString() + ']').addClass('active');
    }
};