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

namespace NTP_Performans
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        string kullanici1;

        public void kullanicisimal(string isim)
        {
            kullanici1 = isim;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.CornflowerBlue;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.BackColor = Color.CornflowerBlue;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            panel2.Location = new Point(12, 12);
            this.Width = 535;
            this.Height = 370;

            verik = new DataSet();
            OleDbDataAdapter adaptor;

            dataGridView1.Columns.Clear();
            verik.Tables.Clear();
            dataGridView1.Refresh();

            frm1.baglan();
            adaptor = new OleDbDataAdapter("select * from Kullanicilar", frm1.baglanti);

            adaptor.Fill(verik, "Kullanicilar");
            dataGridView1.DataSource = verik.Tables["Kullanicilar"];
            adaptor.Dispose();
            frm1.baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = true;
            panel3.Location = new Point(12, 12);

            this.Width = 730;
            this.Height = 470;
        }
        
        DataSet verik;
        Form1 frm1 = new Form1();

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
            frm1.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel2.Location = new Point(12, 435);
            this.Width = 205;
            this.Height = 285;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel3.Visible = false;
            panel3.Location = new Point(225, 12);
            this.Width = 205;
            this.Height = 285;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            verik = new DataSet();
            OleDbDataAdapter adaptor;

            dataGridView2.Columns.Clear();
            verik.Tables.Clear();
            dataGridView2.Refresh();

            frm1.baglan();
            adaptor = new OleDbDataAdapter("select * from Hatlar", frm1.baglanti);

            adaptor.Fill(verik, "Hatlar");
            dataGridView2.DataSource = verik.Tables["Hatlar"];
            adaptor.Dispose();
            frm1.baglanti.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            verik = new DataSet();
            OleDbCommand komut;

            frm1.baglan();
            komut = new OleDbCommand("insert into Hatlar values ('"+textBox1.Text+"',"+textBox2.Text+",'"+textBox3.Text+"','"+textBox4.Text+"')", frm1.baglanti);
            komut.ExecuteNonQuery();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            verik = new DataSet();
            OleDbCommand komut;

            frm1.baglan();
            komut = new OleDbCommand("select * from Hatlar where Hat_Kodu like '%"+textBox6.Text+"%'", frm1.baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                textBox7.Text = oku["Hat_Kodu"].ToString();
                textBox8.Text = oku["DurakSayısı"].ToString();
                textBox9.Text = oku["Rota_Baslangic"].ToString();
                textBox10.Text = oku["Rota_Bitis"].ToString();
            }
            else
            {
                MessageBox.Show("Durak Kodu Hatalı");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            verik = new DataSet();
            OleDbCommand komut;

            frm1.baglan();
            komut = new OleDbCommand("update Hatlar set Hat_Kodu = '" + textBox7.Text + "', DurakSayısı = " + textBox8.Text + ", Rota_Baslangic = '" + textBox9.Text + "', Rota_Bitis = '" + textBox10.Text + "' where Hat_Kodu like '%"+textBox6.Text+"%'", frm1.baglanti);
            komut.ExecuteNonQuery();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            verik = new DataSet();
            OleDbCommand komut;

            frm1.baglan();
            komut = new OleDbCommand("delete from Hatlar where Hat_Kodu like '%" + textBox5.Text + "%'", frm1.baglanti);
            komut.ExecuteNonQuery();
        }
    }
}
