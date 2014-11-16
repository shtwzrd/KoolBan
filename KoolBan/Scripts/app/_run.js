
//Setup routing

Path.map("#/AddNote").to(function() {
    app.Views.Note.addNewNote();
});

Path.map("#/AddColumn").to(function() {
    app.Views.Column.addNewColumn();
});

//Entry point
(function ($) {
    app.initialize();

    ko.validation.init({ grouping: { observable: false } });
    ko.applyBindings(app);

})(jQuery);