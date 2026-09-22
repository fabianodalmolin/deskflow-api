using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DeskFlow.API.Models.Entities
{
    public class Interacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ChamadoId { get; set; }

        [ForeignKey("ChamadoId")]
        [JsonIgnore]
        public Chamado? Chamado { get; set; }

        [Required]
        [StringLength(100)]
        public string Autor { get; set; } = string.Empty;

        [Required]
        public string Mensagem { get; set; } = string.Empty;

        public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
    }
}
