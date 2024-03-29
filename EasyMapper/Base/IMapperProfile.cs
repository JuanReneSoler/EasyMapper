namespace EasyMapper;

public interface IMapperProfile
{
    IMap<TOrigen, TDestino> CreateMap<TOrigen, TDestino>();
    internal TDestino MapObject<TOrigen, TDestino>(TOrigen origen);
}
