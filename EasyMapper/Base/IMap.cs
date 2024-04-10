using System.Linq.Expressions;

namespace EasyMapper;

public interface IMap<TOrigen, TDestino>
{
    internal TDestino CreateInstance(TOrigen origen);
    IMap<TOrigen, TDestino> FromMember<TProp>(Expression<Func<TDestino, TProp>> propertyDestiny, Func<TOrigen, TProp> evaluateFunc);
}
