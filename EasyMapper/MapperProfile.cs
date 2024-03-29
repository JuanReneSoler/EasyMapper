namespace EasyMapper;

internal class MapperProfile : IMapperProfile
{
    private readonly IDictionary<string, IDictionary<string, string>> _maps;

    public MapperProfile()
    {
        _maps = new Dictionary<string, IDictionary<string, string>>();
    }

    public void CreateMap<TOrigen, TDestino>()
    {
        var key = $"{typeof(TOrigen)}=>{typeof(TDestino)}";

        if (!_maps.ContainsKey(key))
        {
            _maps.Add(key, ConvertTypeToInjectionProperties<TOrigen, TDestino>());
        }
    }

    private IDictionary<string, string> ConvertTypeToInjectionProperties<TOrigen, TDestino>()
    {
        var maps = new Dictionary<string, string>();

        Parallel.ForEach(typeof(TDestino).GetProperties(), p =>
        {
            if (typeof(TOrigen).GetProperty(p.Name) is not null)
            {
                maps.Add(p.Name, p.Name);
            }
        });

        return maps;
    }

    private TDestino CreateNewInstanceFromDestinyAndMapProperties<TOrigen, TDestino>(TOrigen origen, IDictionary<string, string> mapProperties)
    {
        var destiny = Activator.CreateInstance<TDestino>();

        Parallel.ForEach(typeof(TDestino).GetProperties(), p =>
        {
            var val = typeof(TOrigen).GetProperty(p.Name)?.GetValue(origen);
            p.SetValue(destiny, val);
        });

        return destiny;
    }

    TDestino IMapperProfile.MapObject<TOrigen, TDestino>(TOrigen origen)
    {
        if (origen is null) throw new Exception($"{typeof(TOrigen)} no puede ser nulo.");

        var key = $"{typeof(TOrigen)}=>{typeof(TDestino)}";

        if (_maps.ContainsKey(key))
        {
            return CreateNewInstanceFromDestinyAndMapProperties<TOrigen, TDestino>(origen, _maps[key]);
        }
        throw new Exception($"No existe un mapping definido para el tipo {typeof(TDestino)} con el tipo {typeof(TOrigen)}");
    }
}
