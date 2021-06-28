using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cidades.Dominio
{
    public class Fronteira
    {
        public int Id { get; set; }

        public string CidadeFronteira { get; set; }

        public int CidadeId { get; set; }

        public Cidade Cidade { get; set; }        
    }
}
