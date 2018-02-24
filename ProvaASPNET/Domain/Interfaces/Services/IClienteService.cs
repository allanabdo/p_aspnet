using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Task<DefaultResult<ClienteModel>> Get(Guid id);
        Task<DefaultResult<bool>> Cadastrar(ClienteModel model);
        Task<DefaultResult<bool>> Alterar(Guid id, ClienteModel model);
        DefaultResult<PageResult<ClienteModel>> Listar(int pagina, int porpagina, ClientePesquisaModel pesquisa);
        Task<DefaultResult<bool>> Excluir(Guid id);
    }
}
