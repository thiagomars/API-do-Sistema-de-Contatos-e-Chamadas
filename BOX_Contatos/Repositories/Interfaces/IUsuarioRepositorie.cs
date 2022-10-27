using BOX_Contatos.DTOs;
using BOX_Contatos.Models;
using Microsoft.AspNetCore.Mvc;

namespace BOX_Contatos.Repositories.Interfaces
{
    public interface IUsuarioRepositorie
    {
        public Task<Usuario> CreateAsync(CreateUserDTO createUserDTO); 
    }
}
