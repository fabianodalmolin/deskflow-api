using DeskFlow.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadosController : ControllerBase
    {
        private readonly IChamadoService _chamadoService;

        public ChamadosController(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
        }

        // GET: api/chamados
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var chamados = await _chamadoService.ListarTodosAsync();
            return Ok(chamados);
        }

        // GET: api/chamados/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var chamado = await _chamadoService.BuscarPorIdAsync(id);
            if (chamado == null)
            {
                return NotFound(new { mensagem = $"Chamado com ID {id} não foi encontrado." });
            }
            return Ok(chamado);
        }

        // Modelos de Entrada Auxiliares (DTOs locais simples)
        public record CriarChamadoInput(string Titulo, string Descricao, string Prioridade, string SolicitanteNome, int CategoriaId);
        public record AtualizarStatusInput(string Status);
        public record FinalizarChamadoInput(string Solucao);
        public record CriarInteracaoInput(string Autor, string Mensagem);

        // POST: api/chamados
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarChamadoInput input)
        {
            var novoChamado = await _chamadoService.CriarAsync(input.Titulo, input.Descricao, input.Prioridade, input.SolicitanteNome, input.CategoriaId);
            return CreatedAtAction(nameof(GetById), new { id = novoChamado.Id }, novoChamado);
        }

        // PUT: api/chamados/5/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] AtualizarStatusInput input)
        {
            var atualizado = await _chamadoService.AtualizarStatusAsync(id, input.Status);
            if (!atualizado)
            {
                return NotFound(new { mensagem = $"Chamado com ID {id} não foi encontrado." });
            }
            return NoContent();
        }

        // PUT: api/chamados/5/finalizar
        [HttpPut("{id}/finalizar")]
        public async Task<IActionResult> Finalizar(int id, [FromBody] FinalizarChamadoInput input)
        {
            var finalizado = await _chamadoService.FinalizarChamadoAsync(id, input.Solucao);
            if (!finalizado)
            {
                return NotFound(new { mensagem = $"Chamado com ID {id} não foi encontrado." });
            }
            return NoContent();
        }

        // POST: api/chamados/5/interacoes
        [HttpPost("{id}/interacoes")]
        public async Task<IActionResult> PostInteracao(int id, [FromBody] CriarInteracaoInput input)
        {
            var novaInteracao = await _chamadoService.AdicionarInteracaoAsync(id, input.Autor, input.Mensagem);
            return CreatedAtAction(nameof(GetById), new { id = id }, novaInteracao);
        }
    }
}
