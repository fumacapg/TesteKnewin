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
    public class UsuarioPersistencia : IUsuarioPersistencia
    {
        private readonly Context _context;

        public UsuarioPersistencia(Context context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuarioPorAutenticacaoAsync(string login, string senha)
        {
            IQueryable<Usuario> query = _context.Usuarios.AsNoTrackingWithIdentityResolution();

            query = query.OrderBy(u => u.Id).Where(u => u.Login.ToLower() == login && u.Senha == senha);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Usuario> GetUsuarioPorIdAsync(int usuarioId)
        {
            IQueryable<Usuario> query = _context.Usuarios.AsNoTracking();

            query = query.OrderBy(u => u.Id).Where(u => u.Id == usuarioId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Usuario[]> GetUsuarioPorLoginAsync(string login)
        {
            IQueryable<Usuario> query = _context.Usuarios.AsNoTracking();

            query = query.OrderBy(u => u.Id).Where(u => u.Login.ToLower() == login.ToLower());

            return await query.ToArrayAsync();
        }
    }
}
