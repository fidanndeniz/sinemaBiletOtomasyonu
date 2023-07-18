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
    public partial class SeansListele : Form
    {
        public SeansListele()
        {
            InitializeComponent();
        }

        
        private void SeansListele_Load(object sender, EventArgs e)
        {
            FormAnaSayfa anasayfa = new FormAnaSayfa();
            anasayfa.Show();

        }
    }
}
