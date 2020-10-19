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
    /// Interaction logic for PregledIstorijeDatumaOdrzavanja.xaml
    /// </summary>
    public partial class PregledIstorijeDatumaOdrzavanjaDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private List<DateTime> _listaDatuma;
        private string _nazivDogadjaja;
        public PregledIstorijeDatumaOdrzavanjaDialog(Dogadjaj dog)
        {
            InitializeComponent();
            this.DataContext = this;

            this._listaDatuma = dog.IstorijaDatumaOdrzavanja;
            this._nazivDogadjaja = dog.Naziv;

        }

        public List<DateTime> ListaDatuma
        {
            get
            {
                return _listaDatuma;
            }
            set
            {
                if (_listaDatuma != value)
                {
                    _listaDatuma = value;
                    OnPropertyChanged("ListaDatuma");
                }
            }
        }

        public string NazivDogadjaja
        {
            get
            {
                return _nazivDogadjaja;
            }
            set
            {
                if (_nazivDogadjaja != value)
                {
                    _nazivDogadjaja = value;
                    OnPropertyChanged("NazivDogadjaja");
                }
            }
        }

        private void povratakNaPregledPinaButton_Click(object sender, RoutedEventArgs e)
        {
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
