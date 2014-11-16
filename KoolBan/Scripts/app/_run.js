
//Setup routing

Path.map("#/AddNote").to(function() {
    app.Views.Note.addNewNote();
});

Path.map("#/AddColumn").to(function() {
    app.Views.Column.addNewColumn();
});

Path.map("#/EditNote/:NoteId").to(function() {
    app.Views.Note.editExistingNote(this.params["NoteId"]);
});

//Entry point
(function ($) {
    app.initialize();

    ko.validation.init({ grouping: { observable: false } });
    ko.applyBindings(app);

})(jQuery);