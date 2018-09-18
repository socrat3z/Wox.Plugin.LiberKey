using System;
using Wox.Plugin;
using WoxLiberKey;

namespace ConsoleApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var main = new Main();
            var query = new Query();

            query.GetType().GetProperty("RawQuery").SetValue(query, "universal", null);
            query.GetType().GetProperty("Search").SetValue(query, "Search", null);

            main.Init(null);
            var results = main.Query(query);
            main.LiberKey.Load();
            Console.WriteLine("Apps: " + main.LiberKey.Apps.Count);
            Console.WriteLine("Results: " );

            foreach (var game in results) Console.WriteLine(game.SubTitle);
            
            
            //foreach (var game in main.LiberKey.Apps) Console.WriteLine(game.Name);

            Console.ReadKey();
        }
    }
}