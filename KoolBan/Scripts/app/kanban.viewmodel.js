function KanbanViewModel() {
    var self = this;
    self.projectId = $('#projectId')[0].value;

    self.columns = ko.observableArray([]);
    self.loading = ko.computed(function() {
        return self.columns == [] ? true : false;
    });


    app.dataModel.projectId = self.projectId;

    self.handleColumnData = function(result) {
        var obs = ko.utils.arrayMap(result, function(item) {
            return ko.mapping.fromJS(item, mapping);
        });

        self.columns(ko.mapping.toJS(obs, mapping));
        console.log(self.columns());

        self.columns()[0].Notes.pop();
        console.log(self.columns()[0]);

        /* Tests removing a note 
        app.dataModel.setColumns({
            Columns: self.columns(),
            ProjectId: self.projectId
        },
        hollabackgurl);
        */
    }

    app.dataModel.getColumns(self.handleColumnData);

    var Column = function (data) {
        ko.mapping.fromJS(data, mapping, this);
    }

    /*
    var hollabackgurl = function(back) {
        alert(back.toString());
        console.log(back);
    } 
    */

    var mapping = {
        create: function (options) {
            options.data.AutoSortedNotes = ko.computed(function() {
                return options.data.Notes.sort(function(a, b) {
                     return b.Description.length - a.Description.length;
                });
            });
            var column = new Column(options.data);

            return column;
        },
        'Notes': {
            create: function (options) {
                options.data.NoteClass = ko.computed(function () {
                    var markup = "tile ";
                    if (options.data.Description.length > 40) {
                        markup += "double ";
                    }
                    if (options.data.Description.length > 106) {
                        markup += "double-vertical ";
                    }
                    markup += "bg-dark" + options.data.Color;
                    markup += " main";

                    return markup;
                });
                options.data.NoteLogo = ko.computed(function() {
                    var logo = "glyphicon glyphicon-";
                    logo += options.data.Logo;
                    return logo;
                });
                return ko.mapping.fromJS(options.data);
            }
        }
    };


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
