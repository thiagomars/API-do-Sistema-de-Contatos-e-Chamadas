using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOX_Contatos.Models
{
    [Table ("contatos")]
    public class Contatos
    {
        [Key]
        [Column ("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string? Nome { get; set; }

        [Column("telefone")]
        public string? Telefone { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("ativo")]
        public bool Ativo { get; set; }

        [Column("dataNascimento")]
        public string? DataNascimento { get; set; }

        [Column("dataCadastro")]
        public string? DataCadastro { get; set; }

        [Column("dataEdicao")]
        public string? DataEdicao { get; set; }

        [Column("id_usuario")]
        public int? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
