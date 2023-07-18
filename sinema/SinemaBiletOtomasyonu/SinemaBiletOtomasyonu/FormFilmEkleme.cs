using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinemaBiletOtomasyonu
{
    public partial class FormFilmEkleme : Form
    {
        public FormFilmEkleme()
        {
            InitializeComponent();
        }
        //sinemaTableAdapters.Film_BilgileriTableAdapter film =new sinemaTableAdapters.Film_BilgileriTableAdapter();  
       sinemaTableAdapters.Film_BilgileriTableAdapter film=new sinemaTableAdapters.Film_BilgileriTableAdapter();

        private void FormFilmEkleme_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)            //film ekleme butonu

        {
             //film ekleme butonu
            try

            {
               film.FilmEkleme(textFilmAdi.Text, textYonetmen.Text, comboFilmTuru.Text, textYapimYili.Text, textSure.Text, tarih.Text, pictureBox1.ImageLocation);
                MessageBox.Show("Film Bilgileri Eklendi", "Kayıt");

            }

            catch (Exception)
            {

                MessageBox.Show("Bu Film Daha Önce eklendi!!!","Uyarı");
            }

            foreach (Control item in Controls) if (item is TextBox) item.Text = "";
            comboFilmTuru.Text = "";

           
        }

    private void button3_Click(object sender, EventArgs e)
        {
            // afiş seç butonu
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
              
        }
    }
}
