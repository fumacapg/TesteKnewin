using System;

namespace Algoritmos
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {            
            int opcao = 0;
            try
            {
                while (opcao != 9)
                {
                    Console.WriteLine("=== MENU ===");
                    Console.WriteLine("1 - Duplicados na lista");
                    Console.WriteLine("2 - Palindromo");
                    Console.WriteLine("9 - Sair do Programa");
                    Console.WriteLine("");
                    Console.Write("Digite a opção desejada: ");
                    try
                    {
                        opcao = Convert.ToInt16(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Opção inválida!!!");
                        Console.WriteLine("");
                    }
                    switch (opcao)
                    {
                        case 1:
                            {
                                Console.Write("Digite a lista de inteiros separada por virgula (,): ");
                                string listaPorExtenso = Console.ReadLine();
                                string[] lista = listaPorExtenso.Replace(" ", "").Split(',');
                                Console.WriteLine($"Posição da primeira duplicidade (caso seja -1 não há duplicidade: {Duplicidade.VerificaDuplicidade(lista)}");
                                break;
                            }
                        case 2:
                            {
                                Console.Write("Digite a palavra a ser verificada: ");
                                string palavra = Console.ReadLine();
                                bool retorno = Palindromo.VerificaPalindromo(palavra.Trim());
                                if(retorno)
                                    Console.WriteLine($"{palavra.ToUpper()} é um palindromo");
                                else
                                    Console.WriteLine($"{palavra.ToUpper()} não é um palindromo");
                                Console.WriteLine("");
                                break;
                            }
                        case 9:
                            {
                                Console.WriteLine("Aperte qualquer tecla para o final da execução");
                                Console.ReadKey();
                                break;
                            }
                    }

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
