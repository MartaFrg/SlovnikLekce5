using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlovnikLekce5
{
    internal class Program
    {
        static string cesta = @"C:\Czechitas\";
        static Dictionary<String, List<String>> slovnik = new Dictionary<String, List<String>>();
        static void Main(string[] args)
        {
            if (!Directory.Exists(cesta)) Directory.CreateDirectory(cesta); 
            if (File.Exists(cesta + "slovnik.txt")) nactiZeSouboru();
            VyberMenu();
        }

        static void VyberMenu()
        {
            String slovo, preklad;
            List<String> prekladList = new List<String>();
            int volba;
            Console.WriteLine("Vyber číselnou volbu: \n 1) Přelož slovo \n 2) Přidej překlad slova do slovníku \n 3) Přidání nového překladu slova \n 4) Ulož změny do souboru \n 5) Ukonči \n");
            while (!int.TryParse(Console.ReadLine(), out volba) || (volba < 1) || (volba > 5)) Console.WriteLine("Zadej číslo od 1 do 5.");
            switch (volba)
            {
                case 1:
                    Console.Write("Zadej slovo k přeložení: ");
                    slovo = Console.ReadLine();
                    Console.WriteLine("překlad slova "+slovo+": "+String.Join(", ", slovnik[slovo]));
                    Console.WriteLine();
                    VyberMenu();
                    break;
                case 2:
                    Console.Write("Přidej nové slovo do slovníku: ");
                    slovo = Console.ReadLine();
                    if (slovnik.ContainsKey(slovo)) {
                        Console.WriteLine("Toto slovo již slovník obsahuje");
                        VyberMenu();
                        break;
                            }
                    Console.WriteLine("Napiš překlad tohoto slova, můžeš zadat i více překladů, pro ukončení odentruj prázdný řádek: ");
                    while (!String.IsNullOrWhiteSpace(preklad = Console.ReadLine())) prekladList.Add(preklad);
                    slovnik.Add(slovo, prekladList);
                    VyberMenu();
                    break;
                case 3:
                    Console.Write("Zadej slovo k opravě překladu: ");
                    slovo = Console.ReadLine();
                    Console.WriteLine("slovo " + slovo + " je ve slovníku přeloženo jako: " + String.Join(", ", slovnik[slovo]));
                    Console.WriteLine("Napiš nový překlad, můžeš zadat i více překladů, pro ukončení odentruj prázdný řádek: ");
                    while (!String.IsNullOrWhiteSpace(preklad = Console.ReadLine())) slovnik[slovo].Add(preklad);
                    VyberMenu();
                    break;
                case 4:
                    foreach(KeyValuePair<String, List<String>> kvp in slovnik)
                    {
                        File.WriteAllText(cesta + "slovnik.txt", kvp.Key + "," + String.Join(",", kvp.Value) + "\n");
                    }
                    Console.WriteLine("Změny byly uloženy do souboru.\n");
                    VyberMenu();
                    break;
                case 5:
                    Console.WriteLine("ukončit.\n");
                    break;
            }
        }
        static void nactiZeSouboru()
        {
            foreach (String s in File.ReadAllLines(cesta + "slovnik.txt")) 
            {
                List<String> polozka = s.Split(',').ToList();
                String slovo = polozka[0];
                polozka.RemoveAt(0);
                slovnik.Add(slovo, polozka);
            }
        }
    }
}
