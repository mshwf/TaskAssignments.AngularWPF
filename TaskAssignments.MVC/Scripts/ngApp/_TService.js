tapp.service('tService', function ($http) {
    this.getAll = function () {
        $http.get('http://localhost:60142/api/Tasks');  
    }

})