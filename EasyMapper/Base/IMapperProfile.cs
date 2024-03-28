namespace EasyMapper;

public interface IMapperProfile
{
    void CreateMap<TOrigen, TDestino>();
    internal TDestino MapObject<TOrigen, TDestino>(TOrigen origen);
}
