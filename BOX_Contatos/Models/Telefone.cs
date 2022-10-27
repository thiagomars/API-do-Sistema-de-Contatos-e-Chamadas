using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOX_Contatos.Models
{
    [Table("telefone")]
    public class Telefone
    {

        [Key]
        [Column ("id")]
        public int Id { get; set; }

        [Column("id_contatos")]
        public int? ContatosId { get; set; } 
        public Contatos Contatos { get; set; }

        [Column("inicioAtendimento")]
        public string? InicioAtendimento { get; set; }

        [Column("fimAtendimento")]
        public string? FimAtendimento { get; set; }

        [Column("assunto")]
        public string? Assunto { get; set; }
    }
}
