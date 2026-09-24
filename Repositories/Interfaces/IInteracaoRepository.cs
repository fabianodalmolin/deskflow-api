using DeskFlow.API.Models.Entities;

namespace DeskFlow.API.Repositories.Interfaces
{
    public interface IInteracaoRepository
    {
        Task AdicionarAsync(Interacao interacao);
        Task<IEnumerable<Interacao>> ObterPorChamadoIdAsync(int chamadoId);
    }
}
