$(function () {
    app.initialize();

    ko.validation.init({ grouping: { observable: false } });
    ko.applyBindings(app);

    //provides basic '#' navigation
    //run this function after the initialization of the
    //default knockout code.
    Sammy(function () {
        //"#:view" is the parameter's name of the data after the hash tag 
        //it is stored in "this.params.view"
        this.get('#:view', function () {
            //call the navigation functions
            app["_navigateTo" + this.params.view]();
        });
    }).run("#Home");//Specify the starting page of your application
});