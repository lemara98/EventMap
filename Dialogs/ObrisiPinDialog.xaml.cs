using EventMap.Classes;
using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EventMap.Dialogs
{
    /// <summary>
    /// Interaction logic for ObrisiPinDialog.xaml
    /// </summary>
    public partial class ObrisiPinDialog : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private MapPin _pinZaBrisanje;
        private Canvas _kanvas;
        private List<MapPin> _sviPinovi;

        public static bool obrisan;

        public ObrisiPinDialog(MapPin p, Canvas c, List<MapPin> sp)
        {
            InitializeComponent();
            this.DataContext = this;

            this._pinZaBrisanje = p;
            this._kanvas = c;
            this._sviPinovi = sp;
        }

        private MapPin PinZaBrisanje
        {
            get
            {
                return _pinZaBrisanje;
            }
            set
            {
                if (_pinZaBrisanje != value)
                {
                    _pinZaBrisanje = value;
                    OnPropertyChanged("PinZaBrisanje");
                }
            }
        }

        private Canvas Kanvas
        {
            get
            {
                return _kanvas;
            }
            set
            {
                if (_kanvas != value)
                {
                    _kanvas = value;
                    OnPropertyChanged("Kanvas");
                }
            }
        }

        private List<MapPin> SviPinovi
        {
            get
            {
                return _sviPinovi;
            }
            set
            {
                if (_sviPinovi != value)
                {
                    _sviPinovi = value;
                    OnPropertyChanged("SviPinovi");
                }
            }
        }

        private void povratakNaGlavniEkranButton_Click(object sender, RoutedEventArgs e)
        {
            obrisan = false;
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void obrisiPin_Click(object sender, RoutedEventArgs e)
        {
            /*
             *  Popuniti za brisanje pina
             */
            obrisan = true;
            
            this.Kanvas.Children.Remove(this.PinZaBrisanje);
            this.SviPinovi.Remove(this.PinZaBrisanje);
            Dogadjaj.gradoviUDrzavi[this.PinZaBrisanje.Drzava.DrzavaText].Remove(this.PinZaBrisanje.Grad);
            if (Dogadjaj.gradoviUDrzavi[this.PinZaBrisanje.Drzava.DrzavaText].Count == 0)
                Dogadjaj.sveDrzave.Remove(this.PinZaBrisanje.Drzava);
            //this.PinZaBrisanje.Drzava.DrzavaText = "";
            //this.PinZaBrisanje.Grad.GradText = "";
            //this.PinZaBrisanje = null;
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
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
