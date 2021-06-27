using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cidades.Dominio
{
    public class Cidade
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int NumeroHabitantes { get; set; }

        //public int? CidadeId { get; set; }

        //public Cidade Fronteira { get; set; }

        //public IEnumerable<Cidade> Fronteiras { get; set; }

        public IEnumerable<Fronteira> Fronteiras { get; set; }
        public IEnumerable<Fronteira> Origens { get; set; }
    }
}
