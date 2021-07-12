using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sequences
{
    public partial class Form_prop : Form
    {
        List<string> seqs = new List<string>();
        List<string> heads = new List<string>();
        List<Sequencia> total = new List<Sequencia>();
        List<string> output = new List<string>();
        List<string> outputCSV = new List<string>();
        public Form_prop()
        {
            InitializeComponent();
        }

        private void btn_carregar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";//só permite caregar txts

            if (openFileDialog.ShowDialog() == DialogResult.OK) //se o usuario selecionou um arquivo e clickou em OK
            {
                String filepath = openFileDialog.FileName;

                String[] indices = File.ReadAllLines(filepath);//separa todas as linhas do arquivo numa lista de strings


                string corpo = "";
                int secCount = 0; //contagem numero de sequencias
                progressBar1.Maximum = indices.Length;

                for (int i = 0; i < indices.Length; i++)
                {
                    if (indices[i].StartsWith(">") && indices[i + 1].StartsWith(">"))
                    {
                        MessageBox.Show("ERRO NO CARREGAMENTO DAS SEQUENCIAS");
                        break;
                    }
                    progressBar1.Value = i + 1;
                    if (indices[i].StartsWith(">")) //pega os headers das sequencias
                    {
                        secCount++;//soma um na contagem
                        heads.Add(indices[i]); //adiciona na lista de headers
                        if (corpo != "") { seqs.Add(corpo); corpo = ""; } //se nao estiver vazio (primeiro indice da lista), adiciona as sequencias de linhas concatenadas anteriores

                    }

                    else
                    {
                        Regex reg = new Regex("[A-Z]|[-]{1,60}$"); //só sequencias que começam e terminam com letra maíuscula até 60 caracteres (o q cabe numa quebra de linha)
                        if (reg.IsMatch(indices[i]) == true) //se a linha que foi lida corresponde ao regex acima ele concatena. só pra quando nao encontrar mais
                        {
                            corpo = corpo + indices[i];

                        }

                        int porcent = 0;
                        char[] teste = indices[i].ToCharArray();
                        for (int j = 0; j < teste.Length; j++)
                        {

                            if (teste[j] == 'A' || teste[j] == 'C' || teste[j] == 'T' || teste[j] == 'G')
                            {
                                porcent++;
                            }

                        }
                        if (porcent < teste.Length / 2)
                        {

                            listView1.Clear();
                            seqs.Clear();
                            heads.Clear();
                            total.Clear();
                            output.Clear();
                            MessageBox.Show("INSIRA UM ARQUIVO DE SEQUENCIAS DE DNA");
                            label1.Text = "";
                        }
                    }

                    if (i == indices.Length - 1)
                    {
                        seqs.Add(corpo); corpo = "";


                    } //adiciona a última sequencia q foi concatenada
                }

                if (secCount == 0)
                {
                    MessageBox.Show("INSIRA UM ARQUIVO DE SEQUENCIAS NO FORMATO FASTA"); // MENSAGEM CASO A SEQUENCIA NAO SEJA DO FORMATO FASTA
                }
                label1.Text = $"{secCount.ToString()} Sequências carregadas";
                label1.Visible = true;
                progressBar1.Value = 0;
            }
            else
            {
                MessageBox.Show("INSIRA UM ARQUIVO .TXT"); // MENSAGEM CASO O ARQUIVO NAO SEJA DE TEXTO
            }

        }

        private void btn_calcular_Click(object sender, EventArgs e)
        {
            
            listView1.Columns.Add("Sequência", 150);
            char[] numb = seqs[0].ToCharArray();
            progressBar1.Maximum = (heads.Count() * 2)+numb.Length;
            for (int i = 0; i < numb.Length; i++)
            {
                listView1.Columns.Add($"{i+1}", 25);
                progressBar1.Value++;
            }

            listView2.Columns.Add("Tamanho", 65);
            listView2.Columns.Add("S.Variaveis", 70);
            listView2.Columns.Add("S.Conservados", 90);
            listView2.Columns.Add("Singletons", 72);
            listView2.Columns.Add("P. informativos", 95);
            listView2.View = View.Details;
            listView2.GridLines = true; //mostra linha de grades
            listView2.FullRowSelect = true; //seleciona toda a linha qdo clica

            listView1.View = View.Details;
            listView1.GridLines = true; //mostra linha de grades
            listView1.FullRowSelect = true; //seleciona toda a linha qdo clica
            //listView1.Sorting = SortOrder.Ascending; //ordena a lista pela primeira coluna

            output.Add("ID\tNºNTs\tTamanho Orf\tGC%\tFrameOrf");
            for (int i = 0; i < heads.Count; i++)
            {
                Sequencia s = new Sequencia(heads[i], seqs[i], 0, "0", 0, "0", 0);

                s.tamanho = s.calcTam();
                total.Add(s);
                output.Add(total[i].ToString());
                outputCSV.Add(total[i].ToCSV());
                progressBar1.Value++;
            }

            PropSeq ps = new PropSeq(0, 0, 0, 0, 0, 0);

            char[] alinhaSizeArray = total[1].seq.ToCharArray();
            int alinhaSize = alinhaSizeArray.Length;

            List<List<char>> ntList = new List<List<char>>();//UMA LISTA DENTRO DA OUTRA.
            for (int i = 0; i < total.Count; i++)
            {
                List<char> sequencia = new List<char>();//LISTA DE SEQUENCIAS
                sequencia.AddRange(total[i].seq);//CADA SEQUENCIA É UMA LISTA DE CHARS
                ntList.Add(sequencia);               
                            

                char[] itemLinha = total[i].seq.ToCharArray();//PRECISA TRANSFORMAR O ARRAY DE CHARS EM ARRAY DE STRINGS PRA PODER CARREGAR NO LISTVIEW1
                string[] stringLinha = new string[itemLinha.Length+1];
                stringLinha[0] = total[i].head;

                for (int j = 1; j < stringLinha.Length; j++)
                {    
                    stringLinha[j] = itemLinha[j-1].ToString();
                    
                }
                listView1.Items.Add(new ListViewItem(stringLinha));//o list view item requer um array como parametro, no caso tem que criar ele pra poder adicionar ao list view
                progressBar1.Value++;
            }
           
            int arraymaior = ntList.Count;//ARRAY DAS SEQUENCIAS
            int arraymenor = ntList[0].Count;//ARRAY DE CHARS --> tem que indicar o numero da maior sequencia da lista, ou usar um alinhamento(DÁ ERRO SE TIVER SEQUENCIAS DE TAMANHOS DIFERENTES)

            for (int i = 0; i < arraymenor; i++)
            {
                List<char> posit = new List<char>();
                int freqA = 0;
                int freqC = 0;
                int freqT = 0;
                int freqG = 0;
                int freqGap = 0;
                int freqN = 0;

                try
                {
                    for (int j = 0; j < arraymaior; j++)
                    {
                        posit.Add(ntList[j][i]);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("SEQUENCIAS COM TAMANHOS DIFERENTES");
                    listView2.Clear();
                    listView1.Clear();
                    label1.Text = "";
                    seqs.Clear();
                    heads.Clear();
                    total.Clear();
                    output.Clear();
                    outputCSV.Clear();
                    break;
                }
                                
                                
                foreach (char nt in posit)
                {
                    switch (nt)
                    {
                        case 'A':
                            freqA++;
                            break;
                        case 'C':
                            freqC++;
                            break;
                        case 'T':
                            freqT++;
                            break;
                        case 'G':
                            freqG++;
                            break;
                        case '-':
                            freqGap++;
                            break;
                        default:
                            freqN++;
                            break;
                    }
                }
                //CALCULA FREQUENCIA DE SINGLETONS
                if ((freqA == posit.Count - 1) || (freqC == posit.Count - 1) || (freqT == posit.Count - 1) || (freqG == posit.Count - 1) || (freqN == posit.Count - 1) || (freqGap == posit.Count - 1))
                {
                    ps.Singletons++;
                }

                //tanto faz essa ou a de cima... tem algo errado ou aqui ou no mega.
                //if ((freqA == 1 && (freqC == posit.Count - 1 || freqT == posit.Count - 1 || freqG == posit.Count - 1)) ||
                //    (freqC == 1 && (freqA == posit.Count - 1 || freqT == posit.Count - 1 || freqG == posit.Count - 1)) ||
                //    (freqT == 1 && (freqC == posit.Count - 1 || freqA == posit.Count - 1 || freqG == posit.Count - 1)) ||
                //    (freqG == 1 && (freqC == posit.Count - 1 || freqT == posit.Count - 1 || freqA == posit.Count - 1)))
                //{
                //    ps.Singletons++;
                //}

                //CALCULA A FREQUENCIA DE SITIOS PARSIMONIA INFORMATIVOS
                if ((freqA >= 2 && freqC >= 2)|| (freqA >= 2 && freqG >= 2) || (freqA >= 2 && freqT >= 2) || (freqG >= 2 && freqC >= 2) || (freqG >= 2 && freqT >= 2) || (freqC >= 2 && freqT >= 2))
                {
                    ps.SPI++;
                }
                //CALC. FREQUENCIA DE SITIOS CONSERVADOS
                if (freqA == posit.Count || freqC == posit.Count || freqT == posit.Count || freqG == posit.Count)
                {
                    ps.SC++;
                }

                //MENSAGEM DE ERRO
                if (freqA+freqC+freqG+freqT+freqN+freqGap != posit.Count)
                {
                    MessageBox.Show("....");
                }
                //CALCULA SITIOS VARIAVEIS
                ps.SV = arraymenor - ps.SC;

            }

            String[] lv2 = new string[] { (alinhaSize).ToString(), ps.SV.ToString(), ps.SC.ToString(), ps.Singletons.ToString(), ps.SPI.ToString() };
            listView2.Items.Add(new ListViewItem(lv2));
            progressBar1.Value = 0;
        }

        private void btn_limpar_Click(object sender, EventArgs e)
        {
            listView2.Clear();
            listView1.Clear();
            label1.Text = "";
            seqs.Clear();
            heads.Clear();
            total.Clear();
            output.Clear();
            outputCSV.Clear();
        }

        private void btn_salvar_parametros_Click(object sender, EventArgs e) //FALTA AJEITAR ESSA
        {
            SaveFileDialog outputFile = new SaveFileDialog();
            outputFile.FileName = "Document"; // Default file name
            outputFile.DefaultExt = ".txt"; // Default file extension
            outputFile.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Displays a SaveFileDialog so the user can save the file

            outputFile.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (outputFile.FileName != "")
            {
                File.WriteAllLines(outputFile.FileName, output); //grava o arquivo 
            }
        }

        private void btn_sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
