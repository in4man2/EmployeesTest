angular.module('EmpApp', ['smart-table', 'ui.bootstrap'])
    .controller('employeeCtrl', ['$rootScope', '$scope', '$http', '$uibModal', '$log', function ($rootScope, $scope, $http, $uibModal, $log) {
      
        $ctrl = this;

        $ctrl.uibModalInstance = null;

        $ctrl.rowEmployeeCollection = [];

        $ctrl.index = function () {
            $http.get("/api/EmployeeEntities").then(function (data) {
                $ctrl.rowEmployeeCollection = data.data;

                //$ctrl.optionsTitles
                $http.get("/api/options/JobTitles").then(function (data) {
                    $ctrl.optionsTitles = data.data;
                });
            })
        };

        $ctrl.selectRow = function (row) {

            $ctrl.selectedEmployeeId = row.id;
            //selectedValues.setSelectedEmployeeId(row.id);
            $ctrl.selectedEmployeeName = row.className;


        };

        $ctrl.editRow = function(row)
        {
            
        }

           
        $ctrl.addEmployeeItem = function addEmployeeItem() {

            $ctrl.uibModalInstance = $uibModal.open({
                //animation: $ctrl.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'myModalContent.html',
                scope: $scope
              
            });
        }
        
   
        $ctrl.removeRow = function removeRow(row) {
            
          
            $ctrl.selectedEmployeeName = row.employeeName;
            $ctrl.selectedEmployeeId = row.id;

            $ctrl.uibModalInstance = $uibModal.open({
                //animation: $ctrl.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'myDeleteContent.html',
                scope: $scope
               
            });

        }

        $ctrl.index();

        $rootScope.$on('refreshEmployeees', eventFunc)

  
        function eventFunc()
        {
            $ctrl.index();
        }
       
        //modals

        $ctrl.newEmployeeName = "";
        $ctrl.newEmployeeJobTitleId = "";
        $ctrl.newEmployeeDate = "";
        $ctrl.newEmployeeRate = "";

        $ctrl.ok = function () {

            var newEmployeeName = $ctrl.newEmployeeName;
            var newEmployeeJT = Number($ctrl.newEmployeeJobTitleId == "" ? 0 : $ctrl.newEmployeeJobTitleId);
            if (newEmployeeJT == 0)
                return;
            var newEmployeeDate = $ctrl.newEmployeeDate;
            var newEmployeeRate = $ctrl.newEmployeeRate;

            var obj = { employeeName: newEmployeeName, jobTitleEntityId: newEmployeeJT, date: newEmployeeDate, rate: newEmployeeRate };
            debugger;
            //dataacess
            $http.post("/api/EmployeeEntities", obj).then(function (e) { //success


                $ctrl.newEmployeeName = "";
                $ctrl.newEmployeeJT = "";
                $ctrl.newEmployeeDate = "";
                $ctrl.newEmployeeRate = "";
                

                $rootScope.$broadcast('refreshEmployeees');
                //$ctrl.rowEmployeeCollection.push(obj);
            }, function (e) { //error
                console.log("error");
                console.log(e);
            });

            $ctrl.uibModalInstance.close(obj);
        };

       /* $ctrl.editEmployee = function () {

            var selEmployeeId = selectedValues.getSelectedEmployeeId();
            if (selEmployeeId == null)
                return;

            var newEmployeeName = $ctrl.newEmployeeName;
            var newEmployeeLocation = $ctrl.newEmployeeLocation;
            var newEmployeeTeacher = $ctrl.newEmployeeTeacher;

            var obj = { className: newEmployeeName, location: newEmployeeLocation, teacher: newEmployeeTeacher };

            //dataacess
            $http.put("/api/EmployeeEntities/" + selEmployeeId, obj).then(function (e) { //success
                selectedValues.setSelectedEmployeeId(null);
                $rootScope.$broadcast('refreshEmployeees');
                //$ctrl.rowEmployeeCollection.push(obj);
            }, function (e) { //error
                console.log("error");
                console.log(e);
                selectedValues.setSelectedEmployeeId(null);
            });

            $ctrl.uibModalInstance.close(obj);
        };*/

        
        $ctrl.removeOk = function () {

            var selEmployeeId = $ctrl.selectedEmployeeId;
            if (selEmployeeId == null)
                return;
            
            //dataacess
            var isEmployeeRemoving = true;
            
            if (isEmployeeRemoving) {
                $http.delete("/api/EmployeeEntities/" + selEmployeeId).then(function (e) { //success
                    $rootScope.$broadcast('refreshEmployeees');
                   
                    $ctrl.selectedEmployeeId = undefined;
                    //$ctrl.rowEmployeeCollection.push(obj);
                }, function (e) { //error
                    console.log("error");
                    console.log(e);
                });

            }
           
            $ctrl.uibModalInstance.close(true);
        };

        $ctrl.cancel = function () {
            $ctrl.uibModalInstance.dismiss('cancel');
            
            $ctrl.newEmployeeName = "";
            $ctrl.newEmployeeJT = "";
            $ctrl.newEmployeeDate = "";
            $ctrl.newEmployeeRate = "";

        };
    }]);
/*
angular.module('CASApp').service('selectedValues', function() {
    var selectedEmployeeId = null;

    this.setSelectedEmployeeId = function (x) {
        selectedEmployeeId = x;
    }
    
    this.getSelectedEmployeeId = function () {
        return selectedEmployeeId;
    }


   
});*/