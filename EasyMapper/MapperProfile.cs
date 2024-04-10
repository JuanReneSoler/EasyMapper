namespace EasyMapper;

internal class MapperProfile : IMapperProfile
{
    private readonly IDictionary<string, object> _maps;

    public MapperProfile()
    {
        _maps = new Dictionary<string, object>();
    }

    public IMap<TOrigen, TDestino> CreateMap<TOrigen, TDestino>()
    {
        var key = $"{typeof(TOrigen)}=>{typeof(TDestino)}";

        if (!_maps.ContainsKey(key))
        {
            var map = Map<TOrigen, TDestino>.CreateMap();
            _maps.Add(key, map);
            return map;
        }
        return (IMap<TOrigen, TDestino>)_maps[key];
    }

    TDestino IMapperProfile.MapObject<TOrigen, TDestino>(TOrigen origen)
    {
        var key = $"{typeof(TOrigen)}=>{typeof(TDestino)}";

        if (_maps.ContainsKey(key))
        {
            var map = (IMap<TOrigen, TDestino>)_maps[key];
            return map.CreateInstance(origen);
        }
        throw new Exception($"No existe un mapping definido para el tipo {typeof(TDestino)} con el tipo {typeof(TOrigen)}");
    }
}
