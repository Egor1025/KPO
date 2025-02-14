namespace ZooManager;

public class Menu
{
    private const string MenuString = "\nПункты меню:\n";
    private static readonly string[] MainActions = {
        "Добавить животное в зоопарк", 
        "Инвентаризовать вещь",
        "Вывести отчет по животным и общему потреблению еды",
        "Вывести список животных, пригодных для контактного зоопарка",
        "Вывести информацию о вещах", 
        "Выйти из программы"
    };

    private const string WhatAnimal = "\nКакое животное вы хотите добавить?\n";
    private static readonly string[] AnimalTypes = { "monkey", "rabbit", "tiger", "wolf" };
    
    private const string WhatThing = "\nКакой предмет вы хотите добавить?\n";
    private static readonly string[] ThingTypes = { "table", "computer" };

    public int ChooseMainAction() => GetActionValue(MenuString, MainActions);

    public Animal ChooseAnimal()
    {
        int animalChoice = GetActionValue(WhatAnimal, AnimalTypes);
        string animalType = AnimalTypes[animalChoice - 1];

        Console.Write("Введите имя животного: ");
        string name = Console.ReadLine() ?? "noname";
        
        int healthLevel = GetActionValue("Укажите его уровень здоровья (от 1 до 10): ", null, 10);

        int kindnessLevel = 0;
        if (animalType == "monkey" || animalType == "rabbit")
            kindnessLevel = GetActionValue("Укажите уровень доброты (от 1 до 10): ", null, 10);
        
        int id = GetActionValue("Укажите его инвентаризационный номер (>=1): ", null, int.MaxValue);

        return AnimalFactory.CreateAnimal(animalType, name, healthLevel, kindnessLevel, id);
    }

    public IInventory ChooseThing()
    {
        int thingChoice = GetActionValue(WhatThing, ThingTypes);
        string thingType = ThingTypes[thingChoice - 1];

        int id = GetActionValue("Укажите инвентаризационный номер предмета (>=1): ", null, int.MaxValue);

        return ThingFactory.CreateThing(thingType, id);
    }

    private static int GetActionValue(string title, IReadOnlyList<string>? actionArr, int length = 0)
    {
        int actionValue;
        bool fullMode = actionArr != null;
        if (fullMode)
        {
            Console.Write(title);
            length = actionArr.Count;
            for (int i = 0; i < length; i++)
                Console.WriteLine($"{i + 1}. {actionArr[i]}");
        } 
        while (true)
        {
            Console.Write(fullMode ? "Укажите номер пункта: " : title);
            if (int.TryParse(Console.ReadLine(), out actionValue) 
                && actionValue >= 1 && actionValue <= length)
                break;
            Console.WriteLine($"Значение должно быть числом от 1 до {length} \n");
        }
        return actionValue;
    }
}