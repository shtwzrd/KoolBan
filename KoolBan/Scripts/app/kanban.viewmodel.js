function KanbanViewModel() {
    var self = this;
    self.projectId = $('#projectId')[0].value;
    self.columns = [ {ColumnName: "Fake" }];

    self.people = ko.observableArray([{ name: 'Franklin', credits: 250 }, { name: 'Mario', credits: 5800 }]);

    app.dataModel.projectId = self.projectId;

    self.handleColumnData = function (result) {
        self.columns = ko.utils.arrayMap(result, function (item) {
            return ko.mapping.fromJS(item);
        });
        console.log(result);
    }

    app.dataModel.getColumns(self.handleColumnData);

    $('.tile.main').draggable({
        revert: 'invalid',
        snap: '.kanban td',
        stack: "div",
    });

    $('.kanban td').droppable({
        accept: '.tile.main',
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
