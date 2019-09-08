using System.ComponentModel.DataAnnotations;

namespace Gem.API.Resources
{
    public class SaveTipoResource
    {
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
    }
}