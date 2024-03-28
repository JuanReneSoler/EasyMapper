namespace EasyMapper;

public interface IMapper
{
    TDestino Map<TOrigen, TDestino>(TOrigen origen);
}
