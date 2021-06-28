using Cidades.Dominio;
using Cidades.Servico.Interface;
using Cidades.Servico.Interface.Seguranca;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cidades.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly ITokenServico _tokenServico;

        public LoginController(IUsuarioServico usuarioServico, ITokenServico tokenServico)
        {
            _usuarioServico = usuarioServico;
            _tokenServico = tokenServico;
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Token(Usuario model)
        {
            var usuario = await _usuarioServico.AutenticacaoAsync(model.Login, model.Senha);

            if (usuario == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = _tokenServico.GenerateToken(usuario);

            usuario.Senha = "";

            return new
            {
                usuario,
                token
            };
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(Usuario model)
        {
            try
            {
                var usuario = await _usuarioServico.AddUsuarios(model);
                if (usuario == null)
                    return BadRequest("Erro ao adicionar um usuário");
                return Ok(usuario);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar um usuário. Erro: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, Usuario model)
        {
            try
            {
                var usuario = await _usuarioServico.UpdateUsuarios(id, model);
                if (usuario == null)
                    return BadRequest($"Erro ao alterar o usuário id: {id}");
                return Ok(usuario);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar alterar um usuário. Erro: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _usuarioServico.DeleteUsuarios(id))
                    return Ok("Excluído com sucesso");
                return BadRequest($"Erro ao excluir o usuário id: {id}");

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir um usuário. Erro: {e.Message}");
            }
        }
    }
}
