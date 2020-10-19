using EventMap.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for PretragaDialog.xaml
    /// </summary>
    public partial class PretragaDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private List<MapPin> _sviPinovi;
        private List<Dogadjaj> _sviDogadjaji;

        public PretragaDialog(List<MapPin> svi)
        {
            InitializeComponent();
            this.DataContext = this;

            this._sviPinovi = svi;
            this._sviDogadjaji = new List<Dogadjaj>();

            foreach (MapPin mp in _sviPinovi)
            {

                this._sviDogadjaji.AddRange(mp.ListaVidljivihDogadjaja);
            }
        }

        public List<MapPin> SviPinovi
        {
            get { return _sviPinovi; }
            set
            {
                if (value != _sviPinovi)
                {
                    _sviPinovi = value;
                    OnPropertyChanged("SviPinovi");
                }
            }
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

        private void jedOznPretraga_Checked(object sender, RoutedEventArgs e)
        {
            this.jedOznTextBox.IsEnabled = true;
            this.jedOznTextBox.Opacity = 1;
            this.jedOznTxt.Opacity = 1;
        }

        private void jedOznPretraga_Unchecked(object sender, RoutedEventArgs e)
        {
            this.jedOznTextBox.IsEnabled = false;
            this.jedOznTextBox.Opacity = 0.2;
            this.jedOznTxt.Opacity = 0.2;
        }

        private void nazivPretraga_Checked(object sender, RoutedEventArgs e)
        {
            this.nazivTextBox.IsEnabled = true;
            this.nazivTextBox.Opacity = 1;
            this.nazivTxt.Opacity = 1;
        }

        private void nazivPretraga_Unchecked(object sender, RoutedEventArgs e)
        {
            this.nazivTextBox.IsEnabled = false;
            this.nazivTextBox.Opacity = 0.2;
            this.nazivTxt.Opacity = 0.2;
        }

        private void opisPretraga_Checked(object sender, RoutedEventArgs e)
        {
            this.opisTextBox.IsEnabled = true;
            this.opisTextBox.Opacity = 1;
            this.opisTxt.Opacity = 1;
        }

        private void opisPretraga_Unchecked(object sender, RoutedEventArgs e)
        {
            this.opisTextBox.IsEnabled = false;
            this.opisTextBox.Opacity = 0.2;
            this.opisTxt.Opacity = 0.2;
        }

        private void tipPretraga_Checked(object sender, RoutedEventArgs e)
        {
            this.tipComboBox.IsEnabled = true;
            this.tipComboBox.Opacity = 1;
            this.tipTxt.Opacity = 1;
        }

        private void tipPretraga_Unchecked(object sender, RoutedEventArgs e)
        {
            this.tipComboBox.IsEnabled = false;
            this.tipComboBox.Opacity = 0.2;
            this.tipTxt.Opacity = 0.2;
        }

        private void posecenostPretraga_Checked(object sender, RoutedEventArgs e)
        {
            this.posecenostComboBox.IsEnabled = true;
            this.posecenostComboBox.Opacity = 1;
            this.posecenostTxt.Opacity = 1;
        }

        private void posecenostPretraga_Unchecked(object sender, RoutedEventArgs e)
        {
            this.posecenostComboBox.IsEnabled = false;
            this.posecenostComboBox.Opacity = 0.2;
            this.posecenostTxt.Opacity = 0.2;
        }

        private void humKarPretraga_Checked(object sender, RoutedEventArgs e)
        {
            this.humanitarniKarakterDaRadioButton.IsEnabled = true;
            this.humanitarniKarakterNeRadioButton.IsEnabled = true;

            this.humanitarniKarakterDaRadioButton.Opacity = 1;
            this.humanitarniKarakterNeRadioButton.Opacity = 1;
        }

        private void humKarPretraga_Unchecked(object sender, RoutedEventArgs e)
        {
            this.humanitarniKarakterDaRadioButton.IsEnabled = false;
            this.humanitarniKarakterNeRadioButton.IsEnabled = false;

            this.humanitarniKarakterDaRadioButton.Opacity = 0.2;
            this.humanitarniKarakterNeRadioButton.Opacity = 0.2;
        }

        private void troskoviPretraga_Checked(object sender, RoutedEventArgs e)
        {
            this.Do10000RadioButton.IsEnabled = true;
            this.Do10000RadioButton.Opacity = 1;

            this.izmedju10000i50000RadioButton.IsEnabled = true;
            this.izmedju10000i50000RadioButton.Opacity = 1;

            this.izmedju50000i100000RadioButton.IsEnabled = true;
            this.izmedju50000i100000RadioButton.Opacity = 1;

            this.preko100000RadioButton.IsEnabled = true;
            this.preko100000RadioButton.Opacity = 1;

            this.troskoviTxt.Opacity = 1;
        }

        private void troskoviPretraga_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Do10000RadioButton.IsEnabled = false;
            this.Do10000RadioButton.Opacity = 0.2;

            this.izmedju10000i50000RadioButton.IsEnabled = false;
            this.izmedju10000i50000RadioButton.Opacity = 0.2;

            this.izmedju50000i100000RadioButton.IsEnabled = false;
            this.izmedju50000i100000RadioButton.Opacity = 0.2;

            this.preko100000RadioButton.IsEnabled = false;
            this.preko100000RadioButton.Opacity = 0.2;

            this.troskoviTxt.Opacity = 0.2;
        }

        private void izvrsiPretraguButton_Click(object sender, RoutedEventArgs e)
        {

            List<Dogadjaj> ld = new List<Dogadjaj>(this._sviDogadjaji);

            CultureInfo culture = new CultureInfo("sr-Latn-CS");

            //int brojUkljucenihKriterijuma = 0;
            

            foreach (Dogadjaj d in this._sviDogadjaji)
            {
                //int brojacIspunjenostiUslova = 0;
                if (this.jedOznPretraga.IsChecked == true && culture.CompareInfo.IndexOf(d.JedCitOzn, this.jedOznTextBox.Text, CompareOptions.IgnoreCase) == -1)
                {
                    ld.Remove(d);
                    continue;
                }

                if (this.nazivPretraga.IsChecked == true && culture.CompareInfo.IndexOf(d.Naziv, this.nazivTextBox.Text, CompareOptions.IgnoreCase) == -1)
                {
                    ld.Remove(d);
                    continue;
                }

                if (this.opisPretraga.IsChecked == true && culture.CompareInfo.IndexOf(d.Opis, this.opisTextBox.Text, CompareOptions.IgnoreCase) == -1)
                {
                    ld.Remove(d);
                    continue;
                }

                if (this.tipPretraga.IsChecked == true && d.Tip != (TIP)this.tipComboBox.SelectedIndex)
                {
                    ld.Remove(d);
                    continue;
                }

                if (this.posecenostPretraga.IsChecked == true && d.Posecenost != (POSECENOST_DOGADJAJA)this.posecenostComboBox.SelectedIndex)
                {
                    ld.Remove(d);
                    continue;
                }

                if (this.humKarPretraga.IsChecked == true && d.HumanitarniKarakter != this.humanitarniKarakterDaRadioButton.IsChecked)
                {
                    ld.Remove(d);
                    continue;
                }

                if (this.troskoviPretraga.IsChecked == true)
                {
                    bool t = false;
                    if (d.Troskovi < 10000u)
                    {
                        if (this.Do10000RadioButton.IsChecked == true) t = true;
                    }
                    else if (d.Troskovi > 10000u && d.Troskovi < 50000u)
                    {
                        if (this.izmedju10000i50000RadioButton.IsChecked == true) t = true;
                    }
                    else if (d.Troskovi > 50000u && d.Troskovi < 100000u)
                    {
                        if (this.izmedju50000i100000RadioButton.IsChecked == true) t = true;
                    }
                    else 
                    {
                        if (this.preko100000RadioButton.IsChecked == true) t = true;
                    }

                    if (!t)
                    {
                        ld.Remove(d);
                    }
                    
                }

            }


            RezultatPretrageDialog rpd = new RezultatPretrageDialog(ld);
            this.Effect = new BlurEffect();
            rpd.ShowDialog();
            this.Effect = null;
            rpd.Close();


        }

        private void napustiPretraguButton_Click(object sender, RoutedEventArgs e)
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
