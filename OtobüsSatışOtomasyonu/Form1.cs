using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtobüsSatışOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmbBoxOtobüs_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbBoxOtobüs.Text)
            {
                case "Efe Tur":KoltukDoldur(8, false);
                    break;
                case "Kamilkoç":KoltukDoldur(12, false);
                    break;
                case "Asya Turizm": KoltukDoldur(10, false);
                    break;
               }
            void KoltukDoldur(int sira, bool arkabeslimi)
            {
                yavaslat:
                foreach(Control ctrl in this.Controls)
                {
                    if( ctrl is Button)
                    {
                        Button btn = ctrl as Button;
                        if (btn.Text == "Kaydet")
                        {
                            continue;
                        }
                        else
                        {
                            this.Controls.Remove(ctrl);
                            goto yavaslat;
                        }
                    }
                }
                int koltukNo = 1;
                for(int i = 0; i < sira; i++)
                {
                    for(int j = 0; j < 5; j++)
                    {
                        if (arkabeslimi == true)
                        {
                            if(i != sira-1 && j == 2)
                            {
                                continue;
                            }
                        
                        }
                        else
                        {
                            if (j == 2)
                            {
                                continue;
                            }
                        }
                        
                        Button koltuk = new Button();
                        koltuk.Height = koltuk.Width = 40;
                        koltuk.Top = 30 + (i * 45);
                        koltuk.Left = 5 + (j * 45);
                        koltuk.Text = koltukNo.ToString();
                        koltukNo++;
                        koltuk.ContextMenuStrip = contextMenuStrip1;
                        koltuk.MouseDown += Koltuk_MouseDown;
                        this.Controls.Add(koltuk);
                    }
                }
            }
        }
        Button tiklanan;
        private void Koltuk_MouseDown(object sender, MouseEventArgs e)
        {
            tiklanan = sender as Button;
        }

        private void rezerveEtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(cmbBoxOtobüs.SelectedIndex == -1 || cmbBoxNereden.SelectedIndex==-1 || cmbBoxNereye.SelectedIndex==-1)
            {
                MessageBox.Show("Lütfen önce gerekli alanları doldurunuz");
                return;
            }
            KayitFormu kf = new KayitFormu();
             DialogResult sonuç=   kf.ShowDialog();
            if(sonuç == DialogResult.OK)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = String.Format("{0} {1}", kf.txtBoxİsim.Text, kf.txtBoxSoyisim.Text);
                lvi.SubItems.Add(kf.mtbTelefon.Text);
                if (kf.rdbBay.Checked)
                {
                    lvi.SubItems.Add("BAY");
                    tiklanan.BackColor = Color.Blue;
                }
                if(kf.rdbBayan.Checked)
                {
                    lvi.SubItems.Add("BAYAN");
                    tiklanan.BackColor = Color.Red;
                }
                lvi.SubItems.Add(cmbBoxNereden.Text);
                lvi.SubItems.Add(cmbBoxNereye.Text);
                lvi.SubItems.Add(tiklanan.Text);
                lvi.SubItems.Add(dtpTarih.Text);
                lvi.SubItems.Add(nudFiyat.Text);
                listView1.Items.Add(lvi);
            }
        }
    }
}
