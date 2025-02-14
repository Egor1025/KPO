namespace ZooManager;

public abstract class Thing : IInventory
{
    public int Number { get; set; }

    protected Thing(int id)
    {
        Number = id;
    }

    public override string ToString() => $"(№ {Number})";
}

public class Table : Thing
{
    public Table(int id) : base(id) {}
    public override string ToString() => "Стол " + base.ToString();
}

public class Computer : Thing
{
    public Computer(int id) : base(id) {}
    public override string ToString() => "Компьютер " + base.ToString();
}