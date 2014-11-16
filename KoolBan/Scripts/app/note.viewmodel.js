function NoteViewModel() {
    var self = this;

    self.addNewNote = function () {
        app.Views.Modal.noteModal();
    }
}
app.addViewModel({
    name: "Note",
    bindingMemberName: "note",
    factory: NoteViewModel
});