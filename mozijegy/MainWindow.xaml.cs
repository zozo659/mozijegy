using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace mozijegy
{
    public class mozi
    {
        public string Cím { get; set; }
        public DateTime Időpont { get; set; }
        public string Terem { get; set; }
        public int Szabadhelyek { get; set; }
        public bool _3D { get; set; }

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
        public List<mozi> mozifilmek = new List<mozi>();

        public MainWindow()
        {
            InitializeComponent();
            mozifilmek.Add(new mozi("Gyűrűk Ura", new DateTime(2025, 12, 15, 10, 12, 31), "1-es terem", 12, true));
            mozifilmek.Add(new mozi("Nagyfiúk", new DateTime(2025, 12, 15, 10, 12, 31), "2-es terem", 12, true));
            mozifilmek.Add(new mozi("Transformers 3", new DateTime(2025, 1, 31, 3, 9, 24), "5-es terem", 10, true));
            mozifilmek.Add(new mozi("Shrek 2", new DateTime(2025, 2, 24, 21, 31, 8), "2-es terem", 6, true));
            mozifilmek.Add(new mozi("Szenfényvesztők 3", new DateTime(2025, 3, 12, 14, 17, 19), "4-es terem", 2, true));
            dataGrid.ItemsSource = mozifilmek;
        }

        private void adatokbetoltese(object sender, RoutedEventArgs e)
        {
            
            dataGrid.ItemsSource = mozifilmek;
            dataGrid.Items.Refresh();
        }

        private void foglalas(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is mozi film && film.Szabadhelyek > 0)
            {
                film.Szabadhelyek--;
                dataGrid.Items.Refresh();
            }
        }

        private void vanhely(object sender, RoutedEventArgs e)
        {
            List<mozi> csakaholvanhely = new List<mozi>();
            foreach (var m in mozifilmek)
            {
                if (m.Szabadhelyek > 0)
                    csakaholvanhely.Add(m);
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

        private void atlaghely(object sender, RoutedEventArgs e)
        {
            if (mozifilmek.Count == 0) return;
            double atlag = mozifilmek.Average(m => m.Szabadhelyek);
            MessageBox.Show($"Átlagos szabad hely: {atlag:0.0}");
        }

        private void csak3d(object sender, RoutedEventArgs e)
        {
            List<mozi> csak3d = new List<mozi>();
            foreach (var m in mozifilmek)
            {
                if (m._3D)
                    csak3d.Add(m);
            }

            dataGrid.ItemsSource = csak3d;
            dataGrid.Items.Refresh();
        }

        private void Hozzaadas_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilmCim.Text) ||
                string.IsNullOrWhiteSpace(txtIdopont.Text) ||
                string.IsNullOrWhiteSpace(txtTerem.Text) ||
                string.IsNullOrWhiteSpace(txtSzabadHely.Text))
            {
                MessageBox.Show("Minden mezőt ki kell tölteni!");
                return;
            }

            if (!int.TryParse(txtSzabadHely.Text, out int szabad))
            {
                MessageBox.Show("A szabad helyek szám legyen!");
                return;
            }

            if (!DateTime.TryParse(txtIdopont.Text, out DateTime idopont))
            {
                MessageBox.Show("Az időpont nem megfelelő formátumú.");
                return;
            }

            var uj = new mozi(txtFilmCim.Text, idopont, txtTerem.Text, szabad, chk3D.IsChecked == true);
            mozifilmek.Add(uj);


            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = mozifilmek;

            UritMezok();
        }

        
        private void Kereses_Click(object sender, RoutedEventArgs e)
        {
            string cim = txtFilmCim.Text;
            if (string.IsNullOrWhiteSpace(cim))
            {
                MessageBox.Show("Írd be a keresett film címét!");
                return;
            }

            var talalat = mozifilmek
                .FirstOrDefault(m => string.Equals(m.Cím, cim, StringComparison.CurrentCultureIgnoreCase));

            if (talalat == null)
            {
                MessageBox.Show("Nincs ilyen film az előadások között.");
                return;
            }

            
            dataGrid.ItemsSource = new List<mozi> { talalat };
            dataGrid.Items.Refresh();

          
            txtFilmCim.Text = talalat.Cím;
            txtIdopont.Text = talalat.Időpont.ToString("yyyy.MM.dd HH:mm");
            txtTerem.Text = talalat.Terem;
            txtSzabadHely.Text = talalat.Szabadhelyek.ToString();
            chk3D.IsChecked = talalat._3D;
        }

        private void UritMezok()
        {
            txtFilmCim.Clear();
            txtIdopont.Clear();
            txtTerem.Clear();
            txtSzabadHely.Clear();
            chk3D.IsChecked = false;
        }
    }
}
