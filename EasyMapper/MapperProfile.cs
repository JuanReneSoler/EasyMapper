namespace EasyMapper;

internal class MapperProfile : IMapperProfile
{
    private readonly IDictionary<string, Func<object, object>> _maps;

    public MapperProfile()
    {
        _maps = new Dictionary<string, Func<object, object>>();
    }

    public void CreateMap<TOrigen, TDestino>()
    {
        var key = $"{typeof(TOrigen)}=>{typeof(TDestino)}";

        if (!_maps.ContainsKey(key))
        {
            Func<TOrigen, TDestino> map = origen =>
            {
                return CreateInstanceAndFillProperties<TOrigen, TDestino>(origen);
            };

            _maps.Add(key, x => map((TOrigen)x) ?? throw new Exception($"No fue posible mapear las propiedades entre los tipos {typeof(TOrigen)} y {typeof(TDestino)}"));
        }
    }

    private TDestino CreateInstanceAndFillProperties<TOrigen, TDestino>(TOrigen origen)
    {
        var result = Activator.CreateInstance<TDestino>();

        Parallel.ForEach(typeof(TDestino).GetProperties(), p =>
        {
            var origenP = typeof(TOrigen).GetProperty(p.Name);
            if (origenP is not null)
            {
                p.SetValue(result, origenP.GetValue(origen));
            }
        });

        return result;
    }

    TDestino IMapperProfile.MapObject<TOrigen, TDestino>(TOrigen origen)
    {
        if (origen is null) throw new Exception($"{typeof(TOrigen)} no puede ser nulo.");

        var key = $"{typeof(TOrigen)}=>{typeof(TDestino)}";

        if (_maps.ContainsKey(key))
        {
            return (TDestino)_maps[key](origen);
        }
        throw new Exception($"No existe un mapping definido para el tipo {typeof(TDestino)} con el tipo {typeof(TOrigen)}");
    }
}
