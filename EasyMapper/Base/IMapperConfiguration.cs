namespace EasyMapper;

public interface IMapperConfiguration
{
    void SetMapperProfile(Action<IMapperProfile> profile);
    IMapper CreateMapper();
    internal TDestino GetMap<TOrigen, TDestino>(TOrigen origen);
}
