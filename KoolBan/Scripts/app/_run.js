
//Setup routing
Path.map("#/stuff").to(function() {
    alert("poop");
});

Path.map("#/AddNote").to(function() {

});

//Entry point
(function ($) {
    app.initialize();

    ko.validation.init({ grouping: { observable: false } });
    ko.applyBindings(app);

})(jQuery);