
//Setup routing
Path.map("#/stuff").to(function() {
    alert("poop");
});

//Entry point
(function ($) {
    app.initialize();

    ko.validation.init({ grouping: { observable: false } });
    ko.applyBindings(app);

})(jQuery);