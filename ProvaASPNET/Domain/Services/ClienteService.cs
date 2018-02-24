using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.EntityFramework.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services
{
    internal class ClienteService : IClienteService
    {
        private readonly ClienteRepository _clienteRepository;
        private readonly PedidoRepository _pedidoRepository;

        public ClienteService(ClienteRepository clienteRepository, PedidoRepository pedidoRepository)
        {
            _clienteRepository = clienteRepository;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<DefaultResult<ClienteModel>> Get(Guid id)
        {
            var entity = await _clienteRepository.GetByIdAsync(id);
            if (entity != null)
            {
                var result = new DefaultResult<ClienteModel>(ClienteModel.FromEntity(entity), true);
                return result;
            }
            return null;
        }

        public async Task<DefaultResult<bool>> Cadastrar(ClienteModel model)
        {
            try
            {
                //verificar se cpf já esta cadastrado
                if (_clienteRepository.CpfExiste(model.Cpf))
                {
                    return new DefaultResult<bool>(false, false, "CPF já existe");
                }

                //verificar se codigo ja esta cadastrado
                if (_clienteRepository.CodigoExiste(model.Codigo))
                {
                    return new DefaultResult<bool>(false, false, "Código já existe");
                }

                //verificar idaide - n permitir menores de 18 anos
                if (model.DataNascimento.AddYears(18) > DateTime.Now)
                {
                    return new DefaultResult<bool>(false, false, "São permitidos apenas clientes maiores de 18 anos.");
                }


                var entity = new ClienteEntity(model.Codigo, model.Nome, model.Cpf, model.DataNascimento);
                await _clienteRepository.InsertAsync(entity);
                return new DefaultResult<bool>(true, true);
            }
            catch (Exception ex)
            {
                return new DefaultResult<bool>(false, false, ex.Message);
            }

        }

        public async Task<DefaultResult<bool>> Alterar(Guid id, ClienteModel model)
        {
            try
            {
                //verificar idaide - n permitir menores de 18 anos
                if (model.DataNascimento.AddYears(18) > DateTime.Now)
                {
                    return new DefaultResult<bool>(false, false, "São permitidos apenas clientes maiores de 18 anos.");
                }

                //procurar cliente pelo id
                var entity = await _clienteRepository.GetByIdAsync(id);
                if (entity != null)
                {
                    //atualizar os dados
                    entity.Update(model.Nome, model.DataNascimento);

                    await _clienteRepository.UpdateAsync(entity);
                    return new DefaultResult<bool>(true, true);
                }
                else
                {
                    return new DefaultResult<bool>(false, false, "Cliente não encontrado");
                }

            }
            catch (Exception ex)
            {
                return new DefaultResult<bool>(false, false, ex.Message);
            }
        }

        public DefaultResult<PageResult<ClienteModel>> Listar(int pagina, int porpagina, ClientePesquisaModel pesquisa)
        {
            var total = _clienteRepository.TotalRegistros(pesquisa);
            if (total > 0)
            {
                var result = _clienteRepository.Lista(porpagina, (porpagina * pagina - porpagina), pesquisa);
                return new DefaultResult<PageResult<ClienteModel>>(new PageResult<ClienteModel>(result.Select(x => ClienteModel.FromEntity(x)).ToList(), total, pagina), true);
            }
            else
            {
                return new DefaultResult<PageResult<ClienteModel>>(null, false, "Não possui registros");
            }
        }

        public async Task<DefaultResult<bool>> Excluir(Guid id)
        {
            try
            {
                //procurar cliente pelo id
                var entity = await _clienteRepository.GetByIdAsync(id);
                if (entity != null)
                {
                    //verificar se possui pedidos
                    var totalPedidos = _pedidoRepository.ClienteTotalPedidos(id);
                    if (totalPedidos > 0)
                    {
                        return new DefaultResult<bool>(false, false, "Cliente possui pedidos. Não é possível realizar a exclusão.");
                    }

                    //excluir
                    await _clienteRepository.DeleteAsync(entity);

                    return new DefaultResult<bool>(true, true);
                }
                else
                {
                    return new DefaultResult<bool>(false, false, "Cliente não encontrado");
                }

            }
            catch (Exception ex)
            {
                return new DefaultResult<bool>(false, false, ex.Message);
            }
        }
    }
}
