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
    self.getColumns = function () {

        function getData() {
            return $.ajax({
                method: 'get',
                ifModified: true,
                data: { projectId: self.projectId },
                url: '/Projects/GetProjectColumnsJson',
                contentType: "application/json; charset=utf-8",
            });
        }

        return getData().done(self.handleData);
    }

    self.handleColumnData = function (result) {
        if (result != null) {
            self.projectColumns = result;
        }
        return self.projectColumns;
    }
};

