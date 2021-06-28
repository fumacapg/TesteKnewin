using Cidades.Dominio;
using Cidades.Servico.Interface;
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
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeServico _cidadeServico;
        public CidadeController(ICidadeServico cidadeServico)
        {
            _cidadeServico = cidadeServico;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cidades = await _cidadeServico.GetAllCidadesAsync(true);
                if (cidades == null)
                    return NotFound("Nenhuma cidade encontrada");
                return Ok(cidades);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar as cidades. Erro: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cidade = await _cidadeServico.GetCidadesPorIdAsync(id, true);
                if (cidade == null)
                    return NotFound($"Nenhuma cidade encontrada com o id: {id}");
                return Ok(cidade);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar a cidade pelo id: {id}. Erro: {e.Message}");
            }
        }

        [HttpGet("nome/{nome}")]
        [Authorize]
        public async Task<IActionResult> GetByNome(string nome)
        {
            try
            {
                var cidades = await _cidadeServico.GetCidadesPorNomeAsync(nome, true);
                if (cidades == null)
                    return NotFound($"Nenhuma cidade encontrada com o nome: {nome.ToUpper()}");
                return Ok(cidades);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar a cidade pelo nome: {nome}. Erro: {e.Message}");
            }
        }

        [HttpGet("fronteiras/{cidadeNome}")]
        [Authorize]
        public async Task<IActionResult> GetFronteiras(string cidadeNome)
        {
            try
            {
                var cidades = await _cidadeServico.GetFronteirasAsync(cidadeNome, true);
                if (cidades == null)
                    return NotFound($"Nenhuma fronteira encontrada para a cidade {cidadeNome.ToUpper()}");
                return Ok(cidades);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar as fronteiras da cidade: {cidadeNome.ToUpper()}. Erro: {e.Message}");
            }
        }

        [HttpPost("habitantes")]
        [Authorize]
        public async Task<IActionResult> GetNumeroHabitantes(int[] cidades)
        {
            try
            {
                var habitantes = await _cidadeServico.GetNumeroHabitantesAsync(cidades);
                if(String.IsNullOrEmpty(habitantes))
                    return NotFound($"Nenhuma das cidades ({string.Join(", ", cidades)}) foi encontrada");
                return Ok(habitantes);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o número de hábitantes das cidades: {cidades}. Erro: {e.Message}");
            }
        }

        [HttpGet("rotas/{id}")]
        [Authorize]
        public async Task<IActionResult> GetRotas(int id)
        {
            try
            {
                var retorno = await _cidadeServico.GetRotasAsync(id);
                if (String.IsNullOrEmpty(retorno))
                    return NotFound($"Nenhuma cidade encontrada com o id: {id}");
                return Ok(retorno);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar as fronteiras da cidade id: {id}. Erro: {e.Message}");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(Cidade model)
        {
            try
            {
                var cidade = await _cidadeServico.AddCidades(model);
                if (cidade == null)
                    return BadRequest("Erro ao adicionar cidade");
                return Ok(cidade);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar uma cidade. Erro: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, Cidade model)
        {
            try
            {
                var cidade = await _cidadeServico.UpdateCidades(id, model);
                if (cidade == null)
                    return BadRequest($"Erro ao alterar a cidade id: {id}");
                return Ok(cidade);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar alterar uma cidade. Erro: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _cidadeServico.DeleteCidades(id))
                    return Ok("Excluído com sucesso");
                return BadRequest($"Erro ao excluir a cidade id: {id}");

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir uma cidade. Erro: {e.Message}");
            }
        }
    }
}
