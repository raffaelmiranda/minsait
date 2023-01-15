using Microsoft.AspNetCore.Mvc;
using SalesManagement.CashFlow.Application.Interfaces;
using SalesManagement.CashFlow.Application.Models;
using SalesManagement.CashFlow.Domain.Entities;
using SalesManagement.CashFlow.Infrastructure.Persistence.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SalesManagement.CashFlow.Api.Controllers.V1
{
    /// <summary>
    /// Controller Lancamento Bancario
    /// </summary>
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class LancamentoBancarioController : ControllerBase
    {
        private readonly ILancamentoBancarioAppService _app;

        /// <summary>
        /// Contrutor
        /// </summary>
        /// <param name="app"></param>
        public LancamentoBancarioController(ILancamentoBancarioAppService app)
        {
            _app = app;
        }

        /// <summary>
        /// Salvar os lançamento bancários
        /// </summary>
        /// <param name="request">dados dos lançamentos</param>
        /// <returns>IActionResult</returns>
        [SwaggerResponse((int)HttpStatusCode.OK, "Salvar", typeof(LancamentoBancarioInserir))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, "Erro de validação")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Erro interno no servidor")]
        [HttpPost]
        [Route("")]
        public IActionResult Salvar([FromBody, SwaggerRequestBody("Parametros do Lançamento Bancário")] LancamentoBancarioInserir request)
        {
            LancamentoBancario model = new LancamentoBancario(request.Descricao, request.Valor, request.TipoLancamentoId, request.Categoria);

            _app.Salvar(model);

            return Created(HttpContext.Request.Path, model);
        }

        /// <summary>
        /// Atualizar os lançamento bancários
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [SwaggerResponse((int)HttpStatusCode.OK, "Atualizar", typeof(LancamentoBancarioAtualizar))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, "Erro de validação")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Erro interno no servidor")]
        [HttpPut]
        [Route("")]
        public IActionResult Atualizar([FromBody, SwaggerRequestBody("Parametros do Lançamento Bancário")] LancamentoBancarioAtualizar request)
        {
            LancamentoBancario model = new LancamentoBancario(request.Descricao, request.Valor, request.TipoLancamentoId, request.Categoria, request.Id);

            _app.Atualizar(model);

            return Ok(model);
        }

        /// <summary>
        /// Remover lançamento bancário
        /// </summary>
        /// <param name="id">Id do lançamento bancário</param>
        /// <returns>IActionResult</returns>
        [SwaggerResponse((int)HttpStatusCode.OK, "Remover lançamento bancário")]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, "Erro de validação")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Erro interno no servidor")]
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult RemoverPorId([FromRoute, SwaggerParameter("Id do lançamento bancário")] int id)
        {
            LancamentoBancario lancamento = _app.ObterPorId(id);

            _app.Remover(lancamento);

            return Ok();
        }

        /// <summary>
        /// Obter todos os lançamento bancários
        /// </summary>
        /// <returns>IActionResult</returns>
        [SwaggerResponse((int)HttpStatusCode.OK, "Obter todos os lançamento bancários", typeof(List<LancamentoBancario>))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, "Erro de validação")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Erro interno no servidor")]
        [HttpGet]
        [Route("")]
        public IActionResult Obter()
        {
            List<LancamentoBancarioObter> response = _app.ObterTodosComTipoLancamento();

            return Ok(response);
        }

        /// <summary>
        /// Obter lançamento bancário por id
        /// </summary>
        /// <param name="id">Id do lançamento bancário</param>
        /// <returns>IActionResult</returns>
        [SwaggerResponse((int)HttpStatusCode.OK, "Obter por id", typeof(LancamentoBancario))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, "Erro de validação")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Erro interno no servidor")]
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult ObterPorId([FromRoute, SwaggerParameter("Id do lançamento bancário")] int id)
        {
            LancamentoBancario response = _app.ObterPorId(id);

            return Ok(response);
        }
    }
}
