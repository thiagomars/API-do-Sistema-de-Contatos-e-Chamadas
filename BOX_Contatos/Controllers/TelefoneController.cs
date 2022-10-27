using BOX_Contatos.Data;
using BOX_Contatos.DTOs;
using BOX_Contatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BOX_Contatos.Controllers
{
    [Route("api/[controller]/{token}")]
    [ApiController]
    public class TelefoneController : ControllerBase
    {

        private readonly AppDbContext _context;
        public TelefoneController(AppDbContext context) => _context = context;

        /*[HttpGet]
        public async Task<IEnumerable<Telefone>> Get() => await _context.telefone.ToListAsync();*/

        //Cerificar se existe chamada em atendimento
        [HttpGet("chamada-em-atendimento")]
        [ActionName(nameof(GetAtendimento))]
        public async Task<ActionResult<Contatos>> GetAtendimento([FromRoute] string token)
        {
            //Validação do token
            var usuario = await _context.usuario.Where(cod => cod.TokenUser == token).FirstOrDefaultAsync();
            if (usuario == null)
            {
                return NotFound("Token não localizado");
            }
            //Busca chamada
            var telefone = await _context.telefone.Where(aux => aux.FimAtendimento == null).FirstOrDefaultAsync();
            if (telefone == null)
                return NotFound("nenhuma chamada em andamento");

            if (usuario.Id == telefone.ContatosId)
                return Ok(telefone);
            
            return NotFound("nenhuma chamada em andamento");
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetTelefoneIndex))]
        public async Task<ActionResult<Contatos>> GetTelefoneIndex([FromRoute] string token, int id)
        {
            //Validação do token
            var usuario = await _context.usuario.Where(cod => cod.TokenUser == token).FirstOrDefaultAsync();
            if (usuario == null)
                return NotFound("Token não localizado");

            var contatos = await _context.contatos.Where(cod => cod.Id == id).FirstOrDefaultAsync();

            //Busca contato
            var telefone = await _context.telefone.FindAsync(id);
            if (telefone == null)
                return NotFound();

            return Ok(telefone);
        }

        [HttpPost]
        [ActionName(nameof(Create))]
        public async Task<ActionResult<Contatos>> Create([FromRoute] string token, IniciarLigacaoDTO iniciarLigacaoDTO)
        {
            var usuario = await _context.usuario.Where(cod => cod.TokenUser == token).FirstOrDefaultAsync();

            if (usuario == null)
                return NotFound("Token não localizado");

            var verificContato = _context.contatos.Any(e => e.Id == iniciarLigacaoDTO.ContatoId);
            if (verificContato == false)
                return NotFound("contato não encontrado");

            var verific = await _context.telefone.Where(aux => aux.FimAtendimento == null).FirstOrDefaultAsync();
            if (verific != null)
                return BadRequest();

            var telefone = new Telefone();
            telefone.ContatosId = iniciarLigacaoDTO.ContatoId;
            telefone.InicioAtendimento = String.Format("{0:s}", DateTime.Now);
            
            _context.telefone.Add(telefone);
            await _context.SaveChangesAsync();

            return Ok(telefone);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(Encerrar))]
        public async Task<IActionResult> Encerrar([FromRoute] string token, int id, EncerrarChamadaDTO encerrarChamadaDTO)
        {
            //Validação do token
            var usuario = await _context.usuario.Where(cod => cod.TokenUser == token).FirstOrDefaultAsync();
            if (usuario == null)
                return NotFound("Token não localizado");

            //Busca o contato pelo id
            var ligacao = await _context.telefone.Where(cod => cod.Id == id).FirstOrDefaultAsync();
            if (ligacao == null)
                return NotFound("ligação não encontrado");

            ligacao.Assunto = encerrarChamadaDTO.Assunto;
            ligacao.FimAtendimento = String.Format("{0:s}", DateTime.Now);

            await _context.SaveChangesAsync();

            return Ok(true);
        }

        [HttpGet("contato/{id}")]
        [ActionName(nameof(GetHistorico))]
        public async Task<ActionResult<Contatos>> GetHistorico([FromRoute] string token, int id)
        {
            //Validação do token
            var usuario = await _context.usuario.Where(cod => cod.TokenUser == token).FirstOrDefaultAsync();
            if (usuario == null)
                return NotFound("Token não localizado");

            //Busca contato
            var telefone = await _context.telefone.Where(cod => cod.ContatosId == id).ToListAsync();
            
            if (telefone == null)
                return NotFound("Nenhum histórico");

            var contato = await _context.contatos.Where(x => x.Id == telefone.First().ContatosId).FirstAsync();

            if (telefone.First().ContatosId == contato.Id && contato.UsuarioId == usuario.Id)
            {
                return Ok(telefone);
            }
                return NotFound("Token ou Id não correspondem");
            
            

        }



    }
}
