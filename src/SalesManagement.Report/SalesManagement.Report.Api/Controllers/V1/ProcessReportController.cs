using Microsoft.AspNetCore.Mvc;
using SalesManagement.Report.Application.Feature.Interfaces;
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
    public class ProcessReportController : ControllerBase
    {
        private readonly IRelatorio _processar;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="processar"></param>
        public ProcessReportController(IRelatorio processar)
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
        [SwaggerResponse((int)HttpStatusCode.OK, "Solicitar relatório", typeof(bool))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Erro interno no servidor")]
        [HttpPost]
        [Route("download")]
        public IActionResult Download(string nomeArquivo)
        {
            byte[] buffer = _processar.Download(nomeArquivo);

            FileContentResult response = new FileContentResult(buffer, "text/csv");

            return response;
        }
    }
}
