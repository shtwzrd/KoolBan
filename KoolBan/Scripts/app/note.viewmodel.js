function NoteViewModel() {
    var self = this;
    self.noteId = ko.observable();
    self.description = ko.observable('');
    self.logo = ko.observable('empty');
    self.color = ko.observable('Green');
    self.columnId = ko.observable();
    self.status = ko.observable('add');

    self.addNewNote = function() {
        self.status('add');
        self.description('');
        self.logo('empty');
        self.color('Green');
        self.noteId(null);
        // New Notes should default to the first Column in the Project
        if (app.dataModel.project) { //Possible to get here before we load initial JSON
            self.columnId(app.dataModel.project.ColumnsOrdered()[0].ColumnId());
        } else { //If project is null, subscribe to dataModel and grab this data when it becomes available
            app.dataModel.subscribe(function() {
                self.columnId(app.dataModel.project.ColumnsOrdered()[0].ColumnId());
            });
        }
        app.Views.Modal.noteModal();
    }

    self.editExistingNote = function(id) {
        self.status('edit');
        self.noteId(id);

        app.dataModel.readNote(self.noteId(), function(data) {
            self.description(data.Description);
            self.logo(data.Logo);
            self.color(data.Color);
            self.columnId(data.ColumnId);
            app.Views.Modal.noteModal();
        });
    }

    self.delete = function() {
        app.dataModel.deleteNote(self.noteId(), self.close);
    }

    self.submit = function() {
        var newNoteModel = {
            Description: self.description(),
            Logo: self.logo(),
            Color: self.color(),
            ColumnId: self.columnId(),
        }
        if (self.noteId() != null) {
            newNoteModel["NoteId"] = self.noteId();
            app.dataModel.updateNote(newNoteModel, self.close);
        } else {
            app.dataModel.createNote(newNoteModel, self.close);
        }

    }

    self.close = function() {
        app.Views.Modal.hide();
    }
}

app.addViewModel({
    name: "Note",
    bindingMemberName: "note",
    factory: NoteViewModel
});