using DevIO.Api.Controllers;
using DevIO.Bussiness.Interfaces;
using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DevIO.Api.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TesteVersaoController : MainController
    {
        private readonly ILogger _logger;

        public TesteVersaoController(
            INotificator notificator, 
            IUser appUser,
            ILogger<TesteVersaoController> logger) : base(notificator, appUser)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Valor()
        {
            throw new Exception("Error");

            //try
            //{
            //    var i = 0;
            //    var result = 42 / i;
            //}
            //catch (DivideByZeroException e)
            //{
            //    e.Ship(HttpContext);
            //}

            _logger.LogTrace("Log de Trace");
            _logger.LogDebug("Log de Debug");
            _logger.LogInformation("Log de Informação");
            _logger.LogWarning("Log de Aviso");
            _logger.LogError("Log de Erro");
            _logger.LogCritical("Log de Problema Critico");

            return "Versão 2";
        }
    }
}
