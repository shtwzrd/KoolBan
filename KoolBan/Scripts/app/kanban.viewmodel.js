function KanbanViewModel() {
    var self = this;
    self.projectId = $('#projectId')[0].value;
    self.columns = ko.observableArray([ {ColumnName: "", Notes: [], Capacity: 0 }]);

    app.dataModel.projectId = self.projectId;

    self.handleColumnData = function (result) {
        var obs = ko.utils.arrayMap(result, function (item) {
            return ko.mapping.fromJS(item);
        });

        self.columns(ko.mapping.toJS(obs));
        console.log(self.columns());
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
