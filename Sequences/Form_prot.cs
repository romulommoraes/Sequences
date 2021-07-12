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
    public partial class Form_prot : Form
    {
        List<string> prots = new List<string>();
        List<string> heads = new List<string>();
        List<Proteina> total = new List<Proteina>();
        List<string> output = new List<string>();
        List<string> outputCSV = new List<string>();



        public Form_prot()
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
                    progressBar1.Value = i + 1;
                    if (indices[i].StartsWith(">")) //pega os headers das sequencias
                    {
                        secCount++;//soma um na contagem
                        listBox1.Items.Add($"{secCount}{indices[i]}"); //adiciona na listbox
                        heads.Add(indices[i]); //adiciona na lista de headers
                        if (corpo != "") { prots.Add(corpo); corpo = ""; } //se nao estiver vazio (primeiro indice da lista), adiciona as sequencias de linhas concatenadas anteriores

                    }
                    else
                    {
                        Regex reg = new Regex("[A-Z]{1,60}$"); //só sequencias que começam e terminam com letra maíuscula até 60 caracteres (o q cabe numa quebra de linha)
                        if (reg.IsMatch(indices[i]) == true) //se a linha que foi lida corresponde ao regex acima ele concatena. só pra quando nao encontrar mais
                        {
                            corpo = corpo + indices[i];

                        }
                        Regex reg2 = new Regex("[A, C, T, G, U]{50,60}$");//trata erro de inserção de sequencias de DNA
                        if (reg2.IsMatch(indices[i]) == true)
                        {
                            listBox1.Items.Clear();
                            listView1.Clear();
                            prots.Clear();
                            heads.Clear();
                            total.Clear();
                            output.Clear();
                            listBox1.Items.Add("INSIRA UM ARQUIVO DE SEQUENCIAS DE AMINOÁCIDOS");


                        }

                    }

                    if (i == indices.Length - 1)
                    {
                        prots.Add(corpo); corpo = "";


                    } //adiciona a última sequencia q foi concatenada


                }
                if (secCount == 0)
                {
                    listBox1.Items.Add("INSIRA UM ARQUIVO DE SEQUENCIAS NO FORMATO FASTA"); // MENSAGEM CASO A SEQUENCIA NAO SEJA DO FORMATO FASTA
                }
                label1.Text = $"{secCount.ToString()} Sequências carregadas";
                label1.Visible = true;
                progressBar1.Value = 0;
            }
            else
            {
                listBox1.Items.Add("INSIRA UM ARQUIVO .TXT"); // MENSAGEM CASO O ARQUIVO NAO SEJA DE TEXTO
            }

        }

        private void btn_limpar_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listView1.Clear();
            label1.Text = "";
            prots.Clear();
            heads.Clear();
            total.Clear();
            output.Clear();
            outputCSV.Clear();
        }

        private void btn_montar_Click(object sender, EventArgs e)
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

        private void btn_ordenar1_Click(object sender, EventArgs e)
        {
            listBox1.Sorted = true; //faz com q a lista fique ordenada
        }

        private void btn_contar_Click(object sender, EventArgs e)
        {
            listView1.Columns.Add("Sequência", 210);
            listView1.Columns.Add("Nº. AA", 50);
            listView1.Columns.Add("Peso Molecular(D)", 100);
            listView1.Columns.Add("Gravy", 60);
            listView1.Columns.Add("pI", 60);
            listView1.View = View.Details;
            listView1.GridLines = true; //mostra linha de grades
            listView1.FullRowSelect = true; //seleciona toda a linha qdo clica
            listView1.Sorting = SortOrder.Descending; //ordena a lista pela primeira coluna
            progressBar1.Maximum = heads.Count();
            outputCSV.Add("Id\tNºAa\tmW(D)\tGravy\tpI");
            for (int i = 0; i < heads.Count; i++)
            {
                Proteina p = new Proteina(heads[i], prots[i], 0, 0, 0, 0);
                p.nA = p.calcNa();
                p.mW = p.calcMW();
                p.pI = Math.Round(p.calcPI(), 2);
                p.gravy = p.calcGravy();
                total.Add(p);
                output.Add(total[i].ToString());
                outputCSV.Add(total[i].ToCSV());

            }


            foreach (Proteina item in total)
            {
                String[] itemLinha = new string[] { item.head, item.nA.ToString(), item.mW.ToString(), item.gravy.ToString(), item.pI.ToString() };
                listView1.Items.Add(new ListViewItem(itemLinha));//o list view item requer um array como parametro, no caso tem que criar ele pra poder adicionar ao list view
                progressBar1.Value++;
            }
            progressBar1.Value = 0;
        }

        private void btn_sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog outputFile = new SaveFileDialog();
            outputFile.FileName = "OutputCSV"; // Default file name
            outputFile.DefaultExt = ".txt"; // Default file extension
            outputFile.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Displays a SaveFileDialog so the user can save the file

            outputFile.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (outputFile.FileName != "")
            {
                File.WriteAllLines(outputFile.FileName, outputCSV); //grava o arquivo 
            }
        }
    }
}


