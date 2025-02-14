namespace ZooManager;

public class Zoo
{
    private const int MinKindness = 5;
    private readonly IVetClinic _vetClinic; // Зависимость через интерфейс
    private readonly List<Animal> _animals = new();
    private readonly List<Herbo> _contactAnimals = new();
    private readonly List<IInventory> _things = new();

    // Внедрение зависимости через конструктор
    public Zoo(IVetClinic vetClinic)
    {
        _vetClinic = vetClinic;
    }

    public bool AddAnimal(Animal animal)
    {
        bool ok = _vetClinic.CheckAnimalHealth(animal);
        if (!ok) return ok;

        _animals.Add(animal);
        if (animal is Herbo { Kindness: >= MinKindness } herbalAnimal)
            _contactAnimals.Add(herbalAnimal);
        return ok;
    }

    public void AddThing(IInventory thing) => _things.Add(thing);

    public IReadOnlyList<Animal> Animals => _animals.AsReadOnly();
    public IReadOnlyList<Herbo> ContactAnimals => _contactAnimals.AsReadOnly();
    public IReadOnlyList<IInventory> Things => _things.AsReadOnly();
}

public class VetClinic : IVetClinic
{
    private const int MinHealth = 7;
    public bool CheckAnimalHealth(Animal animal) => animal.Health >= MinHealth;
}

// Фабрики остаются без изменений
public static class AnimalFactory
{
    public static Animal CreateAnimal(string type, string name, int health, int kindness, int id)
    {
        return type.ToLower() switch
        {
            "monkey" => new Monkey(name, health, kindness, id),
            "rabbit" => new Rabbit(name, health, kindness, id),
            "tiger" => new Tiger(name, health, id),
            "wolf" => new Wolf(name, health, id),
            _ => throw new ArgumentException("Неизвестный тип животного.")
        };
    }
}

public static class ThingFactory
{
    public static IInventory CreateThing(string type, int id)
    {
        return type.ToLower() switch
        {
            "table" => new Table(id),
            "computer" => new Computer(id),
            _ => throw new ArgumentException("Неизвестный тип предмета.")
        };
    }
}