using System.ComponentModel.DataAnnotations;

namespace Gem.API.Resources
{
    public class SaveEntidadeResource
    {
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(50)]
        public string Endereco { get; set; }

        [Required]
        [MaxLength(50)]
        public string Cargo { get; set; }

        [Required]
        [Range(1, 5)]
        public int Status { get; set; } // AutoMapper is going to cast it to the respective enum value

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public int TipoId { get; set; }
    }
}