function KanbanViewModel() {

    var self = this;
    self.projectId = $('#projectId')[0].value;
    self.model = null;
    self.Loading = ko.observable(true);

    self.handleInit = function (result) {

        self.model = ko.viewmodel.fromModel(result, options);
        self.Loading(false);

        self.refreshBoard();
    }

    // Get the initial project Json from the server 
    app.dataModel.projectId = self.projectId;
    app.dataModel.readProject(self.handleInit);


    // Behavior


    self.onDropNote = function (note, column) {
        var noteId = note.id.slice(1);
        var columnId = column.id.slice(1);
        var formerColumnIndex = null;
        var newColumnIndex = null;
        var noteIndex = null;

        var newModel = ko.viewmodel.toModel(self.model);

        for (var i = 0; i < newModel.Columns.length; i++) {
            for (var j = 0; j < newModel.Columns[i].Notes.length; j++) {
                if (newModel.Columns[i].Notes[j].NoteId == noteId) {
                    formerColumnIndex = i;
                    noteIndex = j;
                }
                if (newModel.Columns[i].ColumnId == columnId) {
                    newColumnIndex = i;
                }
            }
        }

        if (newModel.Columns[newColumnIndex].Capacity == 0 ||
            newModel.Columns[newColumnIndex].Notes.length < newModel.Columns[newColumnIndex].Capacity) {
            var noteModel = newModel.Columns[formerColumnIndex].Notes.splice(noteIndex, 1)[0];
            newModel.Columns[newColumnIndex].Notes.push(noteModel);

            ko.viewmodel.updateFromModel(self.model, newModel);
            self.refreshBoard();

            return true;
        }

        return false;
    }

    self.refreshBoard = function () {
        $('.tile.main').draggable({
            revert: 'invalid',
            stack: "div",
        });

        $('.kanban td').droppable({
            accept: '.tile.main',
            tolerance: 'pointer',
            drop: function (event, ui) {
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

    /*  Mapping the model to an observable */
    var options = {
        extend: {
            "{root}": function (model) {
                model.ColumnsOrdered = ko.computed(function () {
                    return model.Columns().sort(function (a, b) {
                        return a.Priority() - b.Priority();
                    });
                });
            },
            "{root}.Columns[i]": function (column) {
                column.AutoSortedNotes = ko.computed(function () {
                    return column.Notes().sort(function (a, b) {
                        return b.Description().length - a.Description().length;
                    });
                });
            },
            "{root}.Columns[i].Notes[i]": function (note) {
                note.NoteClass = ko.computed(function () {
                    var markup = "tile ";
                    if (note.Description().length > 40) {
                        markup += "double ";
                    }
                    if (note.Description().length > 106) {
                        markup += "double-vertical ";
                    }
                    markup += "bg-dark" + note.Color();
                    markup += " main";

                    return markup;
                });
                note.NoteLogo = ko.computed(function () {
                    var logo = "glyphicon glyphicon-";
                    logo += note.Logo();
                    return logo;
                });
                return note;
            }
        }
    };

}

app.addViewModel({
    name: "Home",
    bindingMemberName: "board",
    factory: KanbanViewModel
});