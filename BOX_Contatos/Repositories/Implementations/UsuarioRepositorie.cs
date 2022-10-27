using BOX_Contatos.Data;
using BOX_Contatos.DTOs;
using BOX_Contatos.Models;
using BOX_Contatos.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BOX_Contatos.Repositories.Implementations
{
    public class UsuarioRepositorie : IUsuarioRepositorie
    {
        private readonly AppDbContext _context;

        public UsuarioRepositorie(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> CreateAsync(CreateUserDTO createUserDTO)
        {
            var usuario = new Usuario();
            usuario.Nome = createUserDTO.Nome;
            usuario.TokenUser = Guid.NewGuid().ToString();
            usuario.DataCadastro = String.Format("{0:s}", DateTime.Now);

            _context.usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }
    }
}
