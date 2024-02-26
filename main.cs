using System;
using System.Reflection;

public class Vehicle
{
    private int _enginePower; // Warning related to this field not being used
    public string ModelName;
    internal double MaxSpeed;
    protected bool IsElectric;
    protected internal decimal Price;

    public Vehicle(int enginePower, string modelName, double maxSpeed, bool isElectric, decimal price)
    {
        _enginePower = enginePower;
        ModelName = modelName;
        MaxSpeed = maxSpeed;
        IsElectric = isElectric;
        Price = price;
    }

    public void StartEngine()
    {
        Console.WriteLine($"Запуск двигателя с мощностью {_enginePower} л.с.");
    }

    public double Accelerate(double acceleration)
    {
        Console.WriteLine($"Ускорение на {acceleration} км/ч");
        return MaxSpeed + acceleration;
    }

    public bool CheckElectric()
    {
        Console.WriteLine($"Электромобиль: {IsElectric}");
        return IsElectric;
    }

    // New method to display engine power
    public void DisplayEnginePower()
    {
        Console.WriteLine($"Мощность двигателя: {_enginePower} л.с.");
    }

    static void Main()
    {
        Type vehicleType = typeof(Vehicle);
        TypeInfo typeInfo = vehicleType.GetTypeInfo();
        Console.WriteLine("/ 2 /");
        Console.WriteLine($"Type: {vehicleType}");
        Console.WriteLine($"Type.FullName: {typeInfo.FullName}");

        MemberInfo[] members = vehicleType.GetMembers();
        Console.WriteLine("/ 3 /");

        Console.WriteLine("\nMembers:");
        foreach (MemberInfo member in members)
        {
            Console.WriteLine($"{member.MemberType}: {member.Name}");
        }

        Console.WriteLine("/ 4 /");

        FieldInfo[] fields = vehicleType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        Console.WriteLine("\nFields:");
        foreach (FieldInfo field in fields)
        {
            Console.WriteLine($"{field.FieldType} {field.Name}");
        }

        Console.WriteLine("/ 5 /");

        MethodInfo method = vehicleType.GetMethod("Accelerate");
        Console.WriteLine($"\nMethod: {method.Name}");
        object instance = Activator.CreateInstance(vehicleType, 120, "Tesla Model S", 250.0, true, 75000m);
        object result = method.Invoke(instance, new object[] { 60.0 });
        Console.WriteLine($"Result: {result}");

        // Demonstrating the use of DisplayEnginePower method
        MethodInfo displayPowerMethod = vehicleType.GetMethod("DisplayEnginePower");
        displayPowerMethod.Invoke(instance, null);

        Console.ReadLine();
    }
}
