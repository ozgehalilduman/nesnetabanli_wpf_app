using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_satranc_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button hareketden_tas=new Button();
        Char sira_kimde = 'b';//b ->beyaz , s->siyah

        public MainWindow()
        {
            InitializeComponent();
        }

        private void eylem_click(object sender, RoutedEventArgs e)
        {
            byte islem = 0;
            Button tas = (Button)sender;
            if(hareketden_tas.Tag==null && tas.Tag==null)  
            {
                //MessageBox.Show("HAREKET GERÇEKLEŞMEMELİ");
                islem = 0;
            }
            if (hareketden_tas.Tag == null && tas.Tag != null) 
            { //ilk tasın seçildiği kısım
                if (sira_kimde == tasin_rengi(tas.Tag.ToString()))
                {
                    islem = 1;
                    ////hangi tası hareket ettiriyoruz
                    //MessageBox.Show(hangi_tas(tas.Tag.ToString()));
                    hareket_ihtimalleri(tas);
                }
                else
                {
                    MessageBox.Show("OYUN SIRASI DİGER GRUPTA");
                }
                
            }
            if (hareketden_tas.Tag != null && tas.Tag == null)
            { //hareket için seçilen tus bos bir alana hareket ediyor(AMA hareket edilecek karenin içeriği + olmalı)
                if (hareket_onaylama(tas))
                {
                    islem = 2; 
                }                
                        
            }
            if (hareketden_tas.Tag != null && tas.Tag != null)
            { //hareket eden tasın baska bir tası yeme durumu
                //if (hareket_onaylama(tas))
                //{
                    islem = 3;
                //}
            }
            switch(islem){
                case 1: 
                    btn_hareketeden.Background = hareketden_tas.Background;
                    btn_secilen.Background = tas.Background;
                    //MessageBox.Show("HAREKET EDECEK TAŞIN SEÇİMİ GERÇEKLEŞTİ");
                    hareketden_tas = tas;
                    break;
                case 2: 
                    btn_hareketeden.Background = hareketden_tas.Background;
                    btn_secilen.Background = tas.Background;
                   // MessageBox.Show("HAREKET GERÇEKLEŞİYOR"); 
                    hareketEt(tas,0);
                   // MessageBox.Show("background yer değiştirdi");
                    break;
                case 3: 
                    //MessageBox.Show("HAREKET EDEN TAŞ BAŞKA BİR TAŞIN ÜZERİNE HAREKET ETTİ");
                    //tas = hareketden_tas;
                    hareketEt(tas,1);
                    break;            
            }            
            
        }

        private bool hareket_onaylama(Button tas)
        {
            if (tas.Content.ToString() == "+") { return true; } else {MessageBox.Show("Bu Kareye Hareket Edemezsiniz");return false; }
        }

        private void hareketEt(Button tas,byte eylem)
        {            
            Brush brush_takas;//üstüne gelinen tasın brush ı
            Object tag_takas;//üstüne gelinen tasın tag ı
            brush_takas = tas.Background;            
            tag_takas = tas.Tag;         

            if(eylem==0)
            {//bos bir alana hareket
                tas.Background = hareketden_tas.Background;
                tas.Tag = hareketden_tas.Tag;
                hareketden_tas.Background = brush_takas;
                hareketden_tas.Tag = tag_takas;
                //siranın diger tas grubuna geçmesi
                if (sira_kimde == 'b') { sira_kimde = 's'; } else { sira_kimde = 'b'; }
            }
            if (eylem == 1)
            {//dolu bir tasın üzerine geldi.
                //AYNİ TASIN BİRBİRİNİ YEMESİNİ ENGELLEME
                if (tasin_rengi(tas.Tag.ToString()) != tasin_rengi(hareketden_tas.Tag.ToString()))
                {
                    if (hareket_onaylama(tas))
                    { 
                        btn_yenilen.Tag = tas.Tag;
                        btn_yenilen.Background = tas.Background;
                        tas.Background = hareketden_tas.Background;
                        tas.Tag = hareketden_tas.Tag;
                        hareketden_tas.Background = null;
                        hareketden_tas.Tag = null;
                        //siranın diger tas grubuna geçmesi
                        if (sira_kimde == 'b') { sira_kimde = 's'; } else { sira_kimde = 'b'; }
                    }
                }
                else
                { MessageBox.Show("Hareket Edecek Taş Değiştirildi"); hareketden_tas=tas;hareket_ihtimalleri(tas); }
            }
            //siranın diger tas grubuna geçmesi
            //if (sira_kimde == 'b') { sira_kimde = 's'; }else{ sira_kimde = 'b'; }
            //hangi tası hareket ettiriyoruz
           // MessageBox.Show(hangi_tas(tas.Tag.ToString()));
        }
        private char tasin_rengi(string tasin_adi)
        {
            char sonuc='b';
            int tire = tasin_adi.IndexOf('_')+1;
            string renk=tasin_adi.Substring(tire);

            if(renk=="beyaz"){sonuc = 'b';}
            if(renk == "siyah") { sonuc = 's'; }

            return sonuc;
        }
        private string hangi_tas(string tasin_adi)
        {
            string sonuc = "";
            int tire = tasin_adi.IndexOf('_');
            sonuc = tasin_adi.Substring(0,tire);
            return sonuc;
        }
        private string tasin_konumu(string tasin_adi)
        {
            string sonuc = "";
            int tire = tasin_adi.IndexOf('_') + 1;
            sonuc = tasin_adi.Substring(tire);
            return sonuc;
        }
        private void hareket_ihtimalleri(Button tas)
        { //bu kısımda nerelere gidebileceğini işaretleyeceğim borderla
            string tas_konum = tasin_konumu(tas.Name);
            char tas_renk = tasin_rengi(tas.Tag.ToString());
            string tas_tur = hangi_tas(tas.Tag.ToString());
            string hareket_bilgi = "TASIN konumu=" + " (sutun)" + tas_konum[0] + ",(satır)" + tas_konum[1] + " ,rengi=" + tas_renk + ", turu=" + tas_tur;
            lst_hareket.Items.Add(hareket_bilgi);
            rotaboya(tas_tur, tas_renk, tas_konum[0], tas_konum[1]);
        }

        private void rotaboya(string tas_tur, char tas_renk, char sutun, char satir)
        {
            List<string> isaretlenecekler=new List<string>();
            string isim;
            byte yenisatir;
            byte yenisutun;
            foreach (Button btn in Grid_taslar.Children.OfType<Button>())
            {
                btn.Content = "";
                btn.Foreground = Brushes.Black;
            }
            if (tas_tur == "piyon")
            {
                if (tas_renk == 'b')
                {
                    yenisatir = Convert.ToByte(satir.ToString());
                    yenisatir--;
                    isim = "btn_" + sutun.ToString() + yenisatir.ToString();
                    if (rota_uygunluk_kontrol(isim,tas_tur))
                    {
                        isaretlenecekler.Add(isim);
                        if (satir == '7')
                        {
                            yenisatir--;
                            isim = "btn_" + sutun.ToString() + yenisatir.ToString();
                            isaretlenecekler.Add(isim);
                        }
                    }
                    foreach (Button btn in Grid_taslar.Children.OfType<Button>().Where(x => isaretlenecekler.Contains(x.Name)))
                    {
                        //btn.Foreground = Brushes.White;
                        btn.Content = "+";
                    }
                } //    if (tas_renk == 'b')
                if (tas_renk == 's')
                {
                    yenisatir = Convert.ToByte(satir.ToString());
                    yenisatir++;
                    isim = "btn_" + sutun.ToString() + yenisatir.ToString();
                    if (rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        isaretlenecekler.Add(isim);
                        if (satir == '2')
                        {
                            yenisatir++;
                            isim = "btn_" + sutun.ToString() + yenisatir.ToString();
                            isaretlenecekler.Add(isim);
                        }
                    }
                    foreach (Button btn in Grid_taslar.Children.OfType<Button>().Where(x => isaretlenecekler.Contains(x.Name)))
                    {
                        //btn.Foreground = Brushes.White;
                        btn.Content = "+";
                    }                    
                } //    if (tas_renk == 's')
                  //ozel capraz yeme durumunun kontrolu
                  //ilk olarak isaretlenecekler listesini temizlemliyim
                isaretlenecekler.Clear();
                if (tas_renk == 'b')
                {
                    yenisatir = Convert.ToByte(satir.ToString());                    
                    yenisatir--;
                    //1.hareket noktası
                    yenisutun = Convert.ToByte(sutun.ToString());
                    yenisutun--;
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    if (!rota_uygunluk_kontrol(isim, tas_tur)) { isaretlenecekler.Add(isim); }
                       
                    //2.harekt noktası
                    yenisutun = Convert.ToByte(sutun.ToString());
                    yenisutun++;
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    if (!rota_uygunluk_kontrol(isim, tas_tur)) { isaretlenecekler.Add(isim); }
                }
                if (tas_renk == 's')
                {
                    yenisatir = Convert.ToByte(satir.ToString());
                    yenisatir++;
                    //1.hareket noktası
                    yenisutun = Convert.ToByte(sutun.ToString());
                    yenisutun--;
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    if (!rota_uygunluk_kontrol(isim, tas_tur)) { isaretlenecekler.Add(isim); }
                    //2.harekt noktası
                    yenisutun = Convert.ToByte(sutun.ToString());
                    yenisutun++;
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    if (!rota_uygunluk_kontrol(isim, tas_tur)) { isaretlenecekler.Add(isim); }
                }
                foreach (Button btn in Grid_taslar.Children.OfType<Button>().Where(x => isaretlenecekler.Contains(x.Name)))
                {
                    //btn.Foreground = Brushes.White;
                    btn.Content = "+";
                }
            }//if (tas_tur == "piyon")
            if (tas_tur == "at")
            {
                //1.hareket:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                yenisatir--;yenisutun -= 2;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                //2.hareket:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                yenisatir-=2; yenisutun--;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                //3.hareket:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                yenisatir-= 2; yenisutun++;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                //4.hareket:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                yenisatir--; yenisutun+=2;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                //5.hareket:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                yenisatir++; yenisutun+=2;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                //6.hareket:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                yenisatir += 2; yenisutun++;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                //7.hareket:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                yenisatir += 2; yenisutun--;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                //8.hareket:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                yenisatir++; yenisutun-=2;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                foreach (Button btn in Grid_taslar.Children.OfType<Button>().Where(x => isaretlenecekler.Contains(x.Name)))
                {
                    //btn.Foreground = Brushes.White;
                    btn.Content = "+";
                }
            }// if (tas_tur == "at")
            if (tas_tur == "kale")
            {
                //1.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisutun--; yenisutun >= 1; yenisutun--)
                {
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }
                //2.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisutun++; yenisutun <= 8; yenisutun++)
                {
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }
                //3.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisatir--; yenisatir >= 1; yenisatir--)
                {
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }
                //4.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisatir++; yenisatir <= 8; yenisatir++)
                {
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }

                foreach (Button btn in Grid_taslar.Children.OfType<Button>().Where(x => isaretlenecekler.Contains(x.Name)))
                {
                    //btn.Foreground = Brushes.White;
                    btn.Content = "+";
                }
            }// if (tas_tur == "kale")
            if (tas_tur == "fil")
            {
                //1.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisutun--; yenisutun >= 1; yenisutun--)
                {
                    yenisatir--;
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }
                //2.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisutun++; yenisutun <= 8; yenisutun++)
                {
                    yenisatir++;
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }
                //3.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisatir--; yenisatir >= 1; yenisatir--)
                {
                    yenisutun++;
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }
                //4.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisatir++; yenisatir <= 8; yenisatir++)
                {
                    yenisutun--;
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }

                foreach (Button btn in Grid_taslar.Children.OfType<Button>().Where(x => isaretlenecekler.Contains(x.Name)))
                {
                    //btn.Foreground = Brushes.White;
                    btn.Content = "+";
                }
            }// if (tas_tur == "fil")
            if (tas_tur == "vezir")
            {
                //1.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisutun--; yenisutun >= 1; yenisutun--)
                {
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }
                //2.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisutun++; yenisutun <= 8; yenisutun++)
                {
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }
                //3.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisatir--; yenisatir >= 1; yenisatir--)
                {
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }
                //4.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisatir++; yenisatir <= 8; yenisatir++)
                {
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }
                //5.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisutun--; yenisutun >= 1; yenisutun--)
                {
                    yenisatir--;
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }
                //6.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisutun++; yenisutun <= 8; yenisutun++)
                {
                    yenisatir++;
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }
                //7.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisatir--; yenisatir >= 1; yenisatir--)
                {
                    yenisutun++;
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }
                //8.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                for (yenisatir++; yenisatir <= 8; yenisatir++)
                {
                    yenisutun--;
                    isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                    isaretlenecekler.Add(isim);
                    //eger onunde tas varsa arkasındakileri işaretlemiyor
                    if (!rota_uygunluk_kontrol(isim, tas_tur))
                    {
                        break;
                    }
                }

                foreach (Button btn in Grid_taslar.Children.OfType<Button>().Where(x => isaretlenecekler.Contains(x.Name)))
                {
                    //btn.Foreground = Brushes.White;
                    btn.Content = "+";
                }
            }// if (tas_tur == "vezir")
            /**/
            if (tas_tur == "şah")
            {
                //1.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                yenisutun--;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                //2.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());                
                yenisutun++;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                //3.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());                
                yenisatir--;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                //4.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                yenisatir++;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                //5.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());               
                yenisatir--;
                yenisutun--;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                //6.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());                
                yenisatir++;
                yenisutun++;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                //7.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());                
                yenisutun++;
                yenisatir--;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);
                //8.hareket yonu:
                yenisatir = Convert.ToByte(satir.ToString());
                yenisutun = Convert.ToByte(sutun.ToString());
                yenisatir++;
                yenisutun--;
                isim = "btn_" + yenisutun.ToString() + yenisatir.ToString();
                isaretlenecekler.Add(isim);      

                foreach (Button btn in Grid_taslar.Children.OfType<Button>().Where(x => isaretlenecekler.Contains(x.Name)))
                {
                    //btn.Foreground = Brushes.White;
                    btn.Content = "+";
                }
            }// if (tas_tur == "şah")
            /**/
        }

        private bool rota_uygunluk_kontrol(string btn_isim,string tas_tur)
        {
           Button _btn =(Button)Grid_taslar.Children.OfType<Button>().Where(x => x.Name == btn_isim).FirstOrDefault();
            //+ konulacak yerde bir tas var mı kontrol ediyorum null değilse tas var
            //_btn nin direkt null döndüğü durumlardan dolayı try catch kullanmak zorunda kaldım...
            try
            {
                if (_btn.Tag != null) { return false; } else { return true; }
            }
            catch
            {
                if (_btn!=null) { return false; } else { return true; }

            }
        }
    }
}
