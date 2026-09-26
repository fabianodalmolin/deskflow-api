using DeskFlow.API.Models.Entities;

namespace DeskFlow.API.Services.Interfaces
{
    public interface IChamadoService
    {
        Task<IEnumerable<Chamado>> ListarTodosAsync();
        Task<Chamado?> BuscarPorIdAsync(int id);
        Task<Chamado> CriarAsync(string titulo, string descricao, string prioridade, string solicitanteNome, int categoriaId);
        Task<bool> AtualizarStatusAsync(int id, string novoStatus);
        Task<bool> FinalizarChamadoAsync(int id, string solucao);
        Task<Interacao> AdicionarInteracaoAsync(int chamadoId, string autor, string mensagem);
    }
}
