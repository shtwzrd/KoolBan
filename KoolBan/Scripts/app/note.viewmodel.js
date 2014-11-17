function NoteViewModel() {
    var self = this;
    self.noteId = ko.observable();
    self.description = ko.observable('');
    self.logo = ko.observable('empty');
    self.color = ko.observable('Green');
    self.columnId = ko.observable();
    self.status = ko.observable('add');

    self.addNewNote = function () {
        self.status = ko.observable('add');
        self.description('');
        self.logo('empty');
        self.color('Green');
        self.noteId(null);
        // New Notes should default to the first Column in the Project
        self.columnId(app.dataModel.project.ColumnsOrdered()[0].ColumnId());
        app.Views.Modal.noteModal();
    }

    self.editExistingNote = function (id) {
        self.status = ko.observable('edit');
        self.noteId(id);

        app.dataModel.readNote(self.noteId(), function (data) {
            self.description(data.Description);
            self.logo(data.Logo);
            self.color(data.Color);
            self.columnId(data.ColumnId);
            app.Views.Modal.noteModal();
        });
    }

    self.delete = function () {
        app.dataModel.deleteNote(self.noteId(), close);
    }

    self.submit = function () {
        var newNoteModel = {
            Description: self.description(),
            Logo: self.logo(),
            Color: self.color(),
            ColumnId: self.columnId(),
        }
        if (self.noteId() != null) {
            newNoteModel["NoteId"] = self.noteId();
            app.dataModel.updateNote(newNoteModel, close);
        } else {
            console.log(newNoteModel);
            app.dataModel.createNote(newNoteModel, close);
        }

    }

    function close() {
        setTimeout(
            app.Views.Modal.hide(),
            2000);
    }
}
app.addViewModel({
    name: "Note",
    bindingMemberName: "note",
    factory: NoteViewModel
});