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
    public partial class Monitorizare : Form
    {
        public Monitorizare()
        {
            InitializeComponent();
        }

        private void Verificare_status()
        {
            if (string.IsNullOrEmpty(textBox_CNP.Text))
            {
                MessageBox.Show("Introduceti CNP-ul contului");
            }
            else
            {
                string connetionString = @"Data Source =(LocalDB)\MSSQLLocalDB; AttachDbFilename =C:\Users\Cosmina Nechifor\source\repos\Proiect Banca\test.mdf;Integrated Security = True";
                SqlConnection cnn = new SqlConnection(connetionString);
                try
                {
                    cnn.Open();
                    try
                    {
                        string stare = string.Empty;
                        string cont = "SELECT * FROM Cont where CNP=" + textBox_CNP.Text;
                        SqlCommand sc = new SqlCommand(cont, cnn);
                        SqlDataReader contGet = sc.ExecuteReader();
                        while (contGet.Read())
                        {
                            stare = (contGet["Fisc"].ToString());
                        }
                        sc.Dispose();
                        if (string.IsNullOrEmpty(stare))
                        {
                            label3.Text = "Contul nu exista";
                        }
                        else if (stare.Equals("False"))
                        {
                            label3.Text = "Contul nu este monitorizat";
                        }
                        else if (stare.Equals("True"))
                        {
                            label3.Text = "Contul este monitorizat";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Eroare cautare!" + ex.Message);
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
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Verificare_status();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connetionString = @"Data Source =(LocalDB)\MSSQLLocalDB; AttachDbFilename =C:\Users\Cosmina Nechifor\source\repos\Proiect Banca\test.mdf;Integrated Security = True";
            SqlConnection cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                try
                {
                    string update = "UPDATE Cont SET  Fisc=1  where CNP=" + textBox_CNP.Text;
                    SqlCommand sc = new SqlCommand(update, cnn);
                    sc.ExecuteNonQuery();
                    MessageBox.Show("Urmarire realizata!" );
                    Verificare_status();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare urmarire!" + ex.Message);
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

        private void button3_Click(object sender, EventArgs e)
        {
            string connetionString = @"Data Source =(LocalDB)\MSSQLLocalDB; AttachDbFilename =C:\Users\Cosmina Nechifor\source\repos\Proiect Banca\test.mdf;Integrated Security = True";
            SqlConnection cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                try
                {
                    string update = "UPDATE Cont SET  Fisc=0  where CNP=" + textBox_CNP.Text;
                    SqlCommand sc = new SqlCommand(update, cnn);
                    sc.ExecuteNonQuery();
                    MessageBox.Show("Urmarire oprita!");
                    Verificare_status();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare urmarire!" + ex.Message);
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

        private void button4_Click(object sender, EventArgs e)
        {
            textBox_CNP.Text = string.Empty;
        }

        private void textBox_CNP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }
    }
}
