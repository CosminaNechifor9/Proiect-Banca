using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_Banca
{
    public partial class Form1 : Form
    {
        
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //// TODO: This line of code loads data into the 'testDataSet5.Notificare' table. You can move, or remove it, as needed.
            //this.notificareTableAdapter3.Fill(this.testDataSet5.Notificare);
            //// TODO: This line of code loads data into the 'testDataSet4.Notificare' table. You can move, or remove it, as needed.
            //this.notificareTableAdapter2.Fill(this.testDataSet4.Notificare);
            //// TODO: This line of code loads data into the 'testDataSet3.Notificare' table. You can move, or remove it, as needed.
            //this.notificareTableAdapter1.Fill(this.testDataSet3.Notificare);
            //// TODO: This line of code loads data into the 'testDataSet2.Notificare' table. You can move, or remove it, as needed.
            //this.notificareTableAdapter.Fill(this.testDataSet2.Notificare);
            valoare_EURO.Text = "";
            valoare_RON.Text = "";
            mesaj_tranzactii.Text = "";
            comboBox1.SelectedText = "--Select--";
            comboBox2.SelectedText = "--Select--";
            // TODO: This line of code loads data into the 'testDataSet1.Cont' table. You can move, or remove it, as needed.
            //this.contTableAdapter1.Fill(this.testDataSet1.Cont);
            //// TODO: This line of code loads data into the 'testDataSet.Cont' table. You can move, or remove it, as needed.
            //this.contTableAdapter.Fill(this.testDataSet.Cont);

            try
            {
                //string connetionString = @"Data Source =(LocalDB)\MSSQLLocalDB; AttachDbFilename =C:\Users\Cosmina Nechifor\source\repos\Proiect Banca\test.mdf;Integrated Security = True";
                string connetionString = @"Data Source =(LocalDB)\MSSQLLocalDB; AttachDbFilename =C:\Users\Cosmina Nechifor\source\repos\Proiect Banca\test.mdf;Integrated Security = True";
                SqlConnection cnn = new SqlConnection(connetionString);
                cnn.Open();
                string tabel_date = "select * from Notificare ORDER BY Id DESC";
                SqlDataAdapter da = new SqlDataAdapter(tabel_date, connetionString);
                DataSet ds = new DataSet();
                da.Fill(ds, "Notificare");
                dataGridView1.DataSource = ds.Tables["Notificare"].DefaultView;
                cnn.Close();
            }
            catch
            {
                MessageBox.Show("Eroare conectare");
            }
        }

        // Creare cont
        private void Button1_Click(object sender, EventArgs e)
        {
            
        }

        // Interogare cont
        private void interogare_Click(object sender, EventArgs e)
        {
            mesaj.Text = string.Empty;
            if (string.IsNullOrEmpty(textBox_CNP_actiuni.Text))
            {
                mesaj.Text = "Introduceti CNP-ul contului";
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
                        string cont = "SELECT * FROM Cont where CNP=" + textBox_CNP_actiuni.Text;
                        SqlCommand sc = new SqlCommand(cont, cnn);
                        SqlDataReader contGet = sc.ExecuteReader();
                        while (contGet.Read())
                        {
                            valoare_RON.Text = (contGet["Cont_RON"].ToString());
                            valoare_EURO.Text = (contGet["Cont_EURO"].ToString());
                        }
                        sc.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Eroare interogare!" + ex.Message);
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

        private void valoare_RON_Click(object sender, EventArgs e)
        {

        }

        // depunere bani
        private void depunere_Click(object sender, EventArgs e)
        {
            mesaj.Text = string.Empty;
            if (string.IsNullOrEmpty(textBox_CNP_actiuni.Text))
            {
                mesaj.Text = "Introduceti CNP-ul contului";
            }
            else
            {
                if (comboBox2.SelectedIndex == -1 || string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Introduceti o valoare si selectati un cont!");
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

                            string old_RON = string.Empty;
                            string old_EURO = string.Empty;
                            string fisc = string.Empty;

                            // stabilire valoare actuala
                            string cont = "SELECT * FROM Cont where CNP=" + textBox_CNP_actiuni.Text;
                            SqlCommand sc1 = new SqlCommand(cont, cnn);
                            SqlDataReader contGet = sc1.ExecuteReader();
                            while (contGet.Read())
                            {
                                old_RON = (contGet["Cont_RON"].ToString());
                                old_EURO = (contGet["Cont_EURO"].ToString());
                                fisc = (contGet["Fisc"].ToString());
                            }
                            sc1.Dispose();
                            cnn.Close();

                            cnn.Open();

                            // calculare noua valoare si stabilirea tipului de cont
                            string new_valoare;
                            string tip_cont;
                            if (comboBox2.SelectedItem.Equals("RON"))
                            {
                                tip_cont = "Cont_RON";
                                new_valoare = (int.Parse(old_RON) + int.Parse(textBox2.Text)).ToString();
                            }
                            else
                            {
                                tip_cont = "Cont_EURO";
                                new_valoare = (int.Parse(old_EURO) + int.Parse(textBox2.Text)).ToString();
                            }


                            //actualizare cont
                            string update = "UPDATE Cont SET  " + tip_cont + "=" + int.Parse(new_valoare) + " where CNP=" + textBox_CNP_actiuni.Text;
                            SqlCommand sc = new SqlCommand(update, cnn);
                            sc.ExecuteNonQuery();
                            mesaj_tranzactii.Text = "Tranzactie realizata cu succes";

                            if(fisc=="True")
                            {
                                cnn.Close();
                                cnn.Open();

                                string notificare = "insert into Notificare ([CNP],[Modificare],[data_tranzactie]) values(@CNP,@Modificare,@data)";
                                SqlCommand scn = new SqlCommand(notificare, cnn);
                                scn.Parameters.AddWithValue("@CNP", textBox_CNP_actiuni.Text);
                                scn.Parameters.AddWithValue("@Modificare", " depunere " + tip_cont + ",sold actual:" + new_valoare);
                                scn.Parameters.AddWithValue("@data", DateTime.Now);
                                scn.ExecuteNonQuery();
                            }

                            //cnn.Close();

                            //cnn.Open();
                            //string cont2 = "SELECT * FROM Cont where CNP=" + textBox_CNP_actiuni.Text;
                            //SqlCommand sc2 = new SqlCommand(cont2, cnn);
                            //SqlDataReader contGet2 = sc2.ExecuteReader();
                            //while (contGet2.Read())
                            //{
                            //    valoare_RON.Text = (contGet2["Cont_RON"].ToString());
                            //    valoare_EURO.Text = (contGet2["Cont_EURO"].ToString());
                            //}
                            //sc2.Dispose();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Eroare depunere!" + ex.Message);
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
        }

        //retragere bani
        private void retragere_Click(object sender, EventArgs e)
        {
            mesaj.Text = string.Empty;
            if (string.IsNullOrEmpty(textBox_CNP_actiuni.Text))
            {
                mesaj.Text = "Introduceti CNP-ul contului";
            }
            else
            {
                if (comboBox2.SelectedIndex==-1 || string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Introduceti o valoare si selectati un cont!");
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

                            string old_RON = string.Empty;
                            string old_EURO = string.Empty;
                            string fisc = string.Empty;
                            //stabilire valoare actuala
                            string cont = "SELECT * FROM Cont where CNP=" + textBox_CNP_actiuni.Text;
                            SqlCommand sc1 = new SqlCommand(cont, cnn);
                            SqlDataReader contGet = sc1.ExecuteReader();
                            while (contGet.Read())
                            {
                                old_RON = (contGet["Cont_RON"].ToString());
                                old_EURO = (contGet["Cont_EURO"].ToString());
                                fisc = (contGet["Fisc"].ToString());
                            }
                            sc1.Dispose();

                            // calculare noua valoare si stabilirea tipului de cont
                            string new_valoare;
                            string tip_cont;
                            
                                if (comboBox2.SelectedItem.Equals("RON"))
                                {
                                    tip_cont = "Cont_RON";
                                    new_valoare = (int.Parse(old_RON) - int.Parse(textBox2.Text)).ToString();
                                }
                                else
                                {
                                    tip_cont = "Cont_EURO";
                                    new_valoare = (int.Parse(old_EURO) - int.Parse(textBox2.Text)).ToString();
                                }
                            if (int.Parse(new_valoare) < 1000)
                            {
                                MessageBox.Show("Sold minim 1000. Nu se poate realiza tranzactia. Pentru un sold<1000 se poate realiza doar transfer");
                            }
                            else
                            {
                                cnn.Close();
                                cnn.Open();
                                //actualizare cont
                                string update = "UPDATE Cont SET  " + tip_cont + "=" + int.Parse(new_valoare) + " where CNP=" + textBox_CNP_actiuni.Text;
                                SqlCommand sc = new SqlCommand(update, cnn); ;
                                sc.ExecuteNonQuery();
                                mesaj_tranzactii.Text = "Tranzactie realizata cu succes";
                                if (fisc == "True")
                                {
                                    cnn.Close();
                                    cnn.Open();

                                    string notificare = "insert into Notificare ([CNP],[Modificare],[data_tranzactie]) values(@CNP,@Modificare,@data)";
                                    SqlCommand scn = new SqlCommand(notificare, cnn);
                                    scn.Parameters.AddWithValue("@CNP", textBox_CNP_actiuni.Text);
                                    scn.Parameters.AddWithValue("@Modificare", " retragere " + tip_cont + ",sold actual:" + new_valoare);
                                    scn.Parameters.AddWithValue("@data", DateTime.Now);
                                    scn.ExecuteNonQuery();
                                    Refresh();
                                }
                                //cnn.Close();
                                //cnn.Open();
                                //string cont2 = "SELECT * FROM Cont where CNP=" + textBox_CNP_actiuni.Text;
                                //SqlCommand sc2 = new SqlCommand(cont2, cnn);
                                //SqlDataReader contGet2 = sc2.ExecuteReader();
                                //while (contGet2.Read())
                                //{
                                //    valoare_RON.Text = (contGet2["Cont_RON"].ToString());
                                //    valoare_EURO.Text = (contGet2["Cont_EURO"].ToString());
                                //}
                                //sc2.Dispose();
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Eroare depunere!" + ex.Message);
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
        }

        // lichidare cont
        private void lichidare_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_CNP_actiuni.Text))
            {
                mesaj.Text = "Introduceti CNP-ul contului";
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
                        string old_RON = string.Empty;
                        string old_EURO = string.Empty;
                        //stabilire valoare actuala
                        string cont = "SELECT * FROM Cont where CNP=" + textBox_CNP_actiuni.Text;
                        SqlCommand sc1 = new SqlCommand(cont, cnn);
                        SqlDataReader contGet = sc1.ExecuteReader();
                        while (contGet.Read())
                        {
                            old_RON = (contGet["Cont_RON"].ToString());
                            old_EURO = (contGet["Cont_EURO"].ToString());
                        }
                        sc1.Dispose();
                        
                        if(old_EURO=="0" && old_RON=="0")
                        {
                            if (MessageBox.Show("Sunteti sigur ca vreti sa lichidati contul? ", "Intrebare", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                cnn.Close();
                                cnn.Open();
                                string stergere = "DELETE FROM Cont where CNP=" + textBox_CNP_actiuni.Text;
                                SqlCommand sc = new SqlCommand(stergere, cnn);
                                sc.ExecuteNonQuery();
                                MessageBox.Show("Cont sters!");
                                valoare_EURO.Text = string.Empty;
                                valoare_RON.Text = string.Empty;
                                textBox_CNP_actiuni.Text = string.Empty;
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Pentru lichidare soldul trebuie sa fie 0");
                        }                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Eroare stergere!" + ex.Message);
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

        private void anulare_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            Creare_cont adaugare_cont = new Creare_cont();
            adaugare_cont.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_CNP_actiuni.Text) || string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show ("Introduceti CNP-urile platitorului si beneficiarului");
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
                        //stabilire si calculare sold platitor dupa transfer
                        
                        string old_RON_platitor = string.Empty;
                        string old_EURO_platitor = string.Empty;
                        string fisc_platitor = string.Empty;
                        //stabilire valoare actuala cont beneficiar
                        string cont_platitor = "SELECT * FROM Cont where CNP=" + textBox_CNP_actiuni.Text;
                        SqlCommand sc2 = new SqlCommand(cont_platitor, cnn);
                        SqlDataReader contGet2 = sc2.ExecuteReader();
                        while (contGet2.Read())
                        {
                            old_RON_platitor = (contGet2["Cont_RON"].ToString());
                            old_EURO_platitor = (contGet2["Cont_EURO"].ToString());
                            fisc_platitor = (contGet2["Fisc"].ToString());
                        }
                        sc2.Dispose();

                        string tip_cont;
                        // calculare noua valoare si stabilirea tipului de cont
                        string new_val_platitor;

                        if (comboBox1.SelectedItem.Equals("RON"))
                        {
                            tip_cont = "Cont_RON";
                            new_val_platitor = (int.Parse(old_RON_platitor) - int.Parse(textBox1.Text)).ToString();
                        }
                        else
                        {
                            tip_cont = "Cont_EURO";
                            new_val_platitor = (int.Parse(old_EURO_platitor) - int.Parse(textBox1.Text)).ToString();
                        }
                        if (int.Parse(new_val_platitor) >= 0)
                        {
                            cnn.Close();
                            cnn.Open();
                            //actualizare cont
                            string update_platitor = "UPDATE Cont SET  " + tip_cont + "=" + int.Parse(new_val_platitor) + " where CNP=" + textBox_CNP_actiuni.Text;
                            SqlCommand sc3 = new SqlCommand(update_platitor, cnn); ;
                            sc3.ExecuteNonQuery();
                            if (fisc_platitor == "True")
                            {
                                cnn.Close();
                                cnn.Open();

                                string notificare = "insert into Notificare ([CNP],[Modificare],[data_tranzactie]) values(@CNP,@Modificare,@data)";
                                SqlCommand scn = new SqlCommand(notificare, cnn);
                                scn.Parameters.AddWithValue("@CNP", textBox_CNP_actiuni.Text);
                                scn.Parameters.AddWithValue("@Modificare", " transfer din " + tip_cont + ",sold actual:" + new_val_platitor);
                                scn.Parameters.AddWithValue("@data", DateTime.Now);
                                scn.ExecuteNonQuery();
                                Refresh();
                            }


                            cnn.Close();
                            cnn.Open();
                            string old_RON = string.Empty;
                            string old_EURO = string.Empty;
                            string fisc_beneficiar = string.Empty;
                            //stabilire valoare actuala cont beneficiar
                            string cont = "SELECT * FROM Cont where CNP=" + textBox5.Text;
                            SqlCommand sc1 = new SqlCommand(cont, cnn);
                            SqlDataReader contGet = sc1.ExecuteReader();
                            while (contGet.Read())
                            {
                                old_RON = (contGet["Cont_RON"].ToString());
                                old_EURO = (contGet["Cont_EURO"].ToString());
                                fisc_beneficiar = (contGet["Fisc"].ToString());
                            }
                            sc1.Dispose();


                            // calculare noua valoare si stabilirea tipului de cont
                            string new_val_beneficiar;

                            if (tip_cont == "Cont_RON")
                            {

                                new_val_beneficiar = (int.Parse(old_RON) + int.Parse(textBox1.Text)).ToString();
                            }
                            else
                            {
                                new_val_beneficiar = (int.Parse(old_EURO) + int.Parse(textBox1.Text)).ToString();
                            }
                            cnn.Close();
                            cnn.Open();
                            //actualizare cont
                            string update = "UPDATE Cont SET  " + tip_cont + "=" + int.Parse(new_val_beneficiar) + " where CNP=" + textBox5.Text;
                            SqlCommand sc = new SqlCommand(update, cnn); ;
                            sc.ExecuteNonQuery();

                            textBox5.Text = string.Empty;
                            textBox1.Text = string.Empty;
                            comboBox1.SelectedIndex = -1;
                            comboBox1.SelectedText = "--Select--";
                           
                            if (fisc_beneficiar == "True")
                            {
                                cnn.Close();
                                cnn.Open();

                                string notificare = "insert into Notificare ([CNP],[Modificare],[data_tranzactie]) values(@CNP,@Modificare,@data)";
                                SqlCommand scn = new SqlCommand(notificare, cnn);
                                scn.Parameters.AddWithValue("@CNP", textBox5.Text);
                                scn.Parameters.AddWithValue("@Modificare", " primire transfer in " + tip_cont + ",sold actual:" + new_val_beneficiar);
                                scn.Parameters.AddWithValue("@data", DateTime.Now);
                                scn.ExecuteNonQuery();
                                Refresh();
                            }
                            MessageBox.Show("Transfer realizat cu succes!");
                        }
                        else
                        {
                            MessageBox.Show("Nu aveti suficiente fonduri pentru transfer");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Eroare transfer!" + ex.Message);
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            textBox5.Text = string.Empty;
            textBox1.Text = string.Empty;
            comboBox1.SelectedIndex = -1;
            comboBox1.SelectedText = "--Select--";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Monitorizare monitorizare = new Monitorizare();
            monitorizare.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void Refresh()
        {
           
            try
            {
                //string connetionString = @"Data Source =(LocalDB)\MSSQLLocalDB; AttachDbFilename =C:\Users\Cosmina Nechifor\source\repos\Proiect Banca\test.mdf;Integrated Security = True";
                string connetionString = @"Data Source =(LocalDB)\MSSQLLocalDB; AttachDbFilename =C:\Users\Cosmina Nechifor\source\repos\Proiect Banca\test.mdf;Integrated Security = True";
                SqlConnection cnn = new SqlConnection(connetionString);
                cnn.Open();
                string tabel_date = "select * from Notificare ORDER BY Id DESC";
                SqlDataAdapter da = new SqlDataAdapter(tabel_date, connetionString);
                DataSet ds = new DataSet();
                da.Fill(ds, "Notificare");
                dataGridView1.DataSource = ds.Tables["Notificare"].DefaultView;
                cnn.Close();
            }
            catch
            {
                MessageBox.Show("Eroare conectare");
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label10.Text = "";
            string connetionString = @"Data Source =(LocalDB)\MSSQLLocalDB; AttachDbFilename =C:\Users\Cosmina Nechifor\source\repos\Proiect Banca\test.mdf;Integrated Security = True";
            SqlConnection cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                string tabel_date = "select * from Notificare where CNP like '" + textBox3.Text + "%' ORDER BY Id DESC";
                SqlDataAdapter da = new SqlDataAdapter(tabel_date, connetionString);
                DataSet ds = new DataSet();
                da.Fill(ds, "Notificare");
                dataGridView1.DataSource = ds.Tables["Notificare"].DefaultView;
                if (dataGridView1.Rows.Count == 1)
                {
                    label10.Text="Nu exista acest cont";
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

        private void button5_Click_1(object sender, EventArgs e)
        {
            
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox_CNP_actiuni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }
    }
}
