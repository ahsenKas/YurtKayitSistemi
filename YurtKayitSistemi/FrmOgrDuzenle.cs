using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace YurtKayitSistemi
{
    public partial class FrmOgrDuzenle : Form
    {
        public FrmOgrDuzenle()
        {
            InitializeComponent();
        }

        public string id, ad, soyad, TC, telefon, dogum, bolum;
        public string mail, odaNo, veliAdSoyad, veliTel, adres;

        private void button1_Click(object sender, EventArgs e)
        {
            // Öğrenci silme
            SqlCommand komutsil = new SqlCommand("delete from Ogrenci where ogrid=@k1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@k1", TxtOgrid.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt silndi");

            ////Oda aktif kontenjanı azaltma

            SqlCommand komutoda = new SqlCommand("update odalar set OdaAktif=OdaAktif-1 where OdaNo=@oda", bgl.baglanti());
            komutoda.Parameters.AddWithValue("@oda", CmbOgrOdaNo.Text);
            komutoda.ExecuteNonQuery();
            bgl.baglanti().Close();
        }

        SqlBaglantim bgl = new SqlBaglantim();

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("update Ogrenci set OgrAd=@p2,OgrSoyad=@p3, OgrTC=@p4, OgrTelefon=@p5, OgrDogum=@p6, OgrBolum=@p7, OgrMail=@p8,OgrOdaNo=@p9, OgrVeliAdSoyad=@p10,OgrVeliTelefon=@p11,OgrVeliAdres=@p12 where Ogrid=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtOgrid.Text);
                komut.Parameters.AddWithValue("@p2", TxtOgrAd.Text);
                komut.Parameters.AddWithValue("@p3", TxtOgrSoyad.Text);
                komut.Parameters.AddWithValue("@p4", MskOgrTC.Text);
                komut.Parameters.AddWithValue("@p5", MskOgrTelefon.Text);
                komut.Parameters.AddWithValue("@p6", MskOgrDogum.Text);
                komut.Parameters.AddWithValue("@p7", CmbOgrBolum.Text);
                komut.Parameters.AddWithValue("@p8", TxtOgrMail.Text);
                komut.Parameters.AddWithValue("@p9", CmbOgrOdaNo.Text);
                komut.Parameters.AddWithValue("@p10", TxtVeliAdSoyad.Text);
                komut.Parameters.AddWithValue("@p11", MskVeliTelefon.Text);
                komut.Parameters.AddWithValue("@p12", RchAdres.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kaydınız başarı ile güncellenmiştir. ");
            }
            catch (Exception)
            {
                MessageBox.Show("HATA! \nİşleminiz gerçekleşmedi! \nTekrar Deneyiniz.");
            }

        }       

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void FrmOgrDuzenle_Load(object sender, EventArgs e)
        {
            TxtOgrid.Text = id;
            TxtOgrAd.Text = ad;
            TxtOgrSoyad.Text = soyad;
            MskOgrTC.Text = TC;
            MskOgrTelefon.Text = telefon;
            MskOgrDogum.Text = dogum;
            CmbOgrBolum.Text = bolum;
            TxtOgrMail.Text = mail;
            CmbOgrOdaNo.Text = odaNo;
            TxtVeliAdSoyad.Text = veliAdSoyad;
            MskVeliTelefon.Text = veliTel;
            RchAdres.Text = adres;

        }

        private void TxtOgrAd_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
