using System.Linq.Expressions;

namespace EasyMapper;

internal class Map<TOrigen, TDestino> : IMap<TOrigen, TDestino>
{
    private readonly IDictionary<string, string> _maps;

    private Map()
    {
        _maps = new Dictionary<string, string>();
        Parallel.ForEach(typeof(TDestino).GetProperties(), p =>
        {
            if (typeof(TOrigen).GetProperty(p.Name) is not null)
            {
                _maps.Add(p.Name, p.Name);
            }
        });
    }

    public static IMap<TOrigen, TDestino> CreateMap()
    {
        return new Map<TOrigen, TDestino>();
    }

    public IMap<TOrigen, TDestino> FromMember(Expression<Func<TDestino, object>> propertyDestiny, Expression<Func<TOrigen, object>> valuePropertyOrigin)
    {
        var propertyOriginName = ((MemberExpression)valuePropertyOrigin.Body).Member.Name;
        var propertyDestinyName = ((MemberExpression)propertyDestiny.Body).Member.Name;
        _maps.Add(propertyDestinyName, propertyOriginName);
        return this;
    }

    TDestino IMap<TOrigen, TDestino>.CreateInstance(TOrigen origen)
    {
        var destiny = Activator.CreateInstance<TDestino>();

        Parallel.ForEach(_maps.Keys, k =>
        {
            var val = typeof(TOrigen).GetProperty(_maps[k])?.GetValue(origen);
            typeof(TDestino).GetProperty(k)?.SetValue(destiny, val);
        });

        return destiny;
    }
}
