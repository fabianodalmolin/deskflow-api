using DeskFlow.API.Data;
using DeskFlow.API.Models.Entities;
using DeskFlow.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.API.Repositories
{
    public class InteracaoRepository : IInteracaoRepository
    {
        private readonly AppDbContext _context;

        public InteracaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Interacao interacao)
        {
            await _context.Interacoes.AddAsync(interacao);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Interacao>> ObterPorChamadoIdAsync(int chamadoId)
        {
            return await _context.Interacoes
                .Where(i => i.ChamadoId == chamadoId)
                .OrderByDescending(i => i.DataRegistro) // Traz as mensagens mais recentes primeiro
                .ToListAsync();
        }
    }
}
