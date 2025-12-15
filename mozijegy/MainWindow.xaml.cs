using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mozijegy
{
        public class mozi
    {
        public string Cím {  get; set; }
        public DateTime Időpont {  get; set; }
        public string Terem {  get; set; }
        public int Szabadhelyek { get; set; }
        public bool _3D {  get; set; }
        public mozi(string cím, DateTime időpont, string terem, int szabadhelyek, bool _3D)
        {
            Cím = cím;
            Időpont = időpont;
            Terem = terem;
            Szabadhelyek = szabadhelyek;
            this._3D = _3D;
        }


    }
    public partial class MainWindow : Window
    {
        public List<mozi> mozifilmek=new List<mozi>();
        public MainWindow()
        {
            InitializeComponent();
            mozifilmek.Add(new mozi("Gyűrűk Ura", new DateTime(2025, 12, 15,10,12,31), "1-es terem", 12, true));
            mozifilmek.Add(new mozi("Nagyfiúk", new DateTime(2025, 12, 15, 10, 12, 31), "2-es terem", 12, true));
            mozifilmek.Add(new mozi("Transformers 3", new DateTime(2025, 1,31,3,9,24), "5-es terem", 10, true));
            mozifilmek.Add(new mozi("Shrek 2", new DateTime(2025, 2, 24,21,31,8), "2-es terem", 6, true));
            mozifilmek.Add(new mozi("Szenfényvesztők 3", new DateTime(2025, 3, 12,14,17,19), "4-es terem", 2, true));
            dataGrid.ItemsSource = mozifilmek;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void adatokbetoltese(object sender, RoutedEventArgs e)
        {

        }

        private void foglalas(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is mozi)
            {
                ((mozi)dataGrid.SelectedItem).Szabadhelyek
                    = ((mozi)dataGrid.SelectedItem).Szabadhelyek - 1;
                dataGrid.Items.Refresh();
            }
        }

        private void vanhely(object sender, RoutedEventArgs e)
        {
            List<mozi> csakaholvanhely = new List<mozi>();
            foreach (var mozi in mozifilmek)
            {
                if (mozi.Szabadhelyek > 0)
                    csakaholvanhely.Add(mozi);
            }
            dataGrid.ItemsSource = csakaholvanhely;
            dataGrid.Items.Refresh();
        }

        private void legnepszerubb(object sender, RoutedEventArgs e)
        {
            var leg = mozifilmek
                .OrderBy(m => m.Szabadhelyek)
                .FirstOrDefault();
            if (leg != null)
            {
                dataGrid.ItemsSource = new List<mozi> { leg };
                dataGrid.Items.Refresh();
            }
        }

        private void a(object sender, RoutedEventArgs e)
        {

        }

        private void atlaghely(object sender, RoutedEventArgs e)
        {
            if (mozifilmek.Count == 0) return;
            double atlag = mozifilmek.Average(m => m.Szabadhelyek);
            MessageBox.Show($"Átlagos szabad hely: {atlag:0.0}");
        }
    }
}