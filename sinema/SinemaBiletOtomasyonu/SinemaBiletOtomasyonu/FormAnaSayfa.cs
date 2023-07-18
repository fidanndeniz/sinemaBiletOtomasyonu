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
    public partial class FormAnaSayfa : Form
    {
        public FormAnaSayfa()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-4PQU9GCV\\SQLSERVER;Initial Catalog=Sinema_Bileti;Integrated Security=True");

    


  

       

        private void label10_Click(object sender, EventArgs e)
        {

        }
        int sayac = 0;

        private void FilmveSalonGetir(ComboBox combo,string sql1,string sql2)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sql1, baglanti);
            SqlDataReader read=komut.ExecuteReader();
            while (read.Read())
            {
                combo.Items.Add(read[sql2].ToString());
            }
            baglanti.Close();

        }
        private void FilmAfisiGoster()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from film_bilgileri where filmadi='"+comboFilmAdiBS.SelectedItem+"'", baglanti);
            SqlDataReader read= komut.ExecuteReader();
            while (read.Read())
            {
                pictureBox1.ImageLocation = read["Resim"].ToString();
            }
            baglanti.Close();   
        }
        private void Combo_Dolu_Koltuklar()
        {
            comboKoltukNo.Items.Clear();
            comboKoltukNo.Text = "";

            foreach (Control item in panel1.Controls)
            {
                if (item is Button)
                {
                    if (item.BackColor == Color.Red) { }
                    {
                        comboKoltukNo.Items.Add(item.Text);
                    }
                }
            }
        }


        private void YenidenRenklendir()
        {
            foreach (Control item in panel1.Controls)
            {
                if(item is Button)
                {
                    item.BackColor = Color.White;
                }
            }
        }

        private void Veritabani_Dolu_Koltukalar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from satis_bilgiler where filmadi='"+comboFilmAdiBS.SelectedItem+"'and salonadi='"+comboSalonAdiBS.Text+"' and tarih='"+comboFilmTarihiBS.SelectedItem+"' and saat='"+comboBiletSeansBS.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader(); 
            while (read.Read())
            {
                foreach (Control item in panel1.Controls)
                {

                    if(item is Button) 
                    {
                        if (read["koltukno"].ToString()==item.Text)
                        {
                            item.BackColor = Color.Red;
                        }
                    }
                }
            }
            baglanti.Close();

        }
        private void FormAnaSayfa_Load(object sender, EventArgs e)
        {
            Boş_Koltuklar();
            FilmveSalonGetir(comboFilmAdiBS,"select *from film_bilgileri","filmadi");
            FilmveSalonGetir(comboSalonAdiBS, "select *from salon_bilgileri", "salonadi");

        }

        private void Boş_Koltuklar()
        {
            sayac = 1;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(30, 30);
                    btn.BackColor = Color.White;
                    btn.Location = new Point(j * 30 + 30, i * 30 + 30);
                    btn.Name = sayac.ToString();
                    btn.Text = sayac.ToString();
                    if (j == 4)
                    {
                        continue;
                    } 
                    sayac++;
                    this.panel1.Controls.Add(btn);
                    btn.Click += Btn_Click;
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button b=(Button)sender;
            if (b.BackColor == Color.White)
            {
                textKoltukNo.Text = b.Text;
            }
        }

       

       

        private void comboFilmAdiBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBiletSeansBS.Items.Clear();    
            comboFilmTarihiBS.Items.Clear();
            comboBiletSeansBS.Text = "";
            comboFilmTarihiBS.Text = "";
            comboSalonAdiBS.Text = "";
            foreach (Control item in groupBox1.Controls) if (item is TextBox) item.Text = "";
            FilmAfisiGoster();
            YenidenRenklendir();
            Combo_Dolu_Koltuklar();
        }

        sinemaTableAdapters.Satis_BilgilerTableAdapter satis= new sinemaTableAdapters.Satis_BilgilerTableAdapter();
        private void btnBiletSat_Click(object sender, EventArgs e)
        {
            if (textKoltukNo.Text!="")
            {
                try
                {
                    satis.Satış_Yap(textKoltukNo.Text, comboSalonAdiBS.Text, comboFilmAdiBS.Text, comboFilmTarihiBS.Text, comboBiletSeansBS.Text, textAd.Text, textSoyad.Text, comboUcretBS.Text, DateTime.Now.ToString());
                    foreach (Control item in groupBox1.Controls) if (item is TextBox) item.Text = "";
                    YenidenRenklendir();
                    Veritabani_Dolu_Koltukalar();
                    Combo_Dolu_Koltuklar();
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Hata Oluştu!!!"+hata.Message, "Uyarı");
                } 
            }
            else
            {
                MessageBox.Show("Koltuk Seçimi Yapmadınız", "Uyarı");

            }
        }
        private void Film_Tarihi_Getir()
        {
            comboFilmTarihiBS.Text = "";
            comboBiletSeansBS.Text = "";
            comboFilmTarihiBS.Items.Clear();
            comboBiletSeansBS.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from seans_bilgileri where filmadi='" + comboFilmAdiBS.SelectedItem + "'and salonadi='" + comboSalonAdiBS.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (DateTime.Parse(read["tarih"].ToString()) >= DateTime.Parse(DateTime.Now.ToShortDateString())) { }
                {
                    if (!comboFilmTarihiBS.Items.Contains(read["tarih"].ToString()))
                    {
                        comboFilmTarihiBS.Items.Add(read["tarih"].ToString());

                    }

                }
            }
            baglanti.Close();

        }
        private void comboSalonAdiBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            Film_Tarihi_Getir();
        }
       private void Film_Seansi_Getir()
        {
            comboBiletSeansBS.Text = "";
            comboBiletSeansBS.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from seans_bilgileri where filmadi='" + comboFilmAdiBS.SelectedItem + "'and salonadi='" + comboSalonAdiBS.SelectedItem + "'and tarih='"+comboFilmTarihiBS.SelectedItem +"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (DateTime.Parse(read["tarih"].ToString()) == DateTime.Parse(DateTime.Now.ToShortDateString())) 
                {
                    if (DateTime.Parse(read["seans"].ToString()) > DateTime.Parse(DateTime.Now.ToShortTimeString()))
                    {
                        comboBiletSeansBS.Items.Add(read["seans"].ToString());

                    }

                }
                else if (DateTime.Parse(read["tarih"].ToString()) > DateTime.Parse(DateTime.Now.ToShortDateString())) { }
                {

                        comboBiletSeansBS.Items.Add(read["seans"].ToString());

                  

                }
            }
            baglanti.Close();
        }

        private void comboFilmTarihiBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            Film_Seansi_Getir();
        }

        private void comboBiletSeansBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            YenidenRenklendir();
            Veritabani_Dolu_Koltukalar();
            Combo_Dolu_Koltuklar();
            
        }

        private void btnSeansEkle_Click_1(object sender, EventArgs e)
        {
            FormSeansEkle ekle = new FormSeansEkle();
            ekle.ShowDialog();
            

        }

        private void btnFilmEkle_Click_1(object sender, EventArgs e)
        {
            FormFilmEkleme ekleme = new FormFilmEkleme();
            ekleme.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FormSeansListele listele = new FormSeansListele();
            listele.ShowDialog();
        }

        private void btnSalonEkle_Click_1(object sender, EventArgs e)
        {
            FrmSalonEkle ekle = new FrmSalonEkle();
            ekle.ShowDialog();
        

        }

        private void btnBiletIptal_Click(object sender, EventArgs e)
        {
            if (comboKoltukNo.Text!="")
            {
                try
                {
                    satis.Satis_Iptal(comboFilmAdiBS.Text, comboSalonAdiBS.Text, comboFilmTarihiBS.Text, comboBiletSeansBS.Text, comboKoltukNo.Text);
                    YenidenRenklendir();
                    Veritabani_Dolu_Koltukalar();
                    Combo_Dolu_Koltuklar();
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Hata Oluştu!!!" +hata.Message,"Uyarı");
                }
            }

        }

        private void btnsatislar_Click_1(object sender, EventArgs e)
        {
                FormSatisListeleme satis = new FormSatisListeleme();
            satis.ShowDialog();
        }
    }
}
 
