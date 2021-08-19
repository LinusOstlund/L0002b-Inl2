# L0002b-Inl2

Linus Östlund
L0002b, Inlämning 3
Sommarterminen (juni-augusti) 2021

# Om inlämningen
I repot ligger en `solution`-fil (under [L0002b-Inl2/Inl2-Console/SalesForce/SalesForce/Program.cs](https://github.com/LinusOstlund/L0002b-Inl2/blob/main/Inl2-Console/SalesForce/SalesForce/Program.cs)) som går att köra enligt uppgiftlydelsen. Där ges valet att antingen autogenerera säljare, eller mata in dessa manuellt. Därefter sorteras dessa i säljnivå. För att underlätta sorteringen användes en sk `MultiMap` vilket är en `Dictionary` tillåter nycklar att mappa till fler än ett värde. 
Dessutom skrivs resultatet till en `.txt`-fil som hamnar i samma undermapp som `solution`-filen.

## Algoritmbeskrivning
Nedan följer kärnan i algoritmen. Varje säljare sparas i en `MultiMap`, där enumen `SalaryLevel`är key, och säljarklassen `Seller` är nycklar. Därefter printas dessa i turordning.

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
