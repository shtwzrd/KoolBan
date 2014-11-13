function KanbanViewModel() {
    var self = this;
    self.projectId = $('#projectId')[0].value;
    self.model = ko.observable({
        Columns: ko.observableArray([]),
    });

    self.Loading = ko.observable(true);

    app.dataModel.projectId = self.projectId;

    self.handleColumnData = function (result) {

        self.model = ko.viewmodel.fromModel(result, options);
        self.Loading(false);
        self.Loading.notifySubscribers();

        self.model.Columns()[0].Notes.pop();

        /* Tests removing a note  
        app.dataModel.setColumns({
            Columns: self.columns(),
            ProjectId: self.projectId
        },
        hollabackgurl);
        */
        app.refreshUI();
    }

    app.dataModel.readProject(self.handleColumnData);

    /*
    var hollabackgurl = function(back) {
        alert(back.toString());
        console.log(back);
    } */

    var options = {
        extend: {
            "{root}": function(model) {
                model.ColumnsOrdered = ko.computed(function() {
                    return model.Columns().sort(function(a, b) {
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