using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csmfurdostat
{
    internal class Adatsor
    {
        public int vazonosito { get; set; }
        public int fazonosito { get; set; }
        public bool belepes { get; set; }
        public int ora {  get; set; }
        public int perc { get; set; }
        public int masodperc { get; set; }
        public Adatsor(string sor)
        {
            string[] sz = sor.Split(' ');
            vazonosito = int.Parse(sz[0]);
            fazonosito = int.Parse(sz[1]);
            if (int.Parse(sz[2]) == 0)
            {
                belepes = true;
            }
            else belepes = false;
            ora = int.Parse(sz[3]);
            perc = int.Parse(sz[4]);
            masodperc = int.Parse(sz[5]);
        }
    }
}
