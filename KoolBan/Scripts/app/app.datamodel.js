function AppDataModel() {
    var self = this;
    // Routes
    self.siteUrl = "/";
    self.projectId = null;
    self.projectColumns = null;

    // Route operations

    // Other private operations

    // Operations
    self.startPolling = function(callback) {

        (function poll() {
            setTimeout(function() {
                $.ajax({
                    url: "/Projects/ReadProject",
                    data: { projectId: self.projectId },
                    success: function(data) {
                        callback(data);
                        //Setup the next poll recursively
                        poll();
                    },
                    dataType: "json"
                });
            }, 3000);
        })();
    }

    // Data

    // Data access operations --> Projects
    self.createProject = function (message, callback) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: message,
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

    self.updateProject = function (message, callback) {
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
                data: message,
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
                data: message,
                url: '/Columns/ReadColumn',
                contentType: "application/json; charset=utf-8",
            });
        }

        return getData().done(callback);
    }

    self.updateColumn = function (message, callback) {
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
                data: message,
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
                url: '/Notes/CreateNotes',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

    self.readNote = function (message, callback) {
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
                data: message,
                url: '/Notes/DeleteNote',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

};