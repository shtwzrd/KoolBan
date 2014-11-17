function ColumnViewModel() {
    var self = this;
    self.columnId = ko.observable();
    self.title = ko.observable('');
    self.capacity = ko.observable(0);
    self.priority = ko.observable(1);
    self.status = ko.observable('add');

    self.addNewColumn = function () {
        self.status('add');
        self.title('');
        self.capacity(0);
        self.columnId(null);
        // New Columns should default to the middle of the Project
        if (app.dataModel.project) { //Possible to get here before we load initial JSON
            self.priority(Math.floor(app.dataModel.project.ColumnsOrdered().length / 2));
        } else { //If project is null, subscribe to dataModel and grab this data when it becomes available
            app.dataModel.subscribe(function () {
                self.priority(Math.floor(app.dataModel.project.ColumnsOrdered().length / 2));
            });
        }
        app.Views.Modal.columnModal();
    }

    self.editExistingColumn = function (id) {
        self.status('edit');
        self.columnId(id);

        app.dataModel.readColumn(self.columnId(), function (data) {
            self.title(data.ColumnName);
            self.capacity(data.Capacity);
            self.priority(data.Priority);
            app.Views.Modal.columnModal();
        });
    }

    self.delete = function () {
        app.dataModel.deleteColumn(self.columnId(), self.close);
    }

    self.submit = function () {
        var newColumnModel = {
            ColumnName: self.title(),
            Priority: self.priority(),
            Capacity: self.capacity(),
        }
        if (self.columnId() != null) {
            newColumnModel["ColumnId"] = self.columnId();
            app.dataModel.updateColumn(newColumnModel, self.close);
        } else {
            app.dataModel.createColumn(newColumnModel, self.close);
        }
    }

    self.close = function () {
        app.Views.Modal.hide();
    }
}
app.addViewModel({
    name: "Column",
    bindingMemberName: "column",
    factory: ColumnViewModel
});