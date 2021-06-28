using Cidades.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cidades.Servico.Interface.Seguranca
{
    public interface ITokenServico
    {
        string GenerateToken(Usuario usuario);
    }
}
