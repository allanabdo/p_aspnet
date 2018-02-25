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
    [RoutePrefix("api/pedidos")]
    public class PedidoController : ApiController
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get([FromUri] Guid id)
        {
            var result = await _pedidoService.Get(id);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(result);
            }

            return ResponseMessage(Request.CreateErrorResponse(result.Status, result.Mensagem));
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] PedidoModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _pedidoService.Cadastrar(model);
                if (result.Status == HttpStatusCode.Created)
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

        [HttpGet]
        [Route("lista")]
        public IHttpActionResult Lista([FromUri] int pagina = 1, int porpagina = 20, string codigo = "")
        {
            if (porpagina > 200)
            {
                porpagina = 20;
            }

            var result = _pedidoService.Listar(pagina, porpagina, codigo);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(result.Dados);
            }
            return ResponseMessage(Request.CreateErrorResponse(result.Status, result.Mensagem));
        }

    }
}
