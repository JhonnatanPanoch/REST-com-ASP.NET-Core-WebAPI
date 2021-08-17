using DevIO.Api.Controllers;
using DevIO.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TesteVersaoController : MainController
    {
        public TesteVersaoController(INotificator notificator, IUser appUser) : base(notificator, appUser)
        {
        }

        [HttpGet]
        public string Valor()
        {
            return "Versão 2";
        }
    }
}
