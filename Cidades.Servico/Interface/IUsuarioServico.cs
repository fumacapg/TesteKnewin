using Cidades.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cidades.Servico.Interface
{
    public interface IUsuarioServico
    {
        Task<Usuario> AddUsuarios(Usuario model);

        Task<Usuario> UpdateUsuarios(int usuarioId, Usuario model);

        Task<bool> DeleteUsuarios(int usuarioId);

        Task<Usuario> AutenticacaoAsync(string login, string senha);

        Task<Usuario> GetUsuarioPorIdAsync(int usuarioId);
    }
}
