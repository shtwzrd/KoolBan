function KanbanViewModel() {

    var self = this;
    app.dataModel.projectId = $('#projectId')[0].value;

    // Get the initial project Json from the server 



    // Behavior

    self.onDropNote = function (note, column) {
        var noteId = note.id.slice(1);
        var columnId = column.id.slice(1);
        var formerColumnIndex = null;
        var newColumnIndex = null;
        var noteIndex = null;

        var newModel = ko.viewmodel.toModel(app.dataModel.project);

        for (var i = 0; i < newModel.Columns.length; i++) {
            for (var j = 0; j < newModel.Columns[i].Notes.length; j++) {
                if (newModel.Columns[i].Notes[j].NoteId == noteId) {
                    formerColumnIndex = i;
                    noteIndex = j;
                }
            }
            if (newModel.Columns[i].ColumnId == columnId) {
                newColumnIndex = i;
            }
        }

        if (newModel.Columns[newColumnIndex].Capacity == 0 ||
            newModel.Columns[newColumnIndex].Notes.length < newModel.Columns[newColumnIndex].Capacity) {
            var noteModel = newModel.Columns[formerColumnIndex].Notes.splice(noteIndex, 1)[0];
            noteModel.ColumnId = newModel.Columns[newColumnIndex].ColumnId;
            newModel.Columns[newColumnIndex].Notes.push(noteModel);

            app.dataModel.updateMe(newModel);

            self.refreshBoard();

            app.dataModel.updateNote(noteModel);

            return true;
        }

        return false;
    }

    self.refreshBoard = function () {
        $('.tile.main').draggable({
            revert: 'invalid',
            stack: "div",
            start: function (event, ui) { app.dataModel.lockUpdate = true; },
            stop: function (event, ui) { app.dataModel.lockUpdate = false; },
        });

        $('.kanban td').droppable({
            accept: '.tile.main',
            tolerance: 'pointer',
            drop: function (event, ui) {
                app.dataModel.lockUpdate = false;
                if (!self.onDropNote(ui.draggable[0], event.target)) {
                    ui.draggable.draggable('option', 'revert', true);
                }
            },
            over: function (event, ui) {
                $('#log').text('over');
            },
            out: function (event, ui) {
                $('#log').text('out');
            }
        });

    };

    self.updateUI = function () {
        if (!self.lockUpdate) {
            self.refreshBoard();
        }
    }

    app.dataModel.startPolling(self.updateUI);

}

app.addViewModel({
    name: "Home",
    bindingMemberName: "board",
    factory: KanbanViewModel
});