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
    public partial class FormSeansListele : Form
    {
        public FormSeansListele()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-4PQU9GCV\\SQLSERVER;Initial Catalog=Sinema_Bileti;Integrated Security=True");
        DataTable tablo = new DataTable();
        private void SeansListesi( string sql)
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter(sql, baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();


        }

        private void FormSeansListele_Load(object sender, EventArgs e)
        {
            tablo.Clear();
            SeansListesi("select *from seans_bilgileri where tarih like'" + dateTimePicker1.Text + "'");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tablo.Clear();
            SeansListesi("select *from seans_bilgileri where tarih like'" + dateTimePicker1.Text + "'");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tablo.Clear();
            SeansListesi("select *from seans_bilgileri");

        }
    }
}
