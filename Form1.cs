using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Otopark_Otomasyonu
{
    public partial class Form1 : Form
    {
        private int defaultCapacity = 200; // Varsayılan kapasite sayısı
        private decimal defaultPrice = 7; // Varsayılan fiyat
        public Form1()
        {
            InitializeComponent();
            InitializeDateTimeControls();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtPrice.Text = "7"; // Varsayılan fiyat değeri
        }

        private void InitializeDateTimeControls()
        {
            DateTime currentDate = DateTime.Now;

            dateTimePicker1.Value = currentDate.Date;
            txtTime.Text = currentDate.ToString("HH:mm:ss");

        }


        private bool GecerliPlakaKodu(string plakaKodu)
        {
            // Türkiye plaka kodu formatına uygun bir regex deseni
            string desen = @"^(0[1-9]|[1-7][0-9]|8[01])(([A-Z])(\d{4,5})|([A-Z]{2})(\d{3,4})|([A-Z]{3})(\d{2,3}))$";

            // Regex deseni ile girilen plaka kodu karşılaştırıldı
            if (Regex.IsMatch(plakaKodu, desen))
            {
                return true;
            }

            return false;
        }

        public string conString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=otoparkDB; MultipleActiveResultSets=True";

        private void btnEnter_Click(object sender, EventArgs e)
        {
            string plakaKodu = txtLicensePlate.Text.Trim();

            if (GecerliPlakaKodu(plakaKodu))
            {
                using (SqlConnection baglanti = new SqlConnection(conString))
                {
                    baglanti.Open();

                    if (baglanti.State == System.Data.ConnectionState.Open)
                    {
                        // Giriş sayısını almak için sorgu
                        string girişSayısıSorgusu = "SELECT COUNT(*) FROM OtoparkGirisCikis_TBL";
                        SqlCommand girişSayısıKomut = new SqlCommand(girişSayısıSorgusu, baglanti);
                        int girisSayisi = (int)girişSayısıKomut.ExecuteScalar();

                        int kapasiteLimiti = 200; // Default olarak 200 kapasite

                        if (girisSayisi >= kapasiteLimiti)
                        {
                            MessageBox.Show("Otopark kapasitesi dolu, giriş yapılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            // Otoparka giriş kaydını ekleyin
                            string girişSorgusu = "INSERT INTO OtoparkGirisCikis_TBL (plakaNo, GirisTarihi, GirisSaati) VALUES (@Plaka, @Tarih, @Saat)";
                            SqlCommand girişKomut = new SqlCommand(girişSorgusu, baglanti);
                            girişKomut.Parameters.AddWithValue("@Plaka", txtLicensePlate.Text);
                            girişKomut.Parameters.AddWithValue("@Tarih", dateTimePicker1.Value);
                            girişKomut.Parameters.AddWithValue("@Saat", txtTime.Text);

                            girişKomut.ExecuteNonQuery();

                            // Kapasite güncellendi ve txtCapacity kontrolünde gösterildi
                            kapasiteLimiti = 200 - girisSayisi;
                            txtCapacity.Text = kapasiteLimiti.ToString();

                            // Giriş başarılı mesajı gösterildi
                            MessageBox.Show("Giriş başarıyla tamamlandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Geçerli bir plaka kodu değil.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            string plakaKodu = txtLicensePlate.Text.Trim();

            if (GecerliPlakaKodu(plakaKodu))
            {
                using (SqlConnection baglanti = new SqlConnection(conString))
                {
                    baglanti.Open();

                    if (baglanti.State == System.Data.ConnectionState.Open)
                    {
                        // Plaka kodu ile giriş kaydını arandı
                        string girisKaydiSorgusu = "SELECT GirisTarihi, GirisSaati FROM OtoparkGirisCikis_TBL WHERE plakaNo = @Plaka";
                        SqlCommand girisKaydiKomut = new SqlCommand(girisKaydiSorgusu, baglanti);
                        girisKaydiKomut.Parameters.AddWithValue("@Plaka", plakaKodu);

                        using (SqlDataReader girisKaydiOkuyucu = girisKaydiKomut.ExecuteReader())
                        {
                            if (girisKaydiOkuyucu.Read())
                            {
                                // Giriş kaydı bulundu, çıkışı yapıldı
                                DateTime girişTarihi = girisKaydiOkuyucu.GetDateTime(0);
                                TimeSpan girişSaati = girisKaydiOkuyucu.GetTimeSpan(1);

                                DateTime çıkışTarihi = DateTime.Now;
                                TimeSpan çıkışSaati = çıkışTarihi.TimeOfDay;

                                // Çıkış bilgileri güncellendi
                                string cikisSorgusu = "UPDATE OtoparkGirisCikis_TBL SET CikisTarihi = @CikisTarih, CikisSaati = @CikisSaat WHERE plakaNo = @Plaka";
                                SqlCommand cikisKomut = new SqlCommand(cikisSorgusu, baglanti);
                                cikisKomut.Parameters.AddWithValue("@Plaka", plakaKodu);
                                cikisKomut.Parameters.AddWithValue("@CikisTarih", çıkışTarihi);
                                cikisKomut.Parameters.AddWithValue("@CikisSaat", çıkışSaati);

                                cikisKomut.ExecuteNonQuery();

                                MessageBox.Show("Çıkış başarıyla tamamlandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                MessageBox.Show("Giriş kaydı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Geçerli bir plaka kodu değil.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            string plakaKodu = txtLicensePlate.Text.Trim();
            decimal birimFiyat = string.IsNullOrEmpty(txtPrice.Text) ? 7 : Convert.ToDecimal(txtPrice.Text);
            decimal tutar = 0;

            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                baglanti.Open();

                if (baglanti.State == System.Data.ConnectionState.Open)
                {
                    // Abonelik Kaydı sorgulandı
                    string abonelikSorgu = "SELECT AbonelikTuruID FROM AbonelikKayit_TBL WHERE PlakaNo = @Plaka";
                    SqlCommand abonelikKomut = new SqlCommand(abonelikSorgu, baglanti);
                    abonelikKomut.Parameters.AddWithValue("@Plaka", plakaKodu);
                    int abonelikTuruID = -1; // Varsayılan değer

                    // Eğer plaka kaydı varsa, Abonelik Turu ID'si alınıyor
                    using (SqlDataReader abonelikOkuyucu = abonelikKomut.ExecuteReader())
                    {
                        if (abonelikOkuyucu.Read())
                        {
                            abonelikTuruID = abonelikOkuyucu.GetInt32(0);
                        }
                    }

                    if (abonelikTuruID != -1)
                    {
                        // Abonelik Turu mevcut, Abonelik Turu'nun saatlik birim fiyatı alındı
                        string saatlikFiyatSorgu = "SELECT AboneBirimFiyat FROM AbonelikTurleri_TBL WHERE AbonelikTuruID = @TuruID";
                        SqlCommand saatlikFiyatKomut = new SqlCommand(saatlikFiyatSorgu, baglanti);
                        saatlikFiyatKomut.Parameters.AddWithValue("@TuruID", abonelikTuruID);

                        // Eğer Abonelik Turu'nun saatlik birim fiyatı mevcutsa, onu al
                        using (SqlDataReader fiyatOkuyucu = saatlikFiyatKomut.ExecuteReader())
                        {
                            if (fiyatOkuyucu.Read())
                            {
                                decimal aboneBirimFiyat = fiyatOkuyucu.GetDecimal(0);

                                // OtoparkGirisCikis_TBL tablosundan giriş ve çıkış saatleri alınıyor
                                string saatSorgu = "SELECT GirisSaati, CikisSaati FROM OtoparkGirisCikis_TBL WHERE PlakaNo = @Plaka";
                                SqlCommand saatKomut = new SqlCommand(saatSorgu, baglanti);
                                saatKomut.Parameters.AddWithValue("@Plaka", plakaKodu);

                                using (SqlDataReader saatOkuyucu = saatKomut.ExecuteReader())
                                {
                                    if (saatOkuyucu.Read())
                                    {
                                        TimeSpan girisSaat = saatOkuyucu.GetTimeSpan(0);
                                        TimeSpan cikisSaat = saatOkuyucu.GetTimeSpan(1);

                                        // Farkı al ve saatlik birim fiyatla çarp
                                        TimeSpan kalisSuresi = cikisSaat - girisSaat;
                                        tutar = aboneBirimFiyat * (decimal)kalisSuresi.TotalHours;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // Abonelik Turu yoksa, varsayılan saatlik birim fiyatla hesaplandı
                        // OtoparkGirisCikis_TBL tablosundan giriş ve çıkış saatleri alınıyor
                        string saatSorgu = "SELECT GirisSaati, CikisSaati FROM OtoparkGirisCikis_TBL WHERE PlakaNo = @Plaka";
                        SqlCommand saatKomut = new SqlCommand(saatSorgu, baglanti);
                        saatKomut.Parameters.AddWithValue("@Plaka", plakaKodu);

                        using (SqlDataReader saatOkuyucu = saatKomut.ExecuteReader())
                        {
                            if (saatOkuyucu.Read())
                            {
                                TimeSpan girisSaat = saatOkuyucu.GetTimeSpan(0);
                                TimeSpan cikisSaat = saatOkuyucu.GetTimeSpan(1);

                                TimeSpan kalisSuresi = cikisSaat - girisSaat;
                                tutar = birimFiyat * (decimal)kalisSuresi.TotalHours;
                            }
                        }
                    }

                    // Tutarı "OtoparkGirisCikis_TBL" tablosuna eklemek için SQL UPDATE sorgusu 
                    string updateTutarSorgu = "UPDATE OtoparkGirisCikis_TBL SET tutar = @Tutar WHERE PlakaNo = @Plaka";
                    SqlCommand updateTutarKomut = new SqlCommand(updateTutarSorgu, baglanti);
                    updateTutarKomut.Parameters.AddWithValue("@Tutar", tutar);
                    updateTutarKomut.Parameters.AddWithValue("@Plaka", plakaKodu);
                    updateTutarKomut.ExecuteNonQuery();

                    // Tutarı MessageBox ile gösterildi
                    MessageBox.Show("Toplam Tutar: " + tutar.ToString("0.00") + " TL", "Tutar Hesaplandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            string ad = txtName.Text;
            string soyad = txtSurname.Text;
            string telefonNo = txtPhoneNumber.Text;
            string plakaNo = txtLicensePlate2.Text;
            DateTime baslangicTarihi = dateTimePicker2.Value.Date;
            string abonelikTuruAdi = listBox1.Text;

            int abonelikTuruID = -1;

            // AbonelikTuruID'yi AbonelikTurleri_TBL tablosundan alındı
            using (SqlConnection baglanti = new SqlConnection(conString))
            {
                baglanti.Open();

                if (baglanti.State == System.Data.ConnectionState.Open)
                {
                    string abonelikTuruSorgusu = "SELECT AbonelikTuruID FROM AbonelikTurleri_TBL WHERE AbonelikTuruAdi = @AbonelikTuruAdi";
                    SqlCommand abonelikTuruKomut = new SqlCommand(abonelikTuruSorgusu, baglanti);
                    abonelikTuruKomut.Parameters.AddWithValue("@AbonelikTuruAdi", abonelikTuruAdi);

                    SqlDataReader abonelikTuruOkuyucu = abonelikTuruKomut.ExecuteReader();

                    if (abonelikTuruOkuyucu.Read())
                    {
                        abonelikTuruID = abonelikTuruOkuyucu.GetInt32(0); // AbonelikTuruID alındı
                    }

                    abonelikTuruOkuyucu.Close();
                }
            }

            if (abonelikTuruID != -1)
            {
                using (SqlConnection baglanti = new SqlConnection(conString))
                {
                    baglanti.Open();

                    if (baglanti.State == System.Data.ConnectionState.Open)
                    {
                        string eklemeSorgusu = "INSERT INTO AbonelikKayit_TBL (Ad, Soyad, TelefonNo, PlakaNo, BaslangicTarihi, AbonelikTuruID) " +
                                               "VALUES (@Ad, @Soyad, @TelefonNo, @PlakaNo, @BaslangicTarihi, @AbonelikTuruID)";
                        SqlCommand eklemeKomut = new SqlCommand(eklemeSorgusu, baglanti);
                        eklemeKomut.Parameters.AddWithValue("@Ad", ad);
                        eklemeKomut.Parameters.AddWithValue("@Soyad", soyad);
                        eklemeKomut.Parameters.AddWithValue("@TelefonNo", telefonNo);
                        eklemeKomut.Parameters.AddWithValue("@PlakaNo", plakaNo);
                        eklemeKomut.Parameters.AddWithValue("@BaslangicTarihi", baslangicTarihi);
                        eklemeKomut.Parameters.AddWithValue("@AbonelikTuruID", abonelikTuruID);

                        eklemeKomut.ExecuteNonQuery();

                        MessageBox.Show("Veri başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }


            else
            {
                MessageBox.Show("Abonelik türü bulunamadı. Lütfen geçerli bir abonelik türü seçtiğinizden emin olun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReport1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();

        }

        private void btnReport2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();

        }

    }
}
