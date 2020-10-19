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
    /// Interaction logic for DodajIstorijuDatumaOdrzavanjaDialog.xaml
    /// </summary>
    public partial class DodajIstorijuDatumaOdrzavanjaDialog : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private Dogadjaj _dogadjajistorija;
        private List<DateTime> _nePromenjena;

        public DodajIstorijuDatumaOdrzavanjaDialog(Dogadjaj dog)
        {
            InitializeComponent();
            this.DataContext = this;

            this._dogadjajistorija = dog;
            this._nePromenjena = new List<DateTime>(dog.IstorijaDatumaOdrzavanja);
        }

        public Dogadjaj DogadjajIstorija
        {
            get
            {
                return _dogadjajistorija;
            }
            set
            {
                if (_dogadjajistorija != value)
                {
                    _dogadjajistorija = value;
                    OnPropertyChanged("DogadjajIstorija");
                }
            }
        }

        private void obrisiDatum_Click(object sender, RoutedEventArgs e)
        {
            IList item = listaDatumaDataGrid.SelectedItems;
            if (item.Count == 0) return;
            DateTime dog = (DateTime)item[0];
            this.DogadjajIstorija.IstorijaDatumaOdrzavanja.Remove(dog);
            this.listaDatumaDataGrid.Items.Refresh();
        }

        private void dodajDatum_Click(object sender, RoutedEventArgs e)
        {
            DodajKalendarDialog p = new DodajKalendarDialog(this._dogadjajistorija);
            this.Effect = new BlurEffect();
            p.ShowDialog();
            listaDatumaDataGrid.Items.Refresh();
            this.Effect = null;
            p.Close();
        }

        private void povratakNaPregledPinaButton_Click(object sender, RoutedEventArgs e)
        {
            this._dogadjajistorija.IstorijaDatumaOdrzavanja = this._nePromenjena;
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void potvrdiDatumeButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void dateButton_Click(object sender, RoutedEventArgs e)
        {
            IList item = listaDatumaDataGrid.SelectedItems;
            AzurirajIstorijeDatumaOdrzavanjaDialog.DatumZaMenjanje = (DateTime)item[0];
            int pozicija = 0;
            foreach (DateTime d in this._dogadjajistorija.IstorijaDatumaOdrzavanja)
            {
                if (d.Equals((DateTime)item[0])) break;
                ++pozicija;
            }
            AzurirajKalendarDialog p = new AzurirajKalendarDialog(AzurirajIstorijeDatumaOdrzavanjaDialog.DatumZaMenjanje);
            this.Effect = new BlurEffect();
            p.ShowDialog();
            this._dogadjajistorija.IstorijaDatumaOdrzavanja[pozicija] = AzurirajIstorijeDatumaOdrzavanjaDialog.DatumZaMenjanje;
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
