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
                MessageBox.Show("HAREKET GERÇEKLEŞMEMELİ");
            }
            if (hareketden_tas.Tag == null && tas.Tag != null) 
            { //ilk tasın seçildiği kısım
                islem = 1;
            }
            if (hareketden_tas.Tag != null && tas.Tag == null)
            { //hareket için seçilen tus bos bir alana hareket ediyor
                islem = 2;         
            }
            if (hareketden_tas.Tag != null && tas.Tag != null)
            { //ilk tasın seçildiği kısım
                islem = 3;
            }
            switch(islem){
                case 1: 
                    btn_hareketeden.Background = hareketden_tas.Background;
                    btn_secilen.Background = tas.Background;
                    MessageBox.Show("HAREKET EDECEK TAŞIN SEÇİMİ GERÇEKLEŞTİ");
                    
                    hareketden_tas = tas;                    
                    break;
                case 2: 
                    btn_hareketeden.Background = hareketden_tas.Background;
                    btn_secilen.Background = tas.Background;
                    MessageBox.Show("HAREKET GERÇEKLEŞİYOR");                    
                    tas = hareketden_tas;
                    btn_hareketeden.Background = hareketden_tas.Background;
                    btn_secilen.Background = tas.Background;
                    hareketEt(tas,0);
                    MessageBox.Show("background yer değiştirdi");
                    btn_hareketeden.Background = hareketden_tas.Background;
                    btn_secilen.Background = tas.Background;
                    break;
                case 3: 
                    MessageBox.Show("HAREKET EDEN TAŞ BAŞKA BİR TAŞIN ÜZERİNE HAREKET ETTİ");
                    tas = hareketden_tas;
                    hareketEt(tas,1);
                    break;            
            }            
            
        }
        private void hareketEt(Button tas,byte eylem)
        {
            Brush brush_over;//üstüne gelinen tasın brush ı
            Object tag_over;//üstüne gelinen tasın tag ı

            brush_over = tas.Background;            
            tas.Background = hareketden_tas.Background;         

            tag_over = tas.Tag;
            tas.Tag = hareketden_tas.Tag;
            if(eylem==0)
            {//bos bir alana hareket
                hareketden_tas.Background = brush_over;
                hareketden_tas.Tag = tag_over;
            }
            if (eylem == 1)
            {//dolu bir tasın üzerine geldi.
                
            }
            
        }
    }
}
