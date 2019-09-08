using System.Collections.Generic;

namespace Gem.API.Domain.Models
{
    public class Tipo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IList<Entidade> Entidade { get; set; } = new List<Entidade>();
    }
}