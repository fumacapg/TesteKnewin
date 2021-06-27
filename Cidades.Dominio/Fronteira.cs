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
        public int CidadeOrigemId { get; set; }

        public virtual Cidade CidadeOrigem { get; set; }

        public int CidadeFronteiraId { get; set; }

        public virtual Cidade CidadeFronteira { get; set; }
    }
}
