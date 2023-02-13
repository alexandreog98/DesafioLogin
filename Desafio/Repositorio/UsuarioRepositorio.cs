using Desafio.Data;
using Desafio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Desafio.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorios
    {
        private readonly IMongoCollection<UsuarioModel> _usuarios;

        public UsuarioRepositorio(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);

            _usuarios = database.GetCollection<UsuarioModel>("usuarios");
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _usuarios.Find(usuario => usuario.Login.ToUpper() == login.ToUpper()).FirstOrDefault();
        }

        public UsuarioModel BuscarPorEmailELogin(string email)
        {
            return _usuarios.Find(usuario => usuario.Email.ToUpper() == email.ToUpper()).FirstOrDefault();
        }

        public UsuarioModel BuscarPorID(string id)
        {
            return _usuarios.Find(usuario => usuario.Id == id).FirstOrDefault();
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _usuarios.Find(usuario => true).ToList();
        }


        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.SetSenhaHash();
            _usuarios.InsertOne(usuario);
            return usuario;
        }

        public void AlterarSenha(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = BuscarPorEmailELogin(usuario.Email);
            usuario.SetSenhaHash();

            var filter = Builders<UsuarioModel>.Filter.Eq(s => s.Id, usuario.Id);
            var update = Builders<UsuarioModel>.Update.Set(s => s.Senha, usuario.Senha);
            _usuarios.UpdateOneAsync(filter, update);

        }

        public bool Apagar(string id)
        {
            UsuarioModel usuarioDB = BuscarPorID(id);

            if (usuarioDB == null) throw new Exception("Houve um erro na deleção do usuário!");

            _usuarios.DeleteOne(usuario => usuario.Id == id);

            return true;
        }
    }
}
