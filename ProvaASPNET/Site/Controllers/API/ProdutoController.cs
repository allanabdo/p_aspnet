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
    [RoutePrefix("api/produtos")]
    public class ProdutoController : ApiController
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get([FromUri] Guid id)
        {
            var result = await _produtoService.Get(id);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(result);
            }

            return ResponseMessage(Request.CreateErrorResponse(result.Status, result.Mensagem));
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] ProdutoModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _produtoService.Cadastrar(model);
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

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Alterar([FromUri] Guid id, [FromBody] ProdutoModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _produtoService.Alterar(id, model);
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

            var result = await _produtoService.Excluir(id);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok();
            }
            return ResponseMessage(Request.CreateErrorResponse(result.Status, result.Mensagem));
        }


        [HttpGet]
        [Route("lista")]
        public IHttpActionResult Lista([FromUri] int pagina = 1, int porpagina = 20, string codigo = "")
        {
            if (porpagina > 200)
            {
                porpagina = 20;
            }

            var result = _produtoService.Listar(pagina, porpagina, codigo);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(result.Dados);
            }
            return ResponseMessage(Request.CreateErrorResponse(result.Status, result.Mensagem));
        }

        [HttpGet]
        [Route("codigobarra/{codigobarra}")]
        public IHttpActionResult GetByCodigoBarra([FromUri] string codigobarra)
        {
            var result = _produtoService.GetByCodigoBarra(codigobarra);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return ResponseMessage(Request.CreateErrorResponse(result.Status, result.Mensagem));
        }
    }
}
