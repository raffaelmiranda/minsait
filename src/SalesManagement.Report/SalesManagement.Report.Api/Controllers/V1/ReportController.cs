using Microsoft.AspNetCore.Mvc;
using SalesManagement.Report.Application.Feature.Interfaces;
using SalesManagement.Report.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SalesManagement.Report.Api.Controllers.V1
{
    /// <summary>
    /// Process Report Controller
    /// </summary>
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IRelatorio _processar;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="processar"></param>
        public ReportController(IRelatorio processar)
        {
            _processar = processar;
        }

        /// <summary>
        /// Processar relatório
        /// </summary>
        /// <returns>IActionResult</returns>
        [SwaggerResponse((int)HttpStatusCode.OK, "Solicitar relatório", typeof(bool))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Erro interno no servidor")]
        [HttpPost]
        [Route("processar")]
        public IActionResult Processar()
        {
            _processar.Processar();

            return Created(HttpContext.Request.Path, "Processing request");
        }

        /// <summary>
        /// Download relatório
        /// </summary>
        /// <returns>IActionResult</returns>
        [SwaggerResponse((int)HttpStatusCode.OK, "Solicitar relatório", typeof(FileContentResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Erro interno no servidor")]
        [HttpGet]
        [Route("download")]
        public IActionResult Download([FromQuery, SwaggerParameter("Qual é o nome do relatório?", Required = true)] string nomeArquivo)
        {
            byte[] buffer = _processar.Download(nomeArquivo);

            FileContentResult response = new FileContentResult(buffer, "text/csv");

            return response;
        }

        /// <summary>
        /// Obtém os metadados dos relatórios
        /// </summary>
        /// <returns>IActionResult</returns>
        [SwaggerResponse((int)HttpStatusCode.OK, "Obtém os metadados dos relatórios", typeof(List<Relatorio>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Erro interno no servidor")]
        [HttpGet]
        [Route("")]
        public IActionResult ObterMetadados()
        {
            List<Relatorio> relatorios = _processar.Obter();

            if (relatorios?.Count == 0) return NoContent();

            return Ok(relatorios);
        }
    }
}
