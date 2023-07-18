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

namespace SinemaBiletOtomasyonu
{
    public partial class FormSeansEkle : Form
    {
        public FormSeansEkle()
        {
            InitializeComponent();
        }

        sinemaTableAdapters.Seans_BilgileriTableAdapter filmsenasi = new sinemaTableAdapters.Seans_BilgileriTableAdapter();
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-4PQU9GCV\\SQLSERVER;Initial Catalog=Sinema_Bileti;Integrated Security=True");

  
        private void FormSeansEkle_Load(object sender, EventArgs e)
        {
             FilmVeSalonGoster(comboFilm, "select *from film_bilgileri", "filmadi");
            FilmVeSalonGoster(comboSalon, "select *from salon_bilgileri", "salonadi");


        }
        private void FilmVeSalonGoster(ComboBox combo, string sql, string sql2)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sql, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read()==true)
            {
                combo.Items.Add(read[sql2].ToString());
            }
            baglanti.Close();
        }

        string seans = "";
        private void RadioButtonSeçiliyse()
        {
            if(radioButton1.Checked==true) seans= radioButton1.Text;
            else if (radioButton2.Checked == true) seans = radioButton2.Text;
            else if (radioButton3.Checked == true) seans = radioButton3.Text;
            else if (radioButton4.Checked == true) seans = radioButton4.Text;
            else if (radioButton5.Checked == true) seans = radioButton5.Text;
            else if (radioButton6.Checked == true) seans = radioButton6.Text;
            else if (radioButton7.Checked == true) seans = radioButton7.Text;
            else if (radioButton8.Checked == true) seans = radioButton8.Text;
            else if (radioButton9.Checked == true) seans = radioButton9.Text;
            else if (radioButton10.Checked == true) seans = radioButton10.Text;
            else if (radioButton11.Checked == true) seans = radioButton11.Text;
            else if (radioButton12.Checked == true) seans = radioButton12.Text;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RadioButtonSeçiliyse();

            if (seans != "")
            {
                filmsenasi.SeansEkleme(comboFilm.Text, comboSalon.Text, dateTimePicker1.Text, seans);
                MessageBox.Show("Sean Ekleme İşlemi Yapıldı", "Kayıt");

            }
            else if (seans == "")
            {
                MessageBox.Show("Seans Seçme İşlemi Yapmadınız!!!", "Uyarı");
            }

            comboSalon.Text = "";
            comboFilm.Text = "";
            dateTimePicker1.Text = DateTime.Now.ToShortDateString();

        }

      

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control item3 in groupBox1.Controls)
            {
                item3.Enabled= true;
            }
            DateTime bugün =DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime yeni = DateTime.Parse(dateTimePicker1.Text);
            if (yeni == bugün)
            {
                foreach(Control item in groupBox1.Controls)
                {
                    if (DateTime.Parse(DateTime.Now.ToShortDateString()) > DateTime.Parse(item.Text))
                    {
                        item.Enabled = false;
                    }
                }
                Tarihi_Karsilastir();
            }
            else if (yeni > bugün)
            {
                Tarihi_Karsilastir();
            }
            else if (yeni < bugün)
            {
                MessageBox.Show("Geriye Dönük İşlem Yapılamaz!!!","Uyarı");
                dateTimePicker1.Text=DateTime.Now.ToShortDateString();  
            }
        }

        private void Tarihi_Karsilastir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from seans_bilgileri where salonadi='" + comboSalon.Text + "' and tarih='" + dateTimePicker1.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read() == true)
            {
                foreach (Control item2 in groupBox1.Controls)
                {
                    if (read["seans"].ToString() == item2.Text)
                    {
                        item2.Enabled = false;
                    }
                }
            }
            baglanti.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {


        }

        private void comboSalon_SelectedIndexChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Text = DateTime.Now.ToShortDateString();

        }
    }
}
