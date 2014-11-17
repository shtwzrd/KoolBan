
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

Path.map("#/EditColumn/:ColumnId").to(function() {
    app.Views.Column.editExistingColumn(this.params["ColumnId"]);
});

//Entry point
(function ($) {
    app.initialize();

    ko.validation.init({ grouping: { observable: false } });
    ko.applyBindings(app);

})(jQuery);