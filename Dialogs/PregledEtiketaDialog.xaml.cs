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
    /// Interaction logic for PregledEtiketaDialog.xaml
    /// </summary>
    public partial class PregledEtiketaDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private List<Etiketa> _listaSaEtiketama;

        public PregledEtiketaDialog(Dogadjaj dog)
        {
            InitializeComponent();
            this.DataContext = this;

            this._listaSaEtiketama = dog.ListaEtiketa;
        }

        public List<Etiketa> ListaSaEtiketama
        {
            get { return _listaSaEtiketama; }
            set
            {
                if (value != _listaSaEtiketama)
                {
                    _listaSaEtiketama = value;
                    OnPropertyChanged("ListaSaEtiketama");
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
