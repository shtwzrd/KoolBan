ModalViewModel = function () {
        var _self = this;
    _self.mode = ko.observable("hidden");
    _self.container = $(document.documentElement);

    _self.noteModal = function () {
        if (_self.mode() != "note") {
            _self.mode("note");
            $('#addNoteModal').attr('class', 'win8modal');
            _self.show();
        }
    };

    _self.columnModal = function () {
        if (_self.mode() != "column") {
            _self.mode("column");
            $('#addColumnModal').attr('class', 'win8modal');
            _self.show();
        }
    };

    _self.projectModal = function () {
        if (_self.mode() != "project") {
            _self.mode("project");
            $('#projectModal').attr('class', 'win8modal');
            _self.show();
        }
    };

    _self.show = function () {
        setTimeout(function () {
            _self.container.addClass('win8modal-active');
        }, 0);
    };

    _self.hide = function () {
        $('.win8modal').attr('class', 'win8modal-hide');
        _self.container.removeClass('win8modal-active');
        _self.mode('hidden');
        app.cleanUpLocation();
    }


}
app.addViewModel({
    name: "Modal",
    bindingMemberName: "modal",
    factory: ModalViewModel
});
