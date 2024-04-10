using System.Linq.Expressions;

namespace EasyMapper;

internal class Map<TOrigen, TDestino> : IMap<TOrigen, TDestino>
{
    private readonly IDictionary<string, Func<TOrigen, object>> _maps = new Dictionary<string, Func<TOrigen, object>>();

    private Map()
    {
        Parallel.ForEach(typeof(TOrigen).GetProperties(), p =>
        {
            var oP = typeof(TDestino).GetProperty(p.Name);

            if (p.PropertyType == oP?.PropertyType)
            {
                _maps.Add(p.Name, (obj) =>
                {
                    var result = p.GetValue(obj);

                    if(result is null) throw new Exception($"No se pudo obtener el valor de la propiedad: {p.Name}.");

                    return result;
                });
            }
        });
    }

    public static IMap<TOrigen, TDestino> CreateMap()
    {
        return new Map<TOrigen, TDestino>();
    }

    public IMap<TOrigen, TDestino> FromMember<TProp>(
            Expression<Func<TDestino, TProp>> propertyDestiny,
            Func<TOrigen, TProp> evaluateFunc)
    {
        var propertyDestinyName = ((MemberExpression)propertyDestiny.Body).Member.Name;

        _maps.Add(propertyDestinyName, (obj) =>
        {
            var result = evaluateFunc(obj);

            if (result is null) throw new Exception($"No se pudo efectuar la operacion {evaluateFunc.GetType()}.");

            return result;
        });
        return this;
    }

    TDestino IMap<TOrigen, TDestino>.CreateInstance(TOrigen origen)
    {
        var destiny = Activator.CreateInstance<TDestino>();

        Parallel.ForEach(_maps.Keys, k =>
        {
            var val = _maps[k](origen);
            typeof(TDestino).GetProperty(k)?.SetValue(destiny, val);
        });

        return destiny;
    }
}
