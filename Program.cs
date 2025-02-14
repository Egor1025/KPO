using Microsoft.Extensions.DependencyInjection;
using ZooManager;

class Program
{
    static void Main()
    {
        // Настраиваем DI-контейнер
        var serviceProvider = new ServiceCollection()
            .AddSingleton<Zoo>()
            .AddSingleton<IVetClinic, VetClinic>()
            .AddTransient<ReportGenerator>()
            .AddTransient<Menu>()
            .BuildServiceProvider();

        // Получаем зарегистрированные зависимости
        var zoo = serviceProvider.GetRequiredService<Zoo>();
        var reportGenerator = serviceProvider.GetRequiredService<ReportGenerator>();
        var menu = serviceProvider.GetRequiredService<Menu>();

        Run(zoo, reportGenerator, menu);
    }

    private static void Run(Zoo zoo, ReportGenerator reportGenerator, Menu menu)
    {
        try
        {
            while (true)
            {
                var mainAction = menu.ChooseMainAction();

                switch (mainAction)
                {
                    case 1:
                        Console.WriteLine(
                            zoo.AddAnimal(menu.ChooseAnimal())
                                ? "Животное успешно добавлено!"
                                : "Животное не проходит по здоровью!");
                        break;
                    case 2:
                        zoo.AddThing(menu.ChooseThing());
                        Console.WriteLine("Вещь успешно инвентаризирована!");
                        break;
                    case 3:
                        Console.WriteLine(reportGenerator.GetAnimalReport(zoo));
                        break;
                    case 4:
                        Console.WriteLine(reportGenerator.GetVetReport(zoo));
                        break;
                    case 5:
                        Console.WriteLine(reportGenerator.GetInventReport(zoo));
                        break;
                    case 6:
                        return;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}


public class ReportGenerator
{
    private const string Sep = "----------";

    public string GetVetReport(Zoo zoo)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("\nЖивотные, пригодные для контактного зоопарка:");
        foreach (var animal in zoo.ContactAnimals)
            sb.AppendLine(animal.ToString());
        sb.AppendLine(Sep);
        sb.AppendLine($"Всего: {zoo.ContactAnimals.Count}");
        return sb.ToString();
    }

    public string GetAnimalReport(Zoo zoo)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("\nСписок животных зоопарка и кол-во потребляемой ими еды:");
        foreach (var animal in zoo.Animals)
            sb.AppendLine($"{animal} - {animal.Food} кг");
        sb.AppendLine(Sep);
        sb.AppendLine($"Всего: {zoo.Animals.Count}");
        return sb.ToString();
    }

    public string GetInventReport(Zoo zoo)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("\nПредметы:");
        foreach (var thing in zoo.Things)
            sb.AppendLine(thing.ToString());
        sb.AppendLine(Sep + "\nЖивотные:");
        foreach (var animal in zoo.Animals)
            sb.AppendLine(animal.ToString());
        sb.AppendLine(Sep);
        sb.AppendLine($"Всего: {zoo.Things.Count + zoo.Animals.Count}");
        return sb.ToString();
    }
}