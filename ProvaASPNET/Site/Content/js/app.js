var app = angular.module('siteApp', ['ngRoute']);


// configure our routes
app.config(function ($routeProvider) {
    $routeProvider
        .when('/',
        {
            templateUrl: 'Home/Home'
        }).when('/clientes',
        {
            templateUrl: 'Cliente/Index'
        }).when('/clientes/cadastrar',
        {
            templateUrl: 'Cliente/Cadastrar'
        }).when('/clientes/alterar/:id',
        {
            templateUrl: 'Cliente/Alterar'
        }).when('/produtos',
        {
            templateUrl: 'Produto/Index'
        }).when('/produtos/cadastrar',
        {
            templateUrl: 'Produto/Cadastrar'
        }).when('/produtos/alterar/:id',
        {
            templateUrl: 'Produto/Alterar'
        });
});
