var app = angular.module('siteApp');


//PEDIDO CONTROLLER
app.controller('pedidoController', ['$scope', '$http', '$routeParams', '$window', function ($scope, $http, $routeParams, $window) {
    
    $scope.ClienteEscolhidoId = "";
    $scope.ClienteEscolhidoNome = "";
    $scope.CodigoProduto = "";
    $scope.ProdutosCarrinho = [];
    $scope.ValorTotal = 0;
    $scope.ProdsId = [];
    $scope.NomeClientePesquisa = "";



    $scope.query = function (query) {
        $http.get('http://ws.spotify.com/search/1/track.json', {
            params: {
                q: query.term
            }
        }).then(function (res) {
            var songs = { results: [] };
            angular.forEach(res.data.tracks, function (song) {
                songs.results.push({
                    text: song.artists[0].name + ' - ' + song.name,
                    id: song.href
                });
            });
            query.callback(songs);
        });
    };

    //LISTA
    $scope.InitList = function () {
        $scope.PedidoPesquisa = { Codigo: "", DataInicial: "", DataFinal: "", Cliente: "" }
        $scope.Lista = [];
        if ($routeParams.pagina != undefined) {
            $scope.Pagina = $routeParams.pagina;
        } else {
            $scope.Pagina = 1;
        }

        $scope.TotalRegistros = 0;
        $http({
            method: 'GET',
            url: api_url + '/pedidos/lista?&porpagina=10&pagina=' + $scope.Pagina
        }).then(function successCallback(response) {
            if (response.status === 200) {
                $scope.Lista = response.data.Dados;
                $scope.Pagina = response.data.Pagina;
                $scope.TotalRegistros = response.data.Total;
                var totalPaginas = Math.round($scope.TotalRegistros / 10);
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

    //CADASTRO

    $scope.Cadastrar = function () {
        var dados = {
            ClienteId: $scope.ClienteEscolhidoId,
            ProdutosId: $scope.ProdsId,
            ValorTotal: $scope.ValorTotal

        };
        $http({
            method: 'POST',
            data: dados,
            url: api_url + '/pedidos/'
        }).then(function successCallback(response) {
            if (response.status === 200) {
                if (response.data.Message != null) {
                    $().toastmessage('showSuccessToast', response.data.Message);

                } else {
                    $().toastmessage('showSuccessToast', "Operação realziada com sucesso!");
                }
                $window.location.href = '#!/pedidos';
            }
        }, function errorCallback(response) {
            if (response != null && response.data != null && response.data.Message != null) {
                $().toastmessage('showErrorToast', response.data.Message);
            }
        });
    }

    $scope.PesquisaCliente = function () {
        if (($scope.NomeClientePesquisa !== "" && $scope.NomeClientePesquisa.length > 3) || $scope.NomeClientePesquisa === "") {
            $http({
                method: 'GET',
                url: api_url + '/clientes/lista?pagina=1&porpagina=10&nome=' + $scope.NomeClientePesquisa
            }).then(function successCallback(response) {
                if (response.status === 200) {
                    $scope.ListaClientes = response.data.Dados;
                }
            }, function errorCallback(response) {
                if (response != null && response.data != null && response.data.Message != null) {
                    $().toastmessage('showErrorToast', response.data.Message);
                }
            });
        }
    }

    $scope.PesquisarProduto = function () {
        if (($scope.CodigoProduto !== "" && $scope.CodigoProduto.length > 3) || $scope.CodigoProduto === "") {
            $http({
                method: 'GET',
                url: api_url + '/produtos/lista?pagina=1&porpagina=10&codigo=' + $scope.CodigoProduto
            }).then(function successCallback(response) {
                if (response.status === 200) {
                    $scope.ListaProdutos = response.data.Dados;
                }
            }, function errorCallback(response) {
                if (response != null && response.data != null && response.data.Message != null) {
                    $().toastmessage('showErrorToast', response.data.Message);
                }
            });
        }
    }

    $scope.BuscarPeloCodigoBarras = function() {
        $http({
            method: 'GET',
            url: api_url + '/produtos/codigobarra/' + $scope.CodigoBarrasSearch
        }).then(function successCallback(response) {
            if (response.status === 200) {
                $scope.ListaProdutos = [];
                $scope.ListaProdutos.push(response.data.Dados);
            }
        }, function errorCallback(response) {
            if (response != null && response.data != null && response.data.Message != null) {
                $().toastmessage('showErrorToast', response.data.Message);
            }
        });
    }

    $scope.EscolherProduto = function (id, codigo, valor) {
        var item = {
            Id: id,
            Codigo: codigo,
            ValorVenda: valor
        };



        var possui = false;
        for (var i = 0; i < $scope.ProdutosCarrinho.length; i++) {
            if ($scope.ProdutosCarrinho[i].Id === item.Id) {
                possui = true;
            }
        }
        if (possui === false) {
            $scope.ValorTotal = $scope.ValorTotal + valor;
            $scope.ProdutosCarrinho.push(item);
            $scope.ProdsId.push(id);
        }

    }

    $scope.RemoverProdCarrinho = function (id, valor) {
        $scope.ProdutosCarrinho = $scope.ProdutosCarrinho.filter(function (el) {
            return el.Id !== id;
        });

        var index = $scope.ProdsId.indexOf(id);
        if (index !== -1) {
            $scope.ProdsId.splice(index, 1);
        }

        $scope.ValorTotal = $scope.ValorTotal - valor;
    }

    $scope.EscolherOutroCliente = function () {
        $scope.ClienteEscolhidoId = "";
        $scope.ClienteEscolhidoNome = "";
        
    }

    $scope.EscolherCliente = function (id, nome) {
        $scope.ClienteEscolhidoId = id;
        $scope.ClienteEscolhidoNome = nome;
    }

    $scope.Pesquisar = function () {
        $scope.Lista = [];
        $scope.Pagina = 1;
        $scope.TotalRegistros = 0;

        $http({
            method: 'GET',
            url: api_url + '/pedidos/lista?porpagina=10&pagina=' + $scope.Pagina + "&codigo=" + $scope.PedidoPesquisa.Codigo + "&cliente=" + $scope.PedidoPesquisa.Cliente + "&dataInicial=" + $scope.PedidoPesquisa.DataInicial + "&dataFinal=" + $scope.PedidoPesquisa.DataFinal
        }).then(function successCallback(response) {
            if (response.status === 200) {
                $scope.Lista = response.data.Dados;
                $scope.Pagina = response.data.Pagina;
                $scope.TotalRegistros = response.data.Total;
                var totalPaginas = Math.round($scope.TotalRegistros / 10);
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


