using System.ComponentModel.DataAnnotations;

namespace ContasApp.Presentation.Models
{
    public class ContasEdicaoViewModel
    {
        public Guid Id { get; set; } // campo oculto no formulário

        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome da conta.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data da conta.")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Por favor, informe o valor da conta.")]
        public decimal? Valor { get; set; }

        [Required(ErrorMessage = "Por favor, informe o tipo da conta.")]
        public int? Tipo { get; set; }

        [Required(ErrorMessage = "Por favor, selecione a categoria da conta.")]
        public Guid? CategoriaId { get; set; }

        [MaxLength(500, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe as observações da conta.")]
        public string? Observacoes { get; set; }
    }
}
