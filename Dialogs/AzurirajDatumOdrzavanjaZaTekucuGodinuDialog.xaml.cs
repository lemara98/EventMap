﻿using EventMap.Classes;
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
    /// Interaction logic for AzurirajDatumOdrzavanjaZaTekucuGodinuDialog.xaml
    /// </summary>
    public partial class AzurirajDatumOdrzavanjaZaTekucuGodinuDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private DateTime _tekuciDatum;
    
        public AzurirajDatumOdrzavanjaZaTekucuGodinuDialog(Dogadjaj dog)
        {
            InitializeComponent();
            this.DataContext = this;

            _tekuciDatum = dog.DatumOdrzavanja;
        }

        public DateTime TekuciDatum
        {
            get
            {
                return _tekuciDatum;
            }
            set
            {
                if (value != _tekuciDatum)
                {
                    _tekuciDatum = value;
                    OnPropertyChanged("TekuciDatum");
                }
            }
        }

        private void azurirajDatumOdrzavanjaButton_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression bindDatum = this.kalendar.GetBindingExpression(Calendar.SelectedDateProperty);
            BindingExpression bindDatum2 = this.kalendar.GetBindingExpression(Calendar.DisplayDateProperty);
            bindDatum.UpdateSource();
            bindDatum.UpdateTarget();
            bindDatum2.UpdateSource();
            bindDatum2.UpdateTarget();
            AzurirajPinDialog.TekDat = this._tekuciDatum;
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
