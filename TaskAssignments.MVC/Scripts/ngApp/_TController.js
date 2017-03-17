tapp.controller('tCtrlr', function ($scope, $http, $mdDialog) {

    function load() {
        $http.get('/api/Tasks').then(function (response) {
            $scope.tasks = response.data;
            $scope.users = [];
        });
    }
    load();

    $scope.saveTask = function (newTask) {
        $http.post('/api/Tasks', newTask).then(function (response) {

            if (response.data) {
                load();
                //$scope.users = newTask._Users;
                //$scope.taskTitle = newTask.Title;
                $mdDialog.hide();
            }
        })
    }
    //function Dialog Controller
    function DialogController($scope, $mdDialog) {
        $scope.hide = function () {
            $mdDialog.hide();
        };
        $scope.cancel = function () {
            $mdDialog.cancel();

        };
    }
    function getAllUsers() {
        $http.get('/api/Users').then(function (response) {
            $scope.allUsers = response.data;
        });
    }
    $scope.showDialog = function (task) {
        getAllUsers();
        console.log("task dialog");
        console.log(task);
        var tsk = angular.copy(task);
        $scope.taskToEdit = tsk;
        $scope.taskToEdit.DueDate = new Date(tsk.DueDate);
        $mdDialog.show({
            controller: DialogController,
            contentElement: '#taskEdit',
            parent: angular.element(document.body),
            clickOutsideToClose: true
        });
    };
    $scope.deleteTask = function (id) {

        $http.delete('/api/Tasks/' + id).then(function (response) {
            if (response.data) {
                console.log("The task has been deleted!");
                load();
            }
        })
    }
    $scope.confirmDelete = function (ev, id) {
        var confirm = $mdDialog.confirm()
              .title('Would you like to delete this task?')
              .ariaLabel('Lucky day')
              .targetEvent(ev)
              .ok('Yes!')
              .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.deleteTask(id);
        }, function () {
        });
    };

    $scope.showUsers = function (idx) {
        $scope.tgl = !$scope.tgl;
        if ($scope.tgl) {
            console.log("get users");
            $scope.users = $scope.tasks[idx]._Users;
            $scope.taskTitle = $scope.tasks[idx].Title;
        }
    }
    $http.get('/api/Tasks/MyTasks').then(function (response) {
        console.log("response.data my tasks")
        console.log(response.data)
        $scope.myTasks = response.data.data;
        if ($scope.myTasks == "admin")
            $scope.youAreAdmin = true;
    })
});

tapp.run(['$rootScope', '$route', function ($rootScope, $route) {
    $rootScope.$on('$routeChangeSuccess', function () {
        document.title = $route.current.title;
    });
}]);
tapp.config(function ($routeProvider) {
    $routeProvider.when('/', {
        title: 'Home',
        controller: 'tCtrlr',
        templateUrl: '../HTML/Index.html',
    }),
    $routeProvider.when('/Tasks', {
        title: 'Tasks',
        controller: 'tCtrlr',
        templateUrl: '../HTML/Tasks.html',
    }),
    $routeProvider.when('/MyTasks', {
        title: 'My Tasks',
        controller: 'tCtrlr',
        templateUrl: '../HTML/MyTasks.html',
    })
})