using Desafio.Models;
using System.Collections.Generic;

namespace Desafio.Repositorio
{
    public interface IUsuarioRepositorios
    {
        UsuarioModel BuscarPorLogin(string email);
        UsuarioModel BuscarPorEmailELogin(string email);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel BuscarPorID(string id);
        UsuarioModel Adicionar(UsuarioModel usuario);
        void AlterarSenha(UsuarioModel usuario);
        bool Apagar(string id);
    }
}
