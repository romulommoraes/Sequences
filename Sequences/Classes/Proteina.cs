using System;
using System.Collections.Generic;
using System.Text;

namespace Sequences
{
    class Proteina //otimizar o código colocando os pesos moleculares e gravy num dicionário
    {
        public Proteina(string head, string seq, int nA, double mW, double gravy, double pI)
        {
            this.head = head;
            this.seq = seq;
            this.nA = nA;
            this.mW = mW;
            this.gravy = gravy;
            this.pI = pI;
        }

        public string head { get; set; }
        public string seq { get; set; }
        public int nA { get; set; }
        public double mW { get; set; }
        public double gravy { get; set; }
        public double pI { get; set; }

        public int calcNa()
        {
            char[] na = this.seq.ToCharArray();
            return na.Length;
        }
        public double calcMW()
        {
            char[] ma = this.seq.ToCharArray();
            double peso = 18.02;//peso de uma molécula de água (faz parte da fórmula)

            foreach (char aa in ma)
            {
                switch (aa)
                {
                    case 'D':
                        peso = peso + 115.0886;
                        break;
                    case 'E':
                        peso = peso + 129.1155;
                        break;
                    case 'C':
                        peso = peso + 103.1388;
                        break;
                    case 'Y':
                        peso = peso + 163.1760;
                        break;
                    case 'H':
                        peso = peso + 137.1411;
                        break;
                    case 'K':
                        peso = peso + 128.1741;
                        break;
                    case 'R':
                        peso = peso + 156.1875;
                        break;
                    case 'M':
                        peso = peso + 131.1926;
                        break;
                    case 'F':
                        peso = peso + 147.1766;
                        break;
                    case 'L':
                        peso = peso + 113.1594;
                        break;
                    case 'V':
                        peso = peso + 99.1326;
                        break;
                    case 'A':
                        peso = peso + 71.0788;
                        break;
                    case 'G':
                        peso = peso + 57.0519;
                        break;
                    case 'Q':
                        peso = peso + 128.1307;
                        break;
                    case 'N':
                        peso = peso + 114.1038;
                        break;
                    case 'I':
                        peso = peso + 113.1594;
                        break;
                    case 'W':
                        peso = peso + 186.2132;
                        break;
                    case 'S':
                        peso = peso + 87.0782;
                        break;
                    case 'P':
                        peso = peso + 97.1167;
                        break;
                    case 'T':
                        peso = peso + 101.1051;
                        break;
                    case 'U':
                        peso = peso + 141.05;
                        break;
                    case 'Z':
                        peso = peso + 128.6231;
                        break;
                    case 'O':
                        peso = peso + 255.31;
                        break;
                    case 'B':
                        peso = peso + 114.5962;
                        break;
                    case 'J':
                        peso = peso + 113.1594;
                        break;
                    default:
                        break;
                }
            }

            peso = Math.Round(peso, 2);
            return peso;
        }

        public double calcGravy()
        {
            char[] ma = this.seq.ToCharArray();
            double gravy = 0;

            foreach (char aa in ma)
            {
                switch (aa)
                {
                    case 'D':
                        gravy = gravy - 3.5;
                        break;
                    case 'E':
                        gravy = gravy - 3.5;
                        break;
                    case 'C':
                        gravy = gravy + 2.5;
                        break;
                    case 'Y':
                        gravy = gravy - 1.3;
                        break;
                    case 'H':
                        gravy = gravy - 3.2;
                        break;
                    case 'K':
                        gravy = gravy - 3.9;
                        break;
                    case 'R':
                        gravy = gravy - 4.5;
                        break;
                    case 'M':
                        gravy = gravy + 1.9;
                        break;
                    case 'F':
                        gravy = gravy + 2.8;
                        break;
                    case 'L':
                        gravy = gravy + 3.8;
                        break;
                    case 'V':
                        gravy = gravy + 4.2;
                        break;
                    case 'A':
                        gravy = gravy + 1.8;
                        break;
                    case 'G':
                        gravy = gravy - 0.4;
                        break;
                    case 'Q':
                        gravy = gravy - 3.5;
                        break;
                    case 'N':
                        gravy = gravy - 3.5;
                        break;
                    case 'I':
                        gravy = gravy + 4.5;
                        break;
                    case 'W':
                        gravy = gravy - 0.9;
                        break;
                    case 'S':
                        gravy = gravy - 0.8;
                        break;
                    case 'P':
                        gravy = gravy - 1.6;
                        break;
                    case 'T':
                        gravy = gravy - 0.7;
                        break;
                    case 'U':
                        gravy = gravy + 0;
                        break;
                    case 'Z':
                        gravy = gravy + 0;
                        break;
                    case 'O':
                        gravy = gravy + 0;
                        break;
                    case 'B':
                        gravy = gravy + 0;
                        break;
                    case 'J':
                        gravy = gravy + 0;
                        break;
                    default:
                        break;
                }
            }
            gravy = Math.Round(gravy / ma.Length, 3);
            return gravy;
        }

