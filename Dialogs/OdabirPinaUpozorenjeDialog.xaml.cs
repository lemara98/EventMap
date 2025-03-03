﻿using System;
using System.Collections.Generic;
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
    /// Interaction logic for OdabirPinaUpozorenjeDialog.xaml
    /// </summary>
    public partial class OdabirPinaUpozorenjeDialog : Window
    {
        public OdabirPinaUpozorenjeDialog()
        {
            InitializeComponent();
        }

        private void primeniFiltere_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }
        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
