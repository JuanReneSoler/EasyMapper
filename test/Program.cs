using EasyMapper;

class Object1
{
    public string? Nombre { get; set; }
    public override string ToString() => Nombre ?? string.Empty;
}

class Object2
{
    public string? Nombre { get; set; }
    public override string ToString() => Nombre ?? string.Empty;
}

class Object3
{
    public string? Name { get; set; }
    public override string ToString() => Name ?? string.Empty;
}

class Program
{
    private readonly IMapper _mapper;

    public Program(IMapper Mapper)
    {
        _mapper = Mapper;
    }

    public void Run()
    {
        var obj1 = new Object1 { Nombre = "Juan Soler" };
        var obj2 = _mapper.Map<Object1, Object2>(obj1);
        var obj3 = _mapper.Map<Object1, Object3>(obj1);

        Console.WriteLine(obj1);
        Console.WriteLine(obj2);
        Console.WriteLine(obj3);
    }

    public static void Main()
    {
        var config = new MapperConfiguration();

        config.SetMapperProfile(x =>
        {
            x.CreateMap<Object1, Object2>();
            x.CreateMap<Object1, Object3>();
        });

        var program = new Program(config.CreateMapper());

        program.Run();
    }
}

