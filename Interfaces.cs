namespace ZooManager;

public interface IAlive
{
    string Name { get; set; }
    int Food { get; set; }
    int Health { get; set; }
}

public interface IInventory
{
    int Number { get; set; }
}

public interface IVetClinic
{
    bool CheckAnimalHealth(Animal animal);
}