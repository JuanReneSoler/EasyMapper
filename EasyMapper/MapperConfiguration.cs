
namespace EasyMapper;

public class MapperConfiguration : IMapperConfiguration
{
    private readonly IMapperProfile _profile;

    public MapperConfiguration()
    {
        _profile = new MapperProfile();
    }

    public IMapper CreateMapper()
    {
        return new Mapper(this);
    }

    public void SetMapperProfile(Action<IMapperProfile> profile)
    {
        profile(_profile);
    }

    TDestino IMapperConfiguration.GetMap<TOrigen, TDestino>(TOrigen origen)
    {
        return _profile.MapObject<TOrigen, TDestino>(origen);
    }
}
