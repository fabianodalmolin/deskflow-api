using DeskFlow.API.Models.Entities;
using DeskFlow.API.Repositories.Interfaces;
using DeskFlow.API.Services.Interfaces;

namespace DeskFlow.API.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<Categoria>> ListarTodasAsync()
        {
            return await _categoriaRepository.ObterTodasAsync();
        }

        public async Task<Categoria?> BuscarPorIdAsync(int id)
        {
            return await _categoriaRepository.ObterPorIdAsync(id);
        }

        public async Task<Categoria> CriarAsync(string nome)
        {
            // Regra de Negócio: Impede categorias com nome em branco
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }

            var novaCategoria = new Categoria { Nome = nome.Trim() };
            await _categoriaRepository.AdicionarAsync(novaCategoria);
            return novaCategoria;
        }

        public async Task<bool> AtualizarAsync(int id, string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome da categoria é obrigatório para atualização.");
            }

            var categoriaExistente = await _categoriaRepository.ObterPorIdAsync(id);
            if (categoriaExistente == null)
            {
                return false; // Retorna falso se a categoria não existir no banco
            }

            categoriaExistente.Nome = nome.Trim();
            await _categoriaRepository.AtualizarAsync(categoriaExistente);
            return true;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var categoriaExistente = await _categoriaRepository.ObterPorIdAsync(id);
            if (categoriaExistente == null)
            {
                return false;
            }

            await _categoriaRepository.DeletarAsync(categoriaExistente);
            return true;
        }
    }
}
