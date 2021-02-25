using System;
using pManagerLibrary;
using pManagerLibrary.Entities;

namespace pTesteClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciado a teste de Validação de CPF");

            Console.WriteLine("\nDigite um número de CPF:");
            string str = Console.ReadLine();
            if (Biblio.IsCPF(str))
            {
                Console.WriteLine("CPF Válido");
            }
            else
            {
                Console.WriteLine("CPF Inválido");
            }

            Console.WriteLine("\nDigite um número de CNPJ:");
            str = Console.ReadLine();
            if (Biblio.IsCNPJ(str))
            {
                Console.WriteLine("CNPJ Válido");
            }
            else
            {
                Console.WriteLine("CNPJ Inválido");
            }

            Console.ReadLine();
        }
    }
}
