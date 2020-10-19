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
    /// Interaction logic for AzurirajKalendarDialog.xaml
    /// </summary>
    public partial class AzurirajKalendarDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private DateTime _datum;
    
        public AzurirajKalendarDialog(DateTime datum)
        {
            InitializeComponent();
            this.DataContext = this;

            this._datum = datum;
        }

        public DateTime Datum
        {
            get
            {
                return _datum;
            }
            set
            {
                if (value != _datum)
                {
                    _datum = value;
                    OnPropertyChanged("Datum");
                }
            }
        }

        private void azurirajIstorijuDatumaOdrzavanjaButton_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression bindDatum = this.kalendar.GetBindingExpression(Calendar.SelectedDateProperty);
            BindingExpression bindDatum2 = this.kalendar.GetBindingExpression(Calendar.DisplayDateProperty);
            bindDatum.UpdateSource();
            bindDatum.UpdateTarget();
            bindDatum2.UpdateSource();
            bindDatum2.UpdateTarget();
            AzurirajIstorijeDatumaOdrzavanjaDialog.DatumZaMenjanje = this._datum;
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void povratakNaPregledPinaButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void kalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.Capture(null);
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
