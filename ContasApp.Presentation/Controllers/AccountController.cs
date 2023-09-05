using ContasApp.Data.Entities;
using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContasApp.Presentation.Controllers
{
    /// <summary>
    /// Classe de controle do Asp.NET MVC
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Método para abrir a página / Account/Login
        /// </summary>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Método para abrir a página / Account/Register
        /// </summary>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Método para abrir a página /Account/Passowrd
        /// </summary>
        [HttpPost] // Receber p SUBMIT do formulário
        public IActionResult Register(AccountRegisterViewModel model)
        {
            // verificar se todos os campos passaram nas regras de validação
            if(ModelState.IsValid)
            {
                try
                {
                    // capturando os dados do usuário
                    var usuario = new Usuario();

                    usuario.Id = Guid.NewGuid();
                    usuario.Nome = model.Nome;
                    usuario.Email = model.Email;
                    usuario.Senha = model.Senha;
                    usuario.DataCriacao = DateTime.Now;

                    // gravando o usuário no banco de dados
                    var usuarioRepository = new UsuarioRepository();
                    usuarioRepository.Add(usuario);

                    TempData["Mensagem"] = "Parabéns, sua conta de usuário foi cadastrada com sucesso.";
                }
                catch(Exception e) 
                {
                    TempData["Mensagem"] = "Erro ao cadastra o usuário: " + e.Message;
                }
            }

            return View();
        }

        /// <summary>
        /// Método para abrir a página / Account/ForgotPassword
        /// </summary>
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
