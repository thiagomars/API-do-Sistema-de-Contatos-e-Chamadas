using Microsoft.AspNetCore.Mvc;
using BOX_Contatos.Data;
using BOX_Contatos.Models;
using BOX_Contatos.DTOs;
using BOX_Contatos.Repositories.Interfaces;

namespace BOX_Contatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorie _usuarioRepositorie;
        public UsuarioController(IUsuarioRepositorie usuarioRepositoriext) {
            _usuarioRepositorie = usuarioRepositoriext;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(CreateUserDTO createUserDTO)
        {
            try
            {
                var usuario = await _usuarioRepositorie.CreateAsync(createUserDTO);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
