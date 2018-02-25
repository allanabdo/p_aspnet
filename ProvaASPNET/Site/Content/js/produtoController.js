var app = angular.module('siteApp');


//PRODUTO CONTROLLER
app.controller('produtoController', ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams) {
    $scope.Produto = { Codigo: "", CodigoBarra: "", Descricao: "", ValorVenda: "" };

    //LISTA
    $scope.InitList = function () {
        $scope.ProdutoPesquisa = { Codigo: "" }
        $scope.Lista = [];
        if ($routeParams.pagina != undefined) {
            $scope.Pagina = $routeParams.pagina;
        } else {
            $scope.Pagina = 1;
        }

        $scope.TotalRegistros = 0;
        $http({
            method: 'GET',
            url: api_url + '/produtos/lista?pagina=' + $scope.Pagina
        }).then(function successCallback(response) {
            if (response.status === 200) {
                $scope.Lista = response.data.Dados;
                $scope.Pagina = response.data.Pagina;
                $scope.TotalRegistros = response.data.Total;
                var totalPaginas = Math.round($scope.TotalRegistros / 20);
                $scope.TotalPaginas = [];
                for (var i = 1; i <= totalPaginas; i++) {
                    $scope.TotalPaginas.push(i);
                }

            }
        }, function errorCallback(response) {
            if (response != null && response.data != null && response.data.Message != null) {
                $().toastmessage('showErrorToast', response.data.Message);
            }
        });
    }

    //delete
    $scope.excluir = function (id) {
        $http({
            method: 'DELETE',
            url: api_url + '/produtos/' + id
        }).then(function successCallback(response) {
            if (response.status === 200) {
                if (response.data.Message != null) {
                    $().toastmessage('showSuccessToast', response.data.Message);

                } else {
                    $().toastmessage('showSuccessToast', "Operação realziada com sucesso!");
                }
                $scope.InitList();
            }
        }, function errorCallback(response) {
            if (response != null && response.data != null && response.data.Message != null) {
                $().toastmessage('showErrorToast', response.data.Message);
            }
        });
    };

    //CADASTRO
    $scope.InitCadastro = function () {
        $scope.Produto = { Codigo: "", CodigoBarra: "", Descricao: "", ValorVenda: "" };

    }

    $scope.Cadastrar = function () {
        $http({
            method: 'POST',
            data: $scope.Produto,
            url: api_url + '/produtos/'
        }).then(function successCallback(response) {
            if (response.status === 200) {
                if (response.data.Message != null) {
                    $().toastmessage('showSuccessToast', response.data.Message);

                } else {
                    $().toastmessage('showSuccessToast', "Operação realziada com sucesso!");
                }
                $scope.InitCadastro();
            }
        }, function errorCallback(response) {
            if (response != null && response.data != null && response.data.Message != null) {
                $().toastmessage('showErrorToast', response.data.Message);
            }
        });
    }


    $scope.AlterarInit = function () {
        $http({
            method: 'GET',
            url: api_url + '/produtos/' + $routeParams.id
        }).then(function successCallback(response) {
            if (response.status === 200) {
                $scope.Produto = response.data.Dados;
            }
        }, function errorCallback(response) {
            if (response != null && response.data != null && response.data.Message != null) {
                $().toastmessage('showErrorToast', response.data.Message);
            }
        });
    }


    $scope.Alterar = function () {
        $http({
            method: 'PUT',
            data: $scope.Produto,
            url: api_url + '/produtos/' + $routeParams.id
        }).then(function successCallback(response) {
            if (response.status === 200) {
                if (response.data.Message != null) {
                    $().toastmessage('showSuccessToast', response.data.Message);

                } else {
                    $().toastmessage('showSuccessToast', "Operação realziada com sucesso!");
                }
                $scope.AlterarInit();
            }
        }, function errorCallback(response) {
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
            url: api_url + '/produtos/lista?pagina=' + $scope.Pagina + "&codigo=" + $scope.ProdutoPesquisa.Codigo
        }).then(function successCallback(response) {
            if (response.status === 200) {
                $scope.Lista = response.data.Dados;
                $scope.Pagina = response.data.Pagina;
                $scope.TotalRegistros = response.data.Total;
                var totalPaginas = Math.round($scope.TotalRegistros / 20);
                $scope.TotalPaginas = [];
                for (var i = 1; i <= totalPaginas; i++) {
                    $scope.TotalPaginas.push(i);
                }

            }
        }, function errorCallback(response) {
            if (response != null && response.data != null && response.data.Message != null) {
                $().toastmessage('showErrorToast', response.data.Message);
            }
        });
    }

    $scope.getNumber = function (num) {
        return new Array(num);
    }
}]);


