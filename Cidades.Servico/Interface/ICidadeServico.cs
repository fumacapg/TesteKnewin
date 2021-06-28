using Cidades.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cidades.Servico.Interface
{
    public interface ICidadeServico
    {
        Task<Cidade> AddCidades(Cidade model);
        Task<Cidade> UpdateCidades(int cidadeId, Cidade model);
        Task<bool> DeleteCidades(int cidadeId);
        Task<Cidade[]> GetAllCidadesAsync(bool includeFronteiras = false);
        Task<Cidade> GetCidadesPorIdAsync(int cidadeId, bool includeFronteiras = false);
        Task<Cidade[]> GetCidadesPorNomeAsync(string nome, bool includeFronteiras = false);
        Task<Cidade[]> GetFronteirasAsync(string cidade, bool includeFronteiras = false);
        Task<string> GetNumeroHabitantesAsync(int[] cidadesId);
        Task<string> GetRotasAsync(int cidadeId);
    }
}
