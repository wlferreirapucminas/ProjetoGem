namespace Gem.API.Domain.Models.Queries
{
    public class EntidadeQuery : Query
    {
        public int? TipoId { get; set; }

        public EntidadeQuery(int? tipoId, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            TipoId = tipoId;
        }
    }
}