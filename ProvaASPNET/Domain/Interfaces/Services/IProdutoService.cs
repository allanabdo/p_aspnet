using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IProdutoService
    {
        Task<DefaultResult<ProdutoModel>> Get(Guid id);
        Task<DefaultResult<bool>> Cadastrar(ProdutoModel model);
        Task<DefaultResult<bool>> Alterar(Guid id, ProdutoModel model);
        DefaultResult<PageResult<ProdutoModel>> Listar(int pagina, int porpagina, string codigo = "");
        Task<DefaultResult<bool>> Excluir(Guid id);
        DefaultResult<ProdutoModel> GetByCodigoBarra(string codigoBarra);
    }
}
