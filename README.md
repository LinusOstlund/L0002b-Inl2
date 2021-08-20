# L0002b-Inl2

Linus Östlund
L0002b, Inlämning 2
Sommarterminen (juni-augusti) 2021

# Om inlämningen
I repot finns [Program.cs]((https://github.com/LinusOstlund/L0002b-Inl2/blob/main/Inl2-Console/SalesForce/SalesForce/Program.cs)) (under `L0002b-Inl2/Inl2-Console/SalesForce/SalesForce/Program.cs) som går att köra enligt uppgiftlydelsen. Där ges valet att antingen autogenerera säljare, eller mata in dessa manuellt. Därefter sorteras dem i säljnivå. För att underlätta sorteringen användes en sk `MultiMap` vilket är en `Dictionary` som tillåter nycklar att mappa till fler än ett värde. 

Dessutom skrivs resultatet till en `.txt`-fil som hamnar i samma undermapp som `solution`-filen.

## Algoritmbeskrivning
Nedan följer kärnan i algoritmen. Varje säljare sparas i en `MultiMap`, där enumen `SalaryLevel`är key, och säljarklassen `Seller` är nycklar. Därefter printas dessa i turordning:

```c#
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
```

Komplexiteten är O(n^2) i och med den nästlade `foreach`-loopen.

För att få ett fint utskriftsformat använder jag mig av [ConsoleTables](https://github.com/khalidabuhakmeh/ConsoleTables), vilket är ett externt paket som tillgodoser att paket (*NuGet*) för att printa tabeller i konsolen.
