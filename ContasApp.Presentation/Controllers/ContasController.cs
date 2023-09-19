using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContasApp.Presentation.Controllers
{
    [Authorize]
    public class ContasController : Controller
    {
        /// <summary>
        /// Método para abrir a página /Contas/Cadastro
        /// <summary>
        public IActionResult Cadastro()
        {
            return View();
        }

        /// <summary>
        /// Método para abrir a página /Contas/Consulta
        /// </summary>
        public IActionResult Consulta()
        {
            return View();
        }

        /// <summary>
        /// Método para abrir a página /Contas/Edicao
        /// </summary>
        public IActionResult Edicao()
        {
            return View();
        }
    }
}
