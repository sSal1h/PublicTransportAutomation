using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Speech;
using System.Speech.AudioFormat;

namespace NTP_Performans
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        string kullanici1;
        static Form1 frm1 = new Form1();
        OleDbCommand komut;
        DataSet verik;

        public void kullanicisimal(string isim)
        {
            kullanici1 = isim;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            ToolTip Aciklama = new ToolTip();
            Aciklama.SetToolTip(pictureBox8, "Sesli Komut Menüsü");

            label3.Text = kullanici1;

            frm1.baglanti.Close();
            komut = new OleDbCommand();
            frm1.baglan();
            komut.Connection = frm1.baglanti;
            komut.CommandText = "select * from FavoriKartlar where Uye = '" + kullanici1 + "'";

            OleDbDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                string kartno = oku["KartNO"].ToString();

                frm1.baglanti.Close();

                komut = new OleDbCommand();
                frm1.baglan();
                komut.Connection = frm1.baglanti;
                komut.CommandText = "select Bakiye from Kartlar where Kart_Numarasi = " + kartno + "";

                OleDbDataReader oku1 = komut.ExecuteReader();

                label4.Text = oku1["Bakiye"].ToString();
                frm1.baglanti.Close();
            }
            else
            {
                label2.Text = "Favori Kartınız Yok";
                label4.Text = "";
            }
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.BackColor = Color.CornflowerBlue;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            button4.BackColor = Color.CornflowerBlue;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            button5.BackColor = Color.CornflowerBlue;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            panel2.Location = new Point(12, 12);
            this.Height = 430;
            this.Width = 525;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = true;
            panel3.Location = new Point(12, 12);
            this.Height = 423;
            this.Width = 595;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel4.Visible = true;
            panel4.Location = new Point(12, 12);
            this.Height = 430;
            this.Width = 560;
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel1.Visible = true;
            panel3.Location = new Point(807, 12);
            this.Height = 475;
            this.Width = 283;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (label11.Text == "")
            {
                label11.Text = textBox2.Text;
                textBox2.Text = null;
            }
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = label11.Text;
                label11.Text = null;
            }
            if (textBox1.Text == "")
            {
                textBox1.Text = label6.Text;
                label6.Text = null;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pc4();
        }
        private void pc4()
        {
            panel5.Visible = true;
            panel1.Visible = false;
            panel5.Location = new Point(12, 12);
            this.Height = 428;
            this.Width = 485;

            verik = new DataSet();
            OleDbDataAdapter adaptor;

            dataGridView5.Columns.Clear();
            verik.Tables.Clear();
            dataGridView5.Refresh();

            frm1.baglan();
            adaptor = new OleDbDataAdapter("select Kullanici_Adi,E_Posta,Kullanici_Tipi,Kart_Numarasi from Kullanicilar where Kullanici_Adi= '" + kullanici1 + "'", frm1.baglanti);

            adaptor.Fill(verik, "Kullanicilar");
            dataGridView5.DataSource = verik.Tables["Kullanicilar"];
            adaptor.Dispose();
            frm1.baglanti.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
            panel2.Location = new Point(307, 12);
            this.Height = 475;
            this.Width = 283;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel1.Visible = true;
            panel4.Location = new Point(810, 424);
            this.Height = 475;
            this.Width = 283;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            komut = new OleDbCommand();
            frm1.baglan();
            komut.Connection = frm1.baglanti;
            komut.CommandText = "select * from Hatlar where Hat_Kodu LIKE '%" + textBox2.Text + "%'";

            OleDbDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                verik = new DataSet();
                dataGridView1.Columns.Clear();
                verik.Tables.Clear();
                dataGridView1.Refresh();

                frm1.baglanti.Close();
                OleDbDataAdapter adaptor;
                frm1.baglan();
                adaptor = new OleDbDataAdapter("select * from Hatlar where Hat_Kodu LIKE '%" + textBox2.Text + "%'", frm1.baglanti);

                adaptor.Fill(verik, "Hatlar");
                dataGridView1.DataSource = verik.Tables["Hatlar"];
                adaptor.Dispose();
                frm1.baglanti.Close();
            }
            else
            {
                frm1.baglanti.Close();
                komut = new OleDbCommand();
                frm1.baglan();
                komut.Connection = frm1.baglanti;
                komut.CommandText = "select * from Duraklar where DurakNo=" + textBox2.Text + " OR DurakIsmi='" + textBox2.Text + "'";

                OleDbDataReader oku1 = komut.ExecuteReader();

                if (oku1.Read())
                {
                    verik = new DataSet();
                    dataGridView1.Columns.Clear();
                    verik.Tables.Clear();
                    dataGridView1.Refresh();

                    frm1.baglanti.Close();
                    OleDbDataAdapter adaptor;
                    frm1.baglan();
                    adaptor = new OleDbDataAdapter("select * from Duraklar where DurakNo=" + textBox2.Text + " or DurakIsmi='" + textBox2.Text + "'", frm1.baglanti);

                    adaptor.Fill(verik, "Duraklar");
                    dataGridView1.DataSource = verik.Tables["Duraklar"];
                    adaptor.Dispose();
                    frm1.baglanti.Close();
                }
                else
                {
                    MessageBox.Show("Sonuç Bulunamadı.");
                }
            }

        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (label6.Text == "")
            {
                label6.Text = textBox1.Text;
                textBox1.Text = null;
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = label11.Text;
                label11.Text = null;
            }
            if (textBox1.Text == "")
            {
                textBox1.Text = label6.Text;
                label6.Text = null;
            }
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = label11.Text;
                label11.Text = null;
            }
            if (textBox1.Text == "")
            {
                textBox1.Text = label6.Text;
                label6.Text = null;
            }
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = label11.Text;
                label11.Text = null;
            }
            if (textBox1.Text == "")
            {
                textBox1.Text = label6.Text;
                label6.Text = null;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
            frm1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            komut = new OleDbCommand();
            frm1.baglan();
            komut.Connection = frm1.baglanti;
            komut.CommandText = "select * from Kartlar where Kart_Numarasi = " + textBox1.Text + "";

            OleDbDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                verik = new DataSet();
                tabloyusifirla();

                frm1.baglanti.Close();
                OleDbDataAdapter adaptor;
                frm1.baglan();
                adaptor = new OleDbDataAdapter("select * from Kartlar where Kart_Numarasi =" + textBox1.Text + "", frm1.baglanti);

                adaptor.Fill(verik, "Kartlar");
                dataGridView2.DataSource = verik.Tables["Kartlar"];
                adaptor.Dispose();
                frm1.baglanti.Close();

                komut = new OleDbCommand();
                frm1.baglan();
                komut.Connection = frm1.baglanti;
                komut.CommandText = "select * from GecmisKSorgulari where Kart_No= " + textBox1.Text + " and Arama_Yapan='" + kullanici1 + "'";
                OleDbDataReader oku2 = komut.ExecuteReader();

                if (!oku2.Read())
                {
                    komut = new OleDbCommand();
                    frm1.baglan();
                    komut.Connection = frm1.baglanti;
                    komut.CommandText = "Insert into GecmisKSorgulari values (" + textBox1.Text + ",'" + kullanici1 + "')";
                    komut.ExecuteNonQuery();
                    frm1.baglanti.Close();
                }
            }
            else
            {
                MessageBox.Show("Kart Numarası Hatalı");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            komut = new OleDbCommand();
            frm1.baglan();
            komut.Connection = frm1.baglanti;
            komut.CommandText = "select * from FavoriKartlar where KartNo= " + textBox1.Text + " and Uye='" + kullanici1 + "'";
            OleDbDataReader oku2 = komut.ExecuteReader();

            if (!oku2.Read())
            {
                frm1.baglanti.Close();
                komut = new OleDbCommand();
                frm1.baglan();
                komut.Connection = frm1.baglanti;
                komut.CommandText = "Insert into FavoriKartlar values (" + textBox1.Text + ",'" + kullanici1 + "')";
                komut.ExecuteNonQuery();
                frm1.baglanti.Close();
            }
        }

        public void tabloyusifirla()
        {
            dataGridView2.Columns.Clear();
            verik.Tables.Clear();
            dataGridView2.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            komut = new OleDbCommand();
            frm1.baglan();
            komut.Connection = frm1.baglanti;
            komut.CommandText = "select * from GecmisKSorgulari where Arama_Yapan= '" + kullanici1 + "'";

            OleDbDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                verik = new DataSet();
                tabloyusifirla();

                frm1.baglanti.Close();
                OleDbDataAdapter adaptor;
                frm1.baglan();
                adaptor = new OleDbDataAdapter("select * from GecmisKSorgulari where Arama_Yapan= '" + kullanici1 + "'", frm1.baglanti);

                adaptor.Fill(verik, "GecmisKSorgulari");
                dataGridView2.DataSource = verik.Tables["GecmisKSorgulari"];
                adaptor.Dispose();
                frm1.baglanti.Close();
            }
            else
            {
                MessageBox.Show("Geçmişe Ayit Kayıt Bulunamadı");
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            komut = new OleDbCommand();
            frm1.baglan();
            komut.Connection = frm1.baglanti;
            komut.CommandText = "select * from FavoriKartlar where Uye= '" + kullanici1 + "'";

            OleDbDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                verik = new DataSet();
                tabloyusifirla();

                frm1.baglanti.Close();
                OleDbDataAdapter adaptor;
                frm1.baglan();
                adaptor = new OleDbDataAdapter("select * from FavoriKartlar where Uye = '" + kullanici1 + "'", frm1.baglanti);

                adaptor.Fill(verik, "FavoriKartlar");
                dataGridView2.DataSource = verik.Tables["FavoriKartlar"];
                adaptor.Dispose();
                frm1.baglanti.Close();
            }
            else
            {
                MessageBox.Show("Hiç Favori Kartınız Yok");
            }
        }
        string hat;
        private void button7_Click_1(object sender, EventArgs e)
        {
            komut = new OleDbCommand();
            frm1.baglan();
            komut.Connection = frm1.baglanti;
            komut.CommandText = "select * from Hatlar where Rota_Baslangic like '%" + textBox3.Text + "%' and Rota_Bitis like '%" + textBox4.Text + "%'";

            OleDbDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                hat = oku["Hat_Kodu"].ToString();
                verik = new DataSet();
                dataGridView3.Columns.Clear();
                verik.Tables.Clear();
                dataGridView3.Refresh();

                frm1.baglanti.Close();
                OleDbDataAdapter adaptor;
                frm1.baglan();
                adaptor = new OleDbDataAdapter("select * from Hatlar where Rota_Baslangic like '%" + textBox3.Text + "%' and Rota_Bitis like '%" + textBox4.Text + "%'", frm1.baglanti);

                adaptor.Fill(verik, "Hatlar");
                dataGridView3.DataSource = verik.Tables["Hatlar"];
                adaptor.Dispose();
                frm1.baglanti.Close();

                dataGridView4.Columns.Clear();
                verik.Tables.Clear();
                dataGridView4.Refresh();

                frm1.baglanti.Close();
                OleDbDataAdapter adaptor1;
                frm1.baglan();
                adaptor1 = new OleDbDataAdapter("select * from Duraklar where GecenHatlar LIKE '%" + hat + "%'", frm1.baglanti);
                adaptor1.Fill(verik, "Duraklar");
                dataGridView4.DataSource = verik.Tables["Duraklar"];
                adaptor1.Dispose();
                frm1.baglanti.Close();
            }
            else
            {
                MessageBox.Show("Rota Oluşturulamadı Lütfen Tekrar Deneyin");
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            panel1.Visible = true;
            panel5.Location = new Point(-810, -424);
            this.Height = 475;
            this.Width = 283;
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            button9.BackColor = Color.CornflowerBlue;
        }

        private void button9_MouseLeave(object sender, EventArgs e)
        {
            button9.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button11_MouseHover(object sender, EventArgs e)
        {
            button11.BackColor = Color.CornflowerBlue;
        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            button11.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            verik = new DataSet();
            frm1.baglan();
            komut = new OleDbCommand("delete from Kullanicilar where Kullanici_Adi= '" + kullanici1 + "'", frm1.baglanti);
            komut.ExecuteNonQuery();
            frm1.baglanti.Close();

            MessageBox.Show("Hesabınız Başarıyla Silinmiştir. Çikiş Yapılıyor...");

            this.Close();
            frm1.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            verik = new DataSet();
            frm1.baglan();
            komut = new OleDbCommand("select * from Kullanicilar where Kullanici_Adi= '" + kullanici1 + "'", frm1.baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            
            if (oku.Read())
            {
                textBox5.Text = oku["Kullanici_Adi"].ToString();
                textBox9.Text = oku["E_Posta"].ToString();
                textBox10.Text = oku["Kullanici_Tipi"].ToString();
            }

            frm1.baglanti.Close();

            panel5.Visible = false;
            panel6.Visible = true;
            panel6.Location = new Point(12, 12);
        }

        private void button12_MouseHover(object sender, EventArgs e)
        {
            button12.BackColor = Color.CornflowerBlue;
        }

        private void button12_MouseLeave(object sender, EventArgs e)
        {
            button12.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button12_Click(object sender, EventArgs e)
        {
                frm1.baglan();
                komut = new OleDbCommand();
                frm1.baglan();
                komut.Connection = frm1.baglanti;
                komut.CommandText = "select * from Kullanicilar where Kullanici_Adi='" + kullanici1 + "'";

                OleDbDataReader oku1 = komut.ExecuteReader();

                if (oku1.Read())
                {
                    if (textBox6.Text != "")
                    {
                        if (oku1["Sifre"].ToString() == textBox6.Text)
                        {
                            if (textBox7.Text == textBox8.Text)
                            {
                                frm1.baglanti.Close();
                                frm1.baglan();
                                komut = new OleDbCommand("update Kullanicilar set Kullanici_Adi = '" + textBox5.Text + "',Sifre = '" + textBox7.Text + "',E_Posta = '" + textBox9.Text + "' where Kullanici_Adi = '" + kullanici1 + "'", frm1.baglanti);
                                komut.ExecuteNonQuery();
                                frm1.baglanti.Close();

                                kullanici1 = textBox5.Text;

                                MessageBox.Show("Bilgileriniz Başarıyla Güncellenmiştir");
                            }
                            else
                            {
                                MessageBox.Show("Şifreler Birbiri ile Uyuşmuyor");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Geçerli Şifre Hatalı");
                        }
                    }
                    else
                    {
                        frm1.baglanti.Close();
                        frm1.baglan();
                        komut = new OleDbCommand("update Kullanicilar set Kullanici_Adi = '" + textBox5.Text + "',E_Posta = '" + textBox9.Text + "' where Kullanici_Adi = '" + kullanici1 + "'", frm1.baglanti);
                        komut.ExecuteNonQuery();
                        frm1.baglanti.Close();

                        kullanici1 = textBox5.Text;

                        MessageBox.Show("Bilgileriniz Başarıyla Güncellenmiştir");
                    }
                    
                }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
            panel5.Visible = true;
            panel6.Location = new Point(-100, 500);

            verik = new DataSet();
            OleDbDataAdapter adaptor;

            dataGridView5.Columns.Clear();
            verik.Tables.Clear();
            dataGridView5.Refresh();

            frm1.baglan();
            adaptor = new OleDbDataAdapter("select Kullanici_Adi,E_Posta,Kullanici_Tipi,Kart_Numarasi from Kullanicilar where Kullanici_Adi= '" + kullanici1 + "'", frm1.baglanti);

            adaptor.Fill(verik, "Kullanicilar");
            dataGridView5.DataSource = verik.Tables["Kullanicilar"];
            adaptor.Dispose();
            frm1.baglanti.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
            panel1.Visible = false;
            panel7.Location = new Point(12, 12);
        }

        SpeechSynthesizer Sesoku = new SpeechSynthesizer();
        PromptBuilder pbuilder = new PromptBuilder();
        SpeechRecognitionEngine sperecen = new SpeechRecognitionEngine();

        private void pictureBox9_MouseDown(object sender, MouseEventArgs e)
        {
            Choices list = new Choices();
            list.Add(new string[] { "One", "Two", "Three", "Four" });
            Grammar gramer = new Grammar(new GrammarBuilder(list));
            try
            {
                sperecen.RequestRecognizerUpdate();
                sperecen.LoadGrammar(gramer);
                sperecen.SpeechRecognized += sperecen_SpeechRecognized;
                sperecen.SetInputToDefaultAudioDevice();
                sperecen.RecognizeAsync(RecognizeMode.Multiple);

            }
            catch
            {

                return;
            }
        }

        private void pictureBox9_MouseUp(object sender, MouseEventArgs e)
        {
            sperecen.Dispose();
        }

        void sperecen_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "One":
                    panel1.Visible = false;
                    panel2.Visible = true;
                    panel2.Location = new Point(12, 12);
                    this.Height = 430;
                    this.Width = 525;
                    pbuilder.ClearContent();
                    pbuilder.AppendText("Ok, Loading. Please waiting");
                    Sesoku.Speak(pbuilder);
                    break;
                case "Two":
                    panel1.Visible = false;
                    panel3.Visible = true;
                    panel3.Location = new Point(12, 12);
                    this.Height = 423;
                    this.Width = 595;
                    pbuilder.ClearContent();
                    pbuilder.AppendText("Ok, Loading. Please waiting");
                    Sesoku.Speak(pbuilder);
                    break;
                case "Three":
                    panel1.Visible = false;
                    panel4.Visible = true;
                    panel4.Location = new Point(12, 12);
                    this.Height = 430;
                    this.Width = 560;
                    pbuilder.ClearContent();
                    pbuilder.AppendText("Ok, Loading. Please waiting");
                    Sesoku.Speak(pbuilder);
                    break;
                case "Four":
                    pc4();
                    pbuilder.ClearContent();
                    pbuilder.AppendText("Ok, Loading. Please waiting");
                    Sesoku.Speak(pbuilder);
                    break;
                default:
                    break;
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            panel1.Visible = true;
            panel7.Location = new Point(-1000, -1000);
        }
    }
}
