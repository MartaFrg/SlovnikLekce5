﻿using System;
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
        static bool ulozeno = true;
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
            Console.WriteLine("Vyber číselnou volbu: \n 1) Přelož slovo \n 2) Přidej překlad slova do slovníku \n 3) Změň překlad slova \n 4) Ulož změny do souboru \n 5) Vypiš všechna slova \n 6) Ukonči \n");
            while (!int.TryParse(Console.ReadLine(), out volba) || (volba < 1) || (volba > 6)) Console.WriteLine("Zadej číslo od 1 do 6.");
            switch (volba)
            {
                case 1:
                    Console.Write("Zadej slovo k přeložení: ");
                    slovo = Console.ReadLine();
                    if (slovnik.ContainsKey(slovo))
                    {
                        Console.WriteLine("překlad slova " + slovo + ": " + String.Join(", ", slovnik[slovo]));
                        Console.WriteLine();
                        VyberMenu();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Slovo {0} není ve slovníku", slovo);
                    }
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
                    ulozeno = false;
                    VyberMenu();
                    break;
                case 3:
                    Console.Write("Zadej slovo k opravě překladu: ");
                    slovo = Console.ReadLine();
                    Console.WriteLine("slovo " + slovo + " je ve slovníku přeloženo jako: " + String.Join(", ", slovnik[slovo]));
                    Console.WriteLine("Napiš nový překlad, můžeš zadat i více překladů, pro ukončení odentruj prázdný řádek: ");
                    slovnik[slovo].Clear();
                    while (!String.IsNullOrWhiteSpace(preklad = Console.ReadLine())) slovnik[slovo].Add(preklad);
                    ulozeno = false;
                    Console.WriteLine();
                    VyberMenu();
                    break;
                case 4:
                    File.WriteAllText(cesta + "slovnik.txt", String.Empty);
                    StringBuilder sb = new StringBuilder();
                    foreach(KeyValuePair<String, List<String>> kvp in slovnik)
                    {
                        sb.AppendLine(kvp.Key + "," + String.Join(",", kvp.Value));
                    }
                    File.AppendAllText(cesta + "slovnik.txt", sb.ToString());
                    Console.WriteLine("Změny byly uloženy do souboru.\n");
                    ulozeno = true;
                    VyberMenu();
                    break;
                case 5:
                    foreach (KeyValuePair<String, List<String>> kvp in slovnik) Console.WriteLine("ve slovníku je slovo " + kvp.Key);
                    if (ulozeno) Console.WriteLine("Všechna slova jsou uložena v souboru.");
                    else Console.WriteLine("Některá slova nejsou uložena v souboru!");
                    Console.WriteLine();
                    VyberMenu();
                    break;
                case 6:
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
