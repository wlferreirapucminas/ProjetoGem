using System.ComponentModel;

namespace Gem.API.Domain.Models
{
    public enum EStatus : byte
    {
        [Description("A")]
        Ativo = 1,

        [Description("I")]
        Inativo = 2,

        [Description("O")]
        Outros = 3,

    }
}