function KanbanViewModel() {
    var self = this;
    self.poop = ko.observable("Poop");
   /* 
    Sammy(function () {
        this.get('#home', function () {
            // Make a call to the protected Web API by passing in a Bearer Authorization Header
            $.ajax({
                method: 'get',
                url: app.dataModel.userInfoUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function (data) {
                    self.myHometown('Your Hometown is : ' + data.hometown);
                }
            });
        });
        this.get('/', function () { this.app.runRoute('get', '#home'); });
    });
*/
        $('.tile').draggable({
            revert: 'invalid'
        });
        $('td').droppable({
            accept: '.tile',
            tolerance: 'pointer',
            drop: function (event, ui) {
                alert('inside');
            },
            over: function (event, ui) {
                $('#log').text('over');
            },
            out: function (event, ui) {
                $('#log').text('out');
            }
        });

    return self;

}

app.addViewModel({
    name: "Home",
    bindingMemberName: "board",
    factory: KanbanViewModel
});
