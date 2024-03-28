namespace EasyMapper;

public class Mapper : IMapper
{
    private readonly IMapperConfiguration _config;

    internal Mapper(IMapperConfiguration configuration)
    {
        _config = configuration;
    }

    public TDestino Map<TOrigen, TDestino>(TOrigen origen)
    {
        return _config.GetMap<TOrigen, TDestino>(origen);
    }
}
