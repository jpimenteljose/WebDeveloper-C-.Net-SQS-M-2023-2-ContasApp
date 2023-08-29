﻿using ContasApp.Data.Entities;
using ContasApp.Data.Settings;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Repositories
{
    /// <summary>
    /// Classe para operações de banco de dados da tabela de Usuário
    /// </summary>
    public class UsuarioRepository
    {
        /// <summary>
        /// Método para cadastrar um usuário no banco de dados
        /// </summary>
        public void Add(Usuario usuario)
        {
            var query = @"
                INSERT INTO USUARIO (ID, NOME, EMAIL, SENHA, DATAHORACRIACAO)
                VALUES (@Id, @Nome, @Email, @Senha, @DataHoraCriacao)
            ";

            // abrindo conexão com o banco de dados 
            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                // executando o comando SQL no banco de dados
                connection.Execute(query, usuario);
            }
        }

        /// <summary>
        /// Método para atualizar um usuário no banco de dados
        /// </summary>
        public void Update(Usuario usuario)
        {
            var query = @"
                UPDATE USUARIO 
                SET NOME=@Nome, EMAIL=@Email, SENHA=@Senha
                WHERE ID=@Id
            ";

            // abrindo conexão com o banco de dados 
            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                // executando o comando SQL no banco de dados
                connection.Execute(query, usuario);
            }
        }

        /// <summary>
        /// Método para excluir um usuário no banco de dados
        /// </summary>
        public void Delete(Usuario usuario)
        {
            var query = @"
                DELETE FROM USUARIO 
                WHERE ID=@Id
            ";

            // abrindo conexão com o banco de dados 
            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                // executando o comando SQL no banco de dados
                connection.Execute(query, usuario);
            }
        }

        /// <summary>
        /// Método para consultar um usuário no banco de dados através do ID
        /// </summary>
        public Usuario? GetById(Guid id)
        {
            var query = @"
                SELECT * FROM USUARIO WHERE ID=@Id
            ";

            // abrindo conexão com o banco de dados 
            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                // executando o comando SQL no banco de dados
                return connection.Query<Usuario>(query, new { @Id = id }).FirstOrDefault() ;
            }
        }

        /// <summary>
        /// Método para consultar um usuário no banco de dados através do Email
        /// </summary>
        public Usuario? GetByEmail(string email)
        {
            var query = @"
                SELECT * FROM USUARIO WHERE EMAIL=@Email
            ";

            // abrindo conexão com o banco de dados 
            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                // executando o comando SQL no banco de dados
                return connection.Query<Usuario>(query, new { @Email = email }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Método para consultar um usuário no banco de dados através do Email e Senha
        /// </summary>
        public Usuario? GetByEmailAndSenha(string email, string senha)
        {
            var query = @"
                SELECT * FROM USUARIO WHERE EMAIL=@Email AND SENHA=@Senha
            ";

            // abrindo conexão com o banco de dados 
            using (var connection = new SqlConnection(SqlServerSettings.GetConnectionString()))
            {
                // executando o comando SQL no banco de dados
                return connection.Query<Usuario>(query, new { @Email = email, @Senha = senha }).FirstOrDefault();
            }
        }
    }
}