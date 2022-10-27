using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOX_Contatos.Models {
    [Table ("usuario")]
    public class Usuario {
        [Key]
        [Column ("id")]
        public int Id { get; set; }

        [Column ("nome")]
        public string Nome { get; set; }

        [Column ("token")]
        public string TokenUser { get; set; }

        [Column ("dataCadastro")]
        public string DataCadastro { get; set; }

    }
}
