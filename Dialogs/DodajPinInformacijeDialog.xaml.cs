using EventMap.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for DodajPinInformacijeDialog.xaml
    /// </summary>
    public partial class DodajPinInformacijeDialog : Window
    {
    }
    /// <summary>
    /// Interaction logic for AzurirajPinDialog.xaml
    /// </summary>
    public partial class DodajPinInformacijeDialog : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private MapPin _azurirajuciPin;

        public static bool Potvrdjeno
        {
            get;set;
        }

        public MapPin AzurirajuciPin
        {
            get
            {
                return _azurirajuciPin;
            }
            set
            {
                if (_azurirajuciPin != value)
                {
                    _azurirajuciPin = value;
                    OnPropertyChanged("AzurirajuciPin");
                }
            }
        }

        public DodajPinInformacijeDialog(MapPin p)
        {
            InitializeComponent();
            this.DataContext = this;

            this._azurirajuciPin = p;

        }

        public static DateTime TekDat
        {
            get;
            set;
        }

        private void azurirajPinButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.pinDrzava.Text.Equals(""))
            {
                this.pinDrzava.Background = Brushes.Red;
            }
            else
            {
                this.pinDrzava.Background = Brushes.Transparent;
            }
            if (this.pinGrad.Text.Equals(""))
            {
                this.pinGrad.Background = Brushes.Red;
            }
            else
            {
                this.pinGrad.Background = Brushes.Transparent;
            }
            if (this.pinDrzava.Text.Equals("") || this.pinGrad.Text.Equals(""))
            {
                return;
            }
            // Popuniti za Listu jos
            BindingExpression bindDrzava = this.pinDrzava.GetBindingExpression(TextBox.TextProperty);
            BindingExpression bindGrad = this.pinGrad.GetBindingExpression(TextBox.TextProperty);
            // BindingExpression bindListaDogadjaja = this.listaDogadjajaDataGrid.GetBindingExpression(DataGrid.);
            bindDrzava.UpdateSource();
            bindDrzava.UpdateTarget();
            bindGrad.UpdateSource();
            bindGrad.UpdateTarget();
            MainWindow.Pin.Drzava = this._azurirajuciPin.Drzava;
            MainWindow.Pin.Grad = this._azurirajuciPin.Grad;
            MainWindow.Pin.BrojDogadjaja = this._azurirajuciPin.BrojDogadjaja;
            MainWindow.Pin.ListaDogadjaja = this._azurirajuciPin.ListaDogadjaja;

            foreach (Dogadjaj d in this.AzurirajuciPin.ListaDogadjaja)
            {
                d.Drzava = this._azurirajuciPin.Drzava;
                d.Grad = this._azurirajuciPin.Grad;
                d.Grad.XX = this._azurirajuciPin.X;
                d.Grad.YY = this._azurirajuciPin.Y;
                d.Grad.DrzavaGradova = this._azurirajuciPin.Drzava;
            }

            foreach (Dogadjaj d in MainWindow.Pin.ListaDogadjaja)
            {
                d.Drzava = this._azurirajuciPin.Drzava;
                d.Grad = this._azurirajuciPin.Grad;
                if (!Dogadjaj.sveDrzave.Contains(this._azurirajuciPin.Drzava))
                {
                    Dogadjaj.sveDrzave.Add(this._azurirajuciPin.Drzava);
                    Dogadjaj.gradoviUDrzavi[this._azurirajuciPin.Drzava.DrzavaText] = new List<Grad>();
                }
                bool unutri = false;
                foreach (Grad gr in Dogadjaj.gradoviUDrzavi[this._azurirajuciPin.Drzava.DrzavaText])
                {
                    if (this._azurirajuciPin.Grad.GradText.Equals(gr.GradText))
                    {
                        unutri = true;
                        break;
                    }
                }

                if (!unutri)
                    Dogadjaj.gradoviUDrzavi[this._azurirajuciPin.Drzava.DrzavaText].Add(this._azurirajuciPin.Grad);
            }

            Potvrdjeno = true;
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void otkaziPrimenuFiltera_Click(object sender, RoutedEventArgs e)
        {
            //this.pinDrzava.Text = _pin.Drzava.DrzavaText;
            //this.pinGrad.Text = _pin.Grad.GradText;
            // Popuniti za Listu jos
            this._azurirajuciPin = new MapPin(MainWindow.Pin);
            Potvrdjeno = false;
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void obrisiOdabraniDogadjajIzListe_Click(object sender, RoutedEventArgs e)
        {
            //this._azurirajuciPin.izbrisiDogadjaj((Dogadjaj)this.listaDogadjajaDataGrid.SelectedItem);
            IList item = listaDogadjajaDataGrid.SelectedItems;
            Dogadjaj dog = (Dogadjaj)item[0];
            this._azurirajuciPin.izbrisiDogadjaj(dog);
            listaDogadjajaDataGrid.Items.Refresh();
        }

        private void pregledIstorijeOdrzavanjaButton_Click(object sender, RoutedEventArgs e)
        {
            if (_azurirajuciPin.ListaDogadjaja == null || _azurirajuciPin.ListaDogadjaja.Count == 0)
            {
                OdabirPinaUpozorenjeDialog d = new OdabirPinaUpozorenjeDialog();
                this.Effect = new BlurEffect();
                d.ShowDialog();
                this.Effect = null;
            }
            else
            {

                IList item = listaDogadjajaDataGrid.SelectedItems;
                Dogadjaj dog = (Dogadjaj)item[0];
                AzurirajIstorijeDatumaOdrzavanjaDialog p = new AzurirajIstorijeDatumaOdrzavanjaDialog(dog);
                this.Effect = new BlurEffect();
                p.ShowDialog();
                this.Effect = null;
                p.Close();
            }
        }

        private void azuriranjeListeEtiketaButton_Click(object sender, RoutedEventArgs e)
        {
            if (_azurirajuciPin.ListaDogadjaja == null || _azurirajuciPin.ListaDogadjaja.Count == 0)
            {
                OdabirPinaUpozorenjeDialog d = new OdabirPinaUpozorenjeDialog();
                this.Effect = new BlurEffect();
                d.ShowDialog();
                this.Effect = null;
            }
            else
            {

                IList item = listaDogadjajaDataGrid.SelectedItems;
                Dogadjaj dog = (Dogadjaj)item[0];
                AzurirajEtiketeDialog p = new AzurirajEtiketeDialog(dog);
                this.Effect = new BlurEffect();
                p.ShowDialog();
                this.Effect = null;
                p.Close();
            }
        }

        private void dateButton_Click(object sender, RoutedEventArgs e)
        {
            IList item = listaDogadjajaDataGrid.SelectedItems;
            Dogadjaj dog = (Dogadjaj)item[0];
            TekDat = dog.DatumOdrzavanja;
            int pozicija = 0;
            foreach (Dogadjaj d in this._azurirajuciPin.ListaDogadjaja)
            {
                if (d.Equals(dog)) break;
                ++pozicija;
            }
            AzurirajDatumOdrzavanjaZaTekucuGodinuDialog p = new AzurirajDatumOdrzavanjaZaTekucuGodinuDialog(dog);
            this.Effect = new BlurEffect();
            p.ShowDialog();
            this._azurirajuciPin.ListaDogadjaja[pozicija].DatumOdrzavanja = TekDat;
            listaDogadjajaDataGrid.Items.Refresh();
            this.Effect = null;
            p.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded) return;
            if (!(sender is DataGrid)) return; 
            ComboBox cb = (ComboBox)e.Source;
            IList item = listaDogadjajaDataGrid.SelectedItems;
            Dogadjaj dog = (Dogadjaj)item[0];
            dog.Tip = (TIP)cb.SelectedIndex;

        }

        private void dodajNoviDogadjajUListu_Click(object sender, RoutedEventArgs e)
        {
            DodajNoviDogadjajDialog p = new DodajNoviDogadjajDialog(this._azurirajuciPin);
            this.Effect = new BlurEffect();
            p.ShowDialog();
            listaDogadjajaDataGrid.Items.Refresh();
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
