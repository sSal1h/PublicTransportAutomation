using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.OleDb;

namespace NTP_Performans
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public OleDbConnection baglanti;
        OleDbCommand komut;
        OleDbDataAdapter adaptor;
        public DataSet verikumesi;

        public void baglan()
        {
            baglanti = new OleDbConnection("Provider = Microsoft.jet.oledb.4.0; data source = NTP.mdb");
            baglanti.Open();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 nf3 = new Form3();
            komut = new OleDbCommand();
            baglan();
            komut.Connection = baglanti;
            komut.CommandText = "select * from Kullanicilar where Kullanici_Adi='" + textBox1.Text + "' and Sifre='" + textBox2.Text + "'";

            OleDbDataReader oku = komut.ExecuteReader();
            
            if (oku.Read())
            {
                Form2 nf2 = new Form2();
                MessageBox.Show("Giriş Başarılı");
                textBox1.Clear();
                textBox2.Clear();
                if (oku["Kullanici_Tipi"].ToString() == "Normal")
                {
                    string kullaniciad = oku["Kullanici_Adi"].ToString();
                    nf2.kullanicisimal(kullaniciad);
                    this.Hide();
                    nf2.Show();
                }
                else if (oku["Kullanici_Tipi"].ToString() == "Görevli")
                {
                    string kullaniciad = oku["Kullanici_Adi"].ToString();
                    nf3.kullanicisimal(kullaniciad);
                    this.Hide();
                    nf3.Show();
                }
            }
            else
            {
                MessageBox.Show("Hatalı Giriş");
            }
            baglanti.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = true;

            this.Icon = Resource1.users_add_user_icon_6zs_icon;
            this.Text = "Kayıt Ol";
        }

        string e_posta;
        string sifre;

        private void button2_Click(object sender, EventArgs e)
        {
            baglan();
            komut = new OleDbCommand("select Sifre,E_Posta from Kullanicilar where Kullanici_Adi='" + textBox3.Text + "'", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                sifre = oku["Sifre"].ToString();
                e_posta = oku["E_Posta"].ToString();
                textBox8.Text = e_posta; //ePosta.To.Add(textBox8.Text); ifadesi e_posta değişkenini kabul etmediği için arka planda bir texbox oluşturup onun textine yazdım texbox8 arka plandanır

                MailMessage ePosta = new MailMessage();

                ePosta.From = new MailAddress("toplutasimaprogrami@hotmail.com");
                ePosta.To.Add(textBox8.Text);
                ePosta.Subject = "Şifre Hatırlatma";
                ePosta.Body = "Şifrenizi Unuttuğunun Bilgisini Aldık Şifrenizi Hatırlatmak için Bu Maili Gönderiyoruz Umarız Bir Daha Unutmazsınız :) Şifreniz : " + sifre;
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential("toplutasimaprogrami@hotmail.com", "salih123");
                smtp.Port = 587;
                smtp.Host = "smtp.live.com";
                smtp.EnableSsl = true;

                object userState = ePosta;
                bool kontrol = true;
                try
                {
                    smtp.SendAsync(ePosta, (object)ePosta);
                    MessageBox.Show("Mail Başarılı Bir Şekilde Gönderildi Mail Kutunuzu Kontrol Edin");
                    panel2.Visible = false;
                    panel1.Visible = true;
                    textBox3.Clear();
                }
                catch (SmtpException ex)
                {
                    kontrol = false;
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Mail Gönderme Hatasi");
                }
            }
            else
            {
                MessageBox.Show("Bu İsimde Bir Kullanıcı Bulunamadı");
            }
            baglanti.Close();


        }

        public int uyeno = 3;

        private void button3_Click(object sender, EventArgs e)
        {

            komut = new OleDbCommand();
            baglan();
            komut.Connection = baglanti;
            komut.CommandText = "select * from Kullanicilar where Kullanici_Adi='" + textBox4.Text + "'";

            OleDbDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                textBox4.ForeColor = Color.Red;
                label14.Text = "Bu isimde zaten bir kullanıcı var";
                baglanti.Close();
            }
            else
            {
                textBox4.ForeColor = Color.Black;
                baglanti.Close();

                baglan();
                komut = new OleDbCommand();
                baglan();
                komut.Connection = baglanti;
                komut.CommandText = "select * from Kullanicilar where E_Posta='" + textBox7.Text + "'";

                OleDbDataReader oku1 = komut.ExecuteReader();

                if (oku1.Read())
                {
                    textBox7.ForeColor = Color.Red;
                    label14.Text = "Bu E-Posta zaten Kayıtlı";
                    baglanti.Close();
                }
                else
                {
                    textBox7.ForeColor = Color.Black;
                    label14.Text = null;
                    baglanti.Close();

                    if (textBox5.Text == textBox6.Text)
                    {

                        baglan();
                        komut = new OleDbCommand("insert into Kullanicilar (Uye_No,Kullanici_Adi,Sifre,E_Posta,Kullanici_Tipi) values ("+uyeno+",'" + textBox4.Text + "','" + textBox5.Text + "','" + textBox7.Text + "','Normal')", baglanti);
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Kayıt Başarılı");
                        uyeno++;
                        textBox4.Clear();
                        textBox5.Clear();
                        textBox6.Clear();
                        textBox7.Clear();
                        panel3.Visible = false;
                        panel1.Visible = true;
                        this.Icon = Resource1.reviewer_ApT_icon;
                        this.Text = "Giriş";
                    }

                    else
                    {
                        MessageBox.Show("Şifreler Uyuşmuyor");
                        textBox6.Clear();
                        textBox5.Clear();
                    }
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text == textBox6.Text)
            {
                textBox5.ForeColor = Color.Black;
                textBox6.ForeColor = Color.Black;
                label14.Text = null;
            }
            else
            {
                textBox5.ForeColor = Color.Red;
                textBox6.ForeColor = Color.Red;
                label14.Text = "Şifreler Uyuşmuyor";
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text == textBox6.Text)
            {
                textBox5.ForeColor = Color.Black;
                textBox6.ForeColor = Color.Black;
                label14.Text = null;
            }
            else
            {
                textBox5.ForeColor = Color.Red;
                textBox6.ForeColor = Color.Red;
                label14.Text = "Şifreler Uyuşmuyor";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
            textBox3.Clear();
            if (textBox3.Text == "")
            {
                textBox3.Text = label6.Text;
                label6.Text = null;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // pictureboxın üzerine basılı tutuldugunu anlamak için
        {
            textBox2.PasswordChar = '*';
            if (textBox2.Text == "Şifre")
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '\0';
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox5.PasswordChar = '\0';
            textBox6.PasswordChar = '\0';
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            textBox5.PasswordChar = '*';
            textBox6.PasswordChar = '*';
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Resource1.reviewer_ApT_icon;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.CornflowerBlue;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (label1.Text == "")
            {
                label1.Text = textBox1.Text;
                textBox1.Text = null;
            }
            if (textBox2.Text == "")
            {
                textBox2.Text = label2.Text;
                label2.Text = null;
            }
        }

        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (label2.Text == "")
            {
                label2.Text = textBox2.Text;
                textBox2.Text = null;
            }
            if (textBox1.Text == "")
            {
                textBox1.Text = label1.Text;
                label1.Text = null;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "Şifre")
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
            
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            button4.BackColor = Color.CornflowerBlue;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel1.Visible = true;
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            this.Icon = Resource1.reviewer_ApT_icon;
            this.Text = "Giriş";
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            button6.BackColor = Color.CornflowerBlue;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void textBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (label6.Text == "")
            {
                label6.Text = textBox3.Text;
                textBox3.Text = null;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = label2.Text;
                label2.Text = null;
            }
            if (textBox1.Text == "")
            {
                textBox1.Text = label1.Text;
                label1.Text = null;
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = label6.Text;
                label6.Text = null;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = label6.Text;
                label6.Text = null;
            }
            if (textBox2.Text == "")
            {
                textBox2.Text = label2.Text;
                label2.Text = null;
            }
            if (textBox1.Text == "")
            {
                textBox1.Text = label1.Text;
                label1.Text = null;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
