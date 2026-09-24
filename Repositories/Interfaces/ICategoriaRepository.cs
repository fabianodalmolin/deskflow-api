using DeskFlow.API.Models.Entities;

namespace DeskFlow.API.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> ObterTodasAsync();
        Task<Categoria?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Categoria categoria);
        Task AtualizarAsync(Categoria categoria);
        Task DeletarAsync(Categoria categoria);
    }
}
