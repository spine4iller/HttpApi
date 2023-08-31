using Api.Infra.Interfaces;
using Api.Infra.Requests;
using AutoMapper;

namespace Api.Infra.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SearchRequest, ProviderOneSearchRequest>().ForMember(
                x => x.From,
                o => o.MapFrom(s => s.Origin))
            .ForMember(
                x => x.To,
                o => o.MapFrom(s => s.Destination))
            .ForMember(
                x => x.DateFrom,
                o => o.MapFrom(s => s.OriginDateTime))
            .ForMember(
                x => x.DateTo,
                o => o.Ignore())
            .ForMember(
                x => x.MaxPrice,
                o => o.Ignore());

        CreateMap<SearchRequest, ProviderTwoSearchRequest>().ForMember(
                x => x.Departure,
                o => o.MapFrom(s => s.Origin))
            .ForMember(
                x => x.Arrival,
                o => o.MapFrom(s => s.Destination))
            .ForMember(
                x => x.DepartureDate,
                o => o.MapFrom(s => s.OriginDateTime))
            .ForMember(
                x => x.MinTimeLimit,
                o => o.Ignore());

        CreateMap<ProviderOneRoute, Route>().ForMember(
                x => x.OriginDateTime,
                o => o.MapFrom(s => s.DateFrom))
            .ForMember(x => x.Destination,
                o => o.MapFrom(s => s.To))
            .ForMember(x => x.DestinationDateTime,
                o => o.MapFrom(s => s.DateTo))
            .ForMember(x => x.Origin,
                o => o.MapFrom(s => s.From))
            .ForMember(
                x => x.Id,
                o => o.Ignore());

        CreateMap<ProviderTwoRoute, Route>().ForMember(
                       x => x.OriginDateTime,
                       o => o.MapFrom(s => s.Departure.Date))
                   .ForMember(x => x.Destination,
                       o => o.MapFrom(s => s.Arrival.Point))
                   .ForMember(x => x.DestinationDateTime,
                       o => o.MapFrom(s => s.Arrival.Date))
                   .ForMember(x => x.Origin,
                       o => o.MapFrom(s => s.Departure.Point))
                   .ForMember(x => x.Id,
                o => o.Ignore());
    }
}