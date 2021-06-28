using Cidades.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cidades.Persistencia.Interface
{
    public interface ICidadePersistencia
    {
        Task<Cidade[]> GetAllCidadesAsync(bool includeFronteiras = false);
        Task<Cidade> GetCidadesPorIdAsync(int cidadeId, bool includeFronteiras = false);
        Task<Cidade[]> GetCidadesPorNomeAsync(string nome, bool includeFronteiras = false);
        Task<Cidade[]> GetFronteirasAsync(string cidade, bool includeFronteiras = false);
    }
}
