using EventMap.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    /// Interaction logic for DetaljnijiPregledPinaDialog.xaml
    /// </summary>
    public partial class DetaljnijiPregledPinaDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private MapPin _detaljiPin;

        public MapPin DetaljiPin
        {
            get
            {
                return _detaljiPin;
            }
            set
            {
                if (_detaljiPin != value)
                {
                    _detaljiPin = value;
                    OnPropertyChanged("DetaljiPin");
                }
            }
        }




        public DetaljnijiPregledPinaDialog(MapPin pin)
        {
           this.InitializeComponent();
            this.DataContext = this;

            this._detaljiPin = new MapPin(pin);

        }

        private void povratakNaGlavniEkranButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void pregledIstorijeOdrzavanjaButton_Click(object sender, RoutedEventArgs e)
        {
            if (_detaljiPin.ListaDogadjaja == null || _detaljiPin.ListaDogadjaja.Count == 0)
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
                PregledIstorijeDatumaOdrzavanjaDialog p = new PregledIstorijeDatumaOdrzavanjaDialog(dog);
                this.Effect = new BlurEffect();
                p.ShowDialog();
                this.Effect = null;
                p.Close();
            }
        }

        private void pregledListeEtiketaButton_Click(object sender, RoutedEventArgs e)
        {
            if (_detaljiPin.ListaDogadjaja == null || _detaljiPin.ListaDogadjaja.Count == 0)
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
                PregledEtiketaDialog p = new PregledEtiketaDialog(dog);
                this.Effect = new BlurEffect();
                p.ShowDialog();
                this.Effect = null;
                p.Close();
            }
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
