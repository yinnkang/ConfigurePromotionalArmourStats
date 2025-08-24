using System;
using System.Reflection;
using System.Linq;

class AssemblyExplorer
{
    static void Main()
    {
        try
        {
            Assembly assembly = Assembly.LoadFrom(@"D:\PP Modding\ConfigurePromotionalArmourStats\ModSDK\Assembly-CSharp.dll");
            
            Console.WriteLine("=== Searching for UI-related classes ===");
            var uiTypes = assembly.GetTypes()
                .Where(t => t.Name.ToLower().Contains("inventory") || 
                           t.Name.ToLower().Contains("geoscape") ||
                           t.Name.ToLower().Contains("equipment") ||
                           t.Name.ToLower().Contains("ability") ||
                           t.Name.ToLower().Contains("item"))
                .Take(20);
                
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
                Console.WriteLine("Properties:");
                foreach (var prop in tacticalItemType.GetProperties())
                {
                    Console.WriteLine($"  - {prop.PropertyType.Name} {prop.Name}");
                }
            }
            
            Console.WriteLine("\n=== Searching for ViewElementDef class ===");
            var viewElementType = assembly.GetTypes()
                .FirstOrDefault(t => t.Name == "ViewElementDef");
                
            if (viewElementType != null)
            {
                Console.WriteLine($"Found: {viewElementType.FullName}");
                Console.WriteLine("Properties:");
                foreach (var prop in viewElementType.GetProperties())
                {
                    Console.WriteLine($"  - {prop.PropertyType.Name} {prop.Name}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}