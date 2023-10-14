using System.ComponentModel.DataAnnotations;

namespace ContasApp.Presentation.Models
{
    /// <summary>
    /// Modelo de dados da página de recuperação de senha
    /// </summary>
    public class AccountForgotPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de e-mail válido.")]
        [Required(ErrorMessage = "Por favor, informe o e-mail do usuário.")]
        public string? Email { get; set; }
    }
}
