function ColumnViewModel() {
    var self = this;

    self.addNewColumn = function () {
        app.Views.Modal.columnModal();
    }
}
app.addViewModel({
    name: "Column",
    bindingMemberName: "column",
    factory: ColumnViewModel
});