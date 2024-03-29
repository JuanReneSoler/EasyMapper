using System.Linq.Expressions;

namespace EasyMapper;

public interface IMap<TOrigen, TDestino>
{
    internal TDestino CreateInstance(TOrigen origen);
    IMap<TOrigen, TDestino> FromMember(Expression<Func<TDestino, object>> propertyDestiny, Expression<Func<TOrigen, object>> valuePropertyOrigin);
}
