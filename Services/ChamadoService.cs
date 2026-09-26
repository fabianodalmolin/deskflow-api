using DeskFlow.API.Models.Entities;
using DeskFlow.API.Repositories.Interfaces;
using DeskFlow.API.Services.Interfaces;

namespace DeskFlow.API.Services
{
    public class ChamadoService : IChamadoService
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IInteracaoRepository _interacaoRepository;

        public ChamadoService(
            IChamadoRepository chamadoRepository, 
            ICategoriaRepository categoriaRepository,
            IInteracaoRepository interacaoRepository)
        {
            _chamadoRepository = chamadoRepository;
            _categoriaRepository = categoriaRepository;
            _interacaoRepository = interacaoRepository;
        }

        public async Task<IEnumerable<Chamado>> ListarTodosAsync()
        {
            return await _chamadoRepository.ObterTodosAsync();
        }

        public async Task<Chamado?> BuscarPorIdAsync(int id)
        {
            return await _chamadoRepository.ObterPorIdAsync(id);
        }

        public async Task<Chamado> CriarAsync(string titulo, string descricao, string prioridade, string solicitanteNome, int categoriaId)
        {
            // Validações básicas de negócio
            if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("O título é obrigatório.");
            if (string.IsNullOrWhiteSpace(solicitanteNome)) throw new ArgumentException("O nome do solicitante é obrigatório.");

            // Verifica se a categoria informada realmente existe no banco
            var categoria = await _categoriaRepository.ObterPorIdAsync(categoriaId);
            if (categoria == null) throw new ArgumentException("A categoria informada não existe.");

            var novoChamado = new Chamado
            {
                Titulo = titulo.Trim(),
                Descricao = descricao.Trim(),
                Prioridade = prioridade,
                SolicitanteNome = solicitanteNome.Trim(),
                CategoriaId = categoriaId,
                Status = "Aberto", // Todo chamado nasce Aberto
                DataAbertura = DateTime.UtcNow
            };

            await _chamadoRepository.AdicionarAsync(novoChamado);
            return novoChamado;
        }

        public async Task<bool> AtualizarStatusAsync(int id, string novoStatus)
        {
            var chamado = await _chamadoRepository.ObterPorIdAsync(id);
            if (chamado == null) return false;

            // Impede alterações simples de status se o chamado já foi encerrado
            if (chamado.Status == "Fechado")
            {
                throw new InvalidOperationException("Não é possível alterar o status de um chamado que já está fechado.");
            }

            chamado.Status = novoStatus;
            await _chamadoRepository.AtualizarAsync(chamado);
            return true;
        }

        public async Task<bool> FinalizarChamadoAsync(int id, string solucao)
        {
            if (string.IsNullOrWhiteSpace(solucao))
            {
                throw new ArgumentException("É obrigatório informar a solução para finalizar o chamado.");
            }

            var chamado = await _chamadoRepository.ObterPorIdAsync(id);
            if (chamado == null) return false;

            chamado.Status = "Fechado";
            chamado.Solucao = solucao.Trim();
            chamado.DataFechamento = DateTime.UtcNow; // Salva o momento exato do fechamento

            await _chamadoRepository.AtualizarAsync(chamado);
            return true;
        }

        public async Task<Interacao> AdicionarInteracaoAsync(int chamadoId, string autor, string mensagem)
        {
            if (string.IsNullOrWhiteSpace(autor)) throw new ArgumentException("O autor da interação é obrigatório.");
            if (string.IsNullOrWhiteSpace(mensagem)) throw new ArgumentException("A mensagem não pode estar vazia.");

            var chamado = await _chamadoRepository.ObterPorIdAsync(chamadoId);
            if (chamado == null) throw new ArgumentException("O chamado informado não existe.");

            // Regra de Negócio: Bloqueia interações em tickets finalizados
            if (chamado.Status == "Fechado")
            {
                throw new InvalidOperationException("Não é possível adicionar comentários em um chamado fechado.");
            }

            var novaInteracao = new Interacao
            {
                ChamadoId = chamadoId,
                Autor = autor.Trim(),
                Mensagem = mensagem.Trim(),
                DataRegistro = DateTime.UtcNow
            };

            await _interacaoRepository.AdicionarAsync(novaInteracao);
            return novaInteracao;
        }
    }
}
