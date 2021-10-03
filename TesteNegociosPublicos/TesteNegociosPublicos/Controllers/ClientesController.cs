using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;

namespace TesteNegociosPublicos.Controllers
{
    [Route("clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        public List<Cliente> Index()
        {
            return Cliente.Todos();
        }
    }
}
