using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.EntityFramework.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services
{
    internal class PedidoService : IPedidoService
    {
        private readonly PedidoRepository _pedidoRepository;

        public PedidoService(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<DefaultResult<PedidoModel>> Get(Guid id)
        {
            try
            {
                var entity = await _pedidoRepository.GetByIdAsync(id);
                if (entity != null)
                {
                    var result = new DefaultResult<PedidoModel>(PedidoModel.FromEntity(entity), HttpStatusCode.OK);
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                return new DefaultResult<PedidoModel>(null, HttpStatusCode.InternalServerError, e.Message);
            }

        }

        public async Task<DefaultResult<bool>> Cadastrar(PedidoModel model)
        {
            try
            {
                //verificar e gerar codigo codigo nao existe
                var codigo = GerarCodigo();

                //salvar
                var entity = new PedidoEntity(codigo, model.ClienteId, model.ValorTotal);
                foreach (var prodId in model.ProdutosId)
                {
                    entity.AddProduto(prodId);
                }
                await _pedidoRepository.InsertAsync(entity);

                return new DefaultResult<bool>(true, HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return new DefaultResult<bool>(false, HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public DefaultResult<PageResult<PedidoModel>> Listar(int pagina, int porpagina, string codigo = "")
        {
            var total = _pedidoRepository.TotalRegistros(codigo);
            if (total > 0)
            {
                var result = _pedidoRepository.Lista(porpagina, (porpagina * pagina - porpagina), codigo);
                return new DefaultResult<PageResult<PedidoModel>>(new PageResult<PedidoModel>(result.Select(PedidoModel.FromEntity).ToList(), total, pagina), HttpStatusCode.OK);
            }
            else
            {
                return new DefaultResult<PageResult<PedidoModel>>(null, HttpStatusCode.NotFound, "Não possui registros");
            }
        }

        private string GerarCodigo()
        {
            var codigo = "p_";
            Random rnd = new Random();
            for (var i = 1; i < 10; i++)
            {
                codigo = codigo + rnd.Next(9);

            }

            if (_pedidoRepository.CodigoExsite(codigo))
            {
                GerarCodigo();
            }

            return codigo;
        }
    }
}
