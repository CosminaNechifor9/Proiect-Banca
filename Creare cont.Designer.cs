namespace Proiect_Banca
{
    partial class Creare_cont
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
            this.anulare = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.Euro = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Ron = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.textBox_CNP_creare = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // anulare
            // 
            this.anulare.BackColor = System.Drawing.Color.Transparent;
            this.anulare.ForeColor = System.Drawing.Color.Black;
            this.anulare.Location = new System.Drawing.Point(35, 112);
            this.anulare.Name = "anulare";
            this.anulare.Size = new System.Drawing.Size(77, 23);
            this.anulare.TabIndex = 15;
            this.anulare.Text = "Anulare";
            this.anulare.UseVisualStyleBackColor = false;
            this.anulare.Click += new System.EventHandler(this.anulare_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(81, 81);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(137, 20);
            this.textBox3.TabIndex = 14;
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // Euro
            // 
            this.Euro.AutoSize = true;
            this.Euro.Location = new System.Drawing.Point(32, 84);
            this.Euro.Name = "Euro";
            this.Euro.Size = new System.Drawing.Size(41, 13);
            this.Euro.TabIndex = 13;
            this.Euro.Text = "EURO:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(81, 55);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(137, 20);
            this.textBox2.TabIndex = 12;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // Ron
            // 
            this.Ron.AutoSize = true;
            this.Ron.Location = new System.Drawing.Point(32, 58);
            this.Ron.Name = "Ron";
            this.Ron.Size = new System.Drawing.Size(34, 13);
            this.Ron.TabIndex = 11;
            this.Ron.Text = "RON:";
            // 
            // Button1
            // 
            this.Button1.BackColor = System.Drawing.Color.Transparent;
            this.Button1.ForeColor = System.Drawing.Color.Black;
            this.Button1.Location = new System.Drawing.Point(141, 112);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(77, 23);
            this.Button1.TabIndex = 10;
            this.Button1.Text = "Salveaza";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBox_CNP_creare
            // 
            this.textBox_CNP_creare.Location = new System.Drawing.Point(81, 29);
            this.textBox_CNP_creare.Name = "textBox_CNP_creare";
            this.textBox_CNP_creare.Size = new System.Drawing.Size(137, 20);
            this.textBox_CNP_creare.TabIndex = 9;
            this.textBox_CNP_creare.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_CNP_creare_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "CNP:";
            // 
            // Creare_cont
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 185);
            this.Controls.Add(this.anulare);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.Euro);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Ron);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.textBox_CNP_creare);
            this.Controls.Add(this.label1);
            this.Name = "Creare_cont";
            this.Text = "Creare_cont";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button anulare;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label Euro;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label Ron;
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.TextBox textBox_CNP_creare;
        private System.Windows.Forms.Label label1;
    }
}