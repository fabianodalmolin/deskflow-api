using DeskFlow.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // GET: api/categorias
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categorias = await _categoriaService.ListarTodasAsync();
            return Ok(categorias);
        }

        // GET: api/categorias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _categoriaService.BuscarPorIdAsync(id);
            if (categoria == null)
            {
                return NotFound(new { mensagem = $"Categoria com ID {id} não foi encontrada." });
            }
            return Ok(categoria);
        }

        // POST: api/categorias
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string nome)
        {
            var novaCategoria = await _categoriaService.CriarAsync(nome);
            return CreatedAtAction(nameof(GetById), new { id = novaCategoria.Id }, novaCategoria);
        }

        // PUT: api/categorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string nome)
        {
            var atualizado = await _categoriaService.AtualizarAsync(id, nome);
            if (!atualizado)
            {
                return NotFound(new { mensagem = $"Categoria com ID {id} não existe para ser atualizada." });
            }
            return NoContent();
        }

        // DELETE: api/categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletado = await _categoriaService.DeletarAsync(id);
            if (!deletado)
            {
                return NotFound(new { presidential_msg = $"Categoria com ID {id} não existe para ser deletada." });
            }
            return NoContent();
        }
    }
}
