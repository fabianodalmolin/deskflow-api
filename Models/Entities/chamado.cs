using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskFlow.API.Models.Entities
{
    public class Chamado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public string Prioridade { get; set; } = "Baixa"; // Baixa, Media, Alta

        [Required]
        public string Status { get; set; } = "Aberto"; // Aberto, EmAndamento, Fechado

        [Required]
        [StringLength(100)]
        public string SolicitanteNome { get; set; } = string.Empty;

        public DateTime DataAbertura { get; set; } = DateTime.UtcNow;

        public DateTime? DataFechamento { get; set; }

        public string? Solucao { get; set; }

        // Chave Estrangeira para Categoria
        [Required]
        public int CategoriaId { get; set; }
        
        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }

        // Relacionamento: Um Chamado possui muitas Interações (comentários)
        public ICollection<Interacao> Interacoes { get; set; } = new List<Interacao>();
    }
}
