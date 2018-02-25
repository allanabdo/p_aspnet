using Domain.Interfaces.Services;
using Domain.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Site.Controllers.API
{
    [RoutePrefix("api/clientes")]
    public class ClienteController : ApiController
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get([FromUri] Guid id)
        {
            var result = await _clienteService.Get(id);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return ResponseMessage(Request.CreateErrorResponse(result.Status, result.Mensagem));
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] ClienteModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _clienteService.Cadastrar(model);
                if (result.Status == HttpStatusCode.Created)
                {
                    return Ok();
                }
                else
                {
                    return ResponseMessage(Request.CreateErrorResponse(result.Status, result.Mensagem));
                }
            }
            else
            {
                var erro = ModelState.Values.FirstOrDefault(x => x.Errors.Count > 0)?.Errors.FirstOrDefault()?.ErrorMessage;
                return BadRequest(erro);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Alterar([FromUri] Guid id, [FromBody] ClienteModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _clienteService.Alterar(id, model);
                if (result.Status == HttpStatusCode.OK)
                {
                    return Ok();
                }
                return ResponseMessage(Request.CreateErrorResponse(result.Status, result.Mensagem));
            }
            else
            {
                var erro = ModelState.Values.FirstOrDefault(x => x.Errors.Count > 0)?.Errors.FirstOrDefault()?.ErrorMessage;
                return BadRequest(erro);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Excluir([FromUri] Guid id)
        {

            var result = await _clienteService.Excluir(id);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok();
            }
            return ResponseMessage(Request.CreateErrorResponse(result.Status, result.Mensagem));
        }


        [HttpGet]
        [Route("lista")]
        public IHttpActionResult Lista([FromUri] int pagina = 1, int porpagina = 20, string codigo = "", string nome = "", string cpf = "")
        {
            if (porpagina > 200)
            {
                porpagina = 20;
            }
            var pesquisa = new ClientePesquisaModel { Codigo = codigo, Nome = nome, Cpf = cpf };
            var result = _clienteService.Listar(pagina, porpagina, pesquisa);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(result.Dados);
            }

            return ResponseMessage(Request.CreateErrorResponse(result.Status, result.Mensagem));

        }
    }
}
