using DeskFlow.API.Models.Entities;

namespace DeskFlow.API.Repositories.Interfaces
{
    public interface IChamadoRepository
    {
        Task<IEnumerable<Chamado>> ObterTodosAsync();
        Task<Chamado?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Chamado chamado);
        Task AtualizarAsync(Chamado chamado);
    }
}
