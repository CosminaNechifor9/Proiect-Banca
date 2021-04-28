using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_Banca
{
    public partial class Creare_cont : Form
    {
        public Creare_cont()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string connetionString = @"Data Source =(LocalDB)\MSSQLLocalDB; AttachDbFilename =C:\Users\Cosmina Nechifor\source\repos\Proiect Banca\test.mdf;Integrated Security = True";
            SqlConnection cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                try
                {
                    string cnp = string.Empty;
                    //stabilire valoare actuala cont beneficiar
                    string cont_platitor = "SELECT * FROM Cont where CNP=" + textBox_CNP_creare.Text;
                    SqlCommand sc2 = new SqlCommand(cont_platitor, cnn);
                    SqlDataReader contGet2 = sc2.ExecuteReader();
                    while (contGet2.Read())
                    {
                        cnp = (contGet2["CNP"].ToString());
                    }
                    sc2.Dispose();
                    if (string.IsNullOrEmpty(cnp))
                    {
                        cnn.Close();
                        cnn.Open();
                        if (int.Parse(textBox2.Text) < 1000 || int.Parse(textBox3.Text) < 1000)
                        {
                            MessageBox.Show("Soldul minim al unui client trebuie sa fie 1000");
                        }
                        else
                        {
                            string tabel_date = "insert into Cont ([CNP],[Cont_RON],[Cont_EURO]) values(@CNP,@RON,@EURO)";
                            SqlCommand sc = new SqlCommand(tabel_date, cnn);
                            sc.Parameters.AddWithValue("@CNP", textBox_CNP_creare.Text);
                            sc.Parameters.AddWithValue("@RON", textBox2.Text);
                            sc.Parameters.AddWithValue("@EURO", textBox3.Text);
                            sc.ExecuteNonQuery();
                            if (MessageBox.Show("Cont creat!", "Confirmare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                            {
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Contul deja exista!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare creare cont " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare conectare " + ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void anulare_Click(object sender, EventArgs e)
        {
            textBox_CNP_creare.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
        }

        private void textBox_CNP_creare_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }
    }
}
