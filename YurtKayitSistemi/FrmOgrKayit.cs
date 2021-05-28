﻿using System;
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
    public partial class FrmOgrKayit : Form
    {
        public FrmOgrKayit()
        {
            InitializeComponent();
        }

        SqlBaglantim bgl = new SqlBaglantim();




        private void FrmOgrKayit_Load(object sender, EventArgs e)
        {

            //Bolumleri Listeleme Komutları            
            SqlCommand komut = new SqlCommand("Select BolumAd From Bolumler", bgl.baglanti());
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                CmbOgrBolum.Items.Add(oku[0].ToString());
            }
            bgl.baglanti().Close();

            //Boş Odalaro Listeleme Komutları
            
            SqlCommand komut2 = new SqlCommand("Select OdaNo From Odalar where OdaKapasite != OdaAktif", bgl.baglanti());
            SqlDataReader oku2 = komut2.ExecuteReader();
            while (oku2.Read())
            {
                CmbOgrOdaNo.Items.Add(oku2[0].ToString());
            }
            bgl.baglanti().Close();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //Öğrenci Bilgilerini Kayıt Eden Komutlar
            try
            {
                
                SqlCommand komutkaydet = new SqlCommand("insert into Ogrenci(OgrAd, Ogrsoyad,OgrTC,OgrTelefon,OgrDogum,OgrBolum, OgrMail,OgrOdaNo, OgrVeliAdSoyad, OgrVeliTelefon, OgrVeliAdres)values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
                komutkaydet.Parameters.AddWithValue("@p1", TxtOgrAd.Text);
                komutkaydet.Parameters.AddWithValue("@p2", TxtOgrSoyad.Text);
                komutkaydet.Parameters.AddWithValue("@p3", MskOgrTC.Text);
                komutkaydet.Parameters.AddWithValue("@p4", MskOgrTelefon.Text);
                komutkaydet.Parameters.AddWithValue("@p5", MskOgrDogum.Text);
                komutkaydet.Parameters.AddWithValue("@p6", CmbOgrBolum.Text);
                komutkaydet.Parameters.AddWithValue("@p7", TxtOgrMail.Text);
                komutkaydet.Parameters.AddWithValue("@p8", CmbOgrOdaNo.Text);
                komutkaydet.Parameters.AddWithValue("@p9", TxtVeliAdSoyad.Text);
                komutkaydet.Parameters.AddWithValue("@p10", MskVeliTelefon.Text);
                komutkaydet.Parameters.AddWithValue("@p11", RchAdres.Text);
                komutkaydet.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt Başarılı Bir Şekilde Gerçekleşti");

                //Öğrenci id yi labela çekme
                SqlCommand komut = new SqlCommand("select Ogrid from Ogrenci", bgl.baglanti());
                SqlDataReader oku = komut.ExecuteReader();
                while(oku.Read())
                {
                    label12.Text = oku[0].ToString();
                }
                bgl.baglanti().Close();


                //Öğrenci borç alanı oluşturma
                SqlCommand komutkaydet2 = new SqlCommand("insert into Borclar(Ogrid, OgrAd, OgrSoyad) values (@b1, @b2,@b3)", bgl.baglanti());
                komutkaydet2.Parameters.AddWithValue("@b1", label12.Text);
                komutkaydet2.Parameters.AddWithValue("@b2",TxtOgrAd.Text);
                komutkaydet2.Parameters.AddWithValue("@b3",TxtOgrSoyad.Text);
                komutkaydet2.ExecuteNonQuery();
                bgl.baglanti().Close();
            }

            catch (Exception)
            {
                MessageBox.Show("Hata oluştu!! \n Lütfen Tekrar deneyin");
            }

            //Öğrenci oda kontenjanı arttırma

            SqlCommand komutoda = new SqlCommand("update Odalar set OdaAktif=OdaAktif+1  where OdaNo = @oda1", bgl.baglanti());
            komutoda.Parameters.AddWithValue("@oda1", CmbOgrOdaNo.Text);
            komutoda.ExecuteNonQuery();
            bgl.baglanti().Close();
            
        }
    }
}
//Data Source=AHSEN;Initial Catalog=YurtKayit;Integrated Security=True