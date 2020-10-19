using EventMap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for DodajNoviDogadjajDialog.xaml
    /// </summary>
    public partial class DodajNoviDogadjajDialog : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private MapPin _pin;
        private Dogadjaj _noviDogadjaj;

        public DodajNoviDogadjajDialog(MapPin pin)
        {
            InitializeComponent();
            this.DataContext = this;

            this._pin = pin;
            this._noviDogadjaj = new Dogadjaj();
            this._noviDogadjaj.DatumOdrzavanja = new DateTime(2020, 1, 1);
            this._noviDogadjaj.IstorijaDatumaOdrzavanja = new List<DateTime>();
        }

        public MapPin Pin
        {
            get
            {
                return _pin;
            }
            set
            {
                if (_pin != value)
                {
                    _pin = value;
                    OnPropertyChanged("Pin");
                }
            }
        }

        public Dogadjaj NoviDogadjaj
        {
            get
            {
                return _noviDogadjaj;
            }
            set
            {
                if (_noviDogadjaj != value)
                {
                    _noviDogadjaj = value;
                    OnPropertyChanged("NoviDogadjaj");
                }
            }
        }

        private void napraviNoviDogadjaj_Click(object sender, RoutedEventArgs e)
        {
            bool proveraIspravno = true;

            if (this.jedOznTextBox.Text.Equals(""))
            {
                this.jedOznTextBox.Background = Brushes.Red;
                proveraIspravno = false;
            } 
            else this.jedOznTextBox.Background = Brushes.Transparent;
            if (this.nazivTextBox.Text.Equals(""))
            {
                this.nazivTextBox.Background = Brushes.Red;
                proveraIspravno = false;
            }
            else this.nazivTextBox.Background = Brushes.Transparent;
            if (this.opisTextBox.Text.Equals(""))
            {
                this.opisTextBox.Background = Brushes.Red;
                proveraIspravno = false;
            }
            else this.opisTextBox.Background = Brushes.Transparent;
            uint k = 5;
            if (!UInt32.TryParse(this.troskoviTextBox.Text, out k))
            {
                this.troskoviTextBox.Background = Brushes.Red;
                proveraIspravno = false;
            }
            else this.troskoviTextBox.Background = Brushes.Transparent;

            if (!proveraIspravno) return;


            this.Pin.dodajDogadjaj(new Dogadjaj(this.jedOznTextBox.Text, 
                                                this.nazivTextBox.Text,
                                                this.opisTextBox.Text,
                                                (TIP)this.tipComboBox.SelectedIndex,
                                                (POSECENOST_DOGADJAJA)this.posecenostComboBox.SelectedItem,
                                                Dogadjaj.slikeFestivalaPutanje[(TIP)this.tipComboBox.SelectedIndex],
                                                (bool)this.humanitarniKarakterRadioButton.IsChecked,
                                                UInt32.Parse(this.troskoviTextBox.Text),
                                                this._pin.Drzava.DrzavaText,
                                                this._pin.Grad.GradText,
                                                this._noviDogadjaj.IstorijaDatumaOdrzavanja,
                                                this._noviDogadjaj.DatumOdrzavanja,
                                                this._noviDogadjaj.ListaEtiketa,
                                                this.Pin.X,
                                                this.Pin.Y));
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void ponistiPravljenjeDogadjajaButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void podesiDatum_Click(object sender, RoutedEventArgs e)
        {
            DodajDatumOdrzavanjaZaTekucuGodinuDialog p = new DodajDatumOdrzavanjaZaTekucuGodinuDialog(this._noviDogadjaj);
            this.Effect = new BlurEffect();
            p.ShowDialog();
            this.Effect = null;
            p.Close();
        }

        private void podesiIstorijuButton_Click(object sender, RoutedEventArgs e)
        {
            DodajIstorijuDatumaOdrzavanjaDialog p = new DodajIstorijuDatumaOdrzavanjaDialog(this._noviDogadjaj);
            this.Effect = new BlurEffect();
            p.ShowDialog();
            this.Effect = null;
            p.Close();
        }

        private void podesiEtikete_Click(object sender, RoutedEventArgs e)
        {
            AzurirajEtiketeDialog p = new AzurirajEtiketeDialog(this._noviDogadjaj);
            this.Effect = new BlurEffect();
            p.ShowDialog();
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
