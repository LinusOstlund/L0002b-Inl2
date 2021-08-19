using System;
using ConsoleTables;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;

namespace SalesForce
{
    /* enum SalaryLevel */
    public enum SalaryLevel { Ett = 1, Två = 2, Tre = 3, Fyra = 4 }

    /* class District */
    public class District {
        public District(String name) { Name = name; }
        public string Name { get; }
        public override string ToString() => $"{Name}";
    }

    /* struct Seller */
    public struct Seller {

        public SalaryLevel Salary;

        public Seller(string id, string name, District dist, int soldItems) {
            Id = id; Name = name; Dist = dist; SoldItems = soldItems;
            /* if-case to calculate salary level */
            if (soldItems < 50)
                this.Salary = SalaryLevel.Ett;
            else if (soldItems < 100)
                this.Salary = SalaryLevel.Två;
            else if (soldItems < 200)
                this.Salary = SalaryLevel.Tre;
            else
                this.Salary = SalaryLevel.Fyra;
        }

        public string Id { get; }
        public string Name { get; }
        public District Dist { get; }
        public int SoldItems { get; set; }
        public override string ToString() => $"{Id},\t{Name},\t\t{Dist},\t{SoldItems},\t{Salary}";
    }

    /* Lightweight implementation of multimap. Uses a Dictionary with special case for adding new values */
    class MultiMap<K, V> {

        Dictionary<K, List<V>> dictionary = new Dictionary<K, List<V>>();

        public void Add(K key, V value) {
            // special case since V can be null
            List<V> list;
            if (this.dictionary.TryGetValue(key, out list))
            {
                list.Add(value);
            }
            else
            {
                list = new List<V>();
                list.Add(value);
                this.dictionary[key] = list;
            }
        }

        public IEnumerable<K> Keys {
            get
            {
                return this.dictionary.Keys;
            }
        }

        public List<V> this[K key] {
            get
            {
                List<V> list;
                if (!this.dictionary.TryGetValue(key, out list))
                {
                    list = new List<V>();
                    this.dictionary[key] = list;
                }
                return list;
            }
        }

        public override string ToString() {
            string s = "";
            return s;
        }
    }

    /* Main program which calls methods and organizes data for output */
    class Program
    {
        /* Main method */
        static void Main(string[] args)
        {
            var currentDate = DateTime.Now;
            Seller[] sellers;

            Console.WriteLine(
                $"Greetings! \n" +
                $"Today is {currentDate}. \n" +
                $"Do you want to enter sellers manually or should they be generated automatically?"
                );

        scenario:
            Console.WriteLine("Type M for Manually, or A for Automatic: ");
            switch (Console.ReadLine().ToUpper()) {
                case "M":
                    sellers = manuallyEnterSellers();
                    break;
                case "A":
                    sellers = generateSellers();
                    break;
                default:
                    Console.WriteLine("You must decide! Type A or M");
                    goto scenario; 
            }

            var multimapSellersSortedBySalary = groupBySalaryLevel(sellers); // returns a multimap
            string multimapString = format(multimapSellersSortedBySalary);  // format the multimap to a string

            /* First output all data to the Console, then save it to a file called Database.txt */
            List<String> output = new List<String>();
            output.Add(currentDate.ToString());
            output.Add(multimapString);
            output.ForEach(st => Console.WriteLine(st));
            SaveToFile(output);

            /* Finished without errors! All well */
            Console.WriteLine("This data is also saved with time stamp to Database.txt. \n");
            Console.WriteLine("Done! Press any key to terminate the program.");
            Console.ReadKey();

        }

        /* a method to concurrently Save-to-file */
        static async Task SaveToFile(List<String> s) {
            await File.WriteAllLinesAsync("Database.txt", s);
        }

        /* A method to automatically generate sellers */
        static Seller[] generateSellers() {
            // set up testing
            Console.Write("How many sellers would you like to generate? \nWrite a number: ");
            String numberOfSellers = Console.ReadLine();
            SellerGenerator testGroup = new SellerGenerator(Convert.ToInt32(numberOfSellers));
            return testGroup.sellers;
        }

        /* A method to enter the sellers by hand, one data at a time*/
        static Seller[] manuallyEnterSellers() {
            String name, ssn, district, sales;
            Console.Write("How many sellers are you going to add? \nWrite a number: ");
            int numberOfSellers = Convert.ToInt32(Console.ReadLine());
            Seller[] sellers = new Seller[numberOfSellers];
            for (int i = 0; i < numberOfSellers; i++) {
                Console.WriteLine($"Please enter information for seller {i + 1}. \nEnter name: ");
                name = Console.ReadLine();
            ssn:
                Console.Write("Enter SSN (personnummer): ");
                ssn = Console.ReadLine();
                if (!checkSSNFormat(ssn)) { // capture invalid formats of SSN.
                    Console.WriteLine("Valid format for SSN is YYYYMMDDXXXX. It also has to be a valid date. Try again!");
                    goto ssn;
                }
                Console.Write("Enter district: ");
                district = Console.ReadLine();
                Console.Write("Enter sales: ");
                sales = Console.ReadLine();
                Console.WriteLine($"Adding {name} with ssn {ssn} and sales {sales} to district {district}...");
                sellers[i] = new Seller(ssn, name, new District(district), Convert.ToInt32(sales));
            }
            return sellers;
        }


