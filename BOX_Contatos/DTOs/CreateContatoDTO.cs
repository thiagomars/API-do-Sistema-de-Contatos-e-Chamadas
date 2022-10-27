using System.ComponentModel.DataAnnotations.Schema;

namespace BOX_Contatos.DTOs
{
    public class CreateContatoDTO
    {

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public bool Ativo { get; set; }

        public string DataNascimento { get; set; }

    }
}
