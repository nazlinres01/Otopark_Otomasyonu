using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Otopark_Otomasyonu
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde olay işleyicisi eklenir
            txtPlaka.KeyDown += new KeyEventHandler(txtPlaka_KeyDown);
        }

        private void txtPlaka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Enter tuşuna basıldığında yapılacak işlem burada olmalıdır
                string plaka = txtPlaka.Text;

                // Rapor parametresine plaka bilgisini iletmek için kullanılır
                ReportParameter parameter = new ReportParameter("PlakaParam", plaka);

                // Rapor görüntüleyicisine parametreyi ekleyin
                this.reportViewer1.LocalReport.SetParameters(parameter);

                // Rapor görüntüleyicisini yenileyin
                this.reportViewer1.RefreshReport();
            }
        }
    }
}
