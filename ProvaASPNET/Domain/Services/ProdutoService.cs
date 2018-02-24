using Domain.Entities;
using Domain.EntityFramework.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    internal class ProdutoService : IProdutoService
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly PedidoRepository _pedidoRepository;


        public ProdutoService(ProdutoRepository produtoRepository, PedidoRepository pedidoRepository)
        {
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
        }


        public async Task<DefaultResult<ProdutoModel>> Get(Guid id)
        {
            var entity = await _produtoRepository.GetByIdAsync(id);
            if (entity != null)
            {
                var result = new DefaultResult<ProdutoModel>(ProdutoModel.FromEntity(entity), true);
                return result;
            }
            return null;
        }

        public async Task<DefaultResult<bool>> Cadastrar(ProdutoModel model)
        {
            try
            {

                //verificar se codigo existe
                if (_produtoRepository.CodigoExiste(model.Codigo))
                {
                    return new DefaultResult<bool>(false, false, "Código já existe");
                }

                //verificar se codigo de barras existe
                if (_produtoRepository.CodigoBarraExiste(model.CodigoBarra))
                {
                    return new DefaultResult<bool>(false, false, "Código de barras já existe");
                }

                var entity = new ProdutoEntity(model.Codigo, model.CodigoBarra, model.Descricao, model.ValorVenda);
                await _produtoRepository.InsertAsync(entity);
                return new DefaultResult<bool>(true, true);
            }
            catch (Exception ex)
            {
                return new DefaultResult<bool>(false, false, ex.Message);
            }
        }

        public async Task<DefaultResult<bool>> Alterar(Guid id, ProdutoModel model)
        {
            try
            {
                //procurar produto pelo id
                var entity = await _produtoRepository.GetByIdAsync(id);
                if (entity != null)
                {
                    //atualizar os dados
                    entity.Update(model.Descricao, model.ValorVenda);

                    await _produtoRepository.UpdateAsync(entity);
                    return new DefaultResult<bool>(true, true);
                }
                else
                {
                    return new DefaultResult<bool>(false, false, "Produto não encontrado");
                }

            }
            catch (Exception ex)
            {
                return new DefaultResult<bool>(false, false, ex.Message);
            }
        }

        public async Task<DefaultResult<bool>> Excluir(Guid id)
        {
            //buscar
            var entity = await _produtoRepository.GetByIdAsync(id);
            if (entity != null)
            {
                //verificar se produto está em algum pedido
                if (_pedidoRepository.ProdutoTotalPedidos(id) > 0)
                {
                    return new DefaultResult<bool>(false, false, "Produto está em algum pedido e não pode ser excluido");
                }

                //excluir
                await _produtoRepository.DeleteAsync(entity);
                return new DefaultResult<bool>(true, true);
            }
            else
            {
                return new DefaultResult<bool>(false, false, "Produto não encontrado");
            }
        }


        public DefaultResult<PageResult<ProdutoModel>> Listar(int pagina, int porpagina, string codigo = "")
        {
            var total = _produtoRepository.TotalRegistros(codigo);
            if (total > 0)
            {
                var result = _produtoRepository.Lista(porpagina, (porpagina * pagina - porpagina), codigo);
                return new DefaultResult<PageResult<ProdutoModel>>(new PageResult<ProdutoModel>(result.Select(x => ProdutoModel.FromEntity(x)).ToList(), total, pagina), true);
            }
            else
            {
                return new DefaultResult<PageResult<ProdutoModel>>(null, false, "Não possui registros");
            }
        }
    }
}
