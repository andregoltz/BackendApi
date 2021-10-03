using Microsoft.AspNetCore.Mvc;

namespace TesteNegociosPublicos.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public Apresentacao Index()
        {
            return new Apresentacao();
        }
    }
}
