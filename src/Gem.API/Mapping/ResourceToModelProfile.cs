using AutoMapper;
using Gem.API.Domain.Models;
using Gem.API.Domain.Models.Queries;
using Gem.API.Resources;

namespace Gem.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveTipoResource, Tipo>();

            CreateMap<SaveEntidadeResource, Entidade>()
                .ForMember(src => src.Status, opt => opt.MapFrom(src => (EStatus)src.Status));

            CreateMap<EntidadeQueryResource, EntidadeQuery>();
        }
    }
}