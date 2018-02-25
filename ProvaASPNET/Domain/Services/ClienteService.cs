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
                var result = new DefaultResult<ClienteModel>(ClienteModel.FromEntity(entity), HttpStatusCode.OK);
                return result;
            }
            return new DefaultResult<ClienteModel>(null, HttpStatusCode.NotFound);
        }

        public async Task<DefaultResult<bool>> Cadastrar(ClienteModel model)
        {
            try
            {
    
                DateTime dataNascimento;

                if (!DateTime.TryParse(model.DataNascimento, out dataNascimento))
                {
                    return new DefaultResult<bool>(false, HttpStatusCode.BadRequest, "Data inválida");
                }

                //verificar se cpf já esta cadastrado
                if (_clienteRepository.CpfExiste(model.Cpf))
                {
                    return new DefaultResult<bool>(false, HttpStatusCode.BadRequest, "CPF já existe");
                }

                //verificar se codigo ja esta cadastrado
                if (_clienteRepository.CodigoExiste(model.Codigo))
                {
                    return new DefaultResult<bool>(false, HttpStatusCode.BadRequest, "Código já existe");
                }

                //verificar idaide - n permitir menores de 18 anos
                if (dataNascimento.AddYears(18) > DateTime.Now)
                {
                    return new DefaultResult<bool>(false, HttpStatusCode.BadRequest, "São permitidos apenas clientes maiores de 18 anos.");
                }


                var entity = new ClienteEntity(model.Codigo, model.Nome, model.Cpf, dataNascimento);
                await _clienteRepository.InsertAsync(entity);
                return new DefaultResult<bool>(true, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return new DefaultResult<bool>(false, HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        public async Task<DefaultResult<bool>> Alterar(Guid id, ClienteModel model)
        {
            try
            {
                DateTime dataNascimento;

                if (!DateTime.TryParse(model.DataNascimento, out dataNascimento))
                {
                    return new DefaultResult<bool>(false, HttpStatusCode.BadRequest, "Data inválida");
                }

                //verificar idaide - n permitir menores de 18 anos
                if (dataNascimento.AddYears(18) > DateTime.Now)
                {
                    return new DefaultResult<bool>(false, HttpStatusCode.BadRequest, "São permitidos apenas clientes maiores de 18 anos.");
                }

                //procurar cliente pelo id
                var entity = await _clienteRepository.GetByIdAsync(id);
                if (entity != null)
                {
                    //atualizar os dados
                    entity.Update(model.Nome, dataNascimento);

                    await _clienteRepository.UpdateAsync(entity);
                    return new DefaultResult<bool>(true, HttpStatusCode.OK);
                }
                else
                {
                    return new DefaultResult<bool>(false, HttpStatusCode.NotFound, "Cliente não encontrado");
                }

            }
            catch (Exception ex)
            {
                return new DefaultResult<bool>(false, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public DefaultResult<PageResult<ClienteModel>> Listar(int pagina, int porpagina, ClientePesquisaModel pesquisa)
        {
            var total = _clienteRepository.TotalRegistros(pesquisa);
            if (total > 0)
            {
                var result = _clienteRepository.Lista(porpagina, (porpagina * pagina - porpagina), pesquisa);
                return new DefaultResult<PageResult<ClienteModel>>(new PageResult<ClienteModel>(result.Select(x => ClienteModel.FromEntity(x)).ToList(), total, pagina), HttpStatusCode.OK);
            }
            else
            {
                return new DefaultResult<PageResult<ClienteModel>>(null, HttpStatusCode.NotFound, "Não possui registros");
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
                        return new DefaultResult<bool>(false, HttpStatusCode.BadRequest, "Cliente possui pedidos. Não é possível realizar a exclusão.");
                    }

                    //excluir
                    await _clienteRepository.DeleteAsync(entity);

                    return new DefaultResult<bool>(true, HttpStatusCode.OK);
                }
                else
                {
                    return new DefaultResult<bool>(false, HttpStatusCode.BadRequest, "Cliente não encontrado");
                }

            }
            catch (Exception ex)
            {
                return new DefaultResult<bool>(false, HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
