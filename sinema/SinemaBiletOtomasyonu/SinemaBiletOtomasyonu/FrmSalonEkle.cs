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
    public partial class FrmSalonEkle : Form
    {
        public FrmSalonEkle()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmSalonEkle_Load(object sender, EventArgs e)
        {
            FormAnaSayfa anasayfa = new FormAnaSayfa();
            anasayfa.ShowDialog();
        }
        sinemaTableAdapters.Salon_BilgileriTableAdapter salon = new sinemaTableAdapters.Salon_BilgileriTableAdapter();
        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                salon.SalonEkleme(textBox1.Text);
                MessageBox.Show("Salon Eklendi", "Kayıt");
            }
            catch (Exception)
            {

                MessageBox.Show("Aynı Salon Daha Önce Eklendi!!!", "Uyarı");
            }
            textBox1.Text = "";
        }
    }
}
