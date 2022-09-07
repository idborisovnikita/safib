$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/app")
        .build();

    connection.start().then(function () {
        viewModel.refresh();
        setInterval(function () {
            if (connection.state == "Connected") {
                viewModel.refresh();
            }
        }, 3000);
    }).catch(function (err) {
        return console.error(err);
    });

    connection.on("update", function () {
        viewModel.refresh();
    });

    ko.bindingHandlers.prop = {
        update: function (element, valueAccessor) {
            var props = ko.toJS(valueAccessor());
            for (prop in props) {
                element[prop] = ko.unwrap(props[prop]);
            }
        }
    }

    function AppViewModel() {
        var self = this;
        self.departments = ko.observableArray([]);
        self.refresh = function () {
            fetch("/getdepartments")
                .then(response => {
                    if (response.ok) {
                        $("#service-unavailable").addClass("d-none");
                        return response.json();
                    }
                    $("#service-unavailable").removeClass("d-none");
                })
                .then(data => {
                    self.departments.removeAll();
                    var newData = ko.utils.arrayMap(data, function (department) {
                        return new ModelDepartment(department);
                    });
                    for (var i = 0; i < newData.length; i++) {
                        self.departments.push(newData[i]);
                    }
                });

        }
    };

    function ModelDepartment(department, parent) {
        var self = this;
        self.name = ko.observable(department.name);
        self.enabled = ko.observable(department.enabled);
        self.parent = parent;
        self.departments = ko.observableArray(
            department.departments ?
                ko.utils.arrayMap(department.departments, function (department) {
                    return new ModelDepartment(department, self);
                }.bind(self))
                : null
        );
    }

    var viewModel = new AppViewModel();    

    ko.applyBindings(viewModel);
});