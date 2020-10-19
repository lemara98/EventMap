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
    /// Interaction logic for RezultatPretrageDialog.xaml
    /// </summary>
    public partial class RezultatPretrageDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Dogadjaj> _sviDogadjaji;
        private int _brojDogadjaja;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    
        public RezultatPretrageDialog(List<Dogadjaj> lista)
        {
            InitializeComponent();
            this.DataContext = this;
            this._sviDogadjaji = lista;
            this._brojDogadjaja = lista.Count();

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
                foreach (Dogadjaj d in _sviDogadjaji)
                {
                    if (d.Vidljiv) lista.Add(d);
                }
                return lista;
            }

        }

        public int BrojDogadjaja
        {
            get { return _brojDogadjaja; }
            set
            {
                if (value != _brojDogadjaja)
                {
                    _brojDogadjaja = value;
                    OnPropertyChanged("BrojDogadjaja");
                }
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

        private void povratakNaPretragu_Click(object sender, RoutedEventArgs e)
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
