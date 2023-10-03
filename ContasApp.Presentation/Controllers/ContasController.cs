using ContasApp.Data.Entities;
using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

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
            ViewBag.Categorias = ObterCategorias();
            return View();
        }

        /// <summary>
        /// Métodp para capturar o SUBMIT POST do formulário
        /// </summary>
        [HttpPost]
        public IActionResult Cadastro(ContasCadastroViewModel model)
        {
            // verificar se os campos passaram nas regras de validação
            if(ModelState.IsValid)
            {
                try
                {
                    // capturar o usuário autenticado no arquivo de cookie do ASP.NET
                    var usuario = JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);

                    // capturando os dados da conta
                    var conta = new Conta
                    {
                        Id = Guid.NewGuid(),
                        Nome = model.Nome,
                        Data = model.Data.Value,
                        Valor = model.Valor.Value,
                        Tipo = model.Tipo.Value,
                        Observacoes = model.Observacoes,
                        CategoriaId = model.CategoriaId.Value,
                        UsuarioId = usuario.Id
                    };

                    // gravar a conta no banco de dados
                    var contaRepository = new ContaRepository();
                    contaRepository.Add(conta);

                    TempData["MensagemSucesso"] = "Conta cadastrada com sucesso!";

                    ModelState.Clear(); // limpar os campos do formulário
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preenchimento do " +
                    "formulário de cadastro, por favor verifique.";
            }

            ViewBag.Categorias = ObterCategorias();
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
        /// Método para capturar o SUBMIT POST da página de consulta
        /// </summary>
        [HttpPost]
        public IActionResult Consulta(ContasConsultaViewModel model)
        {

        }

        /// <summary>
        /// Método para abrir a página /Contas/Edicao
        /// </summary>
        public IActionResult Edicao()
        {
            return View();
        }

        /// <summary>
        /// Método para gerar uma lista de categorias para preencher um campo DropDownList na página
        /// </summary>
        public List<SelectListItem> ObterCategorias()
        {
            var lista = new List<SelectListItem>();

            // consultar todas as categorias cadastradas no banco de dados
            var categoriaRepository = new CategoriaRepository();
            var categorias = categoriaRepository.GetAll();

            // preecher a lista de categorias
            foreach (var item in categorias)
            {
                lista.Add(new SelectListItem
                {
                    Value = item.Id.ToString(), // valor do campo
                    Text = item.Descricao       // texto exibido no campo
                });
            }

            return lista;
        }
    }
}
