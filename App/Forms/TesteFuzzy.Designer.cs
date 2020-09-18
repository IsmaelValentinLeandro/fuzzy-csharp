namespace App
{
    partial class TesteFuzzy
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
            this.button2 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.txtUni1 = new System.Windows.Forms.TextBox();
            this.txtUni2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSup2 = new System.Windows.Forms.TextBox();
            this.txtSup1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNuc2 = new System.Windows.Forms.TextBox();
            this.txtNuc1 = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblResultado = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 172);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(188, 37);
            this.button2.TabIndex = 1;
            this.button2.Text = "FUZZY";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtUni1
            // 
            this.txtUni1.Location = new System.Drawing.Point(12, 25);
            this.txtUni1.Name = "txtUni1";
            this.txtUni1.Size = new System.Drawing.Size(42, 20);
            this.txtUni1.TabIndex = 2;
            this.txtUni1.Text = "0";
            this.txtUni1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtUni2
            // 
            this.txtUni2.Location = new System.Drawing.Point(60, 25);
            this.txtUni2.Name = "txtUni2";
            this.txtUni2.Size = new System.Drawing.Size(42, 20);
            this.txtUni2.TabIndex = 3;
            this.txtUni2.Text = "0";
            this.txtUni2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Universo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Suporte";
            // 
            // txtSup2
            // 
            this.txtSup2.Location = new System.Drawing.Point(60, 65);
            this.txtSup2.Name = "txtSup2";
            this.txtSup2.Size = new System.Drawing.Size(42, 20);
            this.txtSup2.TabIndex = 6;
            this.txtSup2.Text = "0";
            this.txtSup2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSup1
            // 
            this.txtSup1.Location = new System.Drawing.Point(12, 65);
            this.txtSup1.Name = "txtSup1";
            this.txtSup1.Size = new System.Drawing.Size(42, 20);
            this.txtSup1.TabIndex = 5;
            this.txtSup1.Text = "0";
            this.txtSup1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Núcleo";
            // 
            // txtNuc2
            // 
            this.txtNuc2.Location = new System.Drawing.Point(60, 107);
            this.txtNuc2.Name = "txtNuc2";
            this.txtNuc2.Size = new System.Drawing.Size(42, 20);
            this.txtNuc2.TabIndex = 9;
            this.txtNuc2.Text = "0";
            this.txtNuc2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNuc1
            // 
            this.txtNuc1.Location = new System.Drawing.Point(12, 107);
            this.txtNuc1.Name = "txtNuc1";
            this.txtNuc1.Size = new System.Drawing.Size(42, 20);
            this.txtNuc1.TabIndex = 8;
            this.txtNuc1.Text = "0";
            this.txtNuc1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(12, 146);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(90, 20);
            this.txtValue.TabIndex = 11;
            this.txtValue.Text = "0";
            this.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Valor Entrada";
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(9, 213);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(0, 13);
            this.lblResultado.TabIndex = 13;
            // 
            // TesteFuzzy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 278);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNuc2);
            this.Controls.Add(this.txtNuc1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSup2);
            this.Controls.Add(this.txtSup1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUni2);
            this.Controls.Add(this.txtUni1);
            this.Controls.Add(this.button2);
            this.Name = "TesteFuzzy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fuzzy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox txtUni1;
        private System.Windows.Forms.TextBox txtUni2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSup2;
        private System.Windows.Forms.TextBox txtSup1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNuc2;
        private System.Windows.Forms.TextBox txtNuc1;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblResultado;
    }
}

