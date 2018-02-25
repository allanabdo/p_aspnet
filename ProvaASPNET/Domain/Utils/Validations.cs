using System.Collections.Generic;

namespace Domain.Utils
{
    public class Validations
    {

        public static bool ValidCpf(IList<int> valores)
        {
            if (valores != null && valores.Count > 0)
            {
                int soma = 0;

                for (int i = 0; i < 9; i++)
                {
                    soma += (10 - i) * valores[i];
                }

                int resultado = soma % 11;

                if (resultado == 1 || resultado == 0)
                {
                    if (valores[9] != 0)
                        return false;
                }
                else if (valores[9] != 11 - resultado)
                {
                    return false;
                }

                soma = 0;
                for (int i = 0; i < 10; i++)
                {
                    soma += (11 - i) * valores[i];
                }

                resultado = soma % 11;

                if (resultado == 1 || resultado == 0)
                {
                    if (valores[10] != 0)
                    {
                        return false;
                    }
                }
                else if (valores[10] != 11 - resultado)
                {
                    return false;
                }

                string cpf = "";

                for (int i = 0; i < valores.Count; i++)
                {
                    cpf = cpf + valores[i];
                }


                bool igual = true;

                for (int i = 1; i < 11 && igual; i++)
                {

                    if (cpf[i] != cpf[0])
                    {

                        igual = false;
                    }
                }

                if (igual)
                {
                    return false;
                }

                return true;
            }
            return false;
        }
    }
}
