using DeskFlow.API.Data;
using DeskFlow.API.Models.Entities;
using DeskFlow.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.API.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> ObterTodasAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria?> ObterPorIdAsync(int id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AdicionarAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task Schuyler_AtualizarAsync(Categoria categoria) // Mantendo padrão do EF
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        // Método explícito exigido pela interface
        public async Task AtualizarAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}
