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
    /// Interaction logic for DodajNovuEtiketuDialog.xaml
    /// </summary>
    public partial class DodajNovuEtiketuDialog : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private Etiketa _novaEtiketa;
        private List<Etiketa> _lista;
    
        public DodajNovuEtiketuDialog(List<Etiketa> l)
        {
            InitializeComponent();
            this.DataContext = this;

            this._novaEtiketa = new Etiketa();
            this._lista = l;
        }

        public Etiketa NovaEtiketa
        {
            get
            {
                return _novaEtiketa;
            }
            set
            {
                if (_novaEtiketa != value)
                {
                    _novaEtiketa = value;
                    OnPropertyChanged("NovaEtiketa");
                }
            }
        }

        public List<Etiketa> Lista
        {
            get
            {
                return _lista;
            }
            set
            {
                if (_lista != value)
                {
                    _lista = value;
                    OnPropertyChanged("Lista");
                }
            }
        }

        private void promeniBojuEtiketeColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            SolidColorBrush b = new SolidColorBrush((Color)promeniBojuEtiketeColorPicker.SelectedColor);
            pozadinaEtikete.Background = b;
            //   this._novaEtiketa.Boja = (Color)promeniBojuEtiketeColorPicker.SelectedColor;
        }

        private void povratakNaPregledPinaButton_Click_1(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void azurirajEtiketeButton_Click(object sender, RoutedEventArgs e)
        {
            this._lista.Add(this._novaEtiketa);
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
