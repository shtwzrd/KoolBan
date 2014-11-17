function ProjectViewModel() {
    var self = this;
    self.name = ko.observable('');
    self.status = ko.observable('add');

    self.addNewProject = function () {
        self.status('add');
        self.name('');
        
        app.Views.Modal.projectModal();
    }

    self.submit = function () {
        self.name(self.name().toLowerCase()
            .replace(/ /g, '-')
            .replace(/[^\w-]+/g, ''));

        var newProject = {
            ProjectId: self.name(),
        }
        self.status("created");
        app.dataModel.createProject(newProject, self.close());
    }

    self.close = function () {
        app.Views.Modal.hide();
        if (self.status() == "created") {
            window.location.href = self.name();
        }
    }
}
app.addViewModel({
    name: "Project",
    bindingMemberName: "project",
    factory: ProjectViewModel
});