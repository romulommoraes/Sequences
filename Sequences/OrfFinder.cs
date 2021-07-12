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
    public partial class Form_orf : Form
    {
        List<string> seqs = new List<string>();
        List<string> heads = new List<string>();
        List<Sequencia> total = new List<Sequencia>();
        List<string> output = new List<string>();
        List<string> outputCSV = new List<string>();
        List<string> framess = new List<string>();

        public Form_orf()
        {
            InitializeComponent();
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
                        lb_carregadas.Items.Add($"{secCount}{indices[i]}"); //adiciona na listbox
                        heads.Add(indices[i]); //adiciona na lista de headers
                        if (corpo != "") { seqs.Add(corpo); corpo = ""; } //se nao estiver vazio (primeiro indice da lista), adiciona as sequencias de linhas concatenadas anteriores

                    }

                    else
                    {
                        Regex reg = new Regex("[A-Z]{1,60}$"); //só sequencias que começam e terminam com letra maíuscula até 60 caracteres (o q cabe numa quebra de linha)
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
                        if (porcent < teste.Length/2)
                        {
                            lb_carregadas.Items.Clear();
                            listView1.Clear();
                            seqs.Clear();
                            heads.Clear();
                            total.Clear();
                            output.Clear();
                            lb_carregadas.Items.Add("INSIRA UM ARQUIVO DE SEQUENCIAS DE DNA");
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
                    lb_carregadas.Items.Add("INSIRA UM ARQUIVO DE SEQUENCIAS NO FORMATO FASTA"); // MENSAGEM CASO A SEQUENCIA NAO SEJA DO FORMATO FASTA
                }
                label1.Text = $"{secCount.ToString()} Sequências carregadas";
                label1.Visible = true;
                progressBar1.Value = 0;
            }
            else
            {
                lb_carregadas.Items.Add("INSIRA UM ARQUIVO .TXT"); // MENSAGEM CASO O ARQUIVO NAO SEJA DE TEXTO
            }

        }

        private void btn_calcular_Click(object sender, EventArgs e)
        {
            listView1.Columns.Add("Sequência", 190);
            listView1.Columns.Add("Tamanho", 65);
            listView1.Columns.Add("ORF-Tam", 65);
            listView1.Columns.Add("ORF-GC%", 65);
            listView1.Columns.Add("Frame", 50);
            listView1.Columns.Add("ORF", 310);
            listView1.View = View.Details;
            listView1.GridLines = true; //mostra linha de grades
            listView1.FullRowSelect = true; //seleciona toda a linha qdo clica
            listView1.Sorting = SortOrder.Ascending; //ordena a lista pela primeira coluna
            progressBar1.Maximum = heads.Count();
            output.Add("ID\tNºNTs\tTamanho Orf\tGC%\tFrameOrf");
            for (int i = 0; i < heads.Count; i++)
            {
                Sequencia s = new Sequencia(heads[i], seqs[i], 0, "0", 0, "0", 0);
                s.tamanho = s.calcTam();
                s.orf = s.acharOrf();
                s.orfTamanho = s.orfTam();
                s.GCcont = s.calcGC();
                total.Add(s);
                output.Add(total[i].ToString());
                outputCSV.Add(total[i].ToCSV());
                progressBar1.Value++;
            }
            progressBar1.Value = 0;



            foreach (Sequencia item in total)
            {
                String[] itemLinha = new string[] { item.head, item.tamanho.ToString(), item.orfTamanho.ToString(), item.GCcont.ToString(), item.orfFrame, item.orf};
                listView1.Items.Add(new ListViewItem(itemLinha));//o list view item requer um array como parametro, no caso tem que criar ele pra poder adicionar ao list view
                //progressBar1.Value++;
            }
           
        }

        private void btn_limpar_Click(object sender, EventArgs e)
        {
            lb_carregadas.Items.Clear();
            listView1.Clear();
            label1.Text = "";
            seqs.Clear();
            heads.Clear();
            total.Clear();
            output.Clear();
            outputCSV.Clear();
        }

        private void btn_sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_salvar_parametros_Click(object sender, EventArgs e)
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

        private void btn_salvar_seq_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string head = listView1.SelectedItems[0].SubItems[0].Text;
                int index = heads.IndexOf(head);
                string seq = seqs[index];
                Sequencia s = new Sequencia(listView1.SelectedItems[0].SubItems[0].Text, seq, int.Parse(listView1.SelectedItems[0].SubItems[1].Text), listView1.SelectedItems[0].SubItems[5].Text, int.Parse(listView1.SelectedItems[0].SubItems[2].Text), listView1.SelectedItems[0].SubItems[4].Text, double.Parse(listView1.SelectedItems[0].SubItems[3].Text));
                framess = s.acharFrames();//só pra diagnóstico
                SaveFileDialog outputFile = new SaveFileDialog();
                outputFile.FileName = "frames"; // Default file name
                outputFile.DefaultExt = ".txt"; // Default file extension
                outputFile.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

                // Displays a SaveFileDialog so the user can save the file

                outputFile.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (outputFile.FileName != "")
                {
                    File.WriteAllLines(outputFile.FileName, framess); //grava o arquivo 
                }
            }
            catch (Exception)
            {
                MessageBox.Show("SELECIONE UMA SEQUÊNCIA");                
            }

        }
    }
}
