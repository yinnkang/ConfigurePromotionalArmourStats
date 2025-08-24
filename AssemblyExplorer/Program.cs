using System;
using System.Reflection;
using System.Linq;

try
{
    Assembly assembly = Assembly.LoadFrom(@"D:\PP Modding\ConfigurePromotionalArmourStats\ModSDK\Assembly-CSharp.dll");
    
    Console.WriteLine("=== Searching for UI-related classes ===");
    var uiTypes = assembly.GetTypes()
        .Where(t => t.Name.ToLower().Contains("inventory") || 
                   t.Name.ToLower().Contains("geoscape") ||
                   t.Name.ToLower().Contains("equipment") ||
                   t.Name.ToLower().Contains("itemview") ||
                   t.Name.ToLower().Contains("abilityview"))
        .Take(15);
        
    foreach (var type in uiTypes)
    {
        Console.WriteLine($"- {type.FullName}");
    }
    
    Console.WriteLine("\n=== Searching for TacticalItemDef class ===");
    var tacticalItemType = assembly.GetTypes()
        .FirstOrDefault(t => t.Name == "TacticalItemDef");
        
    if (tacticalItemType != null)
    {
        Console.WriteLine($"Found: {tacticalItemType.FullName}");
        Console.WriteLine("Properties related to abilities:");
        foreach (var prop in tacticalItemType.GetProperties())
        {
            if (prop.Name.ToLower().Contains("abilit"))
            {
                Console.WriteLine($"  - {prop.PropertyType.Name} {prop.Name}");
            }
        }
    }
    
    Console.WriteLine("\n=== Searching for ability display classes ===");
    var abilityTypes = assembly.GetTypes()
        .Where(t => t.Name.ToLower().Contains("ability") && 
                   (t.Name.ToLower().Contains("element") || 
                    t.Name.ToLower().Contains("view") ||
                    t.Name.ToLower().Contains("display")))
        .Take(10);
        
    foreach (var type in abilityTypes)
    {
        Console.WriteLine($"- {type.FullName}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
