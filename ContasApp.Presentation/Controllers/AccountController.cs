using ContasApp.Data.Entities;
using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json.Serialization;

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
        /// Método para capturar o SUBMIT POST da página /Account/Login
        /// </summary>
        [HttpPost]
        public IActionResult Login(AccountLoginViewModel model)
        {
            // verificando se todos os campos passaram nas regras de validação
            if(ModelState.IsValid)
            {
                try
                {
                    // consultando o usuário no banco de dados através do email e da senha
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmailAndSenha(model.Email, model.Senha);

                    // verificando se o usuário foi encontrado
                    if(usuario != null)
                    {
                        // gravando o cookie no navegador com os dados do usuário autenticado
                        // este cookie irá gerar autorização do usuário para acessar as páginas restritas do sistema [Authorize]

                        // serializando o objeto usuario para JSON
                        var json = JsonConvert.SerializeObject(usuario);

                        // criando a identificação do usuário no Asp.Net
                        var claimsIdentity = new ClaimsIdentity
                            (new[] { new Claim(ClaimTypes.Name, json) }, CookieAuthenticationDefaults.AuthenticationScheme);                            

                        // gravando os dados no Cookie de autenticação do Asp.Net
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                        // redirecionando para outra página HOME -> Controller, Index -> View (Home/Index)
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Mensagem"] = "Acesso negado. Usuário ou Senha inválidos";
                    }
                    
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

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
                    var usuarioRepository = new UsuarioRepository();
                    if (usuarioRepository.GetByEmail(model.Email) != null)
                    {
                        TempData["Mensagem"] = "O email informado já está cadastrado, por favor tente outro.";
                    }
                    else
                    {
                        // capturando os dados do usuário
                        var usuario = new Usuario();

                        usuario.Id = Guid.NewGuid();
                        usuario.Nome = model.Nome;
                        usuario.Email = model.Email;
                        usuario.Senha = model.Senha;
                        usuario.DataCriacao = DateTime.Now;

                        // gravando o usuário no banco de dados
                        usuarioRepository.Add(usuario);

                        TempData["Mensagem"] = "Parabéns, sua conta de usuário foi cadastrada com sucesso.";
                    }
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

        /// <summary>
        /// Método para fazer o logou do sistema
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            // apagar o cookie de autenticação gravado no navegador
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // redirecionar o usuário de volta para a página de Login
            return RedirectToAction("Login");
        }
    }
}
