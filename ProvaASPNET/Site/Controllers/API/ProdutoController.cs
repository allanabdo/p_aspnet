using Domain.Interfaces.Services;
using Domain.Models;
using System;
using System.Linq;
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
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] ProdutoModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _produtoService.Cadastrar(model);
                if (result.Sucesso)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Mensagem);
                }
            }
            else
            {
                var erro = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage;
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
                if (result.Sucesso)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Mensagem);
                }
            }
            else
            {
                var erro = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage;
                return BadRequest(erro);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Excluir([FromUri] Guid id)
        {

            var result = await _produtoService.Excluir(id);
            if (result.Sucesso)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Mensagem);
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

            var result = _produtoService.Listar(pagina, porpagina, codigo);
            if (result.Sucesso)
            {
                return Ok(result.Dados);
            }
            else
            {
                return BadRequest(result.Mensagem);
            }
        }

    }
}