        /**
         * Format multimap as a string according to uppgiftlydelse.
         * ConsoleTables are used to generate pretty tables.
         * GitHub: https://github.com/khalidabuhakmeh/ConsoleTables
         * 
         * Complexity: O(n^2) where n is either the size of the multimap or the longest value List
         */
        static string format(MultiMap<SalaryLevel, Seller> multimap) {
        // set up ConsoleTables
        ConsoleTable[] ct = new ConsoleTable[4];
            for (int i = 0; i < ct.Length; i++) { 
                ct[i] = new ConsoleTable("Namn", "Personnummer", "Distrikt", "Antal sälj");
                ct[i].Options.EnableCount = false;
            }

        // this is a kind of ToString-method for the MultiMap
            string output = "";
            foreach (SalaryLevel key in multimap.Keys) {
                foreach (Seller s in multimap[key])
                {
                    ct[(int)key-1].AddRow(s.Name, s.Id, s.Dist, s.SoldItems);
                }
                if (multimap[key].Count <= 0) continue;
                output += ("\nSäljnivå " + key + " erhölls av " + multimap[key].Count + " st säljare: \n" + ct[(int)key-1].ToString() + "\n"); // Adding each string to output list
            }
            return output;
        }

        /**
         * Turn an array of sellers into a multimap
         * An efficient and logical way to sort Sellers according to the uppgiftlydelse 
         */
        static MultiMap<SalaryLevel, Seller> groupBySalaryLevel(Seller[] sellers) {
            var multimap = new MultiMap<SalaryLevel, Seller>();
            var sorted = sellers.OrderBy(s => s.SoldItems).ToArray().Reverse(); // complexity: O(n)
            foreach (Seller s in sorted) {
                multimap.Add(s.Salary, s);
            }
            return multimap;
        } 

        /* Boolean expression to check if the */
        static Boolean checkSSNFormat(String s) {
            string pattern;
            // yyyy (1900-2009)
            pattern = @"[1-2](0|9)[0-9][0-9]";
            // mm (01-12)
            pattern += @"(0[1-9]|1[0-2])";
            // dd (01-31)
            pattern += @"(0[1-9]|1[0-9]|2[0-9]|3[0-1])";
            // xxxx (0000 - 9999)
            pattern += @"[0-9]{4}$";
            Regex rgx = new Regex(pattern);
            return rgx.IsMatch(s);
        }
    }



/**
A class to generate sellers
Also stores them in a Seller[] array.
 */
class SellerGenerator {
  
        String[] firstNames = { "Linus", "Carl", "Tim", "Johanna", "Eric", "Carl-Alfred", "Filippa", "Maja", "Pelle", "Hussein", "Zara", "Kigge", "Markus", "Iza", "Manfred", "Charlie", "Max", "Eva" };
        String[] lastNames = { "Ostberg", "Jonsson", "Jönssons", "Manson", "Dahl", "Gdansk", "Quist", "Kong", "Danielsson", "Hermann", "Lundberg" };
        District[] districtNames = { new District("Malmö"), new District("Stockholm"), new District("Köping"), new District("Göteborg"), new District("Västervik")};
        public Seller[] sellers; 

        public SellerGenerator(int cap) {
            Random rnd = new Random();
            this.sellers = new Seller[cap];

            for (int i = 0; i < cap; i++)
            {
                sellers[i] = new Seller(    /* Code to generate new sellers */
                                         generatePersonnummer(),
                                         this.firstNames[rnd.Next(1, firstNames.Length)] + " " + this.lastNames[rnd.Next(1, lastNames.Length)],
                                         this.districtNames[rnd.Next(1, districtNames.Length)],
                                         rnd.Next(1, 215)

                                       );
            }

        }

        /* Helper method to generate random personnummer in right format */
        public String generatePersonnummer() {
            Random rnd = new Random();
            int dice;
            String year, month, day, xxxx;

            year = rnd.Next(1960, 2005).ToString();

            dice = rnd.Next(1, 6);
            month = dice >= 2 ? "0" + rnd.Next(1, 9) : rnd.Next(10, 12).ToString();

            dice = rnd.Next(1, 6);
            day = dice < 2 ? "0" + rnd.Next(1, 9) : rnd.Next(10, 31).ToString();

            xxxx = rnd.Next(1000, 9999).ToString();


            return year + month + day + xxxx;
        }

    }

}
