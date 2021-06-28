using Cidades.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cidades.Persistencia.Interface
{
    public interface IUsuarioPersistencia
    {
        Task<Usuario> GetUsuarioPorAutenticacaoAsync(string login, string senha);
        Task<Usuario> GetUsuarioPorIdAsync(int usuarioId);

        Task<Usuario[]> GetUsuarioPorLoginAsync(string login);
    }
}
