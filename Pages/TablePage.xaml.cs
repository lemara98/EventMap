using EventMap.Classes;
using EventMap.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventMap.Pages
{
    /// <summary>
    /// Interaction logic for TablePage.xaml
    /// </summary>
    public partial class TablePage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private List<MapPin> _sviPinovi;
        private List<Dogadjaj> _sviDogadjaji;
        //private List<Dogadjaj> _sviVidljiviDogadjaji;

        public TablePage(List<MapPin> lista)
        {
            InitializeComponent();
            this.DataContext = this;

            this._sviPinovi = lista;
            this._sviDogadjaji = new List<Dogadjaj>();

            foreach (MapPin mp in _sviPinovi)
            {
                
                this._sviDogadjaji.AddRange(mp.ListaVidljivihDogadjaja);
            }

            //this._sviVidljiviDogadjaji = new List<Dogadjaj>();
            //foreach (Dogadjaj d in _sviDogadjaji)
            //{
            //    if (d.Vidljiv) this._sviVidljiviDogadjaji.Add(d);
            //}
            

        }

        public List<MapPin> SviPinovi
        {
            get { return _sviPinovi; }
            set
            {
                if (value != _sviPinovi)
                {
                    _sviPinovi = value;
                    OnPropertyChanged("SviPinovi");
                }
            }
        }

        public List<Dogadjaj> SviDogadjaji
        {
            get { return _sviDogadjaji; }
            set
            {
                if (value != _sviDogadjaji)
                {
                    _sviDogadjaji = value;
                    OnPropertyChanged("SviDogadjaji");
                }
            }
        }

        public List<Dogadjaj> SviVidljiviDogadjaji
        {
            get 
            {
                List<Dogadjaj> lista = new List<Dogadjaj>();
                foreach(Dogadjaj d in _sviDogadjaji)
                {
                    if (d.Vidljiv) lista.Add(d);
                }
                return lista;
            }

        }

        private void pregledIstorijeOdrzavanjaButton_Click(object sender, RoutedEventArgs e)
        {

                IList item = listaSvihDogadjajaDataGrid.SelectedItems;
                Dogadjaj dog = (Dogadjaj)item[0];
                PregledIstorijeDatumaOdrzavanjaDialog p = new PregledIstorijeDatumaOdrzavanjaDialog(dog);
                this.Effect = new BlurEffect();
                p.ShowDialog();
                this.Effect = null;
                p.Close();
        }

        private void pregledListeEtiketaButton_Click(object sender, RoutedEventArgs e)
        {
                IList item = listaSvihDogadjajaDataGrid.SelectedItems;
                Dogadjaj dog = (Dogadjaj)item[0];
                PregledEtiketaDialog p = new PregledEtiketaDialog(dog);
                this.Effect = new BlurEffect();
                p.ShowDialog();
                this.Effect = null;
                p.Close();
        }


        private void ListViewScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
