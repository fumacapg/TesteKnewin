using Cidades.Dominio;
using Cidades.Persistencia.Interface;
using Cidades.Servico.Interface;
using Cidades.Servico.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cidades.Servico
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioPersistencia _usuarioPersistencia;
        private readonly IPadraoPersistencia _padraoPersistencia;

        public UsuarioServico(IUsuarioPersistencia usuarioPersistencia, IPadraoPersistencia padraoPersistencia)
        {
            _usuarioPersistencia = usuarioPersistencia;
            _padraoPersistencia = padraoPersistencia;
        }

        public async Task<Usuario> AddUsuarios(Usuario model)
        {
            try
            {
                model.Login = model.Login.ToLower();
                model.Senha = Criptografia.Codifica(model.Senha);
                _padraoPersistencia.Add(model);
                if (await _padraoPersistencia.SaveChangesAsync())
                {
                    return await _usuarioPersistencia.GetUsuarioPorIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Usuario> UpdateUsuarios(int usuarioId, Usuario model)
        {
            try
            {
                var evento = await _usuarioPersistencia.GetUsuarioPorIdAsync(usuarioId);
                if (evento == null)
                    return null;

                model.Id = evento.Id;
                model.Login = model.Login.ToLower();
                model.Senha = Criptografia.Codifica(model.Senha);

                _padraoPersistencia.Update(model);
                if (await _padraoPersistencia.SaveChangesAsync())
                {
                    return await _usuarioPersistencia.GetUsuarioPorIdAsync(usuarioId);
                }
                return null;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteUsuarios(int usuarioId)
        {
            try
            {
                var usuario = await _usuarioPersistencia.GetUsuarioPorIdAsync(usuarioId);
                if (usuario == null)
                    throw new Exception("O Evento não foi localizado para exclusão");

                _padraoPersistencia.Delete<Usuario>(usuario);
                return await _padraoPersistencia.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Usuario> AutenticacaoAsync(string login, string senha)
        {
            try
            {
                string senhaCriptografada = Criptografia.Codifica(senha);
                var usuarios = await _usuarioPersistencia.GetUsuarioPorLoginAsync(login);
                if (usuarios == null || !usuarios.Any()) return null;

                foreach(var usuario in usuarios)
                {
                    if (Criptografia.Compara(senha, usuario.Senha))
                        return usuario;
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Usuario> GetUsuarioPorIdAsync(int usuarioId)
        {
            try
            {
                var usuario = await _usuarioPersistencia.GetUsuarioPorIdAsync(usuarioId);
                if (usuario == null) return null;
                return usuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
