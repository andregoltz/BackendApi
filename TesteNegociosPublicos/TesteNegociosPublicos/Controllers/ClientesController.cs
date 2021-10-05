using Microsoft.AspNetCore.Cors;
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

        [HttpPost]
        public Cliente Criar([FromBody] Cliente cliente)
        {
            return cliente.Salvar();
        }

        [HttpPut]
        [Route("{id}")]
        public Cliente Atualizar(int id, [FromBody] Cliente cliente)
        {
            cliente.Id = id;
            return cliente.Atualizar();
        }

        [HttpDelete]
        [Route("{id}")]
        public void Excluir(int id)
        {
            Cliente.Excluir(id);
        }
    }
}
