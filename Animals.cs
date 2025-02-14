namespace ZooManager;

public abstract class Animal : IAlive, IInventory
{
    protected Animal(string name, int health, int id)
    {
        Name = name;
        Health = health;
        Number = id;
    }

    public string Name { get; set; }
    public int Food { get; set; }
    public int Health { get; set; }
    public int Number { get; set; }
    
    public override string ToString() => $"{Name} (№ {Number})";
}

public abstract class Herbo : Animal
{
    public int Kindness { get; set; }

    protected Herbo(string name, int health, int kindness, int id) : base(name, health, id)
    {
        Kindness = kindness;
    }
}

public abstract class Predator : Animal
{
    protected Predator(string name, int health, int id) : base(name, health, id) {}
}

public class Monkey : Herbo
{
    public Monkey(string name, int health, int kindness, int id) : base(name, health, kindness, id)
    {
        Food = 3;
    }
    public override string ToString() => "Обезьяна " + base.ToString();
}

public class Rabbit : Herbo
{
    public Rabbit(string name, int health, int kindness, int id) : base(name, health, kindness, id)
    {
        Food = 1;
    }
    public override string ToString() => "Кролик " + base.ToString();
}

public class Tiger : Predator
{
    public override string ToString() => "Тигр " + base.ToString();

    public Tiger(string name, int health, int id) : base(name, health, id)
    {
        Food = 9;
    }
}

public class Wolf : Predator
{
    public Wolf(string name, int health, int id) : base(name, health, id)
    {
        Food = 4;
    }
    public override string ToString() => "Волк " + base.ToString();
}