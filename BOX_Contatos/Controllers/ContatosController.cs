using BOX_Contatos.Data;
using BOX_Contatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BOX_Contatos.DTOs;
using NuGet.Protocol;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Common;

namespace BOX_Contatos.Controllers
{
    [Route("api/[controller]/{token}")]
    [ApiController]

    public class ContatosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ContatosController(AppDbContext context) => _context = context;

        //Verifica se o contato existe
        private bool ContatoExists(int id) => _context.contatos.Any(e => e.Id == id);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contatos>>> Get([FromRoute] string token)
        {
            var usuario = await _context.usuario.Where(cod => cod.TokenUser == token).FirstOrDefaultAsync();
            
            if (usuario == null)
            {
                return NotFound("Token não localizado"); 
            }

            return await _context.contatos.Where(aux => aux.UsuarioId == usuario.Id).ToListAsync();
        }

        [HttpPost]
        [ActionName(nameof(Create))]
        public async Task<ActionResult<Contatos>> Create(CreateContatoDTO createContatoDTO, [FromRoute] string token)
        {
            var usuario = await _context.usuario.Where(cod => cod.TokenUser == token).FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound("Token não localizado");
            }

            var contato = new Contatos();
            contato.Nome = createContatoDTO.Nome;
            contato.Telefone = createContatoDTO.Telefone;
            contato.Email = createContatoDTO.Email;
            contato.Ativo = createContatoDTO.Ativo;
            contato.DataNascimento = createContatoDTO.DataNascimento;
            contato.DataCadastro = String.Format("{0:s}", DateTime.Now);
            contato.UsuarioId = usuario.Id;

            _context.contatos.Add(contato);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), new { nome = createContatoDTO.Nome }, createContatoDTO);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(AtualizarItem))]
        public async Task<IActionResult> AtualizarItem([FromRoute] string token, int id, UpdateContatoDTO updateContatoDTO)
        {
            //Validação do token
            var usuario = await _context.usuario.Where(cod => cod.TokenUser == token).FirstOrDefaultAsync();
            if (usuario == null)
            {
                return NotFound("Token não localizado");
            }

            //Busca o contato pelo id
            var contato = await _context.contatos.Where(cod => cod.Id == id).FirstOrDefaultAsync();
            if (contato == null)
            {
                return NotFound("Usuário não encontrado");
            }

            contato.Nome = updateContatoDTO.Nome;
            contato.Telefone = updateContatoDTO.Telefone;
            contato.Email = updateContatoDTO.Email;
            contato.Ativo = updateContatoDTO.Ativo;
            contato.DataEdicao = String.Format("{0:s}", DateTime.Now);

            await _context.SaveChangesAsync();

            return Ok(true);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetContatoIndex))]
        public async Task<ActionResult<Contatos>> GetContatoIndex([FromRoute] string token, int id)
        {
            //Validação do token
            var usuario = await _context.usuario.Where(cod => cod.TokenUser == token).FirstOrDefaultAsync();
            if (usuario == null)
            {
                return NotFound("Token não localizado");
            }

            //Busca contato
            var contato = await _context.contatos.FindAsync(id);
            if (contato == null)
            {
                return NotFound();
            }

            return contato;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarContato([FromRoute] string token, int id)
        {
            //Validação do token
            var usuario = await _context.usuario.Where(cod => cod.TokenUser == token).FirstOrDefaultAsync();
            if (usuario == null)
            {
                return NotFound("Token não localizado");
            }

            //Deletar Contato
            var contato = await _context.contatos.FindAsync(id);
            if(contato == null)
            {
                return NotFound();
            }

            _context.contatos.Remove(contato);
            await _context.SaveChangesAsync();

            return Ok(true);
        }







        

    }
}
