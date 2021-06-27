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

        public void Add(Cidade entity)
        {
            _context.Add(entity);
        }

        public void Update(Cidade entity)
        {
            _context.Update(entity);
        }

        public void Delete(Cidade entity)
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Cidade[]> GetAllCidadesAsync(bool includeFronteiras = false)
        {
            IQueryable<Cidade> query = _context.Cidades;

            if (includeFronteiras)
                query = query.Include(c => c.Fronteiras).ThenInclude(f => f.CidadeFronteira);

            query = query.OrderBy(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Cidade> GetCidadesPorIdAsync(int cidadeId, bool includeFronteiras = false)
        {
            IQueryable<Cidade> query = _context.Cidades;

            if (includeFronteiras)
                query = query.Include(c => c.Fronteiras).ThenInclude(f => f.CidadeFronteira);

            query = query.AsNoTracking().OrderBy(c => c.Id).Where(c => c.Id == cidadeId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cidade[]> GetCidadesPorNomeAsync(string nome, bool includeFronteiras = false)
        {
            IQueryable<Cidade> query = _context.Cidades;

            if (includeFronteiras)
                query = query.Include(c => c.Fronteiras).ThenInclude(f => f.CidadeFronteira);

            query = query.OrderBy(c => c.Id).Where(c => c.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Cidade[]> GetFronteirasAsync(int cidadeOrigemId, bool includeFronteiras = false)
        {
            IQueryable<Cidade> query = _context.Cidades;

            if (includeFronteiras)
                query = query.Include(c => c.Fronteiras).ThenInclude(f => f.CidadeFronteira);

            query = query.OrderBy(c => c.Id).Where(c => c.Origens.Any(f => f.CidadeOrigemId == cidadeOrigemId));

            return await query.ToArrayAsync();
        }
    }
}
