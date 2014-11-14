function AppDataModel() {
    var self = this;
    // Routes
    self.siteUrl = "/";
    self.projectId = null;
    self.projectColumns = null;

    // Route operations

    // Other private operations

    // Operations

    // Data

    // Data access operations --> Projects
    self.createProject = function (callback, message) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: message ,
                url: '/Projects/CreateProject',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    self.readProject = function (callback) {
        function getData() {
            return $.ajax({
                method: 'get',
                ifModified: true,
                data: { projectId: self.projectId },
                url: '/Projects/ReadProject',
                contentType: "application/json; charset=utf-8",
            });
        }

        return getData().done(callback);
    }

    self.updateProject = function (callback, message) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: message,
                url: '/Projects/UpdateProject',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    // Data access operations --> Columns
    self.createColumn = function (callback) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: message ,
                url: '/Columns/CreateColumn',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    self.readColumn = function (callback, message) {
        function getData() {
            return $.ajax({
                method: 'get',
                ifModified: true,
                data: message,
                url: '/Columns/ReadColumn',
                contentType: "application/json; charset=utf-8",
            });
        }

        return getData().done(callback);
    }

    self.updateColumn = function (callback, message) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: message ,
                url: '/Columns/UpdateColumn',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    self.deleteColumn = function (callback, message) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: message ,
                url: '/Columns/DeleteColumn',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    // Data access operations --> Notes
    self.createNote = function (callback) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: message,
                url: '/Notes/CreatNotes',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    self.readNote = function (callback, message) {
        function getData() {
            return $.ajax({
                method: 'get',
                ifModified: true,
                data: message,
                url: '/Notes/ReadNote',
                contentType: "application/json; charset=utf-8",
            });
        }

        return getData().done(callback);
    }

    self.updateNote = function (callback, message) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: message,
                url: '/Notes/UpdateNote',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    self.deleteNote = function (callback, message) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: message,
                url: '/Notes/DeleteNote',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

};