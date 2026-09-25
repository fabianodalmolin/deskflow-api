using DeskFlow.API.Models.Entities;

namespace DeskFlow.API.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> ListarTodasAsync();
        Task<Categoria?> BuscarPorIdAsync(int id);
        Task<Categoria> CriarAsync(string nome);
        Task<bool> AtualizarAsync(int id, string nome);
        Task<bool> DeletarAsync(int id);
    }
}
