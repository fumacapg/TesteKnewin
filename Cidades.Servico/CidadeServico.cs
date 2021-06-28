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
        private readonly IPadraoPersistencia _padraoPersistencia;

        public CidadeServico(ICidadePersistencia cidadePersistencia, IPadraoPersistencia padraoPersistencia)
        {
            _cidadePersistencia = cidadePersistencia;
            _padraoPersistencia = padraoPersistencia;
        }

        public async Task<Cidade> AddCidades(Cidade model)
        {
            try
            {
                _padraoPersistencia.Add(model);
                if (await _padraoPersistencia.SaveChangesAsync())
                {
                    return await _cidadePersistencia.GetCidadesPorIdAsync(model.Id, true);
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

                _padraoPersistencia.Update(model);
                if (await _padraoPersistencia.SaveChangesAsync())
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

                _padraoPersistencia.Delete(cidade);
                return await _padraoPersistencia.SaveChangesAsync();

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
                if (cidades == null || !cidades.Any()) return null;
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
                if (cidades == null || !cidades.Any()) return null;
                return cidades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Cidade[]> GetFronteirasAsync(string cidade, bool includeFronteiras = false)
        {
            try
            {
                var cidades = await _cidadePersistencia.GetFronteirasAsync(cidade, includeFronteiras);
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
                if (String.IsNullOrEmpty(cidadesEncontradas))
                    return null;
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

                    Fronteira ultimo = cidade.Fronteiras.Last();

                    foreach (Fronteira fronteira in cidade.Fronteiras)
                    {
                        if(fronteira == ultimo)
                        {
                            retorno = retorno[0..^2] + $" e {fronteira.CidadeFronteira}";
                        }
                        else
                            retorno += $"{fronteira.CidadeFronteira}, ";
                    }

                    return retorno;
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
