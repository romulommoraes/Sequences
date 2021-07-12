using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequences
{
    class PropSeq
    {

        public PropSeq(int SV, int SC, int SPI, int Singletons, double porcVariaveis, double porcPI)
        {
            this.SV = SV;
            this.SC = SC;
            this.SPI = SPI;
            this.Singletons = Singletons;
            this.porcVariaveis = porcVariaveis;
            this.porcPI = porcPI;
        }


        public int SV { get; set; }
        public int SC { get; set; }
        public int SPI { get; set; }
        public int Singletons { get; set; }
        public double porcVariaveis { get; set; } 
        public double porcPI { get; set; }


     
    }
}
