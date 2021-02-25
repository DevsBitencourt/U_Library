using System;

namespace pManagerLibrary
{
	/// <summary>
	/// Biblioteca de utilitários
	/// </summary>
    public static class Biblio
    {
        private static double FatorConversor { get; } = 1024;
		/// <summary>
		/// Converte Byte em KiloByte.
		/// </summary>
		/// <param name="len">Representa o valor a ser convertido em KiloByte.</param>
		/// <returns></returns>
        public static double ByteInKb(double len)
        {
            return len / FatorConversor;
        }
		/// <summary>
		/// Converte Byte em MegaByte
		/// </summary>
		/// <param name="len">Representa o valor a ser convertido em MegaByte.</param>
		/// <returns></returns>
		public static double BytesInMB(double len)
        {
            return ((len / FatorConversor) / FatorConversor);
        }
		/// <summary>
		/// Validação de número de CPF
		/// </summary>
		/// <exception cref="ArgumentNullException">Exceção lançada quando parametro <paramref name="cpf"/> está vazio ou nulo.</exception>
		/// <param name="cpf">Representa o número de CPF.</param>
		/// <returns></returns>
        public static bool IsCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
				throw new ArgumentNullException("Argumento cpf não pode ser vázio ou nulo", nameof(cpf));
            }

			cpf = cpf.Trim();

			cpf = cpf.Replace(".", "").Replace("-", "");
		
			if (cpf.Length != 11)
				return false;

			int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
				
			string tempCpf = cpf.Substring(0, 9);
			
			int soma = 0;

			for (int i = 0; i < 9; i++)
            {
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
			}
				
			int resto = soma % 11;

			resto = resto < 2 ? 0 : 11 - resto; 
						
			string digito = resto.ToString();
			
			tempCpf += digito;
			
			soma = 0;
			
			for (int i = 0; i < 10; i++)
            {
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
			}
							
			resto = soma % 11;

			resto = resto < 2 ? 0 : 11 - resto;

			digito += resto.ToString();
			
			return cpf.EndsWith(digito);
		}
		/// <summary>
		/// Validação de número de CNPJ
		/// </summary>
		/// <exception cref="ArgumentNullException">Exceção lançada quando parametro <paramref name="cnpj"/> está vazio ou nulo.</exception>
		/// <param name="cnpj">Representa o número do CPF.</param>
		/// <returns></returns>
		public static bool IsCNPJ(string cnpj)
        {
            if (String.IsNullOrEmpty(cnpj))
            {
				throw new ArgumentNullException("Argumento cnpj não pode ser vázio ou nulo.", nameof(cnpj));
            }

			cnpj = cnpj.Trim();
			cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

			if (cnpj.Length != 14)
				return false;

			int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			
			string tempCnpj = cnpj.Substring(0, 12);
			int soma = 0;
			
			for (int i = 0; i < 12; i++)
            {
				soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
			}

			int resto = (soma % 11);

			resto = resto < 2 ? 0 : 11 - resto;

			string digito = resto.ToString();
			
			tempCnpj += digito;
			
			soma = 0;
			
			for (int i = 0; i < 13; i++)
            {
				soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
			}
			
			resto = (soma % 11);

			resto = resto < 2 ? 0 : 11 - resto;

			digito += resto.ToString();
			
			return cnpj.EndsWith(digito);
		}
		/// <summary>
		/// Validação de número de PIS
		/// </summary>
		/// <exception cref="ArgumentNullException">Exceção lançada quando argumento <paramref name="pis"/>estiver vázio ou nulo.</exception>
		/// <param name="pis">Representa o número do PIS.</param>
		/// <returns></returns>
		public static bool IsPis(string pis)
		{
            if (String.IsNullOrEmpty(pis))
            {
				throw new ArgumentNullException("Argumento pis não pode ser vázio ou nulo.", nameof(pis));
            }

			pis = pis.Trim();
			pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');

			if (pis.Trim().Length != 11)
				return false;

			int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
					
			int soma = 0;
			for (int i = 0; i < 10; i++)
            {
				soma += int.Parse(pis[i].ToString()) * multiplicador[i];
			}

			int resto = (soma % 11) < 2 ? 0 : 11 - (soma % 11);

			return pis.EndsWith(resto.ToString());
		}
	}
}
