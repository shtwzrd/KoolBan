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

    // Data access operations
    self.getColumns = function (callback) {

        function getData() {
            return $.ajax({
                method: 'get',
                ifModified: true,
                data: { projectId: self.projectId },
                url: '/Projects/GetProjectColumnsJson',
                contentType: "application/json; charset=utf-8",
            });
        }

        return getData().done(callback);
    }

    self.setColumns = function (message, callback) {
        function sendData() {
            return $.ajax({
                method: 'post',
                data: JSON.stringify(message),
                url: '/Projects/SetProjectColumnsJson',
                contentType: "application/json; charset=utf-8",
            });
        }

        return sendData().done(callback);
    }

};

