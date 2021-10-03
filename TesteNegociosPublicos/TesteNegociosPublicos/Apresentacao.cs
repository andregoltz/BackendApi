using System.Collections.Generic;

namespace TesteNegociosPublicos
{
    public class Apresentacao
    {
        public Apresentacao()
        {
            rotasApi = new List<string>();
            rotasApi.Add("/clientes");
        }
        public List<string> rotasApi { get; set; }
        public string Mensagem { get { return "Seja bem-vindo a nossa API"; } }
        public List<string> RotasDescricao { get { return this.rotasApi; } }
    }
}
