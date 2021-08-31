using DevIO.Api.Controllers;
using DevIO.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.V1.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TesteVersaoController : MainController
    {
        public TesteVersaoController(INotificator notificator, IUser appUser) : base(notificator, appUser)
        {
        }

        [HttpGet]
        public string Valor()
        {
            return "Versão 1";
        }
    }
}
