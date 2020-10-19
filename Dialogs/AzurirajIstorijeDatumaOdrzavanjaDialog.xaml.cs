using EventMap.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EventMap.Dialogs
{
    /// <summary>
    /// Interaction logic for AzurirajIstorijeDatumaOdrzavanjaDialog.xaml
    /// </summary>
    public partial class AzurirajIstorijeDatumaOdrzavanjaDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private Dogadjaj _dogadjaj;
        private List<DateTime> _listaIstorijeDatumaOdrzavanjaDogadjaja;
        private string _naziv;
        private static DateTime _datumZaMenjanje;
        public static List<DateTime> _nePromenjenaLista;
    
        public AzurirajIstorijeDatumaOdrzavanjaDialog(Dogadjaj dog)
        {
            InitializeComponent();
            this.DataContext = this;

            this._dogadjaj = dog;
            _nePromenjenaLista = new List<DateTime>(dog.IstorijaDatumaOdrzavanja);
            this._listaIstorijeDatumaOdrzavanjaDogadjaja = dog.IstorijaDatumaOdrzavanja;
            this.Naziv = dog.Naziv;
            
        }

        public List<DateTime> ListaIstorijeDatumaOdrzavanjaDogadjaja
        {
            get
            {
                return _listaIstorijeDatumaOdrzavanjaDogadjaja;
            }
            set
            {
                if (value != _listaIstorijeDatumaOdrzavanjaDogadjaja)
                {
                    _listaIstorijeDatumaOdrzavanjaDogadjaja = value;
                    OnPropertyChanged("ListaIstorijeDatumaOdrzavanjaDogadjaja");
                }
            }
        }

        public string Naziv
        {
            get
            {
                return _naziv;
            }
            set
            {
                if (value != _naziv)
                {
                    _naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }

        public static DateTime DatumZaMenjanje
        {
            get
            {
                return _datumZaMenjanje;
            }
            set
            {
                if (value != _datumZaMenjanje)
                {
                    _datumZaMenjanje = value;
                }
            }
        }

        private void povratakNaPregledPinaButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
            this.Visibility = Visibility.Hidden;
        }

        private void dateButton_Click(object sender, RoutedEventArgs e)
        {
            IList item = listaDatumaDataGrid.SelectedItems;
            DatumZaMenjanje = (DateTime)item[0];
            int pozicija = 0;
            foreach (DateTime d in this._listaIstorijeDatumaOdrzavanjaDogadjaja)
            {
                if (d.Equals((DateTime)item[0])) break;
                ++pozicija;
            }
            AzurirajKalendarDialog p = new AzurirajKalendarDialog(DatumZaMenjanje);
            this.Effect = new BlurEffect();
            p.ShowDialog();
            _listaIstorijeDatumaOdrzavanjaDogadjaja[pozicija] = DatumZaMenjanje;
            listaDatumaDataGrid.Items.Refresh();
            this.Effect = null;
            p.Close();
        }

        private void obrisiDatum_Click(object sender, RoutedEventArgs e)
        {
            IList item = listaDatumaDataGrid.SelectedItems;
            if (item[0] == null) return;
            DatumZaMenjanje = (DateTime)item[0];
            int pozicija = 0;
            foreach (DateTime d in this._listaIstorijeDatumaOdrzavanjaDogadjaja)
            {
                if (d.Equals((DateTime)item[0])) break;
                ++pozicija;
            }
            if (_listaIstorijeDatumaOdrzavanjaDogadjaja.Count < pozicija+1) return;
            _listaIstorijeDatumaOdrzavanjaDogadjaja.RemoveAt(pozicija);
            listaDatumaDataGrid.Items.Refresh();
        }

        private void dodajDatum_Click(object sender, RoutedEventArgs e)
        {
            DodajKalendarDialog p = new DodajKalendarDialog(this._dogadjaj);
            this.Effect = new BlurEffect();
            p.ShowDialog();
            listaDatumaDataGrid.Items.Refresh();
            this.Effect = null;
            p.Close();
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.IsActive && this.IsEnabled)
            {
                string str = HelpProvider.GetHelpKey(this);
                HelpProvider.ShowHelp(str, this);
            }

        }
    }
}
