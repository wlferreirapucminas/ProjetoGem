namespace Gem.API.Resources
{
    public class EntidadeResource
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Cargo { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public TipoResource Tipo {get;set;}
    }
}