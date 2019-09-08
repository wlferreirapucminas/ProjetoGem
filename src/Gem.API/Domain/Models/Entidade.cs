namespace Gem.API.Domain.Models
{
    public class Entidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Cargo { get; set; }
        public EStatus Status { get; set; }
        public string Email { get; set; }

        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }

    }
}