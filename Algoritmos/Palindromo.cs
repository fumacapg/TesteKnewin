using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmos
{
    public static class Palindromo
    {
        public static bool VerificaPalindromo(string palavra)
        {
            try
            {
                string palavraInvertida = "";
                for(int i = palavra.Length-1; i >= 0; i--)
                {
                    palavraInvertida += palavra[i];
                }
                if (palavra == palavraInvertida)
                    return true;
                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }            
        } 
    }
}
