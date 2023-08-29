using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Entities
{
    /// <summary>
    /// Classe de modelo de entidade para Usuário
    /// </summary>
    public class Usuario
    {
        #region Atributos

        private Guid _id;
        private string _nome;
        private string _email;
        private string _senha;
        private DateTime _dataCriacao;

        #endregion

        #region Propriedades

        public Guid Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Email { get => _email; set => _email = value; }
        public string Senha { get => _senha; set => _senha = value; }
        public DateTime DataCriacao { get => _dataCriacao; set => _dataCriacao = value; }

        #endregion
    }
}
