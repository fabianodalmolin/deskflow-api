using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeskFlow.API.Models.Entities
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        // Relacionamento: Uma Categoria possui muitos Chamados
        [JsonIgnore]
        public ICollection<Chamado> Chamados { get; set; } = new List<Chamado>();
    }
}
