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
    public partial class FormSatisListeleme : Form
    {
        public FormSatisListeleme()
        {
            InitializeComponent();
        }
        sinemaTableAdapters.Satis_BilgilerTableAdapter satisListesi =new sinemaTableAdapters.Satis_BilgilerTableAdapter();  
            

        private void FormSatisListeleme_Load(object sender, EventArgs e)
        {
            this.satis_BilgilerTableAdapter.SatisListesi(this.sinema.Satis_Bilgiler);
            dataGridView1.DataSource = satisListesi.TariheGoreListele2(dateTimePicker1.Text);
            ToplamUcretHesapla();
        }

        private void ToplamUcretHesapla()
        {
            int ucrettoplami = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ucrettoplami += Convert.ToInt32(dataGridView1.Rows[i].Cells["ucret"].Value);
            }
            label1.Text = "Toplam Satış=" + ucrettoplami + "TL";
        }

       
        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = satisListesi.SatisListesi2();
            ToplamUcretHesapla();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = satisListesi.TariheGoreListele2(dateTimePicker1.Text);
            ToplamUcretHesapla();

        }
    }
}
