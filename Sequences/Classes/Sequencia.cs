using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequences
{
    class Sequencia
    {
        public Sequencia(string head, string seq, int tamanho, string orf, int orfTamanho, string orfFrame, double GCcont  )
        {
            this.head = head;
            this.seq = seq;
            this.tamanho = tamanho;
            this.orf = orf;
            this.orfTamanho = orfTamanho;
            this.orfFrame = orfFrame;
            this.GCcont = GCcont;

        }

        public string head { get; set; }
        public string seq { get; set; }
        public string orf { get; set; }
        public double GCcont { get; set; } //falta botar pra contar o gc da orf
        public int tamanho { get; set; }
        public int orfTamanho { get; set; } 
        public string orfFrame { get; set; } //falta



        public int calcTam()
        {
            char[] nn = this.seq.ToCharArray();
            return nn.Length;
        }

        public double calcGC()
        {
            char[] nn = this.orf.ToCharArray();
            double tam = nn.Length;
            double gcTot = 0;

            foreach (char nt in nn)
            {
                if (nt == 'G' || nt == 'C')
                {
                    gcTot++;
                }
            }
   
            double gcPorcentagem = Math.Round((gcTot / tam*100), 2) ;

            return gcPorcentagem;
        }

        public List<char> reverseComp(List<char> frame)
        {
            frame.Reverse();

            for (int i = 0; i < frame.Count; i++)
            {
                if (frame[i] == 'A')
                {
                    frame[i] = 'T';
                }
                else if (frame[i] == 'C')
                {
                    frame[i] = 'G';
                }
                else if (frame[i] == 'T')
                {
                    frame[i] = 'A';
                }
                else if (frame[i] == 'G')
                {
                    frame[i] = 'C';
                }
                else
                {
                    frame[i] = 'N';
                    
                }
            }
            return frame;
        }

        public string acharOrf() 
        {
            char[] nn = this.seq.ToCharArray();

            //cria os 6 frames
            List<char> frame1 = new List<char>();
            List<char> frame2 = new List<char>();
            foreach (char nt in nn)
            {
                frame1.Add(nt);
            }

            //procura orf nos 6 frames
   
            string orfF1 = cataOrf(frame1, 0);
            string orfF2 = cataOrf(frame1, 1); 
            string orfF3 = cataOrf(frame1, 2);
            frame2 = reverseComp(frame1);
            string orfF4 = cataOrf(frame2, 0);
            string orfF5 = cataOrf(frame2, 1);
            string orfF6 = cataOrf(frame2, 2);

            bool orfAchada = false;
            string finalOrf = "";

            if (orfF1 != "" || orfF2 != "" || orfF3 != "" || orfF4 != "" || orfF5 != "" || orfF6 != "")
            {
                orfAchada = true;
            }

            if (orfAchada == false )
            {
                finalOrf = "NAO ENCONTRADA";
                return finalOrf;
            }
            else //seleciona a maior orf
            {
                finalOrf = orfF1;
                this.orfFrame = "1";
                if (orfF2.Length > finalOrf.Length)
                {
                    finalOrf = orfF2;
                    this.orfFrame = "2";
                }
                if (orfF3.Length > finalOrf.Length)
                {
                    finalOrf = orfF3;
                    this.orfFrame = "3";
                }
                if (orfF4.Length > finalOrf.Length)
                {
                    finalOrf = orfF4;
                    this.orfFrame = "-1";
                }
                if (orfF5.Length > finalOrf.Length)
                {
                    finalOrf = orfF5;
                    this.orfFrame = "-2";
                }
                if (orfF6.Length > finalOrf.Length)
                {
                    finalOrf = orfF6;
                    this.orfFrame = "-3";
                }

                return finalOrf;
            }
        }

        public string cataOrf(List<char> frame, int id)
        {
            string orf = "";
            string codon = "";
            string utr = "";
            for (int i = id; i < frame.Count; i++)                                 
            {
                if (orf.StartsWith("ATG"))
                {
                    codon = codon + frame[i];
                    if (codon.Length == 3)
                    {
                        orf = orf + codon;
                        codon = "";
                    }
                }
                else
                {
                    codon = codon + frame[i];
                    if (codon.Length == 3 && codon == "ATG")
                    {
                        orf = orf + codon;
                        codon = "";
                    }
                    if (codon.Length == 3 && codon != "ATG")
                    {
                        utr = utr + codon;
                        codon = "";
                    }
                }          


                if (orf.StartsWith("ATG") && orf.Length % 3 == 0 && utr.Length % 3 == 0 && orf.Length >= 50)
                {
                    if (orf.EndsWith("TAA") || orf.EndsWith("TAG") || orf.EndsWith("TGA"))
                    {
                        return orf;
                    }
                }

            }
            return "";
        }


        public int orfTam()
        {
            if (this.orf != "NAO ENCONTRADA")
            {
                char[] nn = this.orf.ToCharArray();
                return nn.Length;
            }
            else
            {
                return 0;
            }

        }


        public override string ToString()
        {
            return $"{head}\t{tamanho}\t{orfTamanho}\t{GCcont}\t{orfFrame}";
        }
        public string ToCSV()
        {
            return $"{head}\n{orf}";
        }


        //função diagnóstica... apagar depois ou modificar pra calcular o frame do selecionado
        public List<string> acharFrames()
        {
            char[] nn = this.seq.ToCharArray();
            List<string> maiorOrf = new List<string>();
            //cria os 6 frames
            List<char> frame1 = new List<char>();
            List<char> frame2 = new List<char>();
            foreach (char nt in nn)
            {
                frame1.Add(nt);
            }

            //procura orf nos 6 frames

            string orfF1 = cataOrf(frame1, 0);
            string orfF2 = cataOrf(frame1, 1);
            string orfF3 = cataOrf(frame1, 2);
            frame2 = reverseComp(frame1);
            string orfF4 = cataOrf(frame2, 0);
            string orfF5 = cataOrf(frame2, 1);
            string orfF6 = cataOrf(frame2, 2);

            bool orfAchada = false;
            string finalOrf = "";

            if (orfF1 != "" || orfF2 != "" || orfF3 != "" || orfF4 != "" || orfF5 != "" || orfF6 != "")
            {
                orfAchada = true;
            }

            if (orfAchada == false)
            {
                finalOrf = "NAO ENCONTRADA";
                return maiorOrf;
            }
            else //seleciona a maior orf
            {
                finalOrf = orfF1;
                this.orfFrame = "1";
                if (orfF2.Length > finalOrf.Length)
                {
                    finalOrf = orfF2;
                    this.orfFrame = "2";
                }
                if (orfF3.Length > finalOrf.Length)
                {
                    finalOrf = orfF3;
                    this.orfFrame = "3";
                }
                if (orfF4.Length > finalOrf.Length)
                {
                    finalOrf = orfF4;
                    this.orfFrame = "-1";
                }
                if (orfF5.Length > finalOrf.Length)
                {
                    finalOrf = orfF5;
                    this.orfFrame = "-2";
                }
                if (orfF6.Length > finalOrf.Length)
                {
                    finalOrf = orfF6;
                    this.orfFrame = "-3";
                }

                maiorOrf.Add(">frame 1");
                maiorOrf.Add(orfF1);
                maiorOrf.Add("\n>frame 2");
                maiorOrf.Add(orfF2);
                maiorOrf.Add("\n>frame 3");
                maiorOrf.Add(orfF3);
                maiorOrf.Add("\n>frame -1");
                maiorOrf.Add(orfF4);
                maiorOrf.Add("\n>frame -2");
                maiorOrf.Add(orfF5);
                maiorOrf.Add("\n>frame -3");
                maiorOrf.Add(orfF6);
 
                return maiorOrf;
            }
        }

    }
}
