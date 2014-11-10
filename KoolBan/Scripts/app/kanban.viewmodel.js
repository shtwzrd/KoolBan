function KanbanViewModel() {
    var self = this;
    self.projectId = $('#projectId')[0].value;

    alert(self.projectId);
    app.dataModel.projectId = self.projectId;

    alert(app.dataModel.getColumns());

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
