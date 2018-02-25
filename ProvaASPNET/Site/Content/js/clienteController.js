var app = angular.module('siteApp');


//CLIENTE CONTROLLER
app.controller('clienteController', ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams) {
    $scope.Cliente = { Nome: "", Codigo: "", Cpf: "", DataNascimento: "" };

    //LISTA
    $scope.InitList = function () {
        $scope.ClientePesquisa = { Codigo: "", Nome: "", Cpf: "" }
        $scope.Lista = [];
        if ($routeParams.pagina != undefined) {
            $scope.Pagina = $routeParams.pagina;
        } else {
            $scope.Pagina = 1;
        }
        $scope.TotalRegistros = 0;
        $http({
            method: 'GET',
            url: api_url + '/clientes/lista?pagina=' + $scope.pagina
        }).then(function successCallback(response) {
            if (response.status === 200) {
                $scope.Lista = response.data.Dados;
                $scope.Pagina = response.data.Pagina;
                $scope.TotalRegistros = response.data.Total;
                $scope.TotalPaginas = Math.round($scope.TotalRegistros / 20);

            }
        },
            function errorCallback(response) {
                if (response != null && response.data != null && response.data.Message != null) {
                    $().toastmessage('showErrorToast', response.data.Message);
                }
            });
    }

    //delete
    $scope.excluir = function (id) {
        $http({
            method: 'DELETE',
            url: api_url + '/clientes/' + id
        }).then(function successCallback(response) {
            if (response.status === 200) {
                if (response.data.Message != null) {
                    $().toastmessage('showSuccessToast', response.data.Message);

                } else {
                    $().toastmessage('showSuccessToast', "Operação realziada com sucesso!");
                }
                $scope.InitList();
            }
        },
            function errorCallback(response) {
                if (response != null && response.data != null && response.data.Message != null) {
                    $().toastmessage('showErrorToast', response.data.Message);
                }
            });
    };

    //CADASTRO
    $scope.InitCadastro = function () {
        $scope.Cliente = { Nome: "", Codigo: "", Cpf: "", DataNascimento: "" };

    }

    $scope.Cadastrar = function () {
        $http({
            method: 'POST',
            data: $scope.Cliente,
            url: api_url + '/clientes/'
        }).then(function successCallback(response) {
            if (response.status === 200) {
                if (response.data.Message != null) {
                    $().toastmessage('showSuccessToast', response.data.Message);

                } else {
                    $().toastmessage('showSuccessToast', "Operação realziada com sucesso!");
                }
                $scope.InitCadastro();
            }
        },
            function errorCallback(response) {
                if (response != null && response.data != null && response.data.Message != null) {
                    $().toastmessage('showErrorToast', response.data.Message);
                }
            });
    }


    $scope.AlterarInit = function () {
        $http({
            method: 'GET',
            url: api_url + '/clientes/' + $routeParams.id
        }).then(function successCallback(response) {
            if (response.status === 200) {
                $scope.Cliente = response.data.Dados;
            }
        },
            function errorCallback(response) {
                if (response != null && response.data != null && response.data.Message != null) {
                    $().toastmessage('showErrorToast', response.data.Message);
                }
            });
    }


    $scope.Alterar = function () {
        $http({
            method: 'PUT',
            data: $scope.Cliente,
            url: api_url + '/clientes/' + $routeParams.id
        }).then(function successCallback(response) {
            if (response.status === 200) {
                if (response.data.Message != null) {
                    $().toastmessage('showSuccessToast', response.data.Message);

                } else {
                    $().toastmessage('showSuccessToast', "Operação realziada com sucesso!");
                }
                $scope.AlterarInit();
            }
        },
            function errorCallback(response) {
                if (response != null && response.data != null && response.data.Message != null) {
                    $().toastmessage('showErrorToast', response.data.Message);
                }
            });
    }


    $scope.Pesquisar = function () {
        $scope.Lista = [];
        $scope.Pagina = 1;
        $scope.TotalRegistros = 0;

        $http({
            method: 'GET',
            url: api_url +
            '/clientes/lista?pagina=' +
            $scope.Pagina +
            "&codigo=" +
            $scope.ClientePesquisa.Codigo +
            "&nome=" +
            $scope.ClientePesquisa.Nome +
            "&cpf=" +
            $scope.ClientePesquisa.Cpf
        }).then(function successCallback(response) {
            if (response.status === 200) {
                $scope.Lista = response.data.Dados;
                $scope.Pagina = response.data.Pagina;
                $scope.TotalRegistros = response.data.Total;
                $scope.TotalPaginas = Math.round($scope.TotalRegistros / 20);

            }
        },
            function errorCallback(response) {
                if (response != null && response.data != null && response.data.Message != null) {
                    $().toastmessage('showErrorToast', response.data.Message);
                }
            });
    }

    $scope.getNumber = function (num) {
        return new Array(num);
    }
}
]);