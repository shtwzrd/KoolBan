function AppDataModel() {
    var self = this;
    self.projectId = null;
    self.project = null;
    self.Loading = ko.observable(true);
    self.lockUpdate = false;
    self.subscribers = [];

    self.subscribe = function (callback) {
        self.subscribers.push(callback);
    }

    // Operations
    self.startPolling = function (callback) {
        if (self.Loading() == true) {
            $.ajax({
                url: "/Projects/ReadProject",
                data: { projectId: self.projectId },
                dataType: "json",
                success: function (data) {
                    self.project = ko.viewmodel.fromModel(data, options);
                    self.Loading(false);
                }
            });
        }
        callback();

        function poll() {
            setTimeout(function () {
                $.ajax({
                    url: "/Projects/ReadProject",
                    data: { projectId: self.projectId },
                    dataType: "json",
                    success: function (data) {
                        self.updateMe(data);
                        self.subscribers.forEach(function(notify) {
                            notify();
                        });
                        //Setup the next poll recursively
                        poll();
                    }
                });
            }, 3000);
        }

        poll();
    }

    // Data

    // Data access operations --> Projects
    self.createProject = function (message, callback) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: JSON.stringify(message),
                url: '/Projects/CreateProject',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    self.updateProject = function (message, callback) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: JSON.stringify(message),
                url: '/Projects/UpdateProject',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    // Data access operations --> Columns
    self.createColumn = function (message, callback) {
        message["ProjectId"] = self.projectId;
        function sendData() {
            return $.ajax({
                method: 'post',
                data: JSON.stringify(message),
                url: '/Columns/CreateColumn',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    self.readColumn = function (message, callback) {
        function getData() {
            return $.ajax({
                method: 'get',
                ifModified: true,
                data: { ColumnId: message },
                url: '/Columns/ReadColumn',
                contentType: "application/json; charset=utf-8",
            });
        }

        return getData().done(callback);
    }

    self.updateColumn = function (message, callback) {
        message["ProjectId"] = self.projectId;
        function sendData() {
            return $.ajax({
                method: 'post',
                data: JSON.stringify(message),
                url: '/Columns/UpdateColumn',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    self.deleteColumn = function (message, callback) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: JSON.stringify({ ColumnId: message }),
                url: '/Columns/DeleteColumn',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    // Data access operations --> Notes
    self.createNote = function (message, callback) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: JSON.stringify(message),
                url: '/Notes/CreateNote',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    self.readNote = function (message, callback) {
        function getData() {
            return $.ajax({
                method: 'get',
                data: { NoteId: message },
                url: '/Notes/ReadNote',
                contentType: "application/json; charset=utf-8",
            });
        }

        return getData().done(callback);
    }

    self.updateNote = function (message, callback) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: JSON.stringify(message),
                url: '/Notes/UpdateNote',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    self.deleteNote = function (message, callback) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: JSON.stringify({ noteId : message }),
                url: '/Notes/DeleteNote',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    self.updateMe = function (model) {
        if (!self.lockUpdate) {
            ko.viewmodel.updateFromModel(self.project, model);
        }
    }


    /*  Mapping the model to an observable */
    var options = {
        extend: {
            "{root}": function (proj) {
                proj.ColumnsOrdered = ko.computed(function () {
                    return proj.Columns().sort(function (a, b) {
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
                column.EditLink = ko.computed(function () {
                    return "#/EditColumn/" + column.ColumnId();
                });
                return column;
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
                note.EditLink = ko.computed(function () {
                    return "#/EditNote/" + note.NoteId();
                });
                return note;
            }
        }
    };

};