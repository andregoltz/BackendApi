using System.Collections.Generic;
using System.Linq;
using static System.Convert;

namespace Models
{
    public static class Valida
    {
        public static bool Cpf(long cpf)
        {
            string strCpf = cpf.ToString().PadLeft(11, '0');

            if (strCpf.All(x => x == strCpf[0]))
                return false;

            var listCpf = strCpf.Select(num => ToInt32(num.ToString())).ToList();

            if (listCpf[9] != Mod11Cpf(listCpf, 10))
                return false;

            if (listCpf[10] != Mod11Cpf(listCpf, 11))
                return false;

            return true;
        }

        internal static int Mod11Cpf(List<int> elementos, int @base)
        {
            int soma = 0;
            for (int i = 0; i < (@base - 1); i++)
                soma += (@base - i) * elementos[i];

            int dv1, resto = soma % 11;

            if (resto < 2)
                dv1 = 0;
            else
                dv1 = 11 - resto;

            return dv1;
        }
    }
}
