using AutoMapper;
using Gem.API.Domain.Models;
using Gem.API.Domain.Models.Queries;
using Gem.API.Extensions;
using Gem.API.Resources;

namespace Gem.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Tipo, TipoResource>();

            CreateMap<Entidade, EntidadeResource>()
                .ForMember(src => src.Status,
                           opt => opt.MapFrom(src => src.Status.ToDescriptionString()));

            CreateMap<QueryResult<Entidade>, QueryResultResource<Entidade>>();
        }
    }
}