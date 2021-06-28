using Cidades.Dominio;
using Cidades.Persistencia.Contexto;
using Cidades.Persistencia.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cidades.Persistencia
{
    public class CidadePersistencia : ICidadePersistencia
    {
        private readonly Context _context;

        public CidadePersistencia(Context context)
        {
            _context = context;
        }        

        public async Task<Cidade[]> GetAllCidadesAsync(bool includeFronteiras = false)
        {
            IQueryable<Cidade> query = _context.Cidades.AsNoTrackingWithIdentityResolution();

            if (includeFronteiras)
                query = query.Include(c => c.Fronteiras).ThenInclude(f => f.Cidade);

            query = query.OrderBy(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Cidade> GetCidadesPorIdAsync(int cidadeId, bool includeFronteiras = false)
        {
            IQueryable<Cidade> query = _context.Cidades.AsNoTrackingWithIdentityResolution();

            if (includeFronteiras)
                query = query.Include(c => c.Fronteiras).ThenInclude(f => f.Cidade);

            query = query.OrderBy(c => c.Id).Where(c => c.Id == cidadeId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cidade[]> GetCidadesPorNomeAsync(string nome, bool includeFronteiras = false)
        {
            IQueryable<Cidade> query = _context.Cidades.AsNoTrackingWithIdentityResolution();

            if (includeFronteiras)
                query = query.Include(c => c.Fronteiras).ThenInclude(f => f.Cidade);

            query = query.OrderBy(c => c.Id).Where(c => c.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Cidade[]> GetFronteirasAsync(string cidade, bool includeFronteiras = false)
        {
            IQueryable<Cidade> query = _context.Cidades.AsNoTrackingWithIdentityResolution();

            if (includeFronteiras)
                query = query.Include(c => c.Fronteiras).ThenInclude(f => f.Cidade);

            query = query.OrderBy(c => c.Id).Where(c => c.Fronteiras.Any(f => f.CidadeFronteira.ToLower().Contains(cidade)));

            return await query.ToArrayAsync();
        }
    }
}
