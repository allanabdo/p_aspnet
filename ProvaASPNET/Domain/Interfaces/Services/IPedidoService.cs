using System;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IPedidoService
    {
        Task<DefaultResult<PedidoModel>> Get(Guid id);
        Task<DefaultResult<bool>> Cadastrar(PedidoModel model);
        DefaultResult<PageResult<PedidoModel>> Listar(int pagina, int porpagina, string codigo = "");
    }
}
