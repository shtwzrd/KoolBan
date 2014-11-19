function KanbanViewModel() {

    var self = this;
    app.dataModel.projectId = $('#projectId')[0].value;

    // Behavior

    self.onDropColumn = function (source, target) {
        var sourceId = source.id.slice(1);
        var targetId = target.id.slice(1);

        if (sourceId == targetId) {
            //user dropped column into the same spot -- cancel
            return;
        }

        var sourceIndex = null;
        var targetIndex = null;

        var newModel = ko.viewmodel.toModel(app.dataModel.project);

        for (var i = 0; i < newModel.Columns.length; i++) {
            if (newModel.Columns[i].ColumnId == sourceId) {
                sourceIndex = i;
            }
            if (newModel.Columns[i].ColumnId == targetId) {
                targetIndex = i;
            }
        }

        var swp = newModel.Columns[sourceIndex].Priority;
        newModel.Columns[sourceIndex].Priority = newModel.Columns[targetIndex].Priority;
        newModel.Columns[targetIndex].Priority = swp;

        app.dataModel.updateMe(newModel);

        self.refreshBoard();

        app.dataModel.updateColumns([newModel.Columns[sourceIndex], newModel.Columns[targetIndex]])
    }

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
            }
        });

        $('.kanban .headanchor').draggable({
            revert: 'invalid',
            stack: "div",
            start: function (event, ui) { app.dataModel.lockUpdate = true; },
            stop: function (event, ui) { app.dataModel.lockUpdate = false; },
        });

        $('.kanban th').droppable({
            accept: '.kanban .headanchor',
            tolerance: 'pointer',
            drop: function (event, ui) {
                app.dataModel.lockUpdate = false;
                self.onDropColumn(ui.draggable[0], event.target); 
            },
        });

    };

    self.updateUI = function () {
        if (!self.lockUpdate) {
            self.refreshBoard();
        }
    }

    app.dataModel.startPolling(self.updateUI);
    app.dataModel.subscribe(self.updateUI);

}

app.addViewModel({
    name: "Home",
    bindingMemberName: "board",
    factory: KanbanViewModel
});