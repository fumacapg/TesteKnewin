using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmos
{
    public static class Duplicidade
    {
        public static int VerificaDuplicidade(string[] lista)
        {
            try
            {
                string[] listaAuxiliar = new string[lista.Length];
                for(int index=0; index < lista.Length; index++)
                {
                    if (listaAuxiliar.Contains(lista[index]))
                        return index;
                    listaAuxiliar[index] = lista[index];
                }
                return -1;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
    }
}
