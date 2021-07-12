
namespace Sequences
{
    partial class Form_prop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.listView2 = new System.Windows.Forms.ListView();
            this.btn_salvar_parametros = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_sair = new System.Windows.Forms.Button();
            this.btn_limpar = new System.Windows.Forms.Button();
            this.btn_calcular = new System.Windows.Forms.Button();
            this.btn_carregar = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 66);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1037, 372);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // listView2
            // 
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(492, 12);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(397, 49);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // btn_salvar_parametros
            // 
            this.btn_salvar_parametros.Location = new System.Drawing.Point(300, 38);
            this.btn_salvar_parametros.Name = "btn_salvar_parametros";
            this.btn_salvar_parametros.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_salvar_parametros.Size = new System.Drawing.Size(90, 23);
            this.btn_salvar_parametros.TabIndex = 17;
            this.btn_salvar_parametros.Text = "Salvar params";
            this.btn_salvar_parametros.UseVisualStyleBackColor = true;
            this.btn_salvar_parametros.Click += new System.EventHandler(this.btn_salvar_parametros_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(895, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "SeqCount";
            this.label1.Visible = false;
            // 
            // btn_sair
            // 
            this.btn_sair.Location = new System.Drawing.Point(396, 38);
            this.btn_sair.Name = "btn_sair";
            this.btn_sair.Size = new System.Drawing.Size(90, 23);
            this.btn_sair.TabIndex = 15;
            this.btn_sair.Text = "Sair";
            this.btn_sair.UseVisualStyleBackColor = true;
            this.btn_sair.Click += new System.EventHandler(this.btn_sair_Click);
            // 
            // btn_limpar
            // 
            this.btn_limpar.Location = new System.Drawing.Point(204, 38);
            this.btn_limpar.Name = "btn_limpar";
            this.btn_limpar.Size = new System.Drawing.Size(90, 23);
            this.btn_limpar.TabIndex = 13;
            this.btn_limpar.Text = "Limpar";
            this.btn_limpar.UseVisualStyleBackColor = true;
            this.btn_limpar.Click += new System.EventHandler(this.btn_limpar_Click);
            // 
            // btn_calcular
            // 
            this.btn_calcular.Location = new System.Drawing.Point(108, 38);
            this.btn_calcular.Name = "btn_calcular";
            this.btn_calcular.Size = new System.Drawing.Size(90, 23);
            this.btn_calcular.TabIndex = 12;
            this.btn_calcular.Text = "Calcular";
            this.btn_calcular.UseVisualStyleBackColor = true;
            this.btn_calcular.Click += new System.EventHandler(this.btn_calcular_Click);
            // 
            // btn_carregar
            // 
            this.btn_carregar.Location = new System.Drawing.Point(12, 38);
            this.btn_carregar.Name = "btn_carregar";
            this.btn_carregar.Size = new System.Drawing.Size(90, 23);
            this.btn_carregar.TabIndex = 11;
            this.btn_carregar.Text = "Carregar";
            this.btn_carregar.UseVisualStyleBackColor = true;
            this.btn_carregar.Click += new System.EventHandler(this.btn_carregar_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(474, 20);
            this.progressBar1.TabIndex = 10;
            // 
            // Form_prop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 450);
            this.Controls.Add(this.btn_salvar_parametros);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_sair);
            this.Controls.Add(this.btn_limpar);
            this.Controls.Add(this.btn_calcular);
            this.Controls.Add(this.btn_carregar);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.listView1);
            this.Name = "Form_prop";
            this.Text = "Propriedades de Alinhamento";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.Button btn_salvar_parametros;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_sair;
        private System.Windows.Forms.Button btn_limpar;
        private System.Windows.Forms.Button btn_calcular;
        private System.Windows.Forms.Button btn_carregar;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}