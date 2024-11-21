using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace csmfurdostat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] f = File.ReadAllLines("furdoadat.txt");
            List<Adatsor> adatok = new List<Adatsor>();
            for (int i = 0; i < f.Length; i++)
            {
                adatok.Add(new Adatsor(f[i]));
            }

            var feladat2 = adatok.Where(x => x.belepes == false && x.fazonosito == 0).ToList();

            Console.WriteLine($"2. feladat\nAz első vendég {feladat2[0].ora}:{feladat2[0].perc}:{feladat2[0].masodperc}-kor lépett ki az öltözőből.\nAz utolsó vendég {feladat2[feladat2.Count()-1].ora}:{feladat2[feladat2.Count() - 1].perc}:{feladat2[feladat2.Count() - 1].masodperc}-kor lépett ki az öltözőből.");

            
            Console.WriteLine("\n3. feladat");
            var azonositokSzerint = adatok.GroupBy(x=> x.vazonosito).ToList();
            
            int db = 0;
            int feladat3db = 0;
            foreach (var a in azonositokSzerint)
            {
                db = 0;
                foreach (var b in adatok)
                {
                    if (a.Key == b.vazonosito)
                    {
                        db++;
                    }
                }
                if (db <= 4)
                {
                    feladat3db++;
                }
            }

            Console.WriteLine($"A fürdőben {feladat3db} vendég járt csak egy részlegen.");

            
            Console.WriteLine("\n4. feladat\nA legtöbb időt eltöltő vendég:");
            int ora = 0;
            int perc = 0;
            int masodperc = 0;
            int maxora = 0;
            int maxperc = 0;
            int maxmasodperc = 0;
            int hanyadik = 0;
            for (int i = 0; i < adatok.Count()-1; i++)       
            {
                if (adatok[i].ora < adatok[i+1].ora)
                {
                    if (adatok[i].perc < adatok[i+1].perc)
                    {
                        if (adatok[i].masodperc < adatok[i+1].masodperc)
                        {
                            ora = adatok[i + 1].ora - adatok[i].ora;
                            perc = adatok[i + 1].perc - adatok[i].perc;
                            masodperc = adatok[i + 1].masodperc - adatok[i].masodperc;
                        }
                        else if (adatok[i].masodperc > adatok[i+1].masodperc)
                        {
                            //18:20:20 19:30:10     1 óra 9 perc 50 másodperc
                            ora = adatok[i + 1].ora - adatok[i].ora;
                            perc = adatok[i + 1].perc - adatok[i].perc - 1;
                            masodperc = 60 - adatok[i + 1].masodperc; 
                        }
                        else
                        {
                            //18:20:20 19:30:20
                            ora = adatok[i + 1].ora - adatok[i].ora;
                            perc = adatok[i + 1].perc - adatok[i].perc;
                            masodperc = 0;
                        }
                    }
                    else if (adatok[i].perc > adatok[i + 1].perc)
                    {
                        if (adatok[i].masodperc < adatok[i + 1].masodperc)
                        {
                            //18:20:20 19:10:30     50 perc 10 másodperc
                            ora = adatok[i + 1].ora - adatok[i].ora - 1;
                            perc = 60 - adatok[i+1].perc;
                            masodperc = adatok[i + 1].masodperc - adatok[i].masodperc;
                        }
                        else if (adatok[i].masodperc > adatok[i + 1].masodperc)
                        {
                            //18:20:20 19:10:10     49 perc 50 másodperc
                            ora = adatok[i + 1].ora - adatok[i].ora - 1;
                            perc = 60 - adatok[i + 1].perc -1;
                            masodperc = 60 - adatok[i + 1].masodperc;
                        }
                        else
                        {
                            //18:20:20 19:10:20     50 perc
                            ora = adatok[i + 1].ora - adatok[i].ora - 1;
                            perc = 60 - adatok[i + 1].perc;
                            masodperc = 0;
                        }
                    }
                }
                if (maxora < ora)
                {
                    maxora = ora;
                    maxperc = perc;
                    maxmasodperc = masodperc;
                    hanyadik = adatok[i + 1].vazonosito;
                }
                if (maxora == ora && maxperc < perc)
                {
                    maxora = ora;
                    maxperc = perc;
                    maxmasodperc = masodperc;
                    hanyadik = adatok[i + 1].vazonosito;
                }
                if (maxora == ora && maxperc == perc && masodperc > maxmasodperc)
                {
                    maxora = ora;
                    maxperc = perc;
                    maxmasodperc = masodperc;
                    hanyadik = adatok[i + 1].vazonosito;
                }
            }

            Console.WriteLine($"{hanyadik}. vendég {maxora}:{maxperc}:{maxmasodperc}");





            Console.WriteLine("\n5. feladat");
            Dictionary<string, int> statisztika = new Dictionary<string, int>();
            statisztika.Add("6-9 óra", 0);
            statisztika.Add("9-16 óra", 0);
            statisztika.Add("16-20 óra", 0);
            List<string> szerepelte = new List<string>();
            foreach (var a in adatok)
            {
                if (!szerepelte.Contains(a.vazonosito.ToString()))
                {
                    if (a.ora > 5 && a.ora < 9) statisztika["6-9 óra"]++;
                    else if (a.ora >= 9 && a.ora < 16) statisztika["9-16 óra"]++;
                    else if (a.ora >= 16 && a.ora < 21) statisztika["16-20 óra"]++;
                    szerepelte.Add(a.vazonosito.ToString());
                }        
            }

            foreach (var a in statisztika)
            {
                Console.WriteLine($"{a.Key} között {a.Value} vendég");
            }

            /*
            List<string> szaunaLista = new List<string>();
            int bora = 0;
            int bperc = 0;
            int bmasodperc = 0;
            int kora = 0;
            int kperc = 0;
            int kmasodperc = 0;
            foreach (var a in azonositokSzerint)
            {
                bora = 0;
                kora = 0;
                bperc = 0;
                kperc = 0;
                bmasodperc = 0;
                kmasodperc = 0;
                foreach (var b in adatok)
                {
                    if (a.Key == b.vazonosito)
                    {
                        if (b.fazonosito == 2 && b.belepes)
                        {
                            bora = b.ora;
                            bperc = b.perc;
                            bmasodperc = b.masodperc;
                        }
                        if (b.fazonosito == 2 && !b.belepes)
                        {
                            kora = b.ora;
                            kperc = b.perc;
                            kmasodperc = b.masodperc;
                        }
                        if (bora != 0 && kora != 0 && bora < kora)
                        {
                            kora = kora - bora;
                            kperc = kperc - bperc;
                            kmasodperc = kmasodperc - bmasodperc;
                        }
                    }
                }
                if (kora != 0 && bora != 0)
                {
                    string kiiras = $"{a.Key} {kora}:{kperc}:{kmasodperc}";
                    szaunaLista.Add(kiiras);
                }
                
            }

            foreach (var a in szaunaLista)
            {
                Console.WriteLine(a);
            }*/


            Console.WriteLine("\n7. feladat");
            Dictionary<string,int> helyekstat = new Dictionary<string, int>();
            helyekstat.Add("Uszoda", 0);
            helyekstat.Add("Szaunák", 0);
            helyekstat.Add("Gyógyvizes medencék", 0);
            helyekstat.Add("Strand", 0);
            bool uszoda = true;
            bool szau = true;
            bool gyogy = true;
            bool strand = true;
            foreach (var a in azonositokSzerint)
            {
                uszoda = true;
                szau = true;
                gyogy = true;
                strand = true;
                foreach (var b in adatok)
                {
                    if (a.Key == b.vazonosito)
                    {
                        if (b.fazonosito == 1 && uszoda)
                        {
                            helyekstat["Uszoda"]++;
                            uszoda = false;
                        }
                        if (b.fazonosito == 2 && szau)
                        {
                            helyekstat["Szaunák"]++;
                            szau = false;
                        }
                        if (b.fazonosito == 3 && gyogy)
                        {
                            helyekstat["Gyógyvizes medencék"]++;
                            gyogy = false;
                        }
                        if (b.fazonosito == 4 && strand)
                        {
                            helyekstat["Strand"]++;
                            strand = false;
                        }
                    }
                }
            }

            foreach (var a in helyekstat)
            {
                Console.WriteLine($"{a.Key}: {a.Value}");
            }

            Console.ReadKey();
        }
    }
}
