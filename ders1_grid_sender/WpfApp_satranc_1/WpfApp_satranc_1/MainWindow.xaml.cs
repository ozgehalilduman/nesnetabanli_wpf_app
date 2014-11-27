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
        Button hareketden_tas;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void eylem_click(object sender, RoutedEventArgs e)
        {
            Brush tas_over;//üstüne gelinen tas
            object tas_over_degeri;//ustune gelinen tas konumundaki tasın degeri
            Button tas = (Button)sender;

            tas_over_degeri = tas.Tag;
            tas.Tag = hareketden_tas.Tag;
            hareketden_tas.Tag = tas_over_degeri;

            tas_over = tas.Background;
            tas.Background = hareketden_tas.Background;            
            hareketden_tas.Background = tas_over;
            
        }
    }
}
