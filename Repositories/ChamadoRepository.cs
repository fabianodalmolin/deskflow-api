using DeskFlow.API.Data;
using DeskFlow.API.Models.Entities;
using DeskFlow.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.API.Repositories
{
    public class ChamadoRepository : IChamadoRepository
    {
        private readonly AppDbContext _context;

        public ChamadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chamado>> ObterTodosAsync()
        {
            // O Include traz os dados da Categoria vinculada automaticamente para a listagem
            return await _context.Chamados
                .Include(c => c.Categoria)
                .ToListAsync();
        }

        public async Task<Chamado?> ObterPorIdAsync(int id)
        {
            // O Include traz a Categoria e as Interações (comentários) quando abrimos um chamado específico
            return await _context.Chamados
                .Include(c => c.Categoria)
                .Include(c => c.Interacoes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AdicionarAsync(Chamado chamado)
        {
            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Chamado chamado)
        {
            _context.Chamados.Update(chamado);
            await _context.SaveChangesAsync();
        }
    }
}
