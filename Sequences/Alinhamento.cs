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
    public partial class Conc : Form
    {
        List<string> seqs1 = new List<string>();
        List<string> seqs2 = new List<string>();
        List<string> heads1 = new List<string>();
        List<string> heads2 = new List<string>();
        List<Concat> total = new List<Concat>();
        List<string> output = new List<string>();
        List<string> outputCSV = new List<string>();
        public Conc()
        {
            InitializeComponent();
        }

        private void btn_carr_a1_Click(object sender, EventArgs e)
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
                        lb_a1.Items.Add($"{secCount}{indices[i]}"); //adiciona na listbox
                        heads1.Add(indices[i]); //adiciona na lista de headers
                        if (corpo != "") { seqs1.Add(corpo); corpo = ""; } //se nao estiver vazio (primeiro indice da lista), adiciona as sequencias de linhas concatenadas anteriores

                    }
                    else
                    {
                        Regex reg = new Regex("[A-Z]{1,60}$"); //só sequencias que começam e terminam com letra maíuscula até 60 caracteres (o q cabe numa quebra de linha)
                        if (reg.IsMatch(indices[i]) == true) //se a linha que foi lida corresponde ao regex acima ele concatena. só pra quando nao encontrar mais
                        {
                            corpo = corpo + indices[i];

                        }

                        // VER UMA FORMA DE TRATAR O ERRO DE INSERÇÃO DE SEQUENCIA DE AMINOACIDOS
                        /*
                        Regex reg2 = new Regex("[A, C, T, G, N, R, Y, K, S, W, M, B, D, H, V]{1,60}$");//trata erro de inserção de sequencias de DNA
                        if (reg2.IsMatch(indices[i]) == false)
                        {
                            lb_carregadas.Items.Clear();
                            listView1.Clear();
                            seqs.Clear();
                            heads.Clear();
                            total.Clear();
                            output.Clear();
                            lb_carregadas.Items.Add("INSIRA UM ARQUIVO DE SEQUENCIAS DE DNA");


                        }
                        */
                    }

                    if (i == indices.Length - 1)
                    {
                        seqs1.Add(corpo); corpo = "";


                    } //adiciona a última sequencia q foi concatenada


                }
                if (secCount == 0)
                {
                    lb_a1.Items.Add("INSIRA UM ARQUIVO DE SEQUENCIAS NO FORMATO FASTA"); // MENSAGEM CASO A SEQUENCIA NAO SEJA DO FORMATO FASTA
                }
                label1.Text = $"NºSeqs {secCount.ToString()} ";
                label1.Visible = true;
                progressBar1.Value = 0;

            }
            else
            {
                lb_a1.Items.Add("INSIRA UM ARQUIVO .TXT"); // MENSAGEM CASO O ARQUIVO NAO SEJA DE TEXTO
            }
            //al1 = new List<string>(seqs);
        }

        private void btn_carr_a2_Click(object sender, EventArgs e)
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
                            lb_a2.Items.Add($"{secCount}{indices[i]}"); //adiciona na listbox
                            heads2.Add(indices[i]); //adiciona na lista de headers
                            if (corpo != "") { seqs2.Add(corpo); corpo = ""; } //se nao estiver vazio (primeiro indice da lista), adiciona as sequencias de linhas concatenadas anteriores

                        }
                        else
                        {
                            Regex reg = new Regex("[A-Z]{1,60}$"); //só sequencias que começam e terminam com letra maíuscula até 60 caracteres (o q cabe numa quebra de linha)
                            if (reg.IsMatch(indices[i]) == true) //se a linha que foi lida corresponde ao regex acima ele concatena. só pra quando nao encontrar mais
                            {
                                corpo = corpo + indices[i];

                            }

                        }

                        if (i == indices.Length - 1)
                        {
                            seqs2.Add(corpo); corpo = "";
                          
                    } //adiciona a última sequencia q foi concatenada


                    }
                    if (secCount == 0)
                    {
                        lb_a2.Items.Add("INSIRA UM ARQUIVO DE SEQUENCIAS NO FORMATO FASTA"); // MENSAGEM CASO A SEQUENCIA NAO SEJA DO FORMATO FASTA
                    }
                label2.Text = $"NºSeqs {secCount.ToString()} ";
                label2.Visible = true;
                progressBar1.Value = 0;

            }
                else
                {
                    lb_a2.Items.Add("INSIRA UM ARQUIVO .TXT"); // MENSAGEM CASO O ARQUIVO NAO SEJA DE TEXTO
                }
           // al2 = new List<string>(seqs);
        }

        private void btn_concat_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = heads1.Count;
            string header = $"##Sequencias alinhadas: {textBox1.Text}, {textBox2.Text}##";
            outputCSV.Add(header);

            if (seqs1.Count == seqs2.Count)
            {
                try
                {
                    for (int i = 0; i < heads1.Count; i++)
                    {
                        if (heads1[i] == heads2[i])
                        {
                            Concat s = new Concat(heads1[i], seqs1[i] + seqs2[i], textBox1.Text, textBox2.Text);
                            total.Add(s);
                            output.Add(total[i].ToString());
                            outputCSV.Add(total[i].ToCSV());
                            progressBar1.Value++;
                        }
                    }
                    MessageBox.Show("Sequencias Concatenadas com Sucesso");
                    progressBar1.Value = 0;
                }
                catch (Exception)
                {
                    MessageBox.Show("Existem sequencias com Ids diferentes ou fora de ordem");
                    progressBar1.Value = 0;
                }
            }
            else
            {
                MessageBox.Show("Total de sequências diferente nos arquivos");
            }
        }

        private void btn_salvar_Click(object sender, EventArgs e)
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

        private void btn_limpar1_Click(object sender, EventArgs e)
        {
            lb_a1.Items.Clear();
            label1.Text = "";
            seqs1.Clear();
            heads1.Clear();
            total.Clear();
            output.Clear();
            outputCSV.Clear();
            textBox1.Text = "Seq1 ID";
        }

        private void btn_limpar2_Click(object sender, EventArgs e)
        {
            lb_a2.Items.Clear();
            label2.Text = "";
            seqs2.Clear();
            heads2.Clear();
            total.Clear();
            output.Clear();
            outputCSV.Clear();
            textBox2.Text = "Seq2 ID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
