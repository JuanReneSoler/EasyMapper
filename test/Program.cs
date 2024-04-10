using EasyMapper;

class Object1
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public override string ToString() => $"Soy {Nombre} y tengo {Edad} años de edad.";

    public Object1()
    {
        Nombre = string.Empty;
        Edad = 0;
    }
}

class Object2
{
    public string? Nombre { get; set; }
    public int Edad { get; set; }
    public override string ToString() => $"Nombre: {Nombre}, Edad: {Edad}";
}

class Object3
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public override string ToString() => $"I'm {Name} and i'm {Age} years old.";
}

class Object4
{
    public string Nombre { get; set; }
    public string Edad { get; set; }

    public override string ToString() => $"I'm {Nombre} and i'm {Edad} years old.";

    public Object4()
    {
        Nombre = string.Empty;
        Edad = string.Empty;
    }
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
        var obj1 = new Object1 { Nombre = "Juan Soler", Edad = 30 };
        var obj2 = _mapper.Map<Object1, Object2>(obj1);
        var obj3 = _mapper.Map<Object1, Object3>(obj1);
        var obj4 = _mapper.Map<Object1, Object4>(obj1);

        Console.WriteLine(obj1);
        Console.WriteLine(obj2);
        Console.WriteLine(obj3);
        Console.WriteLine(obj4);
    }

    public static void Main()
    {
        var config = new MapperConfiguration();

        config.SetMapperProfile(x =>
        {
            x.CreateMap<Object1, Object2>();
            x.CreateMap<Object1, Object3>()
                .FromMember(x => x.Name, x => x.Nombre)
                .FromMember(x => x.Age, x => x.Edad);
            x.CreateMap<Object1, Object4>().FromMember(x => x.Edad, x => x.Edad.ToString());
        });

        var program = new Program(config.CreateMapper());

        program.Run();
    }
}