        public override string ToString()
        {
            return $"Proteina: {head}, NºAa: {nA}, mW(D): {mW} , Gravy: {gravy}, pI: {pI} \n Sequencia: {seq} \n";
        }
        public string ToCSV()
        {
            return $"{head}\t{nA}\t{mW}\t{gravy}\t{pI}"; // \t{seq}
        }

        public double calcPI() //promost model
        {

            //VALORES PROMOST
            double[] promostK = new double[3] { 10.00, 9.80, 10.30 };
            double[] promostR = new double[3] { 11.50, 12.50, 11.50 };
            double[] promostH = new double[3] { 4.89, 6.08, 6.89 };
            double[] promostD = new double[3] { 3.57, 4.07, 4.57 };
            double[] promostE = new double[3] { 4.15, 4.45, 4.75 };
            double[] promostC = new double[3] { 8.00, 8.28, 9.00 };
            double[] promostY = new double[3] { 9.34, 9.84, 10.34 };
            double[] promostU = new double[3] { 5.20, 5.43, 5.60 };

            //VALORES PROMOST MID

            double[] promostMidG = new double[2] { 7.50, 3.70 };
            double[] promostMidA = new double[2] { 7.58, 3.75 };
            double[] promostMidS = new double[2] { 6.86, 3.61 };
            double[] promostMidP = new double[2] { 8.36, 3.40 };
            double[] promostMidV = new double[2] { 7.44, 3.69 };
            double[] promostMidT = new double[2] { 7.02, 3.57 };
            double[] promostMidC = new double[2] { 8.12, 3.10 };
            double[] promostMidI = new double[2] { 7.48, 3.72 };
            double[] promostMidL = new double[2] { 7.46, 3.73 };
            double[] promostMidJ = new double[2] { 7.46, 3.73 };
            double[] promostMidN = new double[2] { 7.22, 3.64 };
            double[] promostMidD = new double[2] { 7.70, 3.50 };
            double[] promostMidQ = new double[2] { 6.73, 3.57 };
            double[] promostMidK = new double[2] { 6.67, 3.40 };
            double[] promostMidE = new double[2] { 7.19, 3.50 };
            double[] promostMidM = new double[2] { 6.98, 3.68 };
            double[] promostMidH = new double[2] { 7.18, 3.17 };
            double[] promostMidF = new double[2] { 6.96, 3.98 };
            double[] promostMidR = new double[2] { 6.76, 3.41 };
            double[] promostMidY = new double[2] { 6.83, 3.60 };
            double[] promostMidW = new double[2] { 7.11, 3.78 };
            double[] promostMidX = new double[2] { 7.26, 3.57 };
            double[] promostMidZ = new double[2] { 6.96, 3.535 }; //("E"+"Q")/2
            double[] promostMidB = new double[2] { 7.46, 3.57 }; // ("N"+"D")/2
            double[] promostMidU = new double[2] { 5.20, 5.60 };
            double[] promostMidO = new double[2] { 7.00, 3.50 };

            Dictionary<char, double[]> promost = new Dictionary<char, double[]>()
            {
                {'K', promostK},
                {'R', promostR},
                {'H', promostH},
                {'D', promostD},
                {'E', promostE},
                {'C', promostC},
                {'Y', promostY},
                {'U', promostU}
            };

            Dictionary<char, double[]> promostMid = new Dictionary<char, double[]>()
            {
                {'G', promostMidG},
                {'A', promostMidA},
                {'S', promostMidS},
                {'P', promostMidP},
                {'V', promostMidV},
                {'T', promostMidT},
                {'C', promostMidC},
                {'I', promostMidI},
                {'L', promostMidL},
                {'J', promostMidJ},
                {'N', promostMidN},
                {'D', promostMidD},
                {'Q', promostMidQ},
                {'K', promostMidK},
                {'E', promostMidE},
                {'M', promostMidM},
                {'H', promostMidH},
                {'F', promostMidF},
                {'R', promostMidR},
                {'Y', promostMidY},
                {'W', promostMidW},
                {'X', promostMidX},
                {'Z', promostMidZ},
                {'B', promostMidB},
                {'U', promostMidU},
                {'O', promostMidO}
            };


            char[] ma = this.seq.ToCharArray();
            char ultimo = ma[ma.Length - 1];
            //double pI = 0;

            double NQ = 0.0;
            double pH = 6.51;  // starting po pI = 6.5 - theoretically it should be 7, but average protein pI is 6.5 so we increase the probability of finding the solution
            double pHprev = 0.0;
            double pHnext = 14.0;
            double Epsilon = 0.01;  // epsilon means precision [pI = pH +- E]
            double temp = 0.01;
            double QN1;
            double QN2;
            double QN3;
            double QN4;
            double QN5;
            double QP1;
            double QP2;
            double QP3;
            double QP4;

            int Dcount = 0;
            int Ecount = 0;
            int Ccount = 0;
            int Ycount = 0;
            int Hcount = 0;
            int Kcount = 0;
            int Rcount = 0;

            foreach (char aa in ma)
            {
                switch (aa)
                {
                    case 'D':
                        Dcount++;
                        break;
                    case 'E':
                        Ecount++;
                        break;
                    case 'C':
                        Ccount++;
                        break;
                    case 'Y':
                        Ycount++;
                        break;
                    case 'H':
                        Hcount++;
                        break;
                    case 'K':
                        Kcount++;
                        break;
                    case 'R':
                        Rcount++;
                        break;
                    default:
                        break;
                }
            }

            while (true)
            {
                if (promost.ContainsKey(ma[0]))
                {
                    QN1 = -1.0 / (1.0 + Math.Pow(10, (promost[ma[0]][2] - pH)));
                }
                else
                {
                    QN1 = -1.0 / (1.0 + Math.Pow(10, (promostMid[ma[0]][1] - pH)));
                }

                if (promost.ContainsKey(ultimo))
                {
                    QP2 = 1.0 / (1.0 + Math.Pow(10, (pH - promost[ultimo][0])));
                }
                else
                {
                    QP2 = 1.0 / (1.0 + Math.Pow(10, (pH - promostMid[ultimo][0])));
                }

                QN2 = -Dcount / (1.0 + Math.Pow(10, (promost['D'][1] - pH)));
                QN3 = -Ecount / (1.0 + Math.Pow(10, (promost['E'][1] - pH)));
                QN4 = -Ccount / (1.0 + Math.Pow(10, (promost['C'][1] - pH)));
                QN5 = -Ycount / (1.0 + Math.Pow(10, (promost['Y'][1] - pH)));
                QP1 = Hcount / (1.0 + Math.Pow(10, (pH - promost['H'][1])));
                QP3 = Kcount / (1.0 + Math.Pow(10, (pH - promost['K'][1])));
                QP4 = Rcount / (1.0 + Math.Pow(10, (pH - promost['R'][1])));

                NQ = QN1 + QN2 + QN3 + QN4 + QN5 + QP1 + QP2 + QP3 + QP4;

                // %%%%%%%%%%%%%%%%%%%%%%%%%   BISECTION   %%%%%%%%%%%%%%%%%%%%%%%%
                if (NQ < 0.0)//  # we are out of range, thus the new pH value must be smaller
                {
                    temp = pH;
                    pH = pH - ((pH - pHprev) / 2.0);
                    pHnext = temp;
                }

                else
                {
                    temp = pH;
                    pH = pH + ((pHnext - pH) / 2.0);
                    pHprev = temp;
                }


                if ((pH - pHprev < Epsilon) && (pHnext - pH < Epsilon))//  # terminal condition, finding pI with given precision
                {
                    return pH;
                }

            }

        }
    }
}
