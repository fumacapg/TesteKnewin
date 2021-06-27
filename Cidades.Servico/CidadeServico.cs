using Cidades.Dominio;
using Cidades.Persistencia.Interface;
using Cidades.Servico.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cidades.Servico
{
    public class CidadeServico : ICidadeServico
    {
        private readonly ICidadePersistencia _cidadePersistencia;

        public CidadeServico(ICidadePersistencia cidadePersistencia)
        {
            _cidadePersistencia = cidadePersistencia;
        }

        public async Task<Cidade> AddCidades(Cidade model)
        {
            try
            {
                _cidadePersistencia.Add(model);
                if (await _cidadePersistencia.SaveChangesAsync())
                {
                    return await _cidadePersistencia.GetCidadesPorIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Cidade> UpdateCidades(int cidadeId, Cidade model)
        {
            try
            {
                var cidade = await _cidadePersistencia.GetCidadesPorIdAsync(cidadeId, false);
                if (cidade == null)
                    throw new Exception($"A Cidade: {cidadeId} não foi localizada para alteração");

                model.Id = cidade.Id;

                _cidadePersistencia.Update(model);
                if (await _cidadePersistencia.SaveChangesAsync())
                {
                    return await _cidadePersistencia.GetCidadesPorIdAsync(model.Id, false);
                }
                return null;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteCidades(int cidadeId)
        {
            try
            {
                var cidade = await _cidadePersistencia.GetCidadesPorIdAsync(cidadeId, false);
                if (cidade == null)
                    throw new Exception($"A Cidade: {cidadeId} não foi localizada para exclusão");

                _cidadePersistencia.Delete(cidade);
                return await _cidadePersistencia.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Cidade[]> GetAllCidadesAsync(bool includeFronteiras = false)
        {
            try
            {
                var cidades = await _cidadePersistencia.GetAllCidadesAsync(includeFronteiras);
                if (cidades == null) return null;
                return cidades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Cidade> GetCidadesPorIdAsync(int cidadeId, bool includeFronteiras = false)
        {
            try
            {
                var cidade = await _cidadePersistencia.GetCidadesPorIdAsync(cidadeId, includeFronteiras);
                if (cidade == null) return null;
                return cidade;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Cidade[]> GetCidadesPorNomeAsync(string nome, bool includeFronteiras = false)
        {
            try
            {
                var cidades = await _cidadePersistencia.GetCidadesPorNomeAsync(nome, includeFronteiras);
                if (cidades == null) return null;
                return cidades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Cidade[]> GetFronteirasAsync(int cidadeOrigemId, bool includeFronteiras = false)
        {
            try
            {
                var cidades = await _cidadePersistencia.GetFronteirasAsync(cidadeOrigemId, includeFronteiras);
                if (cidades == null || !cidades.Any()) return null;                
                return cidades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> GetNumeroHabitantesAsync(int[] cidadesId)
        {
            try
            {
                List<Cidade> cidades = new();
                Cidade cidade;
                string cidadesEncontradas = "";
                string cidadesNaoEncontradas = "";

                foreach (int id in cidadesId)
                {
                    cidade = await _cidadePersistencia.GetCidadesPorIdAsync(id, false);
                    if (cidade == null)
                        cidadesNaoEncontradas += $"A cidade de id: {id} não foi localizada!\n";
                    else
                    {
                        if (String.IsNullOrEmpty(cidadesEncontradas))
                            cidadesEncontradas = "A soma de habitantes das cidades: ";
                        cidadesEncontradas += $"{cidade.Nome}, ";
                        cidades.Add(cidade);
                    }
                }
                var retorno = $"{cidadesEncontradas[0..^2]} é de: {string.Format("{0:0,0}", cidades.Sum(c => c.NumeroHabitantes))}\n{cidadesNaoEncontradas}";
                return retorno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> GetRotasAsync(int cidadeId)
        {
            try
            {
                var cidade = await _cidadePersistencia.GetCidadesPorIdAsync(cidadeId, true);
                if (cidade == null) return null;

                if (cidade.Fronteiras != null && cidade.Fronteiras.Any())
                {
                    string retorno = $"Saindo de {cidade.Nome} você pode ir para ";

                    foreach (Fronteira fronteira in cidade.Fronteiras)
                    {
                        retorno += $"{fronteira.CidadeFronteira.Nome}, ";
                    }

                    return retorno[0..^2];
                }
                else
                    return $"A cidade de {cidade.Nome} não permite que se chegue a lugar algum";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        
    }
}
